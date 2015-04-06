using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using dcpu16;

namespace Assembler
{
    public class Assembler
    {
        private readonly List<ushort> dump = new List<ushort>();
        private readonly LineInterpreter interpreter = new LineInterpreter();
        private readonly Dictionary<string, int> labelAddressTable = new Dictionary<string, int>();

        private readonly Dictionary<string, IEnumerable<int>> labelIndexTable =
            new Dictionary<string, IEnumerable<int>>();

        /// <summary>
        /// Binary dump of the program
        /// </summary>
        public List<ushort> Dump
        {
            get { return dump; }
        }


        public void Assemble(IEnumerable<string> code)
        {
            foreach (var lineOfCode in code)
            {
                if (string.IsNullOrWhiteSpace(lineOfCode))
                {
                    continue;
                }

                Statement statement = null;
                if (!lineOfCode.Trim().StartsWith(":")) //TODO:Refactor
                {
                    statement = interpreter.Interpret(lineOfCode);
                }

                if (lineOfCode.Trim().StartsWith(":"))
                {
                    string label = lineOfCode.Trim().TrimStart(new[] {':'}).ToUpper();

                    if (labelAddressTable.ContainsKey(label))
                    {
                        throw new ArgumentException("Dupe Label");
                    }
                    labelAddressTable.Add(label, dump.Count);

                    //Fix Labelling
                    IEnumerable<int> value;
                    if (labelIndexTable.TryGetValue(label, out value))
                    {
                        int labelAddress = dump.Count;
                        foreach (int ind in value)
                        {
                            dump[ind] = (ushort) labelAddress;
                        }
                    }
                }
                else if (statement != null && statement.Instruction == Ins.DAT)
                {
                    AddData(lineOfCode);
                }
                else if (statement != null &&
                         (statement.OperandA is RegisterOperand && statement.OperandB is RegisterOperand))
                {
                    //Instruction is 1 word long
                    if (statement.OperandA.ReferencedAddress() == -1 && statement.OperandB.ReferencedAddress() == -1)
                    {
                        ushort fullOpCode = PackOpCode(statement);
                        dump.Add(fullOpCode);
                    }
                    else
                    {
                        Future();
                    }
                }
                else if (statement != null &&
                         (statement.OperandA is WordOperand && 32 <= statement.OperandA.ReferencedAddress() &&
                          statement.OperandA.ReferencedAddress() <= 63 && statement.OperandB is RegisterOperand))
                {
                    //Instruction is 1 word long
                    //  0x20-0x3f | literal value 0xffff-0x1e (-1..30) (literal) (only for a)

                    int refAddress = statement.OperandA.ReferencedAddress();
                    ushort fullOpCode = PackOpCode((ushort) statement.Instruction, (ushort) refAddress,
                        statement.OperandB.ToOpcode());
                    dump.Add(fullOpCode);
                }
                else if (statement != null &&
                         (statement.OperandA is WordOperand && statement.OperandB is RegisterOperand))
                {
                    //Instruction is 2 words long
                    int refAddress = statement.OperandA.ReferencedAddress();
                    ushort partialOpCode = PackOpCode(statement);
                    dump.Add(partialOpCode);
                    dump.Add((ushort) refAddress);
                }
                else if (statement != null && statement.OperandA is LabelOperand)
                {
                    //Instruction is 2 words long
                    var labelOperand = statement.OperandA as LabelOperand;
                    int labelAddress;
                    if (labelAddressTable.TryGetValue(labelOperand.LabelName, out labelAddress))
                    {
                        labelOperand.Address = labelAddress;
                    }
                    else
                    {
                        labelOperand.Address = 0x0000;
                    }

                    ushort partialOpcode = PackOpCode((ushort) statement.Instruction,
                        (ushort) WordOps.NXT_WORD,
                        statement.OperandB.ToOpcode());

                    dump.Add(partialOpcode);

                    IEnumerable<int> labelIndicesInMemory;
                    if (labelIndexTable.TryGetValue(labelOperand.LabelName, out labelIndicesInMemory))
                    {
                        var indices = labelIndicesInMemory;
                        indices = indices.Concat(new[] {dump.Count});

                        labelIndexTable[labelOperand.LabelName] = indices.ToList();
                    }
                    else
                    {
                        labelIndexTable.Add(labelOperand.LabelName, new List<int> {dump.Count});
                    }
                    dump.Add((ushort) labelOperand.Address);
                }
                else
                {
                    Future();
                }
            }
        }

        private void AddData(string lineOfCode)
        {
            int index = lineOfCode.IndexOf("DAT ", StringComparison.Ordinal);

            string datum = lineOfCode.Substring(index + 4);

            int i = 0;
            while (i < datum.Length)
            {
                if (datum[i].Equals('"'))
                {
                    while (i < datum.Length && datum[++i] != '"')
                    {
                        ushort ascii = Convert.ToUInt16(datum[i]);
                        dump.Add(ascii);
                    }
                    i++;
                }
                else if (datum[i].Equals(','))
                {
                    i++;
                }
                else if (char.IsDigit(datum[i]))
                {
                    StringBuilder number = new StringBuilder();
                    while (i < datum.Length && datum[i] != ',')
                    {
                        number.Append(datum[i++]);
                    }
                    ushort numberValue = ushort.Parse(number.ToString());
                    dump.Add(numberValue);
                    i++;
                }
            }
        }

        private static void Future()
        {
            throw new ArgumentException("Not Yet Supported!!");
        }

        private ushort PackOpCode(Statement statement)
        {
            return PackOpCode((ushort) statement.Instruction, statement.OperandA.ToOpcode(),
                statement.OperandB.ToOpcode());
        }

        private ushort PackOpCode(ushort o, ushort a, ushort b)
        {
            // Opcodes are in the form aaaaaabbbbbooooo 
            a = (ushort) (a << 10);
            b = (ushort) (b << 5);

            return (ushort) (o | a | b);
        }
    }
}