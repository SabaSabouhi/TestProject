using System.Collections.Generic;
using System.ComponentModel.Composition;
using VendingMachine.Common;

namespace VendingMachine.Beverage.LemonTea {
    [Export( typeof(IBeverage) )]
    public class LemonTea: BaseBeverage {
        public override string Name => "Lemon Tes";
        protected override List<string> ActionList { get; set; } = new List<string> {
            "Boiling water",
            "Adding water",
            "Steeping tea bag in hot water",
            "Adding lemon",
        };

    }
}