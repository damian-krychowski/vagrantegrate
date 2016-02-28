using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Vagrantegrate.Tests
{
    [TestFixture]
    internal class VagrantTests
    {
        [Test]
        public void Can_create_simple_vagrant_environment_with_node_echo_server()
        {
            //Arrange
            var sut = IntegrationTestEnvironment.Prepare();

            //Act
         
            //Assert
        }

        private string HttpGetRequest(string url)
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.Accept] = "*/*";
                byte[] result = client.DownloadData(url);
                return Encoding.UTF8.GetString(result);
            }
        }
    }
}
