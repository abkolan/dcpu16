using System;
using System.Reflection;
using NUnit.Framework;

namespace dcpu16.tests
{
    [TestFixture]
    public class LineInterpreterTests
    {

        /*  https://raw.githubusercontent.com/gatesphere/demi-16/master/docs/dcpu-specs/dcpu-1-7.txt
         */
        private LineInterpreter li;

        [SetUp]
        public void SetUp()
        {
            li = new LineInterpreter();
        }

        [TearDown]
        public void TearDown()
        {
            li = null;
        }

        #region Tests for Instructions
        [Test]
        public void Test_for_lower_case()
        {
            string code = "set A,B";
            var statement = li.Interpret(code);

            Assert.AreEqual(Ins.SET, statement.Instruction);
        }

        [Test]
        public void Test_for_with_white_spaces()
        {
            string code = "    set      A,B";
            var statement = li.Interpret(code);

            Assert.AreEqual(Ins.SET, statement.Instruction);

        }


        [Test]
        public void Test_for_SET()
        {
            string code = "SET A,B";
            var statement = li.Interpret(code);

            Assert.AreEqual(Ins.SET, statement.Instruction);
            Assert.AreEqual(0x01, (Int16)Ins.SET);
        }


        [Test]
        public void Test_for_ADD()
        {
            string code = "ADD A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.ADD;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x02, (Int16)expected);
        }

        [Test]
        public void Test_for_SUB()
        {
            string code = "SUB A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.SUB;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x03, (Int16)expected);
        }

        [Test]
        public void Test_for_MUL()
        {
            string code = "MUL A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.MUL;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x04, (Int16)expected);
        }

        [Test]
        public void Test_for_MLI()
        {
            string code = "MLI A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.MLI;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x05, (Int16)expected);
        }


        [Test]
        public void Test_for_DIV()
        {
            string code = "DIV A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.DIV;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x06, (Int16)expected);
        }

        [Test]
        public void Test_for_DVI()
        {
            string code = "DVI A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.DVI;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x07, (Int16)expected);
        }

        [Test]
        public void Test_for_MOD()
        {
            string code = "MOD A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.MOD;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x08, (Int16)expected);
        }

        [Test]
        public void Test_for_MDI()
        {
            string code = "MDI A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.MDI;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x09, (Int16)expected);
        }


        [Test]
        public void Test_for_AND()
        {
            string code = "AND A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.AND;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x0A, (Int16)expected);
        }

        [Test]
        public void Test_for_BOR()
        {
            string code = "BOR A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.BOR;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x0B, (Int16)expected);
        }

        [Test]
        public void Test_for_XOR()
        {
            string code = "XOR A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.XOR;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x0C, (Int16)expected);
        }

        [Test]
        public void Test_for_SHR()
        {
            string code = "SHR A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.SHR;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x0D, (Int16)expected);
        }

        [Test]
        public void Test_for_ASR()
        {
            string code = "ASR A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.ASR;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x0E, (Int16)expected);
        }

        [Test]
        public void Test_for_SHL()
        {
            string code = "SHL A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.SHL;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x0F, (Int16)expected);
        }

        [Test]
        public void Test_for_IFB()
        {
            string code = "IFB A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.IFB;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x10, (Int16)expected);
        }


        [Test]
        public void Test_for_IFC()
        {
            string code = "IFC A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.IFC;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x11, (Int16)expected);
        }


        [Test]
        public void Test_for_IFE()
        {
            string code = "IFE A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.IFE;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x12, (Int16)expected);
        }



        [Test]
        public void Test_for_IFN()
        {
            string code = "IFN A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.IFN;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x13, (Int16)expected);
        }


        [Test]
        public void Test_for_IFG()
        {
            string code = "IFG A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.IFG;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x14, (Int16)expected);
        }


        [Test]
        public void Test_for_IFA()
        {
            string code = "IFA A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.IFA;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x15, (Int16)expected);
        }


        [Test]
        public void Test_for_IFL()
        {
            string code = "IFL A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.IFL;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x16, (Int16)expected);
        }


        [Test]
        public void Test_for_IFU()
        {
            string code = "IFU A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.IFU;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x17, (Int16)expected);
        }


        [Test]
        public void Test_for_ADX()
        {
            string code = "ADX A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.ADX;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x1a, (Int16)expected);
        }



        [Test]
        public void Test_for_SBX()
        {
            string code = "SBX A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.SBX;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x1b, (Int16)expected);
        }


        [Test]
        public void Test_for_STI()
        {
            string code = "STI A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.STI;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x1e, (Int16)expected);
        }


        [Test]
        public void Test_for_STD()
        {
            string code = "STD A,B";
            var statement = li.Interpret(code);
            Ins expected = Ins.STD;

            Assert.AreEqual(expected, statement.Instruction);
            Assert.AreEqual(0x1F, (Int16)expected);
        }


