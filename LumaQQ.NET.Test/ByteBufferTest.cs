using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using LumaQQ.NET.Utils;
namespace LumaQQ.NET.Test
{
    /// <summary>
    /// Summary description for ByteBufferTest
    /// </summary>
    [TestClass]
    public class ByteBufferTest
    {
        ByteBuffer byteBuffer = new ByteBuffer();

        public ByteBufferTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void PushByteTest()
        {
            byteBuffer.Initialize();
            byteBuffer.Put((byte)1);
            byteBuffer.Put((byte)2);
            byteBuffer.Put((byte)3);

            Assert.AreEqual<int>(3, byteBuffer.Length);

            byte[] byteArray = byteBuffer.ToByteArray();

            Assert.AreEqual<byte>((byte)1, byteArray[0]);
            Assert.AreEqual<byte>((byte)2, byteArray[1]);
            Assert.AreEqual<byte>((byte)3, byteArray[2]);

            Assert.AreEqual<byte>((byte)1, byteBuffer.Get());
            Assert.AreEqual<byte>((byte)2, byteBuffer.Get());
            Assert.AreEqual<byte>((byte)3, byteBuffer.Get());

            Assert.AreEqual<int>(3, byteBuffer.Position);
        }

        [TestMethod]
        public void PushUInt16Test()
        {
            byteBuffer.Initialize();
            UInt16 v1 = 0x0067;
            UInt16 v2 = 0x0080;
            UInt16 v3 = 0x0081;
            byteBuffer.PutUShort(v1);
            byteBuffer.PutUShort(v2);
            byteBuffer.PutUShort(v3);

            Assert.AreEqual<int>(6, byteBuffer.Length);

            byte[] byteArray = byteBuffer.ToByteArray();

            Assert.AreEqual<UInt16>(v1, (UInt16)((byteArray[0] << 8) | byteArray[1]));
            Assert.AreEqual<UInt16>(v2, (UInt16)((byteArray[2] << 8) | byteArray[3]));
            Assert.AreEqual<UInt16>(v3, (UInt16)((byteArray[4] << 8) | byteArray[5]));

            Assert.AreEqual<UInt16>(v1, byteBuffer.GetUShort());
            Assert.AreEqual<UInt16>(v2, byteBuffer.GetUShort());
            Assert.AreEqual<UInt16>(v3, byteBuffer.GetUShort());

            Assert.AreEqual<int>(6, byteBuffer.Position);
        }

        [TestMethod]
        public void PushUIntTest()
        {
            byteBuffer.Initialize();
            uint v1 = 0x4567;
            uint v2 = 0x7680;
            uint v3 = 0x7871;
            byteBuffer.PutInt(v1);
            byteBuffer.PutInt(v2);
            byteBuffer.PutInt(v3);

            Assert.AreEqual<int>(12, byteBuffer.Length);

            byte[] byteArray = byteBuffer.ToByteArray();

            Assert.AreEqual<uint>(v1, (uint)((byteArray[0] << 24) | (byteArray[1] << 16) | (byteArray[2] << 8) | byteArray[3]));
            Assert.AreEqual<uint>(v2, (uint)((byteArray[4] << 24) | (byteArray[5] << 16) | (byteArray[6] << 8) | byteArray[7]));
            Assert.AreEqual<uint>(v3, (uint)((byteArray[8] << 24) | (byteArray[9] << 16) | (byteArray[10] << 8) | byteArray[11]));

            Assert.AreEqual<uint>(v1, byteBuffer.GetUInt());
            Assert.AreEqual<uint>(v2, byteBuffer.GetUInt());
            Assert.AreEqual<uint>(v3, byteBuffer.GetUInt());

            Assert.AreEqual<int>(12, byteBuffer.Position);
        }

        [TestMethod]
        public void PushLongTest()
        {
            byteBuffer.Initialize();
            long v1 = 0x6567;
            long v2 = 0x6680;
            long v3 = 0x9881;
            byteBuffer.PutLong(v1);
            byteBuffer.PutLong(v2);
            byteBuffer.PutLong(v3);

            Assert.AreEqual<int>(12, byteBuffer.Length);

            byte[] byteArray = byteBuffer.ToByteArray();

            Assert.AreEqual<long>(v1, (long)((byteArray[0] << 24) | (byteArray[1] << 16) | (byteArray[2] << 8) | byteArray[3]));
            Assert.AreEqual<long>(v2, (long)((byteArray[4] << 24) | (byteArray[5] << 16) | (byteArray[6] << 8) | byteArray[7]));
            Assert.AreEqual<long>(v3, (long)((byteArray[8] << 24) | (byteArray[9] << 16) | (byteArray[10] << 8) | byteArray[11]));

            Assert.AreEqual<long>(v1, byteBuffer.GetLong());
            Assert.AreEqual<long>(v2, byteBuffer.GetLong());
            Assert.AreEqual<long>(v3, byteBuffer.GetLong());

            Assert.AreEqual<int>(12, byteBuffer.Position);
        }

