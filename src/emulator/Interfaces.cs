using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dcpu16;

namespace Emulator3
{
    public interface IOperationResolver
    {
        Action<IOperandVal, IOperandVal, IDCPU> GetOperations(Ins instruction);
    }


    public interface IOperandResover
    {
        IOperandVal GetOperandVal(AllOps allOps);
    }

    public interface IDCPU
    {
        ushort A { get; set; }
        ushort B { get; set; }
        ushort C { get; set; }
        ushort X { get; set; }
        ushort Y { get; set; }
        ushort Z { get; set; }
        ushort I { get; set; }
        ushort J { get; set; }
        ushort PC { get; set; }
        ushort EX { get; set; }
        ushort this[int address] { get; set; }

        //Execution State Related Properties
        ushort InstructionLength { get; }
        bool SkipNext { get; set; }
    }
}