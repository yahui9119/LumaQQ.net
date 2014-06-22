using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using LumaQQ.NET.Utils;
namespace LumaQQ.NET.Test
{
    /// <summary>
    /// Summary description for CrypterTest
    /// </summary>
    [TestClass]
    public class CrypterTest
    {
        public CrypterTest()
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

        private byte[] GetBytes(string s)
        {
            return Encoding.Default.GetBytes(s);
        }
        private string GetString(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }
        [TestMethod]
        public void EncryptTest1()
        {
            string plain = "信息";
            string key = "*(()&*%(%)I^KGLKLadfasdfasdfsdafD";
            Crypt(plain, key);
            plain = "一段信息";
            key = "100asdf01asasdfasdfdf0asdf";
            Crypt(plain, key);
            plain = @"这里是多普达D600的家园,欢迎大家的到来,D600区需要你的加入和支持！谢谢！
=====================================
**看帖回帖是一种美德，更是一种义务，请尊重发帖人的劳动成果。
**提问前请先看置顶帖子和教程帖子，能够解决大部分问题。
**凡是在论坛里的发言，都要以文明的言语进行表达，让我们成为文明奥运的带头人。
=====================================
本版加分规则：
【25分】奖励：凡是在D600区里发表的资源贴，只要通过版主或5个以上会员测试通过的，将给予【25分】奖励。
【10分】奖励：凡是在会员提问贴里成功帮人解决问题的，视情况将给予【10至20分】奖励。
【5 分】奖励：凡是在D600各报道加分贴里报道者，将按照惯例给予【5分】奖励。
【精华】奖励：对够精华帖的奖励【50分】并加【精华】。
=====================================
本版扣分规则：
扣【25分】：凡是在D600区里发表广告或者淫秽方面的帖子，坚决删除并以扣【25分】以示惩戒。情节严重者将由总版直接查封ID。
扣【5 分】：发贴提问的时候，为方便大家对你的帮助，标题要明确，不要发“跪求”、""急""、“救急”、“SOS“等毫无意义的主题。首次警告，警告无效者以扣【5分】为惩罚。
=====================================
本版高亮辨识：
红色==庆祝帖和临时帖
蓝色==程序资源帖
绿色==教程帖
紫色==主题铃声图片帖
橙色==游戏帖";
            key = "asdfasdfawerw3tyhjnghu7!#^(^%##%^^&";
            Crypt(plain, key);

        }

        private void Crypt(string plain, string key)
        {
            byte[] plainBytes = GetBytes(plain);
            byte[] keyBytes = GetBytes(key);
            Crypter crypter = new Crypter();
            byte[] encrypted = crypter.Encrypt(plainBytes, keyBytes);
            byte[] decrypted = crypter.Decrypt(encrypted, keyBytes);
            string decryptedString = GetString(decrypted);
            Assert.AreEqual<string>(plain, decryptedString);
        }
    }
}
