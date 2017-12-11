using Microsoft.EntityFrameworkCore;
using OS.DAL.PgSql.Migrator;
using OS.Domain;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace OS.DAL.PgSql.IntegrationTests
{
    public class VendorRepositoryTests
    {
        public class GivenAnUninsertedVendor : IClassFixture<PostgresFixture>
        {
            private readonly PostgresFixture fixture;
            private readonly DeviceDbContext context;

            public GivenAnUninsertedVendor(PostgresFixture fixture)
            {
                this.fixture = fixture;

                var builder = new DbContextOptionsBuilder<DeviceDbContext>()
                    .UseNpgsql(fixture.ConnectionString, x => x.MigrationsAssembly(MigrationsAssembly.Assembly));

                context = new DeviceDbContext(builder.Options);
                context.Database.Migrate();
            }

            [Fact]
            public void WhenInsertingVendorWithBasicInfo_IdReturned()
            {
                // Arrange
                var mapper = new VendorMapper();
                var repository = new VendorRepository(context, mapper);

                var vendor = new Vendor()
                {
                    Name = "OpenSmog#1",
                    Url = "https://opensmog.org"
                };

                // Act
                var id = repository.Create(vendor);

                // Assert
                id.ShouldNotBe(default(long));
            }

            [Fact]
            public void WhenInsertingVendorWithApiKeys_IdReturned()
            {
                // Arrange
                var mapper = new VendorMapper();
                var repository = new VendorRepository(context, mapper);

                var vendor = new Vendor()
                {
                    Name = "OpenSmog#2",
                    Url = "https://opensmog.org",
                    Keys = new List<VendorApiKey>()
                    {
                        new VendorApiKey() { Key = "513E647B-97EF-4AF4-96A2-24F18D11DF4A", Limit = 100 },
                        new VendorApiKey() { Key = "513E647B-97EF-4AF4-96A2-24F18D11DF4B", Limit = 200 },
                        new VendorApiKey() { Key = "513E647B-97EF-4AF4-96A2-24F18D11DF4C", Limit = 300 },
                    }
                };

                // Act
                var id = repository.Create(vendor);

                // Assert
                id.ShouldNotBe(default(long));
            }
        }
    }
}