        [Test]
        public void Test_for_Invalid_Instruction_More_Chars()
        {
            string code = "SETT A,B";

            ArgumentException argumentException = null;

            try
            {
                var statement = li.Interpret(code);
            }
            catch (ArgumentException exception)
            {
                argumentException = exception;
            }
            string exceptionText = string.Format(Constants.Invalid_Instruction, "SETT");
            Assert.IsNotNull(argumentException);
            Assert.AreEqual(exceptionText, argumentException.Message);

        }

        [Test]
        public void Test_for_Invalid_Instruction_Less_Chars()
        {
            string code = "SE A,B";

            ArgumentException argumentException = null;

            try
            {
                var statement = li.Interpret(code);
            }
            catch (ArgumentException exception)
            {
                argumentException = exception;
            }
            string exceptionText = string.Format(Constants.Invalid_Instruction, "SE");
            Assert.IsNotNull(argumentException);
            Assert.AreEqual(exceptionText, argumentException.Message);

        }

        [Test]
        public void Test_for_Invalid_Instruction_No_Space()
        {
            string code = "SETA,B";

            ArgumentException argumentException = null;

            try
            {
                var statement = li.Interpret(code);
            }
            catch (ArgumentException exception)
            {
                argumentException = exception;
            }
            Assert.IsNotNull(argumentException);
        } 
        #endregion

        [Test]
        public void Test_for_RegisterA_anotherRegister()
        {
            string code = "SET A,B";
            var statement = li.Interpret(code);

            Assert.IsNotNull(statement.OperandA);
            Assert.IsInstanceOf<RegisterOperand>(statement.OperandA);
            Assert.IsInstanceOf<RegisterOperand>(statement.OperandB);

            var registerOperandA = statement.OperandA as RegisterOperand;
            var registerOperandB = statement.OperandB as RegisterOperand;


            Assert.AreEqual(registerOperandA.Register,Registers.B);
            Assert.AreEqual(registerOperandA.ToOpcode(),0x1);

            Assert.AreEqual(registerOperandB.Register, Registers.A);
            Assert.AreEqual(registerOperandB.ToOpcode(), 0x0);

        }

        [Test]
        public void Test_for_RegisterA_another_Register_mem()
        {
            string code = "SET A,[C]";
            var statement = li.Interpret(code);

            Assert.IsNotNull(statement.OperandA);
            Assert.IsInstanceOf<RegisterOperand>(statement.OperandA);
            Assert.IsInstanceOf<RegisterOperand>(statement.OperandB);

            var registerOperandA = statement.OperandA as RegisterOperand;
            var registerOperandB = statement.OperandB as RegisterOperand;


            Assert.AreEqual(registerOperandA.Register, Registers.C_MEM);
            Assert.AreEqual(registerOperandA.ToOpcode(), 0xA);

            Assert.AreEqual(registerOperandB.Register, Registers.A);
            Assert.AreEqual(registerOperandB.ToOpcode(), 0x0);

        }

        [Test]
        public void Test_for_Register_mem_another_Register()
        {
            string code = "SET [X],Y";
            var statement = li.Interpret(code);

            Assert.IsNotNull(statement.OperandA);
            Assert.IsInstanceOf<RegisterOperand>(statement.OperandA);
            Assert.IsInstanceOf<RegisterOperand>(statement.OperandB);

            var registerOperandA = statement.OperandA as RegisterOperand;
            var registerOperandB = statement.OperandB as RegisterOperand;


            Assert.AreEqual(registerOperandA.Register, Registers.Y);
            Assert.AreEqual(registerOperandA.ToOpcode(), 0x4);

            Assert.AreEqual(registerOperandB.Register, Registers.X_MEM);
            Assert.AreEqual(registerOperandB.ToOpcode(), 0xB);

        }

        [Test]
        public void Test_for_Register_mem_another_Register_mem()
        {
            string code = "BOR [J],[I]";
            var statement = li.Interpret(code);

            Assert.IsNotNull(statement.OperandA);
            Assert.IsInstanceOf<RegisterOperand>(statement.OperandA);
            Assert.IsInstanceOf<RegisterOperand>(statement.OperandB);

            var registerOperandA = statement.OperandA as RegisterOperand;
            var registerOperandB = statement.OperandB as RegisterOperand;


            Assert.AreEqual(registerOperandA.Register, Registers.I_MEM);
            Assert.AreEqual(registerOperandA.ToOpcode(), 0xE);

            Assert.AreEqual(registerOperandB.Register, Registers.J_MEM);
            Assert.AreEqual(registerOperandB.ToOpcode(), 0xF);
        }

