using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace English_tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool restart = true;
        private string[] keys;
        int index = -1;
        private int score;
        Dictionary<string, string> vocabulary = new Dictionary<string, string>();

        private void RestartButton_Click(object sender, EventArgs e)
        {
            if (restart)
            {
                if (textBox1.Text.Split(new string[] { " ", Environment.NewLine, ";", ",", ".", ":" },
                        StringSplitOptions.RemoveEmptyEntries).Length > 2)
                {
                    restart = false;
                    index = -1;
                    score = 0;
                    label1.ForeColor = System.Drawing.Color.Black;
                    RestartButton.Text = "New list";
                    string[] temp = textBox1.Text.Split(new string[] { " ", Environment.NewLine, ";", ",", ".", ":" },
                        StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < temp.Length; i += 2)
                    {
                        if (i + 1 < temp.Length)
                        {
                            vocabulary.Add(temp[i].ToLower(), temp[i + 1].ToLower());
                        }
                    }
                         keys = vocabulary.Keys.ToArray<string>();
                        Random rnd = new Random();
                        for (int i = 1; i < keys.Length; i++)
                        {
                            int rndIndex = rnd.Next(i);
                            if (rndIndex != i)
                            {
                                string tempstring = keys[rndIndex];
                                keys[rndIndex] = keys[i];
                                keys[i] = tempstring;
                            }
                        }
                    NextButton.Enabled = true;
                    SkipButton.Enabled = true;
                    textBox1.Text = "";
                    NextWord(false);
                }
                else
                {
                    label1.Text = "Enter the word pairs separated by ; , . : space or newline";
                    label1.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                restart = true;
                RestartButton.Text = "Begin";
                NextButton.Enabled = false;
                vocabulary = new Dictionary<string, string>();
                index = -1;
            }
        }

        private bool showResult = false;
        private void NextWord(bool skip)
        {
            if (!skip)
            {
                if (showResult)
                {
                    if (vocabulary[keys[index]] == textBox1.Text.ToLower())
                    {
                        label1.Text = "Correct!";
                        score++;
                    }
                    else
                    {
                        label1.Text = "Wrong!";
                    }
                    label1.ForeColor = System.Drawing.Color.Red;
                    showResult = false;
                }
                else
                {
                    index++;
                    showResult = true;
                    label1.ForeColor = System.Drawing.Color.Black;
                    label1.Text = keys[index];
                    if (index == vocabulary.Count)
                    {
                        NextButton.Enabled = false;
                        SkipButton.Enabled = false;
                        label1.Text = "this was the last one. You scored " + score + " out of " +  index + " points";
                    }
                }
            }
            else
            {
                showResult = false;
                NextWord(false);
            }
            textBox1.Text = "";
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            NextWord(false);
        }

        private void SkipButton_Click(object sender, EventArgs e)
        {
            NextWord(true);
        }
    }
}
