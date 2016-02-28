namespace Vagrantegrate.Tests.Infrastructure
{
    internal interface IUnitTestFixture
    {
        void SetUp();
        void TearDown();
    }
}