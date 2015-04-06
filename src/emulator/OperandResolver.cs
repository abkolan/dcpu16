namespace Emulator3
{
    public class OperandResolver : IOperandResover
    {
        private readonly IDCPU idcpu;

        public OperandResolver(IDCPU idcpu)
        {
            this.idcpu = idcpu;
        }

        public IOperandVal GetOperandVal(AllOps allOps)
        {
            switch (allOps)
            {
                case AllOps.A:
                    return new RegisterA(idcpu);
                    break;
                case AllOps.A_MEM:
                    return new RegisterAMem(idcpu);
                    break;

                case AllOps.B:
                    return new RegisterB(idcpu);
                    break;
                case AllOps.B_MEM:
                    return new RegisterBMem(idcpu);
                    break;

                case AllOps.C:
                    return new RegisterC(idcpu);
                    break;
                case AllOps.C_MEM:
                    return new RegisterCMem(idcpu);
                    break;

                case AllOps.X:
                    return new RegisterX(idcpu);
                    break;
                case AllOps.X_MEM:
                    return new RegisterXMem(idcpu);
                    break;

                case AllOps.Y:
                    return new RegisterY(idcpu);
                    break;
                case AllOps.Y_MEM:
                    return new RegisterYMem(idcpu);
                    break;

                case AllOps.Z:
                    return new RegisterZ(idcpu);
                    break;
                case AllOps.Z_MEM:
                    return new RegisterZMem(idcpu);
                    break;


                case AllOps.I:
                    return new RegisterI(idcpu);
                    break;
                case AllOps.I_MEM:
                    return new RegisterIMem(idcpu);
                    break;

                case AllOps.J:
                    return new RegisterJ(idcpu);
                    break;
                case AllOps.J_MEM:
                    return new RegisterJMem(idcpu);
                    break;

                case AllOps.PC:
                    return new RegisterPC(idcpu);
                    break;

                case AllOps.LIT_0:
                case AllOps.LIT_1:
                case AllOps.LIT_3:
                case AllOps.LIT_4:
                case AllOps.LIT_5:
                case AllOps.LIT_6:
                case AllOps.LIT_7:
                case AllOps.LIT_8:
                case AllOps.LIT_9:
                case AllOps.LIT_11:
                case AllOps.LIT_12:
                case AllOps.LIT_13:
                case AllOps.LIT_14:
                case AllOps.LIT_15:
                case AllOps.LIT_16:
                case AllOps.LIT_17:
                case AllOps.LIT_18:
                case AllOps.LIT_19:
                case AllOps.LIT_20:
                case AllOps.LIT_21:
                case AllOps.LIT_22:
                case AllOps.LIT_23:
                case AllOps.LIT_24:
                case AllOps.LIT_25:
                case AllOps.LIT_26:
                case AllOps.LIT_27:
                case AllOps.LIT_28:
                case AllOps.LIT_29:
                case AllOps.LIT_30:
                    ushort litValue = (ushort) allOps;
                    litValue -= 33;
                    return new LitOperandVaL(litValue);
                    break;


                default:
                    return new RegisterA(idcpu);
            }
        }


        private class RegisterA : IOperandVal
        {
            public RegisterA(IDCPU idcpu)
            {
                this.idcpu = idcpu;
            }

            private IDCPU idcpu;


            public ushort VAL
            {
                get { return idcpu.A; }
                set { idcpu.A = value; }
            }
        }

        private class RegisterAMem : IOperandVal
        {
            private IDCPU idcpu;

            public RegisterAMem(IDCPU idcpu)
            {
                this.idcpu = idcpu;
            }

            public ushort VAL
            {
                get
                {
                    ushort address = idcpu.A;
                    return idcpu[address];
                }
                set
                {
                    ushort address = idcpu.A;
                    idcpu[address] = value;
                }
            }
        }

        private class RegisterB : IOperandVal
        {
            public RegisterB(IDCPU idcpu)
            {
                this.idcpu = idcpu;
            }

            private IDCPU idcpu;


            public ushort VAL
            {
                get { return idcpu.B; }
                set { idcpu.B = value; }
            }
        }

        private class RegisterBMem : IOperandVal
        {
            private IDCPU idcpu;

            public RegisterBMem(IDCPU idcpu)
            {
                this.idcpu = idcpu;
            }

            public ushort VAL
            {
                get
                {
                    ushort address = idcpu.B;
                    return idcpu[address];
                }
                set
                {
                    ushort address = idcpu.B;
                    idcpu[address] = value;
                }
            }
        }


        private class RegisterC : IOperandVal
        {
            public RegisterC(IDCPU idcpu)
            {
                this.idcpu = idcpu;
            }

            private IDCPU idcpu;


            public ushort VAL
            {
                get { return idcpu.C; }
                set { idcpu.C = value; }
            }
        }

        private class RegisterCMem : IOperandVal
        {
            private IDCPU idcpu;

            public RegisterCMem(IDCPU idcpu)
            {
                this.idcpu = idcpu;
            }

            public ushort VAL
            {
                get
                {
                    ushort address = idcpu.C;
                    return idcpu[address];
                }
                set
                {
                    ushort address = idcpu.C;
                    idcpu[address] = value;
                }
            }
        }

        private class RegisterX : IOperandVal
        {
            public RegisterX(IDCPU idcpu)
            {
                this.idcpu = idcpu;
            }

            private IDCPU idcpu;


            public ushort VAL
            {
                get { return idcpu.X; }
                set { idcpu.X = value; }
            }
        }

        private class RegisterXMem : IOperandVal
        {
            private IDCPU idcpu;

            public RegisterXMem(IDCPU idcpu)
            {
                this.idcpu = idcpu;
            }

            public ushort VAL
            {
                get
                {
                    ushort address = idcpu.X;
                    return idcpu[address];
                }
                set
                {
                    ushort address = idcpu.X;
                    idcpu[address] = value;
                }
            }
        }

        private class RegisterY : IOperandVal
        {
            public RegisterY(IDCPU idcpu)
            {
                this.idcpu = idcpu;
            }

            private IDCPU idcpu;


            public ushort VAL
            {
                get { return idcpu.Y; }
                set { idcpu.Y = value; }
            }
        }

        private class RegisterYMem : IOperandVal
        {
            private IDCPU idcpu;

            public RegisterYMem(IDCPU idcpu)
            {
                this.idcpu = idcpu;
            }

            public ushort VAL
            {
                get
                {
                    ushort address = idcpu.Y;
                    return idcpu[address];
                }
                set
                {
                    ushort address = idcpu.Y;
                    idcpu[address] = value;
                }
            }
        }


        private class RegisterZ : IOperandVal
        {
            public RegisterZ(IDCPU idcpu)
            {
                this.idcpu = idcpu;
            }

            private IDCPU idcpu;


            public ushort VAL
            {
                get { return idcpu.Z; }
                set { idcpu.Z = value; }
            }
        }

        private class RegisterZMem : IOperandVal
        {
            private IDCPU idcpu;

            public RegisterZMem(IDCPU idcpu)
            {
                this.idcpu = idcpu;
            }

            public ushort VAL
            {
                get
                {
                    ushort address = idcpu.Z;
                    return idcpu[address];
                }
                set
                {
                    ushort address = idcpu.Z;
                    idcpu[address] = value;
                }
            }
        }


        private class RegisterI : IOperandVal
        {
            public RegisterI(IDCPU idcpu)
            {
                this.idcpu = idcpu;
            }

            private IDCPU idcpu;


            public ushort VAL
            {
                get { return idcpu.I; }
                set { idcpu.I = value; }
            }
        }

        private class RegisterIMem : IOperandVal
        {
            private IDCPU idcpu;

            public RegisterIMem(IDCPU idcpu)
            {
                this.idcpu = idcpu;
            }

            public ushort VAL
            {
                get
                {
                    ushort address = idcpu.I;
                    return idcpu[address];
                }
                set
                {
                    ushort address = idcpu.I;
                    idcpu[address] = value;
                }
            }
        }

        private class RegisterJ : IOperandVal
        {
            public RegisterJ(IDCPU idcpu)
            {
                this.idcpu = idcpu;
            }

            private IDCPU idcpu;


            public ushort VAL
            {
                get { return idcpu.J; }
                set { idcpu.J = value; }
            }
        }

        private class RegisterJMem : IOperandVal
        {
            private IDCPU idcpu;

            public RegisterJMem(IDCPU idcpu)
            {
                this.idcpu = idcpu;
            }

            public ushort VAL
            {
                get
                {
                    ushort address = idcpu.J;
                    return idcpu[address];
                }
                set
                {
                    ushort address = idcpu.J;
                    idcpu[address] = value;
                }
            }
        }
    }
}