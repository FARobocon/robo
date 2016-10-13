namespace RemoteMissionNUnit
{
    using MissionInterface;
    using NUnit.Framework;
    using RemoteMission;

    [TestFixture]
    public class CommandConverterTest
    {
        [TestCase(50, CommandConverter.Direction.Right, "<R050>")]
        [TestCase(100, CommandConverter.Direction.Left, "<L100>")]
        [TestCase(0, CommandConverter.Direction.Straight, "<F000>")]
        [TestCase(5, CommandConverter.Direction.Back, "<B005>")]
        [TestCase(5, CommandConverter.Direction.Back & ~CommandConverter.Direction.Back, CommandConverter.NoneCommand)]
        [TestCase(5, CommandConverter.Direction.Straight | CommandConverter.Direction.Right, "<R005>")]
        [TestCase(35, CommandConverter.Direction.Straight | CommandConverter.Direction.Left, "<L035>")]
        [TestCase(100, CommandConverter.Direction.Back | CommandConverter.Direction.Right, "<R100>")]
        [TestCase(73, CommandConverter.Direction.Back | CommandConverter.Direction.Left, "<L073>")]
        public void CommandConverterTest1(int speed, CommandConverter.Direction dir, string expected)
        {
            var converter = new CommandConverter();
            RobotOutput output = converter.Convert(speed, dir);
            string text = output.ToString("c");
            Assert.AreEqual(expected, text);
        }
        [TestCase]
        public void CommandConverterTest2()
        {
            Assert.AreEqual("<START>", CommandConverter.StartCommand.ToString("c"));
        }
        [TestCase]
        public void CommandConverterTest3()
        {
            Assert.AreEqual("<STOP>", CommandConverter.StopCommand.ToString("c"));
        }
    }
}
