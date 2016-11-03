namespace RemoteMissionNUnit
{
    using System.Collections.Generic;
    using System.Linq;
    using MissionInterface;
    using NUnit.Framework;
    using RemoteMission;

    [TestFixture]
    public class CommandConverterTest
    {
        [TestCase(50, CommandConverter.Direction.Right, "<R050>")]
        [TestCase(100, CommandConverter.Direction.Left, "<L100>")]
        [TestCase(100, CommandConverter.Direction.None, "<F100>")]
        [TestCase(0, CommandConverter.Direction.Straight, "<F000>")]
        [TestCase(0, CommandConverter.Direction.Right, "<R000>")]
        [TestCase(0, CommandConverter.Direction.Left, "<L000>")]
        [TestCase(5, CommandConverter.Direction.Back, "<B005>")]
        [TestCase(5, CommandConverter.Direction.Back & ~CommandConverter.Direction.Back, "<F005>")]
        [TestCase(5, CommandConverter.Direction.Straight | CommandConverter.Direction.Right, "<R005>")]
        [TestCase(35, CommandConverter.Direction.Straight | CommandConverter.Direction.Left, "<L035>")]
        [TestCase(100, CommandConverter.Direction.Back | CommandConverter.Direction.Right, "<R100>")]
        [TestCase(73, CommandConverter.Direction.Back | CommandConverter.Direction.Left, "<L073>")]
        public void CommandConverterTest1(int speed, CommandConverter.Direction dir, string expected)
        {
            var converter = new CommandConverter();
            RobotOutput output = converter.Convert(speed, dir);
            string text = output.ToString();
            Assert.AreEqual(expected, text);
        }
        [TestCase]
        public void CommandConverterTest2()
        {
            Assert.AreEqual("<START>", CommandConverter.StartCommand.ToString());
        }
        [TestCase]
        public void CommandConverterTest3()
        {
            Assert.AreEqual("<STOP>", CommandConverter.StopCommand.ToString());
        }
        /// <summary>
        /// コマンド生成履歴が残されていることをテストする
        /// </summary>
        [TestCase]
        public void CommandConverterTest4()
        {
            var converter = new CommandConverter();
            RobotOutput output = converter.Convert(100, CommandConverter.Direction.Right);
            List<RobotOutput> queue = new List<RobotOutput>();
            output = converter.Convert(100, CommandConverter.Direction.Left);
            queue.Add(output);
            output = converter.Convert(100, CommandConverter.Direction.Back);
            queue.Add(output);
            output = converter.Convert(100, CommandConverter.Direction.Straight);
            queue.Add(output);
            output = converter.Convert(100, CommandConverter.Direction.Left);
            queue.Add(output);
            output = converter.Convert(100, CommandConverter.Direction.Left);
            // 5件以上の履歴は残らないため、古い履歴は上書きされる
            queue.Add(output);
            queue.SequenceEqual(converter.History);
        }
    }
}