        [TestMethod]
        public void PushByteIndexTest()
        {
            byteBuffer.Initialize();
            byte v1 = 1;
            byte v2 = 2;
            byte v3 = 3;
            byteBuffer.Put(v1);
            byteBuffer.Put(v2);
            Assert.AreEqual<byte>(v1, byteBuffer.Get());

            byteBuffer.Put(0, v3);

            Assert.AreEqual<int>(1, byteBuffer.Position);

            byte[] bytes = byteBuffer.ToByteArray();

            Assert.AreEqual<byte>(v3, bytes[0]);

            Assert.AreEqual<int>(2, byteBuffer.Length);
        }

        [TestMethod]
        public void PushUInt16IndexTest()
        {
            byteBuffer.Initialize();
            UInt16 v1 = 90;
            UInt16 v2 = 100;
            UInt16 v3 = 200;
            byteBuffer.PutUShort(v1);
            byteBuffer.PutUShort(v2);
           
            Assert.AreEqual<UInt16>(v1, byteBuffer.GetUShort());

            byteBuffer.PutUShort(0, v3);
            byte[] byteArray = byteBuffer.ToByteArray();
            Assert.AreEqual<UInt16>(v3, (UInt16)((byteArray[0] << 8) | byteArray[1]));
            Assert.AreEqual<UInt16>(v2, (UInt16)((byteArray[2] << 8) | byteArray[3]));
            //位置和长度不能变
            Assert.AreEqual<int>(4, byteBuffer.Length);
            Assert.AreEqual<int>(2, byteBuffer.Position);
        }

        [TestMethod]
        public void PushUIntIndexTest()
        {
            byteBuffer.Initialize();
            uint v1 = 0x4567;
            uint v2 = 0x7680;
            uint v3 = 0x7871;
            byteBuffer.PutInt(v1);
            byteBuffer.PutInt(v2);          
            Assert.AreEqual<uint>(v1, byteBuffer.GetUInt());
            byteBuffer.PutInt(0,v3);
            byte[] byteArray = byteBuffer.ToByteArray();

            Assert.AreEqual<uint>(v3, (uint)((byteArray[0] << 24) | (byteArray[1] << 16) | (byteArray[2] << 8) | byteArray[3]));
            Assert.AreEqual<uint>(v2, (uint)((byteArray[4] << 24) | (byteArray[5] << 16) | (byteArray[6] << 8) | byteArray[7]));

            //位置和长度不能变
            Assert.AreEqual<int>(4, byteBuffer.Position);
            Assert.AreEqual<int>(8, byteBuffer.Length);
        }

        [TestMethod]
        public void PushLongIndexTest()
        {
            byteBuffer.Initialize();
            long v1 = 0x6567;
            long v2 = 0x6680;
            long v3 = 0x9881;
            byteBuffer.PutLong( v1);
            byteBuffer.PutLong( v2);
            
            Assert.AreEqual<long>(v1, (long)byteBuffer.GetLong());
            byteBuffer.PutLong(4, v3);

            byte[] byteArray = byteBuffer.ToByteArray();

            Assert.AreEqual<long>(v1, (long)((byteArray[0] << 24) | (byteArray[1] << 16) | (byteArray[2] << 8) | byteArray[3]));
            Assert.AreEqual<long>(v3, (long)((byteArray[4] << 24) | (byteArray[5] << 16) | (byteArray[6] << 8) | byteArray[7]));

            //位置和长度不能变
            Assert.AreEqual<int>(4, byteBuffer.Position);
            Assert.AreEqual<int>(8, byteBuffer.Length);
        }

        //#region Insert

        //[TestMethod]
        //public void InsertByteIndexTest()
        //{
        //    byteBuffer.Initialize();
        //    byte v1 = 1;
        //    byte v2 = 2;
        //    byte v3 = 3;
        //    byteBuffer.PushByte(v1);
        //    byteBuffer.PushByte(0, v2);
        //    byteBuffer.PushByte(1, v3);

        //    Assert.AreEqual<int>(3, byteBuffer.Length);

        //    byte[] byteArray = byteBuffer.ToByteArray();

