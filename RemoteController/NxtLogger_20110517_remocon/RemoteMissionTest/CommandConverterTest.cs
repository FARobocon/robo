using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemoteMission;
using MissionInterface;

namespace RemoteMissionTest
{
    [TestClass]
    public class CommandConverterTest
    {
        [TestMethod]
        public void CommandConverterTest1()
        {
            var converter = new CommandConverter();
            RobotOutput output = converter.Convert(50, CommandConverter.Right);
            string text = System.Text.Encoding.ASCII.GetString(output.Data);
            Assert.AreEqual("<R050>", text);
        }
    }
}
