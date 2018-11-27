using System.Collections.Generic;
using System.ComponentModel.Composition;
using VendingMachine.Common;

namespace VendingMachine.Beverage.HotChocolate {
    [Export( typeof(IBeverage) )]
    public class HotChocolate: BaseBeverage {
        public override string Name => "Hot Chocolate";
        protected override List<string> ActionList { get; set; } = new List<string> {
            "Boiling water",
            "Adding drinking chocolate to cup",
            "Adding water",
        };

    }
}
