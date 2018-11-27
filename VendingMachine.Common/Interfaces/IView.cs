namespace VendingMachine.Common {
    public interface IView {
        object DataContext { get; }
        bool? ShowDialog();
        void Close();
    }
}
