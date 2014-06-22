using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using LumaQQ.NET;
using LumaQQ.NET.Entities;
using LumaQQ.NET.Packets;
using LumaQQ.NET.Packets.Out;

namespace LumaQQ.NET.Test
{
    /// <summary>
    /// Summary description for OutPacketsTest
    /// </summary>
    [TestClass]
    public class OutPacketsTest
    {
        QQUser qquser;
        ByteBuffer byteBuffer = new ByteBuffer();
        ByteBuffer expectedBuf = new ByteBuffer();
        public OutPacketsTest()
        {
            qquser = new QQUser(630377892, "hjfabu");
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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            byteBuffer.Initialize();
            expectedBuf.Initialize();
        }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        private byte[] ToBytes(ushort n)
        {
            byte[] b = new byte[2];
            b[0] = (byte)(((n & 0xff00) >> 8) & 0xff);
            b[1] = (byte)((n & 0x00ff) & 0xff);
            return b;
        }
        [TestMethod]
        public void RequestLoginTokenPacketUDPTest()
        {
            byteBuffer.Initialize();
            RequestLoginTokenPacket requestLoginTokenPacket = new RequestLoginTokenPacket(qquser);
            requestLoginTokenPacket.Fill(byteBuffer);
            byte[] sequenceBytes = ToBytes((ushort)requestLoginTokenPacket.Sequence);
            byte[] expectedPacket = new byte[] { 0x2, 0xe, 0x1b, 0x0, 0x62, sequenceBytes[0], sequenceBytes[1], 0x25, 0x92, 0xcd, 0xa4, 0x0, 0x3 };
            expectedBuf.Put(expectedPacket);
            Assert.AreEqual(expectedBuf, byteBuffer);
        }
    }
}
