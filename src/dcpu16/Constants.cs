using System;
using System.Globalization;

namespace dcpu16
{
    public static class Constants
    {
        public static string Invalid_Instruction = "Invalid Instruction {0}";
        public static string Invalid_Address = "Invalid address {0}";
    }

    public enum OperandType
    {
        A, B
    }

    public enum Registers
    {
        A = 0x00,
        B = 0X01,
        C = 0X02,
        X = 0X03,
        Y = 0X04,
        Z = 0X05,
        I = 0X6,
        J = 0X7,

        A_MEM = 0X08,
        B_MEM = 0X09,
        C_MEM = 0XA,
        X_MEM = 0XB,
        Y_MEM = 0XC,
        Z_MEM = 0XD,
        I_MEM = 0XE,
        J_MEM = 0XF,

        A_MEM_NXT = 0x10,
        B_MEM_NXT,
        C_MEM_NXT,
        X_MEM_NXT,
        Y_MEM_NXT,
        Z_MEM_NXT,
        I_MEM_NXT,
        J_MEM_NXT,

        SP = 0X1B,
        SP_MEM = 0x19,
        SP_MEM_NXT = 0X1a,

        PC = 0X1C,
        EX = 0X1D,
    }

    public enum StackOps
    {
        PUSH = 0x18,
        POP = 0X18
    }

    public enum WordOps
    {
        NXT_WRD_MEM = 0X1E,
        NXT_WORD = 0x1f,

        LIT_VAL
    }

    public static class HexLitUtils
    {
        public static bool CheckOpTextIsAnAddress(string opText)
        {
            opText = opText.TrimStart(new[] { '[' });
            opText = opText.TrimEnd(new[] { ']' });

            int address = RemoveXAndConvertToHex(opText);
            return address != int.MinValue;
        }

        public static int RemoveXAndConvertToHex(string opText)
        {
            opText = opText.Trim();

            if (opText.Contains("0X")) // If it's a hex value remove 0x prefix 
            {
                opText = opText.Substring(2);
            }

            int result = int.MinValue;

            try
            {
                result = int.Parse(opText, NumberStyles.HexNumber);
            }
            catch (Exception exception)
            {
                //TODO: Think through this one, does not look right
                //throw  new ArgumentException(string.Format(Constants.Invalid_Address,opText));
            }

            return result;
        }

        public static string RemoveSquareBraces(string opText)
        {
            opText = opText.Replace('[', ' ').TrimStart();

            opText = opText.Replace(']', ' ').TrimEnd();
            return opText;
        }

        public static bool ValidLabel(string opText)
        {
            return (opText.Length > 0) && (char.IsLetterOrDigit(opText[0]));
        }
    }
}