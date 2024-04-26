using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SPZO.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //BaseViewModel inherits INotifyPropertyChanged interface.
        //Every ViewModel inherits this class. Thanks to this I don't have to implement this interface to every ViewModel
        //This class allows oher functions to respond, when property is changed
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
