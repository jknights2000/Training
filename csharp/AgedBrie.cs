using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{
    class AgedBrie : Item
    {
        public AgedBrie(int Sell,int quality)
        {
            Name = "Aged Brie";
            SellIn = Sell;
            Quality = quality;
        }
        public override void Updaterule()
        {
            SellIn -= 1;
            IncreaseQ(1);
        }
    }
}
