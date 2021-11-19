using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        [Test]
        public void name()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual("foo", Items[0].Name);
        }
        [Test]
        public void Sellin()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(-1, Items[0].SellIn);
        }
        [Test]
        public void MinQuality()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].Quality);
        }
        public void MaxQuality()
        {
            IList<Item> Items = new List<Item> { new AgedBrie(0, 50) };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(50, Items[0].Quality);
        }
        [Test]
        public void Sulfras()
        {
            IList<Item> Items = new List<Item> { new Legendary ("Sulfuras, Hand of Ragnaros", 10, 80 ) };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(80, Items[0].Quality);
            Assert.AreEqual(10, Items[0].SellIn);
        }

        [Test]
        public void Brie()
        {
            IList<Item> Items = new List<Item> { new AgedBrie (0,30 ) };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(31, Items[0].Quality);
            Assert.AreEqual(-1, Items[0].SellIn);
        }
        [Test]
        public void PastSellin()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 30 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(28, Items[0].Quality);
            Assert.AreEqual(-1, Items[0].SellIn);
        }
        [Test]
        public void Concert10()
        {
            IList<Item> Items = new List<Item> { new Concert ( "Backstage passes to a TAFKAL80ETC concert", 10, 30 ) };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(32, Items[0].Quality);
            Assert.AreEqual(9, Items[0].SellIn);
        }
        [Test]
        public void Concert5()
        {
            IList<Item> Items = new List<Item> { new Concert("Backstage passes to a TAFKAL80ETC concert", 5, 30) };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(33, Items[0].Quality);
            Assert.AreEqual(4, Items[0].SellIn);
        }
        [Test]
        public void Concert0()
        {
            IList<Item> Items = new List<Item> { new Concert("Backstage passes to a TAFKAL80ETC concert", 1, 30) };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].Quality);
            Assert.AreEqual(0, Items[0].SellIn);
        }

        [Test]
        public void ConjuredItem()
        {
            IList<Item> Items = new List<Item> { new Conjured ("Conjured Axe", 10,  30 ) };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(28, Items[0].Quality);
            Assert.AreEqual(9, Items[0].SellIn);
        }
    }
}
