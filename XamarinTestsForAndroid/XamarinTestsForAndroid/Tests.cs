using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;

namespace XamarinTestsForAndroid
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {            
            app = ConfigureApp
                .Android                
                .ApkFile ("../../../XamarinTestsForAndroid/bin/Debug/test.apk")
                .StartApp();
        }

        
        [Test]
        public void VerifyNewcomersInstruction()
        {
            do
            {
                app.Tap("nextView");
            }
            while (app.Query(x => x.Marked("nextView").Invoke("getText"))[0].ToString() != "Перейти в каталог");
            app.Tap("nextView");
            app.WaitForElement(x => x.Marked("name"));            
            string result = app.Query(x => x.Marked("name").Invoke("getText"))[0].ToString();

            Assert.AreEqual("Электроника", result);
        }

        [Test]
        public void VerifyMobileSearch()
        {
            do
            {
                app.Tap("nextView");
            }
            while (app.Query(x => x.Marked("nextView").Invoke("getText"))[0].ToString() != "Перейти в каталог");
            app.Tap("nextView");
            app.Tap("Электроника");
            app.Tap("Мобильные телефоны");
            app.Tap("Поиск");
            app.EnterText(c => c.Id("search_src_text"), "motorola nexus 6 32gb");
            app.DismissKeyboard();
            app.WaitForElement(x => x.Marked("text_name").Text("Motorola Nexus 6 (32GB)"));
            string result = app.Query(x => x.Marked("text_name").Invoke("getText"))[0].ToString();

            Assert.AreEqual("Motorola Nexus 6 (32GB)", result);           
        }

    }
}

