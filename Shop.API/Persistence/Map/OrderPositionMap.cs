﻿using Shop.API.Models.Dbo;

namespace Shop.API.Persistence.Map;

public class OrderPositionMap : BaseMap<OrderPositionDbo>
{
    const string TableName = "OrderPosition";
    public OrderPositionMap() : base(TableName)
    {
        Map(x => x.LogicalObjectId).Unique().Not.Nullable();
        Map(x => x.Quantity).Not.Nullable();
        Map(x => x.UnitPrice).Not.Nullable();

        References(x => x.Product).Column(nameof(OrderPositionDbo.Product) + Reference)
            .ForeignKey(ForeignKey + nameof(OrderPositionDbo.Product) + "_" + "2222AA");
    }
}
