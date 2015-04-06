using System;
using System.Collections.Generic;
using dcpu16;

namespace Emulator3
{
    public class OperationResolver : IOperationResolver
    {
        private readonly Dictionary<Ins, Action<IOperandVal, IOperandVal, IDCPU>> operationTable =
            new Dictionary<Ins, Action<IOperandVal, IOperandVal, IDCPU>>();


        public OperationResolver()
        {
            operationTable.Add(Ins.SET, SET);
            operationTable.Add(Ins.ADD, ADD);
            operationTable.Add(Ins.SUB, SUB);
            operationTable.Add(Ins.MUL, MUL);
            operationTable.Add(Ins.DIV, DIV);
            operationTable.Add(Ins.DVI, DVI);
            operationTable.Add(Ins.MOD, MOD);
            operationTable.Add(Ins.MDI, MDI);
            operationTable.Add(Ins.AND, AND);
            operationTable.Add(Ins.BOR, BOR);
            operationTable.Add(Ins.ASR, ASR);
            operationTable.Add(Ins.IFB, IFB);
            operationTable.Add(Ins.IFC, IFC);
            operationTable.Add(Ins.IFE, IFE);
            operationTable.Add(Ins.IFN, IFN);
            operationTable.Add(Ins.IFG, IFG);
            operationTable.Add(Ins.IFA, IFA);
            operationTable.Add(Ins.IFL, IFL);
            operationTable.Add(Ins.IFU, IFU);
            operationTable.Add(Ins.STI, STI);
            operationTable.Add(Ins.STD, STD);
        }

        public Action<IOperandVal, IOperandVal, IDCPU> GetOperations(Ins instruction)
        {
            Action<IOperandVal, IOperandVal, IDCPU> value;
            if (operationTable.TryGetValue(instruction, out value))
            {
                return value;
            }
            else
            {
                //TODO: Throw Exception
                return null;
            }
        }


        private static void SET(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            b.VAL = a.VAL;
            idcpu.SkipNext = false;
        }

        private static void IFE(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            idcpu.SkipNext = a.VAL != b.VAL;
        }

        private static void BOR(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            b.VAL = (ushort) (b.VAL | a.VAL);
            idcpu.SkipNext = false;
        }

        private static void ADD(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            b.VAL = (ushort) (b.VAL + a.VAL);
            idcpu.SkipNext = false;
            //TODO: Handle Overflow
        }

        private static void SUB(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            b.VAL = (ushort) (b.VAL - a.VAL);
            idcpu.SkipNext = false;
            //TODO: Handle underflow
        }

        private static void MUL(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            b.VAL = (ushort) (b.VAL*a.VAL);
            idcpu.SkipNext = false;
            //TODO: Handle EX
        }

        private static void MLI(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            b.VAL = (ushort) (b.VAL*a.VAL);
            idcpu.SkipNext = false;
            //TODO: Handle Unsigned
        }

        private static void DIV(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            b.VAL = (ushort) (b.VAL | a.VAL);
            idcpu.SkipNext = false;
        }

        private static void DVI(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            b.VAL = (ushort) (b.VAL | a.VAL);
            idcpu.SkipNext = false;
            //TODO: Handle Unsigned
        }

        private static void MOD(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            if (a.VAL == 0)
            {
                b.VAL = 0;
            }
            else
            {
                b.VAL = (ushort) (b.VAL%a.VAL);
            }

            idcpu.SkipNext = false;
        }

        private static void MDI(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            if (a.VAL == 0)
            {
                b.VAL = 0;
            }
            else
            {
                b.VAL = (ushort) (b.VAL%a.VAL);
            }

            idcpu.SkipNext = false;
        }

        private static void AND(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);


            b.VAL = (ushort) (b.VAL & a.VAL);


            idcpu.SkipNext = false;
        }

        private static void XOR(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            b.VAL = (ushort) (b.VAL ^ a.VAL);

            idcpu.SkipNext = false;
        }

        private static void ASR(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            b.VAL = (ushort) (b.VAL >> a.VAL);

            idcpu.SkipNext = false;

            //TODO: Handle Unsigned
        }

        private static void SHL(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            b.VAL = (ushort) (b.VAL << a.VAL);

            idcpu.SkipNext = false;
            idcpu.EX = (ushort) (((b.VAL << 16) >> a.VAL) & 0xffff);
        }

        private static void IFB(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            idcpu.SkipNext = (a.VAL & b.VAL) == 0;
        }

        private static void IFC(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            idcpu.SkipNext = (a.VAL & b.VAL) != 0;
        }

        private static void IFN(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            idcpu.SkipNext = b.VAL == a.VAL;
        }

        private static void IFG(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            idcpu.SkipNext = !(b.VAL>a.VAL);
        }

        private static void IFA(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            idcpu.SkipNext = !(b.VAL > a.VAL);
        }

        private static void IFL(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            idcpu.SkipNext = !(b.VAL < a.VAL);
        }

        private static void IFU(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);

            idcpu.SkipNext = !(b.VAL < a.VAL);
        }

        private static void STI(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);
            b.VAL = a.VAL;
            idcpu.I++;
            idcpu.J++;

            idcpu.SkipNext = !(b.VAL < a.VAL);
        }

        private static void STD(IOperandVal a, IOperandVal b, IDCPU idcpu)
        {
            MovePCToNext(idcpu);
            b.VAL = a.VAL;
            idcpu.I--;
            idcpu.J--;

            idcpu.SkipNext = !(b.VAL < a.VAL);
        }


        private static void MovePCToNext(IDCPU idcpu)
        {
            idcpu.PC = (ushort) (idcpu.PC + idcpu.InstructionLength);
            idcpu.PC++;
        }
    }
}