        [Test]
        public void Test_for_Register_another_Register_lower_case()
        {
            string code = "ife x,y";
            var statement = li.Interpret(code);

            Assert.IsNotNull(statement.OperandA);
            Assert.IsInstanceOf<RegisterOperand>(statement.OperandA);
            Assert.IsInstanceOf<RegisterOperand>(statement.OperandB);

            var registerOperandA = statement.OperandA as RegisterOperand;
            var registerOperandB = statement.OperandB as RegisterOperand;


            Assert.AreEqual(registerOperandA.Register, Registers.Y);
            Assert.AreEqual(registerOperandA.ToOpcode(), 0x4);

            Assert.AreEqual(registerOperandB.Register, Registers.X);
            Assert.AreEqual(registerOperandB.ToOpcode(), 0x3);
        }


        [Test]
        public void Test_for_Register_memory_greater_than_30_hex()
        {
            string code = "SET A,0x8000";

            var statement = li.Interpret(code);

            Assert.IsNotNull(statement.OperandA);
            Assert.IsInstanceOf<WordOperand>(statement.OperandA);
            Assert.IsInstanceOf<RegisterOperand>(statement.OperandB);

            var wordOperandA = statement.OperandA as WordOperand;
            var registerOperandB = statement.OperandB as RegisterOperand;

            Assert.AreEqual(wordOperandA.WordOp,WordOps.NXT_WORD);
            short expected = 0x1f;
            short actual = (short)wordOperandA.WordOp;
            Assert.AreEqual(expected,actual);
            Assert.AreEqual(wordOperandA.Address,0x8000);
            Assert.AreEqual(registerOperandB.Register,Registers.A);
        }

        [Test]
        public void Test_for_Register_memory_less_than_30_hex_1()
        {
            string code = "SET A,0x0001";

            var statement = li.Interpret(code);

            Assert.IsNotNull(statement.OperandA);
            Assert.IsInstanceOf<WordOperand>(statement.OperandA);
            Assert.IsInstanceOf<RegisterOperand>(statement.OperandB);

            var wordOperandA = statement.OperandA as WordOperand;
            var registerOperandB = statement.OperandB as RegisterOperand;

            Assert.AreEqual(wordOperandA.WordOp, WordOps.LIT_VAL);
            Assert.AreEqual(wordOperandA.Address, 0x22);
            Assert.AreEqual(registerOperandB.Register, Registers.A);
        }


        [Test]
        public void Test_for_Register_memory_less_than_30_hex_2()
        {
            string code = "SET A,0x001E";

            var statement = li.Interpret(code);

            Assert.IsNotNull(statement.OperandA);
            Assert.IsInstanceOf<WordOperand>(statement.OperandA);
            Assert.IsInstanceOf<RegisterOperand>(statement.OperandB);

            var wordOperandA = statement.OperandA as WordOperand;
            var registerOperandB = statement.OperandB as RegisterOperand;

            Assert.AreEqual(wordOperandA.WordOp, WordOps.LIT_VAL);
            Assert.AreEqual(wordOperandA.Address, 0x3F);
            Assert.AreEqual(registerOperandB.Register, Registers.A);
        }

        [Test]
        public void Test_for_Register_memory_less_than_30_hex_3()
        {
            string code = "SET A,0x0000";

            var statement = li.Interpret(code);

            Assert.IsNotNull(statement.OperandA);
            Assert.IsInstanceOf<WordOperand>(statement.OperandA);
            Assert.IsInstanceOf<RegisterOperand>(statement.OperandB);

            var wordOperandA = statement.OperandA as WordOperand;
            var registerOperandB = statement.OperandB as RegisterOperand;

            Assert.AreEqual(wordOperandA.WordOp, WordOps.LIT_VAL);
            Assert.AreEqual(wordOperandA.Address, 0x21);
            Assert.AreEqual(registerOperandB.Register, Registers.A);
        }

        [Test]
        public void Test_for_register_memory_location()
        {
            string code = "SET A,[0x8000]";

            var statement = li.Interpret(code);

            Assert.IsNotNull(statement.OperandA);
            Assert.IsInstanceOf<WordOperand>(statement.OperandA);
            Assert.IsInstanceOf<RegisterOperand>(statement.OperandB);

            var wordOperandA = statement.OperandA as WordOperand;
            var registerOperandB = statement.OperandB as RegisterOperand;

            Assert.AreEqual(wordOperandA.WordOp, WordOps.NXT_WRD_MEM);

            short expected = 0x1e;
            short actual = (short)wordOperandA.WordOp;
            Assert.AreEqual(expected,actual);

            Assert.AreEqual(expected,wordOperandA.ToOpcode());
            Assert.AreEqual(0x8000, wordOperandA.ReferencedAddress());

            Assert.AreEqual(wordOperandA.Address, 0x8000);
            Assert.AreEqual(registerOperandB.Register, Registers.A);
        }



    }
}