using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RemoteMission;
using MissionInterface;

namespace RemoteMissionNUnit
{
    [TestFixture]
    public class CommandConverterTest
    {
        [TestCase(50, CommandConverter.Right, "<R050>")]
        [TestCase(100, CommandConverter.Left, "<L100>")]
        [TestCase(0, CommandConverter.Straight, "<F000>")]
        [TestCase(5, CommandConverter.Right, "<B005>")]
        public void CommandConverterTest1(int speed, byte dir, string expected)
        {
            var converter = new CommandConverter();
            RobotOutput output = converter.Convert(speed, dir);
            string text = System.Text.Encoding.ASCII.GetString(output.Data);
            Assert.AreEqual(expected, text);
        }
    }
}
