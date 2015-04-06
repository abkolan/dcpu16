namespace Emulator3
{
    public class LitOperandVaL : IOperandVal
    {
        private ushort backingLiteral;

        public LitOperandVaL(ushort backingLiteral)
        {
            this.backingLiteral = backingLiteral;
        }

        public ushort VAL
        {
            get { return backingLiteral; }
            set { backingLiteral = value; }
        }
    }

    public class MemAddressOperandVaL : IOperandVal
    {
        private readonly ushort address;
        private readonly IDCPU idcpu;

        public MemAddressOperandVaL(ushort address, IDCPU idcpu)
        {
            this.address = address;
            this.idcpu = idcpu;
        }

        public ushort VAL
        {
            get { return idcpu[address]; }
            set { idcpu[address] = value; }
        }
    }


    public class RegisterPC : IOperandVal
    {
        private readonly IDCPU idcpu;

        public RegisterPC(IDCPU idcpu)
        {
            this.idcpu = idcpu;
        }

        public ushort VAL
        {
            get { return this.idcpu.PC; }
            set { this.idcpu.PC = value; }
        }
    }

    public interface IOperandVal
    {
        ushort VAL { get; set; }
    }
}