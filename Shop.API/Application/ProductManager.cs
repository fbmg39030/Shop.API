﻿using Shop.API.Models.Dbo;
using Shop.API.Models.Dto;
using Shop.API.Models.Request;
using Shop.API.Models.Result;
using Shop.API.Persistence.Dao;
using Shop.API.Persistence.QueryParams;
using System.Net;
using System.Xml.Linq;

namespace Shop.API.Application;

public static class ProductManager
{
    private const string ClassName = nameof(ProductManager);

    public static ProductResult Query(ProductQp qp)
    {
        var queryResult = ProductDao.QueryByParameters(qp);
        var dtoList = ProductDto.FromDboList(queryResult, new());
        var result = ProductResult.FromDtoList(HttpStatusCode.OK, dtoList);

        //Log.Information("{Class}.{Method} query result: {@result}", ClassName, nameof(Query), result);
        return result;
    }

    public static ProductResult AddOrUpdate(ProductAddOrUpdateRequest request)
    {
        ProductDbo productDbo;
        //update
        if (request.LogicalObjectId != Guid.Empty)
        {
            productDbo = ProductDao.QueryByLogicalObjectId(request.LogicalObjectId);
            if(productDbo == null)
            {
                return ProductResult.FromMessage(HttpStatusCode.NotFound, $"Could not find Product with loid: " + request.LogicalObjectId);
            }
            productDbo.Name1 = request.Name1;
            productDbo.Description = request.Description;
            productDbo.Price = request.Price;
        }
        //create
        else
        {
            productDbo = new ProductDbo()
            {
                LogicalObjectId = Guid.NewGuid(),
                Name1 = request.Name1,
                Description = request.Description,
                Price = request.Price,
            };
        }
        ProductDao.SaveOrUpdate(productDbo);

        var productDto = ProductDto.FromDbo(productDbo, new());
        var result = ProductResult.FromDto(HttpStatusCode.OK, productDto);
        return result;
    }

}
