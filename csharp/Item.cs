namespace csharp
{
    public class Item
    {
        public string Name { get; set; }
        public int SellIn { get; set; }
        public int Quality { get; set; }

        public override string ToString()
        {
            return this.Name + ", " + this.SellIn + ", " + this.Quality;
        }  
        public virtual void Updaterule()
        {
            SellIn = SellIn - 1;
            if (SellIn >= 0)
            {
                DecreaseQ(1);
            }
            else
            {
                DecreaseQ(2);
            }
        }
        public void IncreaseQ( int increaseamount)
        {
            if (Quality + increaseamount < 50)
            {
                Quality += increaseamount;
            }
            else
            {
                Quality = 50;
            }
        }
        public void DecreaseQ( int decreaseamount)
        {
            if (Quality - decreaseamount > 0)
            {
                Quality -= decreaseamount;
            }
        }
    }
}
