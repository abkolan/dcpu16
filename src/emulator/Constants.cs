using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Emulator3
{
    public enum AllOps
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

        PUSH = 0x18,
        POP = 0X18,


        NXT_WRD_MEM = 0X1E,
        NXT_WRD = 0x1f,

        /// <summary>
        /// Literal Minus 1
        /// </summary>
        LIT_M_1 = 0x20,

        LIT_0,
        LIT_1,
        LIT_2,
        LIT_3,
        LIT_4,
        LIT_5,
        LIT_6,
        LIT_7,
        LIT_8,
        LIT_9,
        LIT_10,
        LIT_11,
        LIT_12,
        LIT_13,
        LIT_14,
        LIT_15,
        LIT_16,
        LIT_17,
        LIT_18,
        LIT_19,
        LIT_20,
        LIT_21,
        LIT_22,
        LIT_23,
        LIT_24,
        LIT_25,
        LIT_26,
        LIT_27,
        LIT_28,
        LIT_29,
        LIT_30

    }

    public delegate void ActionRef<T1, T2>(ref T1 arg1, ref T2 arg2);

   
}
