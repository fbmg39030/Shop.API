﻿using Shop.API.Models.Dbo;

namespace Shop.API.Persistence.Map;

public class OrderMap : BaseMap<OrderDbo>
{
    const string TableName = "ShopOrder";
    public OrderMap() : base(TableName)
    {
        Map(x => x.LogicalObjectId).Unique().Not.Nullable();
        Map(x => x.UserId);
        Map(x => x.TotalAmount);

        HasMany(x => x.OrderPositionList).Cascade.AllDeleteOrphan().Not.LazyLoad().KeyColumn(TableName + Reference);
    }

}
