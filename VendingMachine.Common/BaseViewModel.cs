using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VendingMachine.Common {
    public abstract class BaseViewModel: INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>( ref T field, T value, [CallerMemberName] string propertyName = null ) {
            if ( Equals( field, value ) ) { return false; }

            field = value;
            RaisePropertyChanged( propertyName );
            return true;
        }

        public void RaisePropertyChanged( [CallerMemberName] string propertyName = null )
            => OnPropertyChanged( new PropertyChangedEventArgs( propertyName ) );
        protected virtual void OnPropertyChanged( PropertyChangedEventArgs e )
            => PropertyChanged?.Invoke( this, e );
    }
}
