using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Assembler.Tests
{
    [TestFixture]
    public class AssemblerTests
    {
        [Test]
        public void Test_For_Set_Reg_Reg()
        {
            Assembler testAssembler = new Assembler();

            var code = new List<string> {"SET A,B"};
            testAssembler.Assemble(code);

            Assert.AreEqual(1, testAssembler.Dump.Count);
            Assert.AreEqual(0x0401, testAssembler.Dump[0]);
        }

        [Test]
        public void Test_For_Set_Reg_Reg_Mem()
        {
            Assembler testAssembler = new Assembler();

            var code = new List<string> {"SET A,[B]"};
            testAssembler.Assemble(code);

            Assert.AreEqual(1, testAssembler.Dump.Count);
            Assert.AreEqual(0x2401, testAssembler.Dump[0]);
        }

        [Test]
        public void Test_For_Set_Reg_Lit_less_than_30()
        {
            Assembler testAssembler = new Assembler();

            var code = new List<string> {"SET A,0x0000"};
            testAssembler.Assemble(code);

            Assert.AreEqual(1, testAssembler.Dump.Count);
            Assert.AreEqual(0x8401, testAssembler.Dump[0]);
        }

        [Test]
        public void Test_For_Set_Reg_greater_than_30()
        {
            Assembler testAssembler = new Assembler();

            var code = new List<string> {"SET A,0x8000"};
            testAssembler.Assemble(code);

            Assert.AreEqual(2, testAssembler.Dump.Count);

            Assert.AreEqual(0x7c01, testAssembler.Dump[0]);
            Assert.AreEqual(0x8000, testAssembler.Dump[1]);
        }

        [Test]
        public void Test_For_Set_Reg_Mem_At_Address_Location()
        {
            Assembler testAssembler = new Assembler();

            var code = new List<string> {"SET A,[0x8000]"};
            testAssembler.Assemble(code);

            Assert.AreEqual(2, testAssembler.Dump.Count);

            //0x7801 0x8000
            Assert.AreEqual(0x7801, testAssembler.Dump[0]);
            Assert.AreEqual(0x8000, testAssembler.Dump[1]);
        }

        [Test]
        public void Test_For_Set_Reg_Label()
        {
            Assembler testAssembler = new Assembler();

            var code = new List<string> {"SET X,data", ":data", "DAT \"Hello, World!\",0"};
            testAssembler.Assemble(code);


            List<ushort> dump = new List<ushort>()
            {
                0x7c61,
                0x0002,
                0x0048,
                0x0065,
                0x006c,
                0x006c,
                0x006f,
                0x002c,
                0x0020,
                0x0057,
                0x006f,
                0x0072,
                0x006c,
                0x0064,
                0x0021,
                0x0000
            };

            CollectionAssert.AreEqual(dump, testAssembler.Dump);
        }

        [Test]
        public void Test_For_Set_Reg_Label2()
        {
            Assembler testAssembler = new Assembler();
            var code = new List<string> {"SET PC,end", ":end", "SET PC,end"};
            testAssembler.Assemble(code);


            List<ushort> dump = new List<ushort>()
            {
                0x7f81,
                0x0002,
                0x7f81,
                0x0002
            };

            CollectionAssert.AreEqual(dump, testAssembler.Dump);
        }

        [Test]
        public void Test_For_Data()
        {
            Assembler testAssembler = new Assembler();
            var code = new List<string> {"DAT \"Hello, World!\",0"};
            testAssembler.Assemble(code);


            List<ushort> dump = new List<ushort>()
            {
                0x0048,
                0x0065,
                0x006c,
                0x006c,
                0x006f,
                0x002c,
                0x0020,
                0x0057,
                0x006f,
                0x0072,
                0x006c,
                0x0064,
                0x0021,
                0x0000
            };

            CollectionAssert.AreEqual(dump, testAssembler.Dump);
        }

        [Test]
        public void Test_For_IFE_Reg_Mem_Reg()
        {
            Assembler testAssembler = new Assembler();

            var code = new List<string> {"IFE [X], J"};
            testAssembler.Assemble(code);

            Assert.AreEqual(1, testAssembler.Dump.Count);
            Assert.AreEqual(0x1d72, testAssembler.Dump[0]);
        }


        [Test]
        public void Test_For_IFE_Reg_Mem_Reg_Mem()
        {
            Assembler testAssembler = new Assembler();

            var code = new List<string> {"IFE [Y], [Z]"};
            testAssembler.Assemble(code);

            Assert.AreEqual(1, testAssembler.Dump.Count);
            Assert.AreEqual(0x3592, testAssembler.Dump[0]);
        }

        [Test]
        public void Test_For_IFG_Reg_Mem_Lit_less_than_30()
        {
            Assembler testAssembler = new Assembler();

            var code = new List<string> {"IFG [Y], 0x0004"};
            testAssembler.Assemble(code);

            Assert.AreEqual(1, testAssembler.Dump.Count);
            Assert.AreEqual(0x9594, testAssembler.Dump[0]);
        }

        [Test]
        public void Test_For_SET_Reg_Mem_Lit_Greater_Than_30()
        {
            Assembler testAssembler = new Assembler();

            var code = new List<string> {"SET [A],0x8001"};
            testAssembler.Assemble(code);

            Assert.AreEqual(2, testAssembler.Dump.Count);
            Assert.AreEqual(0x7d01, testAssembler.Dump[0]);
            Assert.AreEqual(0x8001, testAssembler.Dump[1]);
        }

        [Test]
        public void Test_For_SET_Reg_Mem_Address_Mem()
        {
            Assembler testAssembler = new Assembler();

            var code = new List<string> {"SET [A],[0x8001]"};
            testAssembler.Assemble(code);

            Assert.AreEqual(2, testAssembler.Dump.Count);
            Assert.AreEqual(0x7901, testAssembler.Dump[0]);
            Assert.AreEqual(0x8001, testAssembler.Dump[1]);
        }

        [Test]
        public void Test_Hello_World()
        {
            var assembler = new Assembler();

            assembler.Assemble(File.ReadAllLines("HelloWorld.asm"));

            int[] ints =
            {
                0x7cc1, 0x8000, 0x7c61, 0x0011, 0x8572, 0x7f81, 0x000f, 0x2c01, 0x7c0b, 0x7000, 0x01c1, 0x8862, 0x88c2,
                0x7f81, 0x0004, 0x7f81, 0x000f, 0x0048, 0x0065, 0x006c, 0x006c, 0x006f, 0x002c, 0x0020, 0x0057, 0x006f,
                0x0072, 0x006c, 0x0064, 0x0021, 0x0000
            };

            var dump = ints.Select(i => (uint) i).ToList();

            CollectionAssert.AreEqual(dump, assembler.Dump);
        }
    }
}