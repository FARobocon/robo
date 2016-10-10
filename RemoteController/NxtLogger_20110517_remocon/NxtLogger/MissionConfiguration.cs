namespace NxtLogger
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using MissionInterface;

    public partial class MissionConfiguration : Form
    {
        private readonly List<IMissionInterface> missions = new List<IMissionInterface>();
        private readonly SendMissionDelegate retryDelegate;

        public MissionConfiguration(SendMissionDelegate retryDelegate)
        {
            this.retryDelegate = retryDelegate;

            this.InitializeComponent();
        }

        internal List<IMissionInterface> MissionList
        {
            get { return this.missions; }
        }

        private void Button1Click(object sender, EventArgs e)
        {
            if (this.RestoredMission.Checked)
            {
                Missions.RestoredMission m = new Missions.RestoredMission();
                this.missions.Add(m);
                m.Init(this.retryDelegate);
                this.Hide();
                return;
            }
            if (this.RemoteControlMission.Checked)
            {
                Missions.RemoteControlForm m = new Missions.RemoteControlForm();
                this.missions.Add(m);
                m.Init(this.retryDelegate);
                this.Hide();
            }
        }

        
    }
}
