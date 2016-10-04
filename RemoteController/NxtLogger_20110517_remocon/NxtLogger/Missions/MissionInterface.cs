using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NxtLogger.Missions
{
    interface MissionInterface
    {
        RobotOutput Run(RobotInput robotInput);
        void Init(RetryMissionDelegate retryDelegate);
    }
}
