using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using VendingMachine.Common;

namespace VendingMachine.ViewModels {
    public class ProductionViewModel:BaseViewModel {
        private const int Delay = 1200;
        private ObservableCollection<string> _actionList;
        public ObservableCollection<string> ActionList {
            get => _actionList;
            set => SetProperty( ref _actionList, value );
        }
        private readonly List<string> _actions;
        public DelegateCommand DoneCommand { get; set; }
        public IView View { private get; set; }
        public bool IsDone {
            get => _isDone;
            set => SetProperty( ref _isDone, value );
        }
        public IBeverage Beverage {
            private get { return _beverage; }
            set {
                _beverage = value;
                _beverage.StartProduction();
            }
        }
        private  Timer _timer;
        private IBeverage _beverage;
        private bool _isDone;
        public ProductionViewModel() {
            DoneCommand = new DelegateCommand( OnDoneCommand, CanDoneCommand );
            IsDone = false;
            ActionList = new ObservableCollection<string>();
            _actions = new List<string>();
            _timer = new Timer( TimerCallback, 0, TimeSpan.FromMilliseconds( Delay ), TimeSpan.FromMilliseconds( Delay ) );
        }

        private void TimerCallback(object state) {
            if ( Beverage == null ) return;
            _timer.Change( Timeout.Infinite, Timeout.Infinite );
            try {
                var action = Beverage.NextAction();
                if ( !string.IsNullOrEmpty( action ) ) {
                    _actions.Add( action );
                    ActionList = new ObservableCollection<string>( _actions );
                    _timer.Change( TimeSpan.FromMilliseconds( Delay ), TimeSpan.FromMilliseconds( Delay ) );
                } else {
                    _timer.Dispose();
                    //DoneCommand.RaiseCanExecuteChanged();
                    IsDone = true;
                }

            } catch ( Exception ex ) {
                Debug.WriteLine( ex.Message );
                // Nothing to do, just in case of closing form
            }
        }

        private bool CanDoneCommand( object parameters ) => true;// Beverage?.IsDone ?? false;
        private void OnDoneCommand( object parameters ) {
            View.Close();
        }
    }
}
