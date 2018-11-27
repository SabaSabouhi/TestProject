using System;
using System.Windows.Input;

namespace VendingMachine.Common {
    public class DelegateCommand: ICommand {
        public event EventHandler CanExecuteChanged;

        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        #region .cTors.
        public DelegateCommand( Action execute, Func<bool> canExecute = null )
            : this( execute != null
                    ? p => execute()
                    : (Action<object>)null, canExecute != null
                    ? p => canExecute()
                    : (Func<object, bool>)null ) {
        }

        public DelegateCommand( Action<object> execute, Func<object, bool> canExecute = null ) {
            _execute = execute ?? throw new ArgumentNullException( nameof( execute ) );
            _canExecute = canExecute;
        }
        #endregion

        public bool CanExecute( object parameter ) => _canExecute == null || _canExecute( parameter );

        public void Execute( object parameter ) {
            if ( !CanExecute( parameter ) )
                throw new InvalidOperationException( "The command cannot be executed because the canExecute action returned false." );

            _execute( parameter );
        }

        public void RaiseCanExecuteChanged() => OnCanExecuteChanged( EventArgs.Empty );

        protected virtual void OnCanExecuteChanged( EventArgs e ) => CanExecuteChanged?.Invoke( this, e );
    }
}
