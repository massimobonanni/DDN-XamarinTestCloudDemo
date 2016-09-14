using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MyTips.Annotations;

namespace MyTips.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Interface INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Gestione proprietà
        private readonly Dictionary<string, object> propertyDictionary = new Dictionary<string, object>();
        protected bool SetProperty<T>(T value, [CallerMemberName()] string propertyName = null)
        {
            if (propertyName == null)
                throw new ArgumentNullException();
            var retVal = false;
            if (propertyDictionary.ContainsKey(propertyName))
            {
                var oldValue = (T)propertyDictionary[propertyName];
                if (!AreEquals(oldValue, value))
                {
                    propertyDictionary[propertyName] = value;
                    OnPropertyChanged(propertyName);
                    retVal = true;
                }
            }
            else
            {
                propertyDictionary[propertyName] = value;
                OnPropertyChanged(propertyName);
                retVal = true;
            }
            return retVal;
        }
        private bool AreEquals(object obj1, object obj2)
        {
            if (obj1 == null & obj2 == null)
                return true;
            if (obj1 != null & obj2 == null)
                return false;
            if (obj1 == null)
                return false;
            return obj1.Equals(obj2);
        }

        protected T GetProperty<T>([CallerMemberName()] string propertyName = null)
        {
            if (propertyName == null)
                throw new ArgumentNullException();
            T retval = default(T);
            if (propertyDictionary.ContainsKey(propertyName))
            {
                try
                {
                    retval = (T)propertyDictionary[propertyName];
                }
                catch (Exception ex)
                {
                    retval = default(T);
                }
            }
            return retval;
        }
        #endregion

    }
}
