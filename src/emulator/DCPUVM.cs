using System;
using System.Collections.Generic;
using dcpu16;

namespace Emulator3
{
    public class DCPUVM : IDCPU
    {
        #region Registers

        private ushort length;

        public ushort A { get; set; }

        public ushort B { get; set; }

        public ushort C { get; set; }

        public ushort X { get; set; }

        public ushort Y { get; set; }

        public ushort Z { get; set; }

        public ushort I { get; set; }

        public ushort J { get; set; }

        public ushort PC { get; set; }

        public ushort EX { get; set; }

        #endregion

        public ushort InstructionLength
        {
            get { return length; }
        }

        public bool SkipNext { get; set; }

        private readonly IOperationResolver operationResolver;
        private readonly IOperandResover operandResolver;

        private ushort sp;
        private readonly ushort[] memory = new ushort[0x10000];


        public event EventHandler<VideoArgs> RefreshVideo;

        public DCPUVM()
        {
            sp = 0xffff;
            PC = 0x0000;
            operandResolver = new OperandResolver(this);
            operationResolver = new OperationResolver();
        }


        public void Execute(IEnumerable<ushort> code)
        {
            LoadToMemory(code);
            StartExecution();
        }

        private void StartExecution()
        {
            while (true)
            {
                var currentWord = this[PC];
                length = 0;
                Ins opCode = GetOpCode(currentWord);
                Action<IOperandVal, IOperandVal, IDCPU> operation = null;

                if (opCode != Ins.SPL)
                {
                    operation = operationResolver.GetOperations(opCode);
                }


                AllOps opA = GetOpA(currentWord);
                IOperandVal operandA, operandB;


                if (opA == AllOps.NXT_WRD || opA == AllOps.NXT_WRD_MEM)
                {
                    length++;
                    var nextWord = this[PC + length];

                    if (opA == AllOps.NXT_WRD)
                    {
                        operandA = new LitOperandVaL(nextWord);
                    }
                    else
                    {
                        operandA = new MemAddressOperandVaL(nextWord, this);
                    }
                }
                else
                {
                    operandA = operandResolver.GetOperandVal(opA);
                }


                AllOps opB = GetOpB(currentWord);


                if (opB == AllOps.NXT_WRD || opB == AllOps.NXT_WRD_MEM)
                {
                    length++;
                    var nextWord = this[PC + length];

                    if (opA == AllOps.NXT_WRD)
                    {
                        operandB = new LitOperandVaL(nextWord);
                    }
                    else
                    {
                        operandB = new MemAddressOperandVaL(nextWord, this);
                    }
                }
                else
                {
                    operandB = operandResolver.GetOperandVal(opB);
                }

                if (!SkipNext)
                {
                    operation(operandA, operandB, this);
                }
                else
                {
                    SkipNext = false;
                    PC = (ushort) (PC + InstructionLength);
                    PC++;
                }
            }
        }


        private const ushort opCodeBH = 0x03f0;

        private AllOps GetOpB(ushort word0)
        {
            int operandB = ((word0 & opCodeBH) >> 5);

            AllOps opB = (AllOps) operandB;
            return opB;
        }

        private AllOps GetOpA(ushort word0)
        {
            int operandA = ((word0 & opCodeAH) >> 10);

            AllOps opA = (AllOps) operandA;
            return opA;
        }

        private const ushort opCodeAH = 0xfc00;


        private const ushort opcodeH = 0x1f;

        private Ins GetOpCode(ushort word0)
        {
            int opCodeNum = word0 & opcodeH;

            Ins opCode = (Ins) opCodeNum;
            return opCode;
        }

        private void LoadToMemory(IEnumerable<ushort> code)
        {
            int i = 0;

            foreach (ushort word in code)
            {
                memory[i++] = word;
            }
        }

        public ushort this[int address]
        {
            get { return memory[address]; }
            set
            {
                memory[address] = value;
                if (address >= 32768 && address <= 33279)
                {
                    if (RefreshVideo != null)
                    {
                        RefreshVideo(this, new VideoArgs {Address = address, Value = value});
                    }
                }
            }
        }
    }

    public class VideoArgs : EventArgs
    {
        public int Address { get; set; }
        public ushort Value { get; set; }
    }
}