namespace dcpu16
{
    public enum Ins
    {
        NONE = 0x1d,
        SPL = 0x00,
        SET = 0x01,
        ADD = 0x02,
        SUB = 0x03,
        MUL = 0x04,
        MLI = 0x05,
        DIV = 0x06,
        DVI = 0x07,
        MOD = 0x08,
        MDI = 0x09,
        AND = 0x0A,
        BOR = 0x0B,
        XOR = 0x0C,
        SHR = 0x0D,
        ASR = 0x0E,
        SHL = 0x0F,
        IFB = 0x10,
        IFC = 0x11,
        IFE = 0x12,
        IFN = 0x13,
        IFG = 0x14,
        IFA = 0x15,
        IFL = 0x16,
        IFU = 0x17,
        ADX = 0x1A,
        SBX = 0x1B,
        STI = 0x1E,
        STD = 0x1F,
        DAT
    }
}