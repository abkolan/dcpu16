using System;
using System.Collections.Generic;

namespace dcpu16
{
    public class OperandFactory
    {
        private readonly Dictionary<string, Registers> regOpCodes = new Dictionary<string, Registers>();

        public OperandFactory()
        {
            regOpCodes.Add("A", Registers.A);
            regOpCodes.Add("B", Registers.B);
            regOpCodes.Add("C", Registers.C);
            regOpCodes.Add("X", Registers.X);
            regOpCodes.Add("Y", Registers.Y);
            regOpCodes.Add("Z", Registers.Z);
            regOpCodes.Add("I", Registers.I);
            regOpCodes.Add("J", Registers.J);
            regOpCodes.Add("SP", Registers.SP);
            regOpCodes.Add("PC", Registers.PC);
            regOpCodes.Add("EX", Registers.EX);

            regOpCodes.Add("[A]", Registers.A_MEM);
            regOpCodes.Add("[B]", Registers.B_MEM);
            regOpCodes.Add("[C]", Registers.C_MEM);
            regOpCodes.Add("[X]", Registers.X_MEM);
            regOpCodes.Add("[Y]", Registers.Y_MEM);
            regOpCodes.Add("[Z]", Registers.Z_MEM);
            regOpCodes.Add("[I]", Registers.I_MEM);
            regOpCodes.Add("[J]", Registers.J_MEM);
            regOpCodes.Add("[SP]", Registers.SP_MEM);
        }

        public OperandBase CreateOperand(string opText, OperandType operandType)
        {
            OperandBase result = null;

            //TODO: Change this
            Registers registerValue;
            if (regOpCodes.TryGetValue(opText, out registerValue))
            {
                result = new RegisterOperand() {Register = registerValue};
            }

            else if (HexLitUtils.CheckOpTextIsAnAddress(opText))
            {
                //check if it's an address
                WordOps currentWordOp = WordOps.LIT_VAL;
                if (opText.StartsWith("[") && opText.EndsWith("]"))
                {
                    currentWordOp = WordOps.NXT_WRD_MEM;
                    opText = HexLitUtils.RemoveSquareBraces(opText);
                }
                if (opText.Contains("X"))
                {
                    int address = HexLitUtils.RemoveXAndConvertToHex(opText);
                    if (currentWordOp != WordOps.NXT_WRD_MEM)
                    {
                        if (address == -1 || address <= 30)
                        {
                            currentWordOp = WordOps.LIT_VAL;
                            if (address == -1)
                            {
                                address = 0x20;
                            }
                            else
                            {
                                address += 0x21;
                            }
                            result = new WordOperand() {WordOp = currentWordOp, Address = address};
                        }
                        else
                        {
                            currentWordOp = WordOps.NXT_WORD;
                            result = new WordOperand() {WordOp = currentWordOp, Address = address};
                        }
                    }
                    else
                    {
                        result = new WordOperand() {WordOp = currentWordOp, Address = address};
                    }
                }
            }
            else if (HexLitUtils.ValidLabel(opText))
            {
                result = new LabelOperand() {LabelName = opText};
            }
            else
            {
                throw new ArgumentException("Not Yet Supported");
            }
            return result;
        }
    }
}