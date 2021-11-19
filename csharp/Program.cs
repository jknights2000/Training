using System;
using System.Collections.Generic;

namespace csharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("OMGHAI!");

            IList<Item> Items = new List<Item>{
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new AgedBrie (2,  0),
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Legendary ( "Sulfuras, Hand of Ragnaros", 0, 80),
                new Legendary ( "Sulfuras, Hand of Ragnaros", 0, 80),
                new Concert
                (
                    "Backstage passes to a TAFKAL80ETC concert",
                    15,
                    20
                ),
                new Concert
                (
                    "Backstage passes to a TAFKAL80ETC concert",
                    10,
                    49
                ),
                new Concert
                (
                    "Backstage passes to a TAFKAL80ETC concert",
                    5,
                    49
                ),
				// this conjured item does not work properly yet
				new Conjured ( "Conjured Mana Cake", 3, 6)
            };

            var app = new GildedRose(Items);


            for (var i = 0; i < 31; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                for (var j = 0; j < Items.Count; j++)
                {
                    System.Console.WriteLine(Items[j]);
                }
                Console.WriteLine("");
                app.UpdateQuality();
            }
            

            //ApprovalTest at = new ApprovalTest();
            //at.ThirtyDays();
        }
        /*
        public static void RunTests()
        {
            
            GildedRoseTest gt = new GildedRoseTest();
            gt.name();
            gt.Sellin();
            gt.MinQuality();
            gt.MaxQuality();
            gt.Sulfras();
            gt.Brie();
            gt.PastSellin();
            gt.Concert10();
            gt.Concert5();
            gt.Concert0();
            gt.ConjuredAlone();
            gt.ConjuredItem();
        }
        */
    }
}
