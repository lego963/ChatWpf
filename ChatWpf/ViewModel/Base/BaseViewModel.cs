using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ChatWpf.Core.Expressions;

namespace ChatWpf.ViewModel.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private readonly object _propertyValueCheckLock = new object();

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        protected async Task RunCommandAsync(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            lock (_propertyValueCheckLock)
            {
                if (updatingFlag.GetPropertyValue())
                    return;
                updatingFlag.SetPropertyValue(true);
            }

            try
            {
                await action();
            }
            finally
            {
                updatingFlag.SetPropertyValue(false);
            }
        }

        protected async Task<T> RunCommandAsync<T>(Expression<Func<bool>> updatingFlag, Func<Task<T>> action, T defaultValue = default(T))
        {
            lock (_propertyValueCheckLock)
            {
                if (updatingFlag.GetPropertyValue())
                    return defaultValue;

                updatingFlag.SetPropertyValue(true);
            }

            try
            {
                return await action();
            }
            finally
            {
                updatingFlag.SetPropertyValue(false);
            }
        }
    }
}
