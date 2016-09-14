using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTips.ViewModels;
using System.Globalization;
using MyTips.Models;

namespace MyTips.Test
{
    [TestClass]
    public class MainPageViewModelTest
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }


        [TestMethod]
        public void CanCalculateTip_IfSelectedTipIsNullThenReturnFalse()
        {
            var target = new MainPageViewModel();
            target.SelectedTipIndex = -1;
            target.BillAmount = 100;

            var actual = target.CanCalculateTip();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CanCalculateTip_IfBillAmountIsLessEqualThenZeroThenReturnFalse()
        {
            var stateTips = new List<StateTip>()
            {
                new StateTip()
                {
                    State = "Test",
                    Tips = new Tip[1] {new Tip() {Feedback = Feedback.Neutral, AmoutPercent = 10}}
                }
            };

            var target = new MainPageViewModel();
            target.StateTips = new ObservableCollection<StateTip>(stateTips);
            target.SelectedStateTipsIndex = 0;
            target.SelectedTipIndex=0;
            target.BillAmount = -1;

            var actual = target.CanCalculateTip();

            Assert.IsFalse(actual);
        }

        [TestMethod]
        [DeploymentItem("CalculateTipData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                   "|DataDirectory|\\CalculateTipData.xml",
                   "Row",
                    DataAccessMethod.Sequential)]
        public void CalculateTip()
        {
            var provider = new CultureInfo("en-US");
            var billAmount = decimal.Parse((string)TestContext.DataRow["billAmount"], provider);
            var tipPercentage = decimal.Parse((string)TestContext.DataRow["TipPercentage"], provider);
            var totalAmount = decimal.Parse((string)TestContext.DataRow["TotalAmount"], provider);

            var stateTips = new List<StateTip>()
            {
                new StateTip()
                {
                    State = "Test",
                    Tips = new Tip[1] {new Tip() {Feedback = Feedback.Neutral, AmoutPercent = tipPercentage } }
                }
            };

            var target = new MainPageViewModel();
            target.StateTips = new ObservableCollection<StateTip>(stateTips);
            target.SelectedStateTipsIndex = 0;
            target.SelectedTipIndex=0;
            target.BillAmount = billAmount;

            target.CalculateTip();

            Assert.AreEqual(target.TotalAmount, totalAmount);
        }

    }
}
