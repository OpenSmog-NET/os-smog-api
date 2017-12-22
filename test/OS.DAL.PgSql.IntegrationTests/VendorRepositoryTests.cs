using OS.Domain;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace OS.DAL.PgSql.IntegrationTests
{
    public class VendorRepositoryTests
    {
        public static Vendor CreateVendor(string name, string url, bool addApiKeys = false)
        {
            return addApiKeys ?
                new Vendor()
                {
                    Name = name,
                    Url = url,
                    Keys = new List<VendorApiKey>()
                    {
                        new VendorApiKey() { Key = "513E647B-97EF-4AF4-96A2-24F18D11DF4A", Limit = 100 },
                        new VendorApiKey() { Key = "513E647B-97EF-4AF4-96A2-24F18D11DF4B", Limit = 200 },
                        new VendorApiKey() { Key = "513E647B-97EF-4AF4-96A2-24F18D11DF4C", Limit = 300 },
                    }
                } :
                new Vendor()
                {
                    Name = name,
                    Url = url
                };
        }

        public static Vendor[] CreateVendors(string name, string url)
        {
            return new Vendor[]
            {
                new Vendor() { Name = $"{name}#1", Url = url },
                new Vendor() { Name = $"{name}#2", Url = url },
                new Vendor() { Name = $"{name}#3", Url = url }
            };
        }

        [Collection(Constants.VendorRepositoryTestsCollection)]
        public class GivenAnInsertedVendor
        {
            private readonly VendorMapper mapper = new VendorMapper();
            private readonly VendorRepository repository;

            public GivenAnInsertedVendor(VendorRepositoryFixture fixture)
            {
                repository = new VendorRepository(fixture.Context, mapper);
            }

            [Fact]
            public void WhenQueryingVendorWithApiKeysById_EntityShouldBeReturnedWithoutIncludedProperties()
            {
                // Arrange
                var id = repository.Insert(CreateVendor("OpenSmog#3", "https://opensmog.org", true));

                // Act
                var vendor = repository.Get(id);

                // Assert
                vendor.ShouldNotBe(null);
                vendor.Name.ShouldNotBeNullOrEmpty();
                vendor.Url.ShouldNotBeNullOrEmpty();
                vendor.Keys.ShouldNotBe(null);
                vendor.Keys.Count.ShouldBeGreaterThan(0);
            }
        }

        [Collection(Constants.VendorRepositoryTestsCollection)]
        public class GivenAnUninsertedVendor
        {
            private readonly VendorMapper mapper = new VendorMapper();
            private readonly VendorRepository repository;

            public GivenAnUninsertedVendor(VendorRepositoryFixture fixture)
            {
                repository = new VendorRepository(fixture.Context, mapper);
            }

            [Fact]
            public void WhenInsertingVendorWithApiKeys_IdReturned()
            {
                // Arrange
                var vendor = CreateVendor("OpenSmog#2", "https://opensmog.org", true);

                // Act
                var id = repository.Insert(vendor);

                // Assert
                id.ShouldNotBe(default(long));
            }

            [Fact]
            public void WhenInsertingVendorWithBasicInfo_IdReturned()
            {
                // Arrange
                var vendor = CreateVendor("OpenSmog#1", "https://opensmog.org");

                // Act
                var id = repository.Insert(vendor);

                // Assert
                id.ShouldNotBe(default(long));
            }
        }
    }
}