using MyTips.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MyTips.Annotations;
using MyTips.Services;
using Xamarin.Forms;

namespace MyTips.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private class LocalizedTip : Tip, INotifyPropertyChanged
        {
            public LocalizedTip(Tip tip)
            {
                this.AmoutPercent = tip.AmoutPercent;
                this.Feedback = tip.Feedback;
            }

            public string Description
            {
                get
                {
                    var localizeFeedback = LocalizationUtility.GetFeedback(this.Feedback,
                        CultureInfo.CurrentUICulture);
                    return $"{localizeFeedback} ({this.AmoutPercent:##}%)";
                }
            }

            public override string ToString()
            {
                return Description;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private StateTipService stateTipService = new StateTipService();

        public MainPageViewModel()
        {
            this.CalculateTipCommand = new Command(() => CalculateTip(), () => CanCalculateTip());
            StateTips = new ObservableCollection<StateTip>(stateTipService.GetAll());
        }

        public bool CanCalculateTip()
        {
            if (SelectedTipIndex == -1) return false;
            if (BillAmount <= 0) return false;
            return true;
        }

        public void CalculateTip()
        {
            if (!CanCalculateTip()) return;
            var tip = GetSelectedTip(SelectedTipIndex);
            var total = BillAmount * (1 + tip.AmoutPercent / 100);
            if (total - Math.Floor(total) <= 0.25M)
                TotalAmount = Math.Floor(total);
            else if (total - Math.Floor(total) <= 0.5M)
                TotalAmount = Math.Floor(total) + 0.5M;
            else if (total - Math.Floor(total) <= 0.75M)
                TotalAmount = Math.Floor(total) + 0.5M;
            else
                TotalAmount = Math.Round(total);

            ActualTip = TotalAmount - BillAmount;
            ActualTipPercent = ActualTip / TotalAmount * 100;

        }


        public decimal BillAmount
        {
            get { return GetProperty<decimal>(); }
            set
            {
                SetProperty(value);
                ((Command)this.CalculateTipCommand).ChangeCanExecute();
            }
        }
        public decimal ActualTip
        {
            get { return GetProperty<decimal>(); }
            set { SetProperty(value); }
        }
        public decimal ActualTipPercent
        {
            get { return GetProperty<decimal>(); }
            set { SetProperty(value); }
        }

        public ObservableCollection<StateTip> StateTips
        {
            get { return GetProperty<ObservableCollection<StateTip>>(); }
            set
            {
                if (SetProperty(value))
                    SelectedStateTipsIndex = -1;
                ((Command)this.CalculateTipCommand).ChangeCanExecute();
            }
        }

        public int SelectedStateTipsIndex
        {
            get { return GetProperty<int>(); }
            set
            {
                if (SetProperty(value))
                {
                    var selectedStateTips = GetSelectedStateTip(value);
                    Tips = selectedStateTips?.Tips.Select(a => new LocalizedTip(a));
                    ((Command)this.CalculateTipCommand).ChangeCanExecute();
                }
            }
        }

        private StateTip GetSelectedStateTip(int index)
        {
            if (index == -1) return null;
            return StateTips[index];
        }

        public IEnumerable<Tip> Tips
        {
            get { return GetProperty<IEnumerable<Tip>>(); }
            set
            {
                if (SetProperty(value))
                {
                    var index = -1;
                    if (value != null)
                        for (int i = 0; i < Tips.Count(); i++)
                        {
                            if (Tips.ElementAt(i).Feedback == Feedback.Neutral)
                            {
                                index = i;
                                break;
                            }
                        }
                    SelectedTipIndex = index;
                    ((Command)this.CalculateTipCommand).ChangeCanExecute();
                }
            }
        }

        public int SelectedTipIndex
        {
            get { return GetProperty<int>(); }
            set
            {
                SetProperty(value);
                ((Command)this.CalculateTipCommand).ChangeCanExecute();
            }
        }


        private Tip GetSelectedTip(int selectedTipIndex)
        {
            if (selectedTipIndex == -1) return null;
            return Tips.ElementAtOrDefault(selectedTipIndex);
        }

        public decimal TotalAmount
        {
            get { return GetProperty<decimal>(); }
            set { SetProperty(value); }
        }

        public ICommand CalculateTipCommand { protected set; get; }
    }
}
