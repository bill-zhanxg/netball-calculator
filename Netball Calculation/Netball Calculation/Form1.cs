﻿using Netball_Calculation.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Netball_Calculation
{
    public partial class Form1 : Form
    {
        public List<mainData> RoundList = new List<mainData>();
        private bool isModified = false;
        public int RoundNum = 1;

        public Form1()
        {
            InitializeComponent();
            writeHomeData(new homeData()
            {
                gender = Settings.Default.gender,
                yearLevel = Settings.Default.yearLevel,
                homeSchool = Settings.Default.homeSchoolName,
                visitorSchool = Settings.Default.visitorSchoolName,
            }); ;
            chartTypeComboBox.SelectedIndex = 0;
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bringHomePanel();
        }

        private void addNewRoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newRound();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bringPreferencesPanel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (RoundNum > 1)
            {
                mainPanel.Tag = 1;
                update();
            }
            else
            {
                newRound();
            }
        }

        #region TextBox Text Changes

        private void homegs_TextChanged(object sender, EventArgs e)
        {
            changeName("homegs");
        }

        private void homega_TextChanged(object sender, EventArgs e)
        {
            changeName("homega");
        }

        private void visigs_TextChanged(object sender, EventArgs e)
        {
            changeName("visigs");
        }

        private void visiga_TextChanged(object sender, EventArgs e)
        {
            changeName("visiga");
        }

        private void changeName(string which)
        {
            int index = RoundList.FindIndex(o => o.roundNum == int.Parse(mainPanel.Tag.ToString()));
            if (index == -1)
            {
                MessageBox.Show("There is an error, please restart the application!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (which == "homegs")
                {
                    RoundList[index].homeGS = homegs.Text;
                }
                else if (which == "homega")
                {
                    RoundList[index].homeGA = homega.Text;
                }
                else if (which == "visigs")
                {
                    RoundList[index].visiGS = visigs.Text;
                }
                else if (which == "visiga")
                {
                    RoundList[index].visiGA = visiga.Text;
                }

                update();
            }
        }

        #endregion

        #region Score Buttons Click

        private void homeGsScoreButton_Click(object sender, EventArgs e)
        {
            changeScore("homegs");
        }

        private void homeGaScoreButton_Click(object sender, EventArgs e)
        {
            changeScore("homega");
        }

        private void visiGsScoreButton_Click(object sender, EventArgs e)
        {
            changeScore("visigs");
        }

        private void visiGaScoreButton_Click(object sender, EventArgs e)
        {
            changeScore("visiga");
        }

        private void homeGsScoreUndoButton_Click(object sender, EventArgs e)
        {
            changeScore("homegsundo");
        }

        private void homeGaScoreUndoButton_Click(object sender, EventArgs e)
        {
            changeScore("homegaundo");
        }

        private void visiGsScoreUndoButton_Click(object sender, EventArgs e)
        {
            changeScore("visigsundo");
        }

        private void visiGaScoreUndButton_Click(object sender, EventArgs e)
        {
            changeScore("visigaundo");
        }

        private void changeScore(string which)
        {
            int index = RoundList.FindIndex(o => o.roundNum == int.Parse(mainPanel.Tag.ToString()));
            if (index == -1)
            {
                MessageBox.Show("There is an error, please restart the application!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (which == "homegs")
                {
                    RoundList[index].homeGSScore++;
                }
                else if (which == "homega")
                {
                    RoundList[index].homeGAScore++;
                }
                else if (which == "visigs")
                {
                    RoundList[index].visiGSScore++;
                }
                else if (which == "visiga")
                {
                    RoundList[index].visiGAScore++;
                }
                else if (which == "homegsundo")
                {
                    RoundList[index].homeGSScore--;
                }
                else if (which == "homegaundo")
                {
                    RoundList[index].homeGAScore--;
                }
                else if (which == "visigsundo")
                {
                    RoundList[index].visiGSScore--;
                }
                else if (which == "visigaundo")
                {
                    RoundList[index].visiGAScore--;
                }

                update();
            }
        }

        #endregion

        private void tabRoudnsClicked(object sender, EventArgs e)
        {
            mainPanel.Tag = (sender as ToolStripItem).Tag;
            update();
        }

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadFile();
        }

        private void saveAsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToFile();
        }

        private void summaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSummary();
        }

        private void creditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Form f = new AboutBox1())
            {
                f.ShowDialog();
            }
        }

        private void chartTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectText = chartTypeComboBox.SelectedItem.ToString();
            switch (selectText)
            {
                case "Column":
                    summaryChart.Series["Schools"].ChartType = SeriesChartType.Column;
                    break;
                case "Point":
                    summaryChart.Series["Schools"].ChartType = SeriesChartType.Point;
                    break;
                case "Line":
                    summaryChart.Series["Schools"].ChartType = SeriesChartType.Line;
                    break;
                case "Spline":
                    summaryChart.Series["Schools"].ChartType = SeriesChartType.Spline;
                    break;
                case "StepLine":
                    summaryChart.Series["Schools"].ChartType = SeriesChartType.StepLine;
                    break;
                case "Bar":
                    summaryChart.Series["Schools"].ChartType = SeriesChartType.Bar;
                    break;
                case "Area":
                    summaryChart.Series["Schools"].ChartType = SeriesChartType.Area;
                    break;
                case "SplineArea":
                    summaryChart.Series["Schools"].ChartType = SeriesChartType.SplineArea;
                    break;
                case "Pie":
                    summaryChart.Series["Schools"].ChartType = SeriesChartType.Pie;
                    break;
                case "Doughnut":
                    summaryChart.Series["Schools"].ChartType = SeriesChartType.Doughnut;
                    break;
                case "Range":
                    summaryChart.Series["Schools"].ChartType = SeriesChartType.Range;
                    break;
                case "SplineRange":
                    summaryChart.Series["Schools"].ChartType = SeriesChartType.SplineRange;
                    break;
                case "Radar":
                    summaryChart.Series["Schools"].ChartType = SeriesChartType.Radar;
                    break;
                case "Kagi":
                    summaryChart.Series["Schools"].ChartType = SeriesChartType.Kagi;
                    break;
                case "Funnel":
                    summaryChart.Series["Schools"].ChartType = SeriesChartType.Funnel;
                    break;
                case "Pyramid":
                    summaryChart.Series["Schools"].ChartType = SeriesChartType.Pyramid;
                    break;
                default:
                    MessageBox.Show("Huh, I think there is an error. Can not find the type you selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isModified == true)
            {
                DialogResult result = MessageBox.Show("Do you want to save before exit?", "You sure?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    e.Cancel = true;
                    saveToFile();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void previousRoundButton_Click(object sender, EventArgs e)
        {
            if (mainPanel.Tag == null)
            {
                MessageBox.Show("Can not find the game number! Please try again", "An Error occurred!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int thisRound = int.Parse(mainPanel.Tag.ToString());
            if (thisRound == 1)
            {
                bringHomePanel();
            }
            else
            {
                thisRound--;
                mainPanel.Tag = thisRound;
                update();
            }
        }

        private void nextRoundButton_Click(object sender, EventArgs e)
        {
            if (mainPanel.Tag == null)
            {
                MessageBox.Show("Can not find the game number! Please try again", "An Error occurred!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int thisRound = int.Parse(mainPanel.Tag.ToString());
            if (thisRound == RoundNum - 1)
            {
                newRound();
            }
            else
            {
                thisRound++;
                mainPanel.Tag = thisRound;
                update();
            }
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            bringHomePanel();
        }

        private void setting_Leave(object sender, EventArgs e)
        {
            Settings.Default[(sender as TextBox).Tag.ToString()] = (sender as TextBox).Text;
            Settings.Default.Save();
        }

        private void setting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                preferencesPanel.Focus();
                // these last two lines will stop the beep sound
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void settingGender_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.gender = (sender as RadioButton).Tag.ToString();
            Settings.Default.Save();
        }

        private void settingYearLevel_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.yearLevel = (sender as RadioButton).Tag.ToString();
            Settings.Default.Save();
        }

        private void bringHomePanel()
        {
            homePanel.BringToFront();
            if (RoundNum > 1)
            {
                homeNewRound.Text = "Next Round --->";
            }
            else
            {
                homeNewRound.Text = "New Round --->";
            }
        }

        private void bringPreferencesPanel()
        {
            Settings ob = Settings.Default;
            string homeSchoolName = ob.homeSchoolName;
            string visitorSchoolName = ob.visitorSchoolName;
            string gender = ob.gender;
            string yearLevel = ob.yearLevel;
            string homeGS = ob.homeGS;
            string homeGA = ob.homeGA;
            string visitorGS = ob.visitorGS;
            string visitorGA = ob.visitorGA;
            preferencesHomeSchoolName.Text = homeSchoolName;
            preferencesVisitorSchoolName.Text = visitorSchoolName;
            preferencesHomeGS.Text = homeGS;
            preferencesHomeGA.Text = homeGA;
            preferencesVisitorGS.Text = visitorGS;
            preferencesVisitorGA.Text = visitorGA;
#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
            RadioButton checkGender = preferencesGenderTablePanel.Controls.OfType<RadioButton>().FirstOrDefault(o => o.Tag.ToString() == gender);
            RadioButton checkYearLevel = preferencesYearLevelTablePanel.Controls.OfType<RadioButton>().FirstOrDefault(o => o.Tag.ToString() == yearLevel);
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast

            if (checkGender != null) checkGender.PerformClick();
            if (checkYearLevel != null) checkYearLevel.PerformClick();

            preferencesPanel.BringToFront();
        }

        private void newRound()
        {
            int thisNum = RoundNum;
            RoundNum++;
            mainPanel.BringToFront();
            mainData data = new mainData()
            {
                roundNum = thisNum,
                homeGS = Settings.Default.homeGS,
                homeGA = Settings.Default.homeGA,
                visiGS = Settings.Default.visitorGS,
                visiGA = Settings.Default.visitorGA,
                homeGSScore = 0,
                homeGAScore = 0,
                visiGSScore = 0,
                visiGAScore = 0,
            };

            RoundList.Add(data);

            mainPanel.Tag = thisNum;
            update();

            ToolStripItem i = tabsToolStripMenuItem.DropDownItems.Add($"Round {thisNum}");
            i.Tag = thisNum;
            i.Click += tabRoudnsClicked;
        }

        private void update()
        {
            isModified = true;
            mainPanel.BringToFront();
            if (mainPanel.Tag != null)
            {
                int gameNum = int.Parse(mainPanel.Tag.ToString());
                mainData data = RoundList.Where(o => o.roundNum == gameNum).FirstOrDefault();
                if (data == null)
                {
                    MessageBox.Show("Can not find the game number! Please try again", "An Error occurred!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                roundLabel.Text = $"Round {data.roundNum}:";
                homegs.Text = data.homeGS;
                homega.Text = data.homeGA;
                visigs.Text = data.visiGS;
                visiga.Text = data.visiGA;

                if (string.IsNullOrWhiteSpace(data.homeGS))
                {
                    homeGsScoreButton.Text = $"Home\nGS\nScored";
                    homeGsScoreLabel.Text = $"GS Scores:";
                }
                else
                {
                    homeGsScoreButton.Text = $"Home\nGS ({data.homeGS})\nScored";
                    homeGsScoreLabel.Text = $"GS ({data.homeGS}) Scores:";
                }

                if (string.IsNullOrWhiteSpace(data.homeGA))
                {
                    homeGaScoreButton.Text = $"Home\nGA\nScored";
                    homeGaScoreLabel.Text = $"GA Scores:";
                }
                else
                {
                    homeGaScoreButton.Text = $"Home\nGA ({data.homeGA})\nScored";
                    homeGaScoreLabel.Text = $"GA ({data.homeGA}) Scores:";
                }

                if (string.IsNullOrWhiteSpace(data.visiGS))
                {
                    visiGsScoreButton.Text = $"Visitors\nGS\nScored";
                    visiGsScoreLabel.Text = $"GS Scores:";
                }
                else
                {
                    visiGsScoreButton.Text = $"Visitors\nGS ({data.visiGS})\nScored";
                    visiGsScoreLabel.Text = $"GS ({data.visiGS}) Scores:";
                }

                if (string.IsNullOrWhiteSpace(data.visiGA))
                {
                    visiGaScoreButton.Text = $"Visitors\nGA\nScored";
                    visiGaScoreLabel.Text = $"GA Scores:";
                }
                else
                {
                    visiGaScoreButton.Text = $"Visitors\nGA ({data.visiGA})\nScored";
                    visiGaScoreLabel.Text = $"GA ({data.visiGA}) Scores:";
                }

                homeGsScore.Text = data.homeGSScore.ToString();
                homeGaScore.Text = data.homeGAScore.ToString();
                visiGsScore.Text = data.visiGSScore.ToString();
                visiGaScore.Text = data.visiGAScore.ToString();

                homeTotalScore.Text = (data.homeGSScore + data.homeGAScore).ToString();
                visiTotalScore.Text = (data.visiGSScore + data.visiGAScore).ToString();

                if (data.roundNum == RoundNum - 1)
                {
                    nextRoundButton.Text = "New Round --->";
                }
                else
                {
                    nextRoundButton.Text = "Next Round --->";
                }
                if (data.roundNum == 1)
                {
                    previousRoundButton.Text = "<--- Home";
                }
                else
                {
                    previousRoundButton.Text = "<--- Previous Round";
                }
            }
            else
            {
                MessageBox.Show("Can not find the game number! Please try again", "An Error occurred!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadFile()
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Title = "Load Game Data";
            f.Filter = "JSON files (*.json)|*.json";
            if (f.ShowDialog() == DialogResult.OK)
            {
                isModified = false;
                IEnumerable<string> lines = File.ReadLines(f.FileName);
                try
                {
                    string s = lines.First();
                    var k = new JavaScriptSerializer().Deserialize<List<mainData>>(s);
                    RoundList = k;

                    if (RoundList.Count > 0)
                    {
                        mainPanel.Tag = 1;
                        update();
                    }
                    RoundNum = 1;
                    ToolStripItem home = tabsToolStripMenuItem.DropDownItems[0];
                    tabsToolStripMenuItem.DropDownItems.Clear();
                    tabsToolStripMenuItem.DropDownItems.Add(home);
                    foreach (mainData data in RoundList)
                    {
                        ToolStripItem i = tabsToolStripMenuItem.DropDownItems.Add($"Round {data.roundNum}");
                        i.Tag = data.roundNum;
                        i.Click += tabRoudnsClicked;
                        RoundNum++;
                    }

                    string v = lines.ElementAt(1);
                    var homeD = new JavaScriptSerializer().Deserialize<homeData>(v);
                    writeHomeData(homeD);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"There is an error while reading the file, make sure it's not been edited! Error: {ex.Message}", "Error Reading File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveToFile()
        {
            var rounds = new JavaScriptSerializer().Serialize(RoundList);
            var home = new JavaScriptSerializer().Serialize(getHomeData());

            SaveFileDialog f = new SaveFileDialog();
            f.Title = "Save Game Data";
            f.FileName = $"Netball Data {DateTime.Now.ToString().Replace("/", "-").Replace(":", "_")}";
            f.DefaultExt = "json";
            f.Filter = "JSON files (*.json)|*.json";
            if (f.ShowDialog() == DialogResult.OK)
            {
                isModified = false;
                using (StreamWriter outputFile = new StreamWriter(f.FileName))
                {
                    outputFile.WriteLine(rounds);
                    outputFile.WriteLine(home);
                }
            }
        }

        private homeData getHomeData()
        {
            RadioButton checkedGender = genderTablePanel.Controls.OfType<RadioButton>().FirstOrDefault(o => o.Checked);
            RadioButton checkedYearLevel = yearLevelTablePanel.Controls.OfType<RadioButton>().FirstOrDefault(o => o.Checked);
            string gender = checkedGender.Name;
            string yearLevel = checkedYearLevel.Name;
            string home = homeTextBox.Text;
            string visitors = visitorsTextBox.Text;
            return new homeData()
            {
                gender = gender,
                yearLevel = yearLevel,
                homeSchool = home,
                visitorSchool = visitors,
                genderText = checkedGender.Text,
                yearLevelText = checkedYearLevel.Text,
            };
        }

        private void writeHomeData(homeData data)
        {
            RadioButton checkGender = genderTablePanel.Controls.OfType<RadioButton>().FirstOrDefault(o => o.Name == data.gender);
            RadioButton checkYearLevel = yearLevelTablePanel.Controls.OfType<RadioButton>().FirstOrDefault(o => o.Name == data.yearLevel);
            foreach (RadioButton radioButton in genderTablePanel.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked) radioButton.Checked = false;
            };
            foreach (RadioButton radioButton in yearLevelTablePanel.Controls.OfType<RadioButton>())
            {
                if (radioButton.Checked) radioButton.Checked = false;
            }
            checkGender.Checked = true;
            checkYearLevel.Checked = true;

            homeTextBox.Text = data.homeSchool;
            visitorsTextBox.Text = data.visitorSchool;
        }

        private void showSummary()
        {
            homeData homeData = getHomeData();
            int homeTotalScore = 0;
            int visitorTotalScore = 0;
            string summaryTitleText = "";
            if (string.IsNullOrWhiteSpace(homeData.homeSchool))
            {
                summaryTitleText += "Home School    VS    Visitors School";
            }
            else
            {
                summaryTitleText += $"Home School ({homeData.homeSchool})    VS    Visitors School";
            }
            if (!string.IsNullOrWhiteSpace(homeData.visitorSchool))
            {
                summaryTitleText += $" ({homeData.visitorSchool})";
            }
            summaryTitleLabel.Text = summaryTitleText;
            string lableText = $"Gender: \"{homeData.genderText}\"\nYear Level: \"{homeData.yearLevelText}\"\n\n";
            foreach (mainData data in RoundList)
            {
                homeTotalScore += data.homeGSScore + data.homeGAScore;
                visitorTotalScore += data.visiGSScore + data.visiGAScore;
                lableText += $"Round {data.roundNum}:\nHome GS Name: " + (string.IsNullOrWhiteSpace(data.homeGS) ? "\"NoNameSpecified\"" : $"\"{data.homeGS}\"") + "    Home GA Name: " + (string.IsNullOrWhiteSpace(data.homeGA) ? "\"NoNameSpecified\"" : $"\"{data.homeGA}\"") + "\nVisitors GS Name: " + (string.IsNullOrWhiteSpace(data.visiGS) ? "\"NoNameSpecified\"" : $"\"{data.visiGS}\"") + "    Visitors GA Name: " + (string.IsNullOrWhiteSpace(data.visiGA) ? "\"NoNameSpecified\"\n" : $"\"{data.visiGA}\"\n");
                lableText += $"Home GS Scored: *{data.homeGSScore}*    Home GA Scored: *{data.homeGAScore}*    Home Total Scored (Round): *{data.homeGSScore + data.homeGAScore}*\nVisitors GS Scored: *{data.visiGSScore}*    Visitors GA Scored: *{data.visiGAScore}*    Visitors Total Scored (Round): *{data.visiGSScore + data.visiGAScore}*\n\n";
            }
            lableText += $"Home School Total Score: *{homeTotalScore}*\nVisitor School Total Score: *{visitorTotalScore}*\n\n";
            summaryLabel.Text = lableText;

            summaryChart.Series["Schools"].Points.Clear();
            summaryChart.Series["Schools"].Points.AddXY($"Home ({homeData.homeSchool})", homeTotalScore);
            summaryChart.Series["Schools"].Points.AddXY($"Visitors ({homeData.visitorSchool})", visitorTotalScore);

            summaryPanel.BringToFront();
        }
    }

    public class mainData
    {
        public int roundNum { get; set; }
        public string homeGS { get; set; }
        public string homeGA { get; set; }
        public string visiGS { get; set; }
        public string visiGA { get; set; }
        public int homeGSScore { get; set; }
        public int homeGAScore { get; set; }
        public int visiGSScore { get; set; }
        public int visiGAScore { get; set; }
    }

    public class homeData
    {
        public string gender { get; set; }
        public string yearLevel { get; set; }
        public string homeSchool { get; set; }
        public string visitorSchool { get; set; }
        public string genderText { get; set; }
        public string yearLevelText { get; set; }
    }
}