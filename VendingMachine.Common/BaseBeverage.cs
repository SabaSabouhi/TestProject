using System.Collections.Generic;

namespace VendingMachine.Common {
    public abstract class BaseBeverage: IBeverage {
        public abstract string Name { get; }
        private int _actionIndex;
        protected virtual List<string> ActionList { get; set; }
    
        public bool IsDone => _actionIndex >= ActionList.Count;

        public void StartProduction() => _actionIndex = 0;
        public string NextAction() {
            if ( IsDone ) return null;
            var action = ActionList[_actionIndex];
            _actionIndex++;
            return action;
        }

    }
}
