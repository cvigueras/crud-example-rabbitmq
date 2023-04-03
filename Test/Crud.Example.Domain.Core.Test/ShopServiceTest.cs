using System;
using Crud.Example.Domain.Core.Services;
using Crud.Example.Domain.Entities;
using Crud.Example.Domain.Repositories;
using Moq;
using NUnit.Framework;

namespace Crud.Example.Domain.Core.Test
{
    internal class ShopServiceTest
    {
        private Mock<IShopRepository>? _shopRepository;
        private ShopService? _shopService;
        Shop? _shop;

        [SetUp]
        public void Setup()
        {
            _shopRepository = new Mock<IShopRepository>();
            _shopService = new ShopService(_shopRepository.Object);
            _shop = new Shop
            {
                Address = "Avenida Los pinos",
                ExpirationDateTime = DateTime.Now,
                Name = "Shop_prueba_test",
                Processed = true,
            };
        }

        [Test]
        public void AddShop_WhenShopIsNotNull_ExpectedResultGraterThanZero()
        {
            //Assert
            int expectedResult = 1;
            _shopRepository!.Setup(x => x.Add(_shop!)).Returns(expectedResult);
            //Act
            var result = _shopService?.CreateShop(_shop!);

            //Arrange
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void AddShop_WhenShopIsNull_ExpectedResultZero()
        {
            //Assert
            int expectedResult = 0;
            var shop = new Shop();
            shop = null;
            _shopRepository!.Setup(x => x.Add(shop!)).Returns(expectedResult);

            //Act
            var result = _shopService?.CreateShop(shop!);

            //Arrange
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void RemoveShopByName_WhenNameIsNull_ExpectedFalse()
        {
            //Assert
            string? nameShop = null;
            var shop = new Shop();
            _shopRepository!.Setup(x => x.RemoveShopByName(nameShop!)).Returns(shop);

            //Act
            var result = _shopService?.RemoveShopByName(nameShop!);

            //Arrange
            Assert.AreEqual(false, result);
        }

        [Test]
        public void RemoveShopByName_WhenNameIsNotNull_ExpectedTrue()
        {
            //Assert
            _shopRepository!.Setup(x => x.RemoveShopByName(_shop!.Name!)).Returns(_shop);

            //Act
            var result = _shopService?.RemoveShopByName(_shop!.Name!);

            //Arrange
            Assert.AreEqual(true, result);
        }
    }
}