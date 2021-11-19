using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{
    class Conjured:Item
    {
        public Conjured(string name, int sell, int quality)
        {
            Name = name;
            SellIn = sell;
            Quality = quality;
        }
        public override void Updaterule()
        {
            SellIn -= 1;
            DecreaseQ( 2);
        }
    }
}
