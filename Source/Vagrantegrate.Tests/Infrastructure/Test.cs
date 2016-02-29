using NUnit.Framework;

namespace Vagrantegrate.Tests.Infrastructure
{
    internal class Test<TFixture>
        where TFixture : ITestFixture, new()
    {
        public TFixture Fixture { get; }

        public Test()
        {
            Fixture = new TFixture();
        }

        [SetUp]
        public void SetUp()
        {
            Fixture.SetUp();
        }

        [TearDown]
        public void TearDown()
        {
            Fixture.TearDown();
        }
    }
}