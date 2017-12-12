﻿using Microsoft.EntityFrameworkCore;
using OS.DAL.PgSql.Migrator;
using OS.Domain;
using System.IO;
using System.Linq;
using Xunit;

namespace OS.DAL.PgSql.IntegrationTests
{
    public class DeviceRepositoryTests : IClassFixture<PostgresFixture>
    {
        private readonly DeviceMapper mapper = new DeviceMapper();
        private readonly DeviceRepository repository;
        private readonly PostgresFixture fixture;
        private readonly DeviceDbContext context;
        private readonly long vendorId;

        public DeviceRepositoryTests(PostgresFixture fixture)
        {
            this.fixture = fixture;

            var builder = new DbContextOptionsBuilder<DeviceDbContext>()
                .UseNpgsql(fixture.ConnectionString, x => x.MigrationsAssembly(MigrationsAssembly.Assembly));

            context = new DeviceDbContext(builder.Options);
            context.Database.Migrate();

            var vendorRepository = new VendorRepository(context, new VendorMapper());
            vendorId = vendorRepository.Insert(new Vendor() { Name = "OpenSmog", Url = "http://opensmog.org" });

            repository = new DeviceRepository(context, mapper);
        }

        [Theory]
        [InlineData("DeviceRepository.TestData01.json")]
        public void Test(string fileName)
        {
            // Arrange
            repository.Insert(GetTestDevices(fileName).Select(d => { d.VendorId = vendorId; return d; }).ToArray());
            // Act
            // Assert
        }

        private Device[] GetTestDevices(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open))
            using (var reader = new StreamReader(stream))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Device[]>(reader.ReadToEnd());
            }
        }
    }
}