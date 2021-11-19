using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{
    class Legendary:Item
    {
        public Legendary(string name, int sell, int quality)
        {
            Name = name;
            SellIn = sell;
            Quality = quality;
        }
        public override void Updaterule()
        {
            return;
        }
    }
}
