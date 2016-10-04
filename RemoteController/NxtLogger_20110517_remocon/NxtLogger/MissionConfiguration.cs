namespace NxtLogger
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public partial class MissionConfiguration : Form
    {
        private readonly List<Missions.MissionInterface> missions = new List<Missions.MissionInterface>();
        private readonly RetryMissionDelegate retryDelegate;

        public MissionConfiguration(RetryMissionDelegate retryDelegate)
        {
            this.retryDelegate = retryDelegate;

            this.InitializeComponent();
        }

        internal List<Missions.MissionInterface> MissionList
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
