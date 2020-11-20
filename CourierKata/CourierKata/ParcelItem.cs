namespace CourierKata
{
    public class ParcelItem
    {
        public decimal Price { get; }
        public ParcelType TypeOfParcel { get; }

        public ParcelItem(decimal price, ParcelType typeOfParcel)
        {
            Price = price;
            TypeOfParcel = typeOfParcel;
        }
    }
}