using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Vagrantegrate.Tests
{
    [TestFixture]
    internal class UriExtensionsTests
    {
        [Test]
        public void Can_get_file_folder()
        {
            //Arrange
            var uri = new Uri("C:/FileFolder/FileDeeperFolder/File.txt");

            //Act
            var folderPath = uri.FileLocationUri();

            //Assert
            folderPath.Should().Be(new Uri("C:/FileFolder/FileDeeperFolder/"));
        }

        [Test]
        public void Can_get_path_relative_to_root()
        {
            //Arrange
            var uri = new Uri("C:/FileFolder/FileDeeperFolder/File.txt");

            //Act
            var folderPath = uri.LocationPathRelativeToRoot();

            //Assert
            folderPath.Should().BeEquivalentTo("/FileFolder/FileDeeperFolder/");
        }
    }
}
