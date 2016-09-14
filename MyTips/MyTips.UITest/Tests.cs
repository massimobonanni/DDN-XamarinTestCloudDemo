using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace MyTips.UITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp
                .Android
                .ApkFile("../../../MyTips/MyTips.Droid/bin/Release/MyTips.MyTips.apk")
                .StartApp();
        }

        [Test(Description = "Scenario di calcolo completo")]
        public void CalculateTipComplete()
        {
            //app.Repl();
            app.Tap(a => a.Marked("BillAmountText"));
            app.ClearText();
            app.EnterText("20");
            app.Screenshot("Inserimento totale scontrino");
            app.Tap("StateTipsPicker_Container");
            app.Tap(a => a.Marked("numberpicker_input"));
            app.Tap(a => a.Marked("button1"));
            app.Screenshot("Selezione nazione");
            app.Tap(a => a.Marked("CalculateButton"));
            app.Screenshot("Calcolo mancia");
            var results = app.Query(c => c.Marked("TotalAmountLabel"));
            app.Screenshot("Visualizzazione risultato");
            Assert.IsTrue(Convert.ToDecimal(results[0].Text) > 0);
        }

        [Test(Description = "Il tasto di calcolo non è disponibile se il totale dello scontrino non è positivo")]
        public void CalculateNotAvailableIfBillAmountLessZero()
        {
            //app.Repl();
            app.ClearText(a => a.Marked("BillAmountText"));
            app.EnterText("-1");
            app.Screenshot("Inserimento totale scontrino");
            app.DismissKeyboard();
            app.Screenshot("Disabilitazione tastiera");
            app.Query(a => a.Button("CalculateButton"));
            var enable = app.Query(a => a.Button("CalculateButton"))[0].Enabled;
            app.Screenshot("Verifica dell'abilitazione del tasto Calcola");
            Assert.IsFalse(enable);
        }

        [Test(Description = "Il tasto di calcolo non è disponibile se non è stato selezionato lo stato")]
        public void CalculateNotAvailableIfStateNotSelected()
        {
            //app.Repl();
            app.ClearText(a => a.Marked("BillAmountText"));
            app.EnterText("20");
            app.Screenshot("Inserimento totale scontrino");
            app.DismissKeyboard();
            app.Screenshot("Disabilitazione tastiera");
            app.Query(a => a.Button("CalculateButton"));
            var enable = app.Query(a => a.Button("CalculateButton"))[0].Enabled;
            Assert.IsFalse(enable);
        }

        [Test(
            Description =
                "Il tasto di calcolo non è disponibile se è stato selezionato lo stato ma lo scontrino è negativo")]
        public void CalculateNotAvailableIfStateSelectedAndBillAmountLessZero()
        {
            //app.Repl();
            app.ClearText(a => a.Marked("BillAmountText"));
            app.EnterText("-1");
            app.Screenshot("Inserimento totale scontrino");
            app.DismissKeyboard();
            app.Screenshot("Disabilitazione tastiera");
            app.Tap("StateTipsPicker_Container");
            app.Tap(a => a.Marked("numberpicker_input"));
            app.Query(c => c.Class("numberPicker").Invoke("setValue", 2));
            app.Tap(a => a.Marked("button1"));
            app.Screenshot("Selezione stato");
            app.Query(a => a.Button("CalculateButton"));
            var enable = app.Query(a => a.Button("CalculateButton"))[0].Enabled;
            Assert.IsFalse(enable);
        }

        [Test(
            Description =
                "Il tasto di calcolo non è disponibile se è stato selezionato lo stato ma lo scontrino è negativo")]
        public void test()
        {
            app.Repl();
        }
    }
}

