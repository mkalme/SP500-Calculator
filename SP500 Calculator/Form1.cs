using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace SP500_Calculator
{
    public partial class Form1 : Form
    {
        private static Panel[] allTools;
        List<Action> list = new List<Action>();

        public Form1()
        {
            InitializeComponent();
            mainFunction();
        }

        private void mainFunction() {
            setupTabs();
            setupTools();
        }

        private void setupTabs() {
            ComboBox[] comboBoxArray = {yearComboBox1, endYearComboBox1, yearComboBox2, endYearComboBox2, yearComboBox3, endYearComboBox3,
            comboBox2, comboBox3, comboBox4, comboBox5, comboBox7};

            for (int i = Storage.startingYear; i <= Storage.endingYear; i++)
            {
                for (int b = 0; b < comboBoxArray.Length; b++) {
                    comboBoxArray[b].Items.Add(i);
                }
            }
        }

        private void setupTools() {
            allTools = new Panel[] {toolNr1, toolNr2, toolNr3, toolNr4, toolNr5};

            Size = new Size(468, 489);
        }

        public static void comboBoxSelectedIndexChanged(ComboBox monthComboBox, ComboBox yearComboBox) {
            String[] arrayMonths = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            monthComboBox.Items.Clear();
            for (int i = 0; i < 12; i++)
            {
                if (!string.IsNullOrEmpty(Storage.array[yearComboBox.SelectedIndex, i]))
                {
                    monthComboBox.Items.Add(new DateTime(2015, i + 1, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("us")));
                }
            }
        }

        public bool checkIfEnableCalculateButton(Object[] arrayOfObjects)
        {
            bool enable = true;

            try
            {
                for (int i = 0; i < arrayOfObjects.Length; i++)
                {
                    if (arrayOfObjects[i].GetType() == typeof(TextBox))
                    {
                        enable = !string.IsNullOrEmpty(((TextBox)arrayOfObjects[i]).Text);
                    }
                    else if (arrayOfObjects[i].GetType() == typeof(RichTextBox))
                    {
                        enable = !string.IsNullOrEmpty(((RichTextBox)arrayOfObjects[i]).Text);
                    }
                    else if (arrayOfObjects[i].GetType() == typeof(ComboBox))
                    {
                        enable = !string.IsNullOrEmpty(((ComboBox)arrayOfObjects[i]).Text);
                    }
                    else
                    {
                        enable = false;
                    }

                    if (enable == false)
                    {
                        i = arrayOfObjects.Length;
                    }
                }
            }
            catch
            {
                errorMessage();
                enable = false;
            }

            return enable;
        }

        private void setText(String text)
        {
            try
            {
                Clipboard.SetText(string.IsNullOrEmpty(text) ? " " : text);
            }
            catch
            {
                errorMessage();
            }
        }

        private static void errorMessage()
        {
            MessageBox.Show("There was an error.\t\t\t\t", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        //=================================== DATA TAB ===================================
        private void calculateDataButton_Click(object sender, EventArgs e)
        {
            if(checkIfEnableCalculateButton(new Object[] { monthComboBox1, yearComboBox1, endMonthComboBox1, endYearComboBox1 }))
            {
                try
                {
                    Data.form = this;
                    Data.calculate();
                }
                catch {
                    errorMessage();
                }
            }
        }

        private void yearComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSelectedIndexChanged(monthComboBox1, yearComboBox1);
        }

        private void endYearComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSelectedIndexChanged(endMonthComboBox1, endYearComboBox1);
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            setText(Data.listArray[0]);
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            setText(Data.listArray[1]);
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            setText(Data.listArray[2]);
        }


        //=================================== ACCUMULATION TAB ===================================
        private void calculateAccumulationButton_Click(object sender, EventArgs e)
        {
            try
            {
                Accumulation.form = this;
                Accumulation.calculate();
            }
            catch
            {
                errorMessage();
            }
        }

        private void yearComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSelectedIndexChanged(monthComboBox2, yearComboBox2);
        }

        private void endYearComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSelectedIndexChanged(endMonthComboBox2, endYearComboBox2);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            setText(Accumulation.growthMonth);
        }


        //=================================== GRAPH TAB ===================================
        private void calculateGraphButton_Click(object sender, EventArgs e) {
        
            try
            {
                Graph.form = this;
                Graph.calculate();
            }
            catch
            {
                errorMessage();
            }
        }

        private void yearComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSelectedIndexChanged(monthComboBox3, yearComboBox3);
        }

        private void endYearComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSelectedIndexChanged(endMonthComboBox3, endYearComboBox3);
        }

        private void copyClipbaordLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            setText(richTextBox1.Text);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            setText(richTextBox2.Text);
        }


        //=================================== MORE TOOLS TAB ===================================
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = toolComboBox.SelectedIndex;

            for (int i = 0; i < allTools.Length; i++) {
                allTools[i].Visible = false;
            }

            tabPage4.Controls.Add(allTools[selectedIndex]);

            allTools[selectedIndex].Location = new Point(6, 62);
            allTools[selectedIndex].Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                switch (toolComboBox.SelectedIndex)
                {
                    case 0:
                        Tools_AccumulationFixed.form = this;
                        Tools_AccumulationFixed.calculate();
                        break;
                    case 1:
                        Tools_PercentageCalculator.form = this;
                        Tools_PercentageCalculator.calculate();
                        break;
                    case 2:
                        Tools_SP500RawCalc.form = this;
                        Tools_SP500RawCalc.calculate();
                        break;
                    case 3:
                        Tools_NetWorthGraphCalc.form = this;
                        Tools_NetWorthGraphCalc.calculate();
                        break;
                    case 4:
                        Tools_IntrinsicValueSP.form = this;
                        Tools_IntrinsicValueSP.calculate();
                        break;
                }
            }
            catch {
                errorMessage();
            }
        }

        //TOOL NR 1
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            setText(Tools_AccumulationFixed.text);
        }

        //TOOL NR 3
        private void browseButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox8.Text = openFileDialog1.FileName;
        }

        //TOOL NR 4
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox13.Enabled = !checkBox2.Checked;
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            comboBoxSelectedIndexChanged(comboBox1, comboBox2);
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            setText(richTextBox3.Text);
        }

        //TOOL NR 5
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSelectedIndexChanged(comboBox8, comboBox7);
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSelectedIndexChanged(comboBox6, comboBox5);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            setText(richTextBox4.Text);
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            setText(richTextBox5.Text);
        }
    }
}
