using OS.DAL.Queries;
using OS.Domain;
using Shouldly;
using Xunit;

namespace OS.DAL.PgSql.IntegrationTests
{
    [Collection(Constants.DeviceRepositoryTestsCollection)]
    [Trait("IntegrationTests", "DeviceRepositoryTests")]
    public class DeviceRepositoryTests
    {
        private readonly DeviceRepository repository;
        private readonly DeviceRepositoryFixture fixture;

        public DeviceRepositoryTests(DeviceRepositoryFixture fixture)
        {
            this.fixture = fixture;
            repository = new DeviceRepository(this.fixture.Context, new DeviceMapper());
        }

        [Theory]
        [InlineData("DeviceRepository.TestData01.json")]
        public void WhenTestDataIsInserted_TheDataCountMatches(string fileName)
        {
            // Arrange
            var devices = EnsureDevicesAreInserted(fileName);

            // Act
            var count = repository.Count();

            // Assert
            count.ShouldBe(devices.Length);
        }

        [Theory]
        [InlineData("DeviceRepository.TestData01.json", nameof(Device.Type), CriteriumOperator.Eq, DeviceType.DIY, 3)]
        [InlineData("DeviceRepository.TestData01.json", nameof(Device.Type), CriteriumOperator.Eq, DeviceType.API, 1)]
        [InlineData("DeviceRepository.TestData01.json", nameof(Device.Type), CriteriumOperator.Eq, DeviceType.Retail, 2)]
        public void WhenTypeFilterIsApplied_ResultIsReturned(string fileName, string property, CriteriumOperator @operator, object @value, int expectedCount)
        {
            // Arrange
            var devices = EnsureDevicesAreInserted(fileName);

            var query = new Query();
            query.FilterCriteria.Add(new FilterCriterium()
            {
                PropertyName = property,
                Operator = @operator,
                Value = (int)@value
            });

            // Act
            var result = repository.Get(query);

            // Assert
            result.Items.Count.ShouldBe(expectedCount);
        }

        private Device[] EnsureDevicesAreInserted(string fileName)
        {
            var devices = fileName.Get<Device>(d =>
            {
                d.VendorId = this.fixture.VendorId;
                return d;
            });
            var query = new Query();
            query.FilterCriteria.Add(new FilterCriterium()
            {
                PropertyName = nameof(Device.Name),
                Operator = CriteriumOperator.Sw,
                Value = "devicerepository.testdata01"
            });

            if (repository.Count(query) != 0) return devices;

            try
            {
                repository.Insert(devices);
            }
            catch (Npgsql.PostgresException pex)
            {
            }

            return devices;
        }
    }
}