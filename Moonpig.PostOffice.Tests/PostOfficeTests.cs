namespace Moonpig.PostOffice.Tests
{
    using System;
    using System.Collections.Generic;
    using FakeItEasy;
    using Moonpig.PostOffice.Api.Services;
    using Moonpig.PostOffice.Data;
    using Shouldly;
    using Xunit;

    public class PostOfficeTests
    {
        private readonly IDespatchService despatchService;
        private readonly IProductService productService;
        private readonly ISupplierService supplierService;

        public PostOfficeTests()
        {
            productService = A.Fake<IProductService>();
            supplierService = A.Fake<ISupplierService>();
            despatchService = new DespatchService(productService, supplierService);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void DespatchDateIsEqualToTodayPlusLeadTime_GivenOneProduct_AndTodayIsAMonday(int leadTime)
        {
            var productId = 1;
            var supplierId = 1;
            var today = new DateTime(2018, 1, 1);

            var fakeProduct = new Product
            {
                ProductId = productId,
                SupplierId = supplierId,
            };
            var fakeSupplier = new Supplier
            {
                SupplierId = supplierId,
                LeadTime = leadTime
            };

            A.CallTo(() => productService.GetProductById(productId)).Returns(fakeProduct);
            A.CallTo(() => supplierService.GetSupplierById(supplierId)).Returns(fakeSupplier);

            var date = despatchService.GetDespatchDate(new List<int>(){ productId }, today);

            date.Date.Date.ShouldBe(today.Date.AddDays(fakeSupplier.LeadTime));
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(2, 4)]
        [InlineData(3, 4)]
        public void DespatchDateIsEqualToTodayPlusLargestLeadTime_GivenTwoProducts(int smallerLeadTime, int largerLeadTime)
        {
            var productOneId = 1;
            var productTwoId = 2;
            var supplierOneId = 1;
            var supplierTwoId = 1;
            var today = new DateTime(2018, 1, 1);

            var fakeProductOne = new Product
            {
                ProductId = productOneId,
                SupplierId = supplierOneId,
            };

            var fakeProductTwo = new Product
            {
                ProductId = productTwoId,
                SupplierId = supplierTwoId,
            };

            var fakeSupplierOne = new Supplier
            {
                SupplierId = supplierOneId,
                LeadTime = smallerLeadTime
            };

            var fakeSupplierTwo = new Supplier
            {
                SupplierId = supplierTwoId,
                LeadTime = largerLeadTime
            };

            A.CallTo(() => productService.GetProductById(productOneId)).Returns(fakeProductOne);
            A.CallTo(() => productService.GetProductById(productTwoId)).Returns(fakeProductTwo);

            A.CallTo(() => supplierService.GetSupplierById(supplierOneId)).Returns(fakeSupplierOne);
            A.CallTo(() => supplierService.GetSupplierById(supplierTwoId)).Returns(fakeSupplierTwo);

            var date = despatchService.GetDespatchDate(new List<int>() { productOneId, productTwoId }, today);

            date.Date.Date.ShouldBe(today.Date.AddDays(fakeSupplierTwo.LeadTime));
        }

        [Fact]
        public void DespatchDateIsMonday_GivenTodayIsFriday_WhenLeadTimeIsOneDay()
        {
            var productId = 1;
            var supplierId = 1;
            var leadTime = 1;
            var today = new DateTime(2018, 1, 5);

            var fakeProduct = new Product
            {
                ProductId = productId,
                SupplierId = supplierId,
            };
            var fakeSupplier = new Supplier
            {
                SupplierId = supplierId,
                LeadTime = leadTime
            };

            A.CallTo(() => productService.GetProductById(productId)).Returns(fakeProduct);
            A.CallTo(() => supplierService.GetSupplierById(supplierId)).Returns(fakeSupplier);

            var date = despatchService.GetDespatchDate(new List<int>() { productId }, today);

            date.Date.Date.ShouldBe(new DateTime(2018, 1, 8));
        }

        [Fact]
        public void DespatchDateIsTuesday_GivenTodayIsSaturday_WhenLeadTimeIsOneDay()
        {
            var productId = 1;
            var supplierId = 1;
            var leadTime = 1;
            var today = new DateTime(2018, 1, 6);

            var fakeProduct = new Product
            {
                ProductId = productId,
                SupplierId = supplierId,
            };
            var fakeSupplier = new Supplier
            {
                SupplierId = supplierId,
                LeadTime = leadTime
            };

            A.CallTo(() => productService.GetProductById(productId)).Returns(fakeProduct);
            A.CallTo(() => supplierService.GetSupplierById(supplierId)).Returns(fakeSupplier);

            var date = despatchService.GetDespatchDate(new List<int>() { productId }, today);

            date.Date.Date.ShouldBe(new DateTime(2018, 1, 9));
        }

        [Fact]
        public void DespatchDateIsTuesday_GivenTodayIsSunday_WhenLeadTimeIsOneDay()
        {
            var productId = 1;
            var supplierId = 1;
            var leadTime = 1;
            var today = new DateTime(2018, 1, 7);

            var fakeProduct = new Product
            {
                ProductId = productId,
                SupplierId = supplierId,
            };
            var fakeSupplier = new Supplier
            {
                SupplierId = supplierId,
                LeadTime = leadTime
            };

            A.CallTo(() => productService.GetProductById(productId)).Returns(fakeProduct);
            A.CallTo(() => supplierService.GetSupplierById(supplierId)).Returns(fakeSupplier);

            var date = despatchService.GetDespatchDate(new List<int>() { productId }, today);

            date.Date.Date.ShouldBe(new DateTime(2018, 1, 9));
        }

        [Fact]
        public void DespatchDateIsMonday_GivenTodayIsFriday_WhenLeadTimeIsSixDays()
        {
            var productId = 1;
            var supplierId = 1;
            var leadTime = 6;
            var today = new DateTime(2018, 1, 5);

            var fakeProduct = new Product
            {
                ProductId = productId,
                SupplierId = supplierId,
            };
            var fakeSupplier = new Supplier
            {
                SupplierId = supplierId,
                LeadTime = leadTime
            };

            A.CallTo(() => productService.GetProductById(productId)).Returns(fakeProduct);
            A.CallTo(() => supplierService.GetSupplierById(supplierId)).Returns(fakeSupplier);

            var date = despatchService.GetDespatchDate(new List<int>() { productId }, today);

            date.Date.Date.ShouldBe(new DateTime(2018, 1, 15));
        }

        [Fact]
        public void DespatchDateIsMonday_GivenTodayIsFriday_WhenLeadTimeIsElevenDays()
        {
            var productId = 1;
            var supplierId = 1;
            var leadTime = 11;
            var today = new DateTime(2018, 1, 5);

            var fakeProduct = new Product
            {
                ProductId = productId,
                SupplierId = supplierId,
            };

            var fakeSupplier = new Supplier
            {
                SupplierId = supplierId,
                LeadTime = leadTime
            };

            A.CallTo(() => productService.GetProductById(productId)).Returns(fakeProduct);
            A.CallTo(() => supplierService.GetSupplierById(supplierId)).Returns(fakeSupplier);

            var date = despatchService.GetDespatchDate(new List<int>() { productId }, today);

            date.Date.Date.ShouldBe(new DateTime(2018, 1, 22));
        }

        [Fact]
        public void NoDespatchDateReturned_GivenProductIdDoesNotExist()
        {
            var productId = -1;
            var supplierId = 1;
            var leadTime = 3;
            var today = new DateTime(2018, 1, 1);

            var fakeSupplier = new Supplier
            {
                SupplierId = supplierId,
                LeadTime = leadTime
            };

            A.CallTo(() => productService.GetProductById(productId)).Returns(null);
            A.CallTo(() => supplierService.GetSupplierById(supplierId)).Returns(fakeSupplier);

            var date = despatchService.GetDespatchDate(new List<int>() { productId }, today);

            Assert.Null(date);
        }
    }
}
