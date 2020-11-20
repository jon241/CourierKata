# CourierKata
This is a technical test to create a class library (written in C#) to calculate the parcel posting costs from their dimensions.

## Implemented requirements
- Input can be in any form you choose
- Output should be a collection of items with their individual cost and type, as well as total cost.
- The cheapest option for sending each parcel should be selected.
- Each parcel size has a fixed cost. In my case this configurable and injected via the constructor.
  - Small parcel: all dimensions < 10cm. Cost $3
  - Medium parcel: all dimensions < 50cm. Cost $8
  - Large parcel: all dimensions < 100cm. Cost $15
  - XL parcel: any dimension >= 100cm. Cost $25
- Speedy Shipping is double the total cost and listed separately in the summary with the cost.

## What next and how


## How to use
Create list of `ParcelConfigs`. You could get this from any source

```C#
List<ParcelConfig> parcelsConfig = new List<ParcelConfig>();
parcelsConfig.Add(new ParcelConfig()
{
    Type = ParcelType.Small,
    Price = 3,
    MinDim = 0
});
parcelsConfig.Add(new ParcelConfig()
{
    Type = ParcelType.Medium,
    Price = 8,
    MinDim = 10
});
parcelsConfig.Add(new ParcelConfig()
{
    Type = ParcelType.Large,
    Price = 15,
    MinDim = 50
});
parcelsConfig.Add(new ParcelConfig()
{
    Type = ParcelType.XL,
    Price = 25,
    MinDim = 100
});
```

Then pass to the `ParcelOrders` constructor

```C#
ParcelOrders orders = new ParcelOrders(parcelsConfig);
```

Now have a list of parcel dimensions you want itemised costs for.

```C#
List<Dimensions> parcelDims = new List<Dimensions>() { 
    new Dimensions(9, 9, 9),
    new Dimensions(10, 10, 10),
    new Dimensions(50, 50, 50),
    new Dimensions(100, 100, 100)
};
```

Pass this to the `CalculateCosts` method.

```C#
ItemisedSummary summary = orders.CalculateCosts(parcelDims);
```

This returns an itemised summary of the costs, total and speedy shipping. For example:

```C#
// summary.Items[0].Type - Small
// summary.Items[0].Price - 3
// summary.Items[1].Type - Medium
// summary.Items[1].Price - 8
// summary.Items[2].Type - Large
// summary.Items[2].Price - 15
// summary.Items[3].Type - XL
// summary.Items[3].Price - 25
// summary.SpeedyShipping - 102
// summary.Total 51
```