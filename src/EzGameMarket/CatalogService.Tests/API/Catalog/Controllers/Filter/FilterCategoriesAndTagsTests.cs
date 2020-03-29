﻿using CatalogService.API.Controllers;
using CatalogService.API.Services.Service.Implementations;
using CatalogService.API.ViewModels.Products;
using CatalogService.Tests.API.FakeImplementation;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CatalogService.Tests.API.Catalog.Controllers.Filter
{
    public class FilterCategoriesAndTagsTests
    {
        [Fact]
        public async void Get_ItemsFromCategoriesAndTags_ShouldReturnSuccessAnd2Items()
        {
            var dbContext = new FakeCatalogDbContext();
            var filterService = new FilterService(dbContext.DBContext);

            var pageIndex = 0;
            var pageSize = 30;

            var categories = new List<string>()
            {
                "FPS",
                "Akció"
            };

            var tags = new List<string>()
            {
                "FPS",
                "AKCIÓ",
                "PTW",
                "CSGO"
            };

            var expectedItemsCount = 2;
            var expectedPageSize = 30;
            var expectedPageIndex = 0;

            var controller = new FilterController(filterService);
            var actionResult = await controller.GetFilteredItems(pageIndex, pageSize, categories, tags);

            //Assert
            Assert.IsType<ActionResult<PaginationViewModel<CatalogItem>>>(actionResult);
            Assert.NotNull(actionResult.Value);
            var products = Assert.IsAssignableFrom<PaginationViewModel<CatalogItem>>(actionResult.Value);
            Assert.Equal(expectedItemsCount, products.TotalItemsCount);
            Assert.Equal(expectedItemsCount, products.Data.Count());
            Assert.Equal(expectedPageSize, products.ItemsPerPage);
            Assert.Equal(expectedPageIndex, products.ActualPage);
        }

        [Fact]
        public async void Get_ItemsFromCategoriesAndTags_ShouldReturnSuccessAnd0Items()
        {
            var dbContext = new FakeCatalogDbContext();
            var filterService = new FilterService(dbContext.DBContext);

            var pageIndex = 0;
            var pageSize = 30;

            var categories = new List<string>()
            {
                "TPS"
            };

            var tags = new List<string>()
            {
                "AKA",
            };

            var expectedItemsCount = 0;
            var expectedPageSize = 30;
            var expectedPageIndex = 0;

            var controller = new FilterController(filterService);
            var actionResult = await controller.GetFilteredItems(pageIndex, pageSize, categories,tags);

            //Assert
            Assert.IsType<ActionResult<PaginationViewModel<CatalogItem>>>(actionResult);
            Assert.NotNull(actionResult.Value);
            var products = Assert.IsAssignableFrom<PaginationViewModel<CatalogItem>>(actionResult.Value);
            Assert.Equal(expectedItemsCount, products.TotalItemsCount);
            Assert.Equal(expectedItemsCount, products.Data.Count());
            Assert.Equal(expectedPageSize, products.ItemsPerPage);
            Assert.Equal(expectedPageIndex, products.ActualPage);
        }
    }
}
