using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp
{
    class Concert:Item
    {
        public Concert(string name, int sell, int quality)
        {
            Name = name;
            SellIn = sell;
            Quality = quality;
        }
        public override void Updaterule()
        {
            SellIn -= 1;
            if (SellIn > 0)
            {
                IncreaseQ(1);
                if (SellIn <= 5)
                {
                    IncreaseQ(2);
                }
                else if (SellIn <= 10)
                {
                    IncreaseQ(1);
                }
            }
            else
            {
                Quality = 0;
            }
        }
        
    }
}
