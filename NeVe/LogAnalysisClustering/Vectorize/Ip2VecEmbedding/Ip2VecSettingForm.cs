using LogAnalysisClustering.Misc;
using LogAnalysisClustering.Vectorize.Ip2VecEmbedding.Model;
using LogAnalysisClustering.Vectorize.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LogAnalysisClustering.Vectorize.Misc.Contexts;

namespace LogAnalysisClustering.DataProcess.Ip2VecClustering
{
    public partial class Ip2VecSettingForm : Form
    {
        //public Dictionary<Ip2VecSettings.GetMem, List<Ip2VecSettings.GetMem>> ContextRelationDict { get; set; }

        public Ip2VecSettings Ip2VecSettings { get; set; }

        private bool TargetThreatName { get; set; } = false;

        private bool TargetSourceIp { get; set; } = false;

        private bool TargetSourcePort { get; set; } = false;

        private bool TargetTargetIp { get; set; } = false;

        private bool TargetTargetPort { get; set; } = false;

        private bool TargetProtocol { get; set; } = false;

        private Dictionary<Button, GroupBox> ControlLinker { get; set; } = new Dictionary<Button, GroupBox>();

        public Ip2VecSettingForm()
        {
            InitializeComponent();
            this.ThreatNameContextGroup.Text = Definitions.ThreatNameCN;
            this.SourceIpContextGroup.Text = Definitions.SourceIpCN;
            this.SourcePortContextGroup.Text = Definitions.SourcePortCN;
            this.TargetIpContextGroup.Text = Definitions.TargetIpCN;
            this.TargetPortContextGroup.Text = Definitions.TargetPortCN;
            this.ProtocolContextGroup.Text = Definitions.ProtocolCN;
        }

        private void SetDefaultSettings()
        {
            foreach (var el in this.TargetSelectorGroup.Controls)
            {
                if (el is Button sp && Ip2VecSettings.DefaultIp2VecContextsSettings.ContainsKey(sp.Text))
                {
                    sp.PerformClick();
                    var contexts = Ip2VecSettings.DefaultIp2VecContextsSettings[sp.Text];
                    foreach (var con in this.ControlLinker[sp].Controls)
                    {
                        if (con is CheckBox cb && contexts.Contains(cb.Text))
                        {
                            cb.Checked = true;
                        }
                    }
                }
            }
        }

        private void Ip2VecSettings_Load(object sender, EventArgs e)
        {
            this.AddContextButton(this.ThreatNameContextGroup);
            this.AddContextButton(this.SourceIpContextGroup);
            this.AddContextButton(this.SourcePortContextGroup);
            this.AddContextButton(this.TargetIpContextGroup);
            this.AddContextButton(this.TargetPortContextGroup);
            this.AddContextButton(this.ProtocolContextGroup);

            this.ControlLinker.Add(this.ThreatNameTargetSelector, this.ThreatNameContextGroup);
            this.ControlLinker.Add(this.SourceIpTargetSelector, this.SourceIpContextGroup);
            this.ControlLinker.Add(this.SourcePortTargetSelector, this.SourcePortContextGroup);
            this.ControlLinker.Add(this.TargetIpTargetSelector, this.TargetIpContextGroup);
            this.ControlLinker.Add(this.TargetPortTargetSelector, this.TargetPortContextGroup);
            this.ControlLinker.Add(this.ProtocolTargetSelector, this.ProtocolContextGroup);

            this.SetDefaultSettings();
        }

