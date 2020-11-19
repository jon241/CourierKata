namespace CourierKata
{
    public class Cost
    {
        public decimal Price { get; }
        public ParcelType TypeOfParcel { get; }

        public Cost(int price, ParcelType typeOfParcel)
        {
            Price = price;
            TypeOfParcel = typeOfParcel;
        }
    }
}