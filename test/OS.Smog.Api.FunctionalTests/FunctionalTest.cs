using Xunit;

namespace OS.Smog.Api.FunctionalTests
{
    public class FunctionalTest : IClassFixture<FunctionalTestFixture>
    {
        private readonly FunctionalTestFixture fixture;

        public FunctionalTest(FunctionalTestFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Test()
        {
        }
    }
}