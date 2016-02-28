using NUnit.Framework;

namespace Vagrantegrate.Tests.Infrastructure
{
    internal class UnitTest<TFixture>
        where TFixture : IUnitTestFixture, new()
    {
        public TFixture Fixture { get; }

        public UnitTest()
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