namespace MissionInterface
{
    public delegate void SendMissionDelegate(RobotOutput output);

    public interface IMissionInterface
    {
        RobotOutput Run(RobotInput robotInput);
        void Init(SendMissionDelegate sendDelegate);
    }
}