        private void AddContextButton(GroupBox gp)
        {
            var tnButton = new CheckBox
            {
                Name = gp.Name + Definitions.ThreatNameEN,
                Text = Definitions.ThreatNameCN,
                Location = new Point(6, 20),
                Size = new Size(75, 29),
                Enabled = false
            };
            gp.Controls.Add(tnButton);

            var sipButton = new CheckBox
            {
                Name = gp.Name + Definitions.SourceIpEN,
                Text = Definitions.SourceIpCN,
                Location = new Point(87, 20),
                Size = new Size(75, 29),
                Enabled = false
            };
            gp.Controls.Add(sipButton);

            var spButton = new CheckBox
            {
                Name = gp.Name + Definitions.SourcePortEN,
                Text = Definitions.SourcePortCN,
                Location = new Point(168, 20),
                Size = new Size(75, 29),
                Enabled = false
            };
            gp.Controls.Add(spButton);

            var tipButton = new CheckBox
            {
                Name = gp.Name + Definitions.TargetIpEN,
                Text = Definitions.TargetIpCN,
                Location = new Point(249, 20),
                Size = new Size(75, 29),
                Enabled = false
            };
            gp.Controls.Add(tipButton);

            var tpButton = new CheckBox
            {
                Name = gp.Name + Definitions.TargetPortEN,
                Text = Definitions.TargetPortCN,
                Location = new Point(330, 20),
                Size = new Size(75, 29),
                Enabled = false
            };
            gp.Controls.Add(tpButton);

            var pButton = new CheckBox
            {
                Name = gp.Name + Definitions.ProtocolEN,
                Text = Definitions.ProtocolCN,
                Location = new Point(411, 20),
                Size = new Size(75, 29),
                Enabled = false
            };
            gp.Controls.Add(pButton);
        }

        private void ThreatNameTargetSelector_Click(object sender, EventArgs e)
        {
            this.TargetThreatName = !this.TargetThreatName;
            this.TargetSelector_Click(this.TargetThreatName, this.ThreatNameTargetSelector, this.ThreatNameContextGroup, sender);
        }

        private void SourceIpTargetSelector_Click(object sender, EventArgs e)
        {
            this.TargetSourceIp = !this.TargetSourceIp;
            this.TargetSelector_Click(this.TargetSourceIp, this.SourceIpTargetSelector, this.SourceIpContextGroup, sender);
        }

        private void SourcePortTargetSelector_Click(object sender, EventArgs e)
        {
            this.TargetSourcePort = !this.TargetSourcePort;
            this.TargetSelector_Click(this.TargetSourcePort, this.SourcePortTargetSelector, this.SourcePortContextGroup, sender);
        }

        private void TargetIpTargetSelector_Click(object sender, EventArgs e)
        {
            this.TargetTargetIp = !this.TargetTargetIp;
            this.TargetSelector_Click(this.TargetTargetIp, this.TargetIpTargetSelector, this.TargetIpContextGroup, sender);
        }

        private void TargetPortTargetSelector_Click(object sender, EventArgs e)
        {
            this.TargetTargetPort = !this.TargetTargetPort;
            this.TargetSelector_Click(this.TargetTargetPort, this.TargetPortTargetSelector, this.TargetPortContextGroup, sender);
        }

        private void ProtocolTargetSelector_Click(object sender, EventArgs e)
        {
            this.TargetProtocol = !this.TargetProtocol;
            this.TargetSelector_Click(this.TargetProtocol, this.ProtocolTargetSelector, this.ProtocolContextGroup, sender);
        }

        private void TargetSelector_Click(bool check, Button targetButton, GroupBox targetGroup, object sender)
        {
            targetButton.FlatAppearance.BorderSize = check ? 3 : 1;
            targetButton.FlatAppearance.BorderColor = check ? Color.Black : Color.Gray;
            foreach (var item in targetGroup.Controls)
            {
                if (item is CheckBox button && button.Text != (sender as Button).Text)
                {
                    button.Enabled = check;
                }
            }
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            this.Ip2VecSettings = new Ip2VecSettings();
            foreach (var c in this.Controls)
            {
                if (c is Button b && Contexts.ContextFuncDict.ContainsKey(b.Text))
                {
                    var contexts = new List<Contexts.GetMem>();
                    foreach (var item in this.ThreatNameContextGroup.Controls)
                    {
                        if (item is CheckBox check && check.Checked)
                        {
                            contexts.Add(Contexts.ContextFuncDict[check.Text]);
                        }
                    }
                    this.Ip2VecSettings.ContextRelationDict.Add(Contexts.ContextFuncDict[b.Text], contexts);
                }
            }
        }
    }
}
