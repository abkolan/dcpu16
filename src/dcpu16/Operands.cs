using System;

namespace dcpu16
{
    public abstract class OperandBase
    {
        public abstract ushort ToOpcode();

        public abstract int ReferencedAddress();
    }

    public class RegisterOperand : OperandBase
    {
        public int Address { get; set; }

        public override int ReferencedAddress()
        {
            return Address;
        }

        public RegisterOperand()
        {
            Address = -1;
        }

        public Registers Register { get; set; }

        public override ushort ToOpcode()
        {
            return (ushort) Register;
        }
    }

    public class StackOperand : OperandBase
    {
        public override int ReferencedAddress()
        {
            throw new NotImplementedException();
        }

        public StackOps StackOp { get; set; }


        public override ushort ToOpcode()
        {
            return (ushort) StackOp;
        }
    }

    public class LabelOperand : OperandBase
    {
        public int Address { get; set; }

        public string LabelName { get; set; }

        public override ushort ToOpcode()
        {
            return (ushort) Address;
        }

        public override int ReferencedAddress()
        {
            return (ushort) Address;
        }
    }

    public class WordOperand : OperandBase
    {
        public WordOps WordOp { get; set; }

        public int Address { get; set; }

        public override ushort ToOpcode()
        {
            return (ushort) WordOp;
        }

        public override int ReferencedAddress()
        {
            return Address;
        }
    }
}