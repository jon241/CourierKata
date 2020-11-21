# CourierKata
This is a technical test to create a class library (written in C#) to calculate the parcel posting costs from their dimensions.
You are not meant to do more than 2 hours work on this test but admittedly I did more. I only did the 
first 2 steps and made sure my refactoring was done to help explain future modifications. Each step was a new 
branch off main and the deleted once merged. I also learnt a little more syntax for this Readme file.

## Implemented requirements
- Input can be in any form you choose
- Output should be a collection of items with their individual cost and type, as well as total cost.
  - It was assumed this requirement was for an object response and not as a string output as I didn't
    read past the current implementing step (as requested in the spec) until enough time was already spent 
    on this project.
- The cheapest option for sending each parcel should be selected.
- Each parcel size has a fixed cost. In my case this configurable and injected via the constructor.
  - Small parcel: all dimensions < 10cm. Cost $3
  - Medium parcel: all dimensions < 50cm. Cost $8
  - Large parcel: all dimensions < 100cm. Cost $15
  - XL parcel: any dimension >= 100cm. Cost $25
- Speedy Shipping is double the sub total cost and listed separately in the summary object and added to the
  overall total.

## What next and how (in expected order)
- Add weight limit for each parcel type. If a parcel is over weight then a price of $2 is charged per kg.
    - Small parcel: 1kg
    - Medium parcel: 3kg
    - Large parcel: 6kg
    - XL parcel: 10kg
  - `Dimensions` class rename to `ParcelProperties` to reflect its different collection of properties.
  - Add new `integer` property to the `ParcelConfig` class, `MaxWeight`.
  - `ParcelOrders.CalculateCosts` method, add after `ParcelConfig` retrieved, if over maxWeight then
    `(maxWeight - actualParcelWeight) * overWeightCharge` and then update the `ParcelItem` price.
- Add Heavy to `ParcelType` enum, add ParcelConfig instance to list - Heavy parcel (maxWeight 50kg), $50. +$1/kg over.
  - Requires new `integer` property adding to `ParcelProperties`, OverWeightCost. These list of objects then 
    have their own weight cost.
  - `ParcelOrders.CalculateCosts` method, use the OverWeightCost value in the weight cost calculation.
- Apply discounts for multiple parcels.
  - Add `Discount` property to ItemisedSummary to appear as a negative number.
  - `ParcelOrders.CalculateCosts` method, move summary total calculation outside foreach loop.
  - Create list of discounts objects, each with own business logic. Configured list injected via constructor.
    Use factory pattern to get each discount while going through list, check for discount and apply if possible.

  - Apply SpeedyShipping cost after discount is calculated and applied.

## How to use this library
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

This returns an itemised summary of the costs, speedy shipping and total. For example:

```C#
// summary.Items[0].Type - Small
// summary.Items[0].Price - 3
// summary.Items[1].Type - Medium
// summary.Items[1].Price - 8
// summary.Items[2].Type - Large
// summary.Items[2].Price - 15
// summary.Items[3].Type - XL
// summary.Items[3].Price - 25
// summary.SpeedyShipping - 51
// summary.Total 102
```

Do note, the order of the dimensions list will return the items in that order in the summary. I.e

```C#
List<Dimensions> parcelDims = new List<Dimensions>() { 
    new Dimensions(10, 10, 10),
    new Dimensions(9, 9, 9),
    new Dimensions(50, 50, 50),
    new Dimensions(100, 100, 100)
};
```

Returns as

```C#
// summary.Items[0].Type - Medium
// summary.Items[0].Price - 8
// summary.Items[1].Type - Small
// summary.Items[1].Price - 3
// summary.Items[2].Type - Large
// summary.Items[2].Price - 15
// summary.Items[3].Type - XL
// summary.Items[3].Price - 25
// summary.SpeedyShipping - 51
// summary.Total 102
```