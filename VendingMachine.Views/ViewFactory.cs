using VendingMachine.Common;

namespace VendingMachine.Views {
    public class ViewFactory:IViewFactory {
        public IView GetView( string name ) {
            switch ( name ) {
                case "MainWindow": return new MainWindow();
                case "ProductionView": return new ProductionView();
                default: return null;
            }
        }
    }
}
