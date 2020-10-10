﻿using Addresses.API.Controllers;
using Addresses.API.Data;
using Addresses.API.Services.Repositories.Implementations;
using Addresses.Tests.FakeImplementations;
using Microsoft.AspNetCore.Mvc;
using Shared.Services.API.Communication.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace Addresses.API.Tests.Controllers.Address
{
    public class SetDefaultAddressActionTests
    {
        [Fact]
        public async void Delete_ShouldReturnSuccess()
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(MethodBase.GetCurrentMethod().DeclaringType.FullName);
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var userID = "kriszw";
            var newDefAddressID = 2;

            var repo = new UserAddressRepository(dbContext);

            //Act
            var controller = new AddressController(repo);
            var actionResult = await controller.SetDeafultAddressForUser(userID, newDefAddressID);
            var actualData = await repo.GetDefaultForUser(userID);

            //Assert
            Assert.NotNull(actualData);
            Assert.NotNull(actionResult);
            Assert.Equal(newDefAddressID, actualData.ID);
            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(requestObject.Value);
            Assert.True(actual.Success);
        }

        [Theory]
        [MemberData(nameof(CreateAddInvalidTestData))]
        public async void Delete_ShouldFail(string userID, int id, string expectedMSG, Type expectedActionResultObjectType)
        {
            //Arrange
            var dbOptions = FakeDbCreatorFactory.CreateDbOptions(MethodBase.GetCurrentMethod().DeclaringType.FullName + $"{userID}-{id}-{expectedMSG}-{expectedActionResultObjectType.FullName}");
            await FakeDbCreatorFactory.InitDbContext(dbOptions);
            var dbContext = new AddressesDbContext(dbOptions);

            var repo = new UserAddressRepository(dbContext);

            //Act
            var controller = new AddressController(repo);
            var actionResult = await controller.SetDeafultAddressForUser(userID, id);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<EmptyAPIResponse>>(actionResult);
            Assert.IsType(expectedActionResultObjectType, actionResult.Result);
            var requestObject = Assert.IsAssignableFrom<ObjectResult>(actionResult.Result);
            var actual = Assert.IsAssignableFrom<EmptyAPIResponse>(requestObject.Value);
            Assert.False(actual.Success);
            Assert.Equal(expectedMSG, actual.Message);
        }

        public static object[][] CreateAddInvalidTestData() => new object[][]
        {
            new object[] {"", 1, "A userID nem lehet üres", typeof(BadRequestObjectResult) },
            new object[] {"asd", -1, "A cím azonosítója nem lehet kisebb mint 1", typeof(BadRequestObjectResult) },
            new object[] {"asd", 1, "A asd felhasználónak nincs cím a rendszerben", typeof(NotFoundObjectResult) },
            new object[] {"asd", 100, "A 100 azonosítóval nem létezik cím a rendszerben", typeof(NotFoundObjectResult) },
            new object[] {"kriszw", 3, "A 3 azonosítóval a cím nem a kriszw felhasználóhoz van kötve", typeof(ConflictObjectResult) },
        };
    }
}