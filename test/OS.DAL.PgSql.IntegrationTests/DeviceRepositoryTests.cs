using OS.DAL.Queries;
using OS.Domain;
using Shouldly;
using System.Collections.Generic;
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

        public static readonly FilterCriterium ThisTestCriterium = new FilterCriterium()
        {
            PropertyName = nameof(Device.Name),
            Operator = CriteriumOperator.Sw,
            Value = "devicerepository.testdata01"
        };

        public static readonly IReadOnlyDictionary<string, FilterCriterium> FilterCriteria =
            new Dictionary<string, FilterCriterium>()
            {
                { "1", new FilterCriterium() { PropertyName = nameof(Device.Type), Operator = CriteriumOperator.Eq, Value = (int)DeviceType.DIY  } },
                { "2", new FilterCriterium() { PropertyName = nameof(Device.Type), Operator = CriteriumOperator.Eq, Value = (int)DeviceType.API } },
                { "3", new FilterCriterium() { PropertyName = nameof(Device.Type), Operator = CriteriumOperator.Eq, Value = (int)DeviceType.Retail  } }
            };

        [Theory]
        [InlineData("DeviceRepository.TestData01.json")]
        public void WhenTestDataIsInserted_TheDataCountMatches(string fileName)
        {
            // Arrange
            var devices = fixture.EnsureDevicesAreInserted(fileName, repository, ThisTestCriterium);

            // Act
            var count = repository.Count();

            // Assert
            count.ShouldBe(devices.Length);
        }

        [Theory]
        [InlineData("DeviceRepository.TestData01.json", "1", 3)]
        [InlineData("DeviceRepository.TestData01.json", "2", 1)]
        [InlineData("DeviceRepository.TestData01.json", "3", 2)]
        public void WhenTypeFilterIsApplied_ResultIsReturned(string fileName, string key, int expectedCount)
        {
            // Arrange
            var devices = fixture.EnsureDevicesAreInserted(fileName, repository, ThisTestCriterium);

            var query = new Query();
            query.FilterCriteria.Add(ThisTestCriterium);
            query.FilterCriteria.Add(FilterCriteria[key]);

            // Act
            var result = repository.Get(query);

            // Assert
            result.Items.Count.ShouldBe(expectedCount);
        }
    }
}