namespace VendingMachine.Common {
    public interface IViewFactory {
        IView GetView( string name );
    }
}
