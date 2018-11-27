using System.Collections.Generic;
using System.ComponentModel.Composition;
using VendingMachine.Common;

namespace VendingMachine.Beverage.IcedCoffee {
    [Export( typeof(IBeverage) )]
    public class IcedCoffee: BaseBeverage {
        public override string Name => "Iced Coffee";
        protected override List<string> ActionList { get; set; } = new List<string> {
            "Crushing Ice",
            "Adding ice to blender",
            "Adding coffee syrup to blender",
            "Blending ingredients",
            "Adding ingredients",
        };

    }
}
