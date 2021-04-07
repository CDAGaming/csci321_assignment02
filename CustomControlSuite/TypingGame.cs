using System;
using System.Windows.Forms;

namespace CustomControlSuite
{
    public partial class TypingGame : Form
    {
        private bool isGameRunning = false;

        public TypingGame()
        {
            InitializeComponent();
        }

        // ParseStringLength: Get Evaluative String length based on Difficulty
        private int ParseStringLength()
        {
            return easyRad.Checked ? 5 :
                hardRad.Checked ? 15 : 10;
        }

        // GenerateRandomString: Generate a Random String with the specified character array
        private string GenerateRandomString()
        {
            string randomString = "";
            char[] characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            Random randIndex = new Random();
            for (int i = 0; i < ParseStringLength(); i++)
            {
                randomString += characters[randIndex.Next(0, characters.Length - 1)].ToString();
            }
            return randomString;
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            if (!isGameRunning)
            {
                clock1.ResetTiming();
                isGameRunning = true;
                clock1.StartTiming();
                pauseBtn.Visible = true;
                startBtn.Text = GenerateRandomString();
                startBtn.Enabled = false;
                resTextBox.Focus();
                resTextBox.CharacterCasing = CharacterCasing.Upper;
            }
        }

        private void ResTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && isGameRunning)
            {
                string res = resTextBox.Text.ToUpper();
                if (res == startBtn.Text)
                {
                    clock1.PauseTiming();
                    pauseBtn.Visible = false;
                    startBtn.Text = $"Time taken: {clock1.currentHour}:{clock1.currentMinute}:{clock1.currentSeconds}. Click to try again.";
                    isGameRunning = false;
                    resTextBox.Text = "";
                    startBtn.Enabled = true;
                }
            }
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            if (pauseBtn.Text == "PAUSE")
            {
                clock1.PauseTiming();
                startBtn.Visible = false;
                resTextBox.Enabled = false;
                pauseBtn.Text = "RESUME";
            }
            else if (pauseBtn.Text == "RESUME")
            {
                clock1.StartTiming();
                startBtn.Visible = true;
                resTextBox.Enabled = true;
                pauseBtn.Text = "PAUSE";
                resTextBox.Focus();
            }
        }
    }
}
