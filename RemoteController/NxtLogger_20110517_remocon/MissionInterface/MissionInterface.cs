namespace MissionInterface
{
    public delegate void RetryMissionDelegate(RobotOutput output);

    public interface IMissionInterface
    {
        RobotOutput Run(RobotInput robotInput);
        void Init(RetryMissionDelegate retryDelegate);
    }
}
