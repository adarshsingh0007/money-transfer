 using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MoneyTransferApp.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected BaseViewModel()
        {

        }



        public event PropertyChangedEventHandler PropertyChanged;



        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}