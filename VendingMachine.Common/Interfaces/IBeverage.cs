namespace VendingMachine.Common {
    public interface IBeverage {
        string Name { get; }
        bool IsDone { get; }
        void StartProduction();
        string NextAction();
    }
}
