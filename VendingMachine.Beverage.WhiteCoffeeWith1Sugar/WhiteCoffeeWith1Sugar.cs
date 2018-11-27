using System.Collections.Generic;
using System.ComponentModel.Composition;
using VendingMachine.Common;

namespace VendingMachine.Beverage.WhiteCoffeeWith1Sugar {
    [Export( typeof(IBeverage) )]
    public class WhiteCoffeeWith1Sugar: BaseBeverage {
        public override string Name => "White Coffee with 1 sugar";
        protected override List<string> ActionList { get; set; } = new List<string> {
            "Boiling water",
            "Adding sugar",
            "Adding coffee granules to cup",
            "Adding water",
            "Adding milk",
        };

    }
}