        //    Assert.AreEqual<byte>(v2, byteArray[0]);
        //    Assert.AreEqual<byte>(v3, byteArray[1]);
        //    Assert.AreEqual<byte>(v1, byteArray[2]);

        //    Assert.AreEqual<byte>(v2, byteBuffer.PopByte());
        //    Assert.AreEqual<byte>(v3, byteBuffer.PopByte());
        //    Assert.AreEqual<byte>(v1, byteBuffer.PopByte());

        //    Assert.AreEqual<int>(3, byteBuffer.Position);
        //}

        //[TestMethod]
        //public void InsertUInt16IndexTest()
        //{
        //    byteBuffer.Initialize();
        //    UInt16 v1 = 90;
        //    UInt16 v2 = 100;
        //    UInt16 v3 = 200;
        //    byteBuffer.PushUInt16(v1);
        //    byteBuffer.PushUInt16(2, v2);
        //    byteBuffer.PushUInt16(0, v3);

        //    Assert.AreEqual<int>(6, byteBuffer.Length);

        //    byte[] byteArray = byteBuffer.ToByteArray();

        //    Assert.AreEqual<UInt16>(v3, (UInt16)((byteArray[0] << 8) | byteArray[1]));
        //    Assert.AreEqual<UInt16>(v1, (UInt16)((byteArray[2] << 8) | byteArray[3]));
        //    Assert.AreEqual<UInt16>(v2, (UInt16)((byteArray[4] << 8) | byteArray[5]));

        //    Assert.AreEqual<UInt16>(v3, byteBuffer.PopUInt16());
        //    Assert.AreEqual<UInt16>(v1, byteBuffer.PopUInt16());
        //    Assert.AreEqual<UInt16>(v2, byteBuffer.PopUInt16());

        //    Assert.AreEqual<int>(6, byteBuffer.Position);
        //}

        //[TestMethod]
        //public void InsertUIntIndexTest()
        //{
        //    byteBuffer.Initialize();
        //    uint v1 = 0x4567;
        //    uint v2 = 0x7680;
        //    uint v3 = 0x7871;
        //    byteBuffer.PushInt(0, v1);
        //    byteBuffer.PushInt(0, v2);
        //    byteBuffer.PushInt(4, v3);

        //    Assert.AreEqual<int>(12, byteBuffer.Length);

        //    byte[] byteArray = byteBuffer.ToByteArray();

        //    Assert.AreEqual<uint>(v2, (uint)((byteArray[0] << 24) | (byteArray[1] << 16) | (byteArray[2] << 8) | byteArray[3]));
        //    Assert.AreEqual<uint>(v3, (uint)((byteArray[4] << 24) | (byteArray[5] << 16) | (byteArray[6] << 8) | byteArray[7]));
        //    Assert.AreEqual<uint>(v1, (uint)((byteArray[8] << 24) | (byteArray[9] << 16) | (byteArray[10] << 8) | byteArray[11]));

        //    Assert.AreEqual<uint>(v2, byteBuffer.PopUInt());
        //    Assert.AreEqual<uint>(v3, byteBuffer.PopUInt());
        //    Assert.AreEqual<uint>(v1, byteBuffer.PopUInt());

        //    Assert.AreEqual<int>(12, byteBuffer.Position);
        //}

        //[TestMethod]
        //public void InsertLongIndexTest()
        //{
        //    byteBuffer.Initialize();
        //    long v1 = 0x6567;
        //    long v2 = 0x6680;
        //    long v3 = 0x9881;
        //    byteBuffer.PushLong(0, v1);
        //    byteBuffer.PushLong(0, v2);
        //    byteBuffer.PushLong(4, v3);

        //    Assert.AreEqual<int>(12, byteBuffer.Length);

        //    byte[] byteArray = byteBuffer.ToByteArray();

        //    Assert.AreEqual<long>(v2, (long)((byteArray[0] << 24) | (byteArray[1] << 16) | (byteArray[2] << 8) | byteArray[3]));
        //    Assert.AreEqual<long>(v3, (long)((byteArray[4] << 24) | (byteArray[5] << 16) | (byteArray[6] << 8) | byteArray[7]));
        //    Assert.AreEqual<long>(v1, (long)((byteArray[8] << 24) | (byteArray[9] << 16) | (byteArray[10] << 8) | byteArray[11]));

        //    Assert.AreEqual<long>(v2, byteBuffer.PopLong());
        //    Assert.AreEqual<long>(v3, byteBuffer.PopLong());
        //    Assert.AreEqual<long>(v1, byteBuffer.PopLong());

        //    Assert.AreEqual<int>(12, byteBuffer.Position);
        //}

        //#endregion
    }
}
