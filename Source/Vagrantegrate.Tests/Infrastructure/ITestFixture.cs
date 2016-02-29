namespace Vagrantegrate.Tests.Infrastructure
{
    internal interface ITestFixture
    {
        void SetUp();
        void TearDown();
    }
}