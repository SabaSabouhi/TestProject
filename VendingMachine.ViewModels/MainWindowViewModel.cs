using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VendingMachine.Common;

namespace VendingMachine.ViewModels {

    public class MainWindowViewModel:BaseViewModel {
        private ObservableCollection<string> _beverages;
        public ObservableCollection<string> Beverages {
            get => _beverages;
            set => SetProperty( ref _beverages, value );
        }
        private List<IBeverage> _beverageParts;
        public DelegateCommand ProduceCommand { get; set; }

        public MainWindowViewModel() {
            Beverages = new ObservableCollection<string>();
            ProduceCommand = new DelegateCommand( OnProduceCommand );
        }

        public void AddBeverageParts(List<IBeverage> beverageParts) {
            _beverageParts = beverageParts;
            foreach ( var beverage in beverageParts ) {
                Beverages.Add( beverage.Name );
            }
        }

        private void OnProduceCommand( object parameters ) {
            var beverageName = (string)parameters;
            var beverage = _beverageParts.FirstOrDefault( x => x.Name == beverageName );

            var view = Global.ViewFactory.GetView( "ProductionView" );
            var viewModel = (ProductionViewModel)view.DataContext;
            viewModel.Beverage = beverage;
            viewModel.View = view;
            view.ShowDialog();
        }
    }
}
