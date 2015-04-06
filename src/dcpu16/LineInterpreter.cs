using System;

namespace dcpu16
{
    public class LineInterpreter
    {
        private readonly  OperandFactory factory = new OperandFactory();

        public Statement Interpret(string lineOfCode)
        {
            lineOfCode = lineOfCode.Trim();
            
            var instruction = GetInstruction(lineOfCode);

            var result = new Statement {Instruction = instruction};

            if (result.Instruction == Ins.DAT)
            {
                return result;
            }

            //TODO: change this
            int spaceIndex = lineOfCode.IndexOf(' ');
            int commaIndex = lineOfCode.IndexOf(',');

            string opBText = lineOfCode.Substring(spaceIndex+1,commaIndex-spaceIndex-1).ToUpper();
            string opAText = lineOfCode.Substring(commaIndex + 1, lineOfCode.Length - commaIndex-1).ToUpper();

            var operandA = factory.CreateOperand(opAText.Trim(),OperandType.A);
            var operandB = factory.CreateOperand(opBText.Trim(),OperandType.B);

            result.OperandA = operandA;
            result.OperandB = operandB;

            return result;
        }

        private static Ins GetInstruction(string lineOfCode)
        {
            lineOfCode = lineOfCode.TrimStart();
            lineOfCode = lineOfCode.TrimEnd();

            int spaceIndex = lineOfCode.IndexOf(' ');

            if (spaceIndex == -1)
            {
                //TODO: Info in the exception
                throw new ArgumentException();
            }

            string instructionText = lineOfCode.Substring(0, spaceIndex).ToUpper();


            if (instructionText.Length != 3)
            {
                throw new
                    ArgumentException(string.Format(Constants.Invalid_Instruction, instructionText));
            }


            Ins instruction;

            Enum.TryParse(instructionText, true, out instruction);
            return instruction;
        }
    }
}