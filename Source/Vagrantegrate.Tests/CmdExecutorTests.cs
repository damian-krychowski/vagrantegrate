using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Vagrantegrate.CommandLine;

namespace Vagrantegrate.Tests
{
    [TestFixture]
    internal class CmdExecutorTests
    {
        [Test]
        public void Can_execute_echo_command()
        {
            //Arrange
            var sut = new CmdExecutor();

            //Act
            sut.WithCommand("echo Vagrantegrate")
               .Execute();
        }
    }
}
