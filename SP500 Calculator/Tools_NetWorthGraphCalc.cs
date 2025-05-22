using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SP500_Calculator
{
    class Tools_NetWorthGraphCalc
    {
        public static Form1 form;
        public static int numberOfMonths = 0;
        public static String text = "";

        public static void calculate() {
            if (form.checkIfEnableCalculateButton(new Object[] { form.comboBox1, form.comboBox2, form.textBox11, form.textBox12, form.textBox16, form.comboBox3, 
                form.comboBox4}) && (form.checkBox2.Checked ? true : form.checkIfEnableCalculateButton(new Object[] { form.textBox13 })))
            {
                calc();
            }
        }

        public static void calc() {
            //CLEAR
            numberOfMonths = 0;
            text = "";

            int selectedYear = Int32.Parse(form.comboBox2.Text);
            int selectedMonth = form.comboBox1.SelectedIndex;

            int selectedEndYear = Int32.Parse(form.comboBox3.Text) - 1;
            int selectedEndMonth = 11;

            //GET NUMBER OF MONTHS
            DateTime start = new DateTime(selectedYear, selectedMonth + 1, 1);
            DateTime end = new DateTime(selectedEndYear, selectedEndMonth + 1, 1);
            numberOfMonths = 1 + ((end.Year - start.Year) * 12) + end.Month - start.Month;

            double result = getFirst();

            
            selectedYear = Int32.Parse(form.comboBox3.Text);
            selectedMonth = 0;

            selectedEndYear = Int32.Parse(form.comboBox4.Text);
            selectedEndMonth = getLastMonth(form.comboBox4.SelectedIndex);

            start = new DateTime(selectedYear, selectedMonth + 1, 1);
            end = new DateTime(selectedEndYear, selectedEndMonth + 1, 1);
            numberOfMonths = 1 + ((end.Year - start.Year) * 12) + end.Month - start.Month;
            result = getSecond(result);

            form.richTextBox3.Text = text;
        }

        public static double getSecond(double result) {
            double withdrawal = Double.Parse(form.textBox16.Text.Replace(".", ","));

            if (!form.checkBox2.Checked)
            {
                double percentage = Math.Pow(Methods.percentToNum(Double.Parse(form.textBox13.Text.Replace(".", ",").Replace("%", ""))), 1.0 / 12);
                for (int i = 0; i < numberOfMonths; i++) {
                    if (i % 12 == 0) {
                        result += withdrawal;
                    }
                    result *= percentage;
                    text += result + (i == numberOfMonths - 1 ? "" : "\n");
                }
            }
            else {
                int firstIndex = form.comboBox3.SelectedIndex;
                int secondIndex = 0;
                for (int i = 0; i < numberOfMonths; i++)
                {
                    if (i % 12 == 0)
                    {
                        result += withdrawal;
                    }
                    result *= Methods.percentToNum(Double.Parse(Storage.array[firstIndex, secondIndex]));
                    text += result + (i == numberOfMonths - 1 ? "" : "\n");

                    secondIndex++;
                    if (secondIndex == 12)
                    {
                        secondIndex = 0;
                        firstIndex++;
                    }
                }
            }

            return result;
        }

        public static double getFirst() {
            double result = Double.Parse(form.textBox11.Text.Replace(".", ","));
            double monthlyDeposit = Double.Parse(form.textBox12.Text.Replace(".", ","));
            if (!form.checkBox2.Checked)
            {
                double percentage = Math.Pow(Methods.percentToNum(Double.Parse(form.textBox13.Text.Replace(".", ",").Replace("%", ""))), 1.0 / 12);
                for (int i = 0; i < numberOfMonths; i++) {
                    result = (result + monthlyDeposit) * percentage;
                    text += result + "\n";
                }
            }
            else {
                int firstIndex = form.comboBox2.SelectedIndex;
                int secondIndex = form.comboBox1.SelectedIndex;

                for (int i = 0; i < numberOfMonths; i++)
                {
                    result = (result + monthlyDeposit) * Methods.percentToNum(Double.Parse(Storage.array[firstIndex, secondIndex]));
                    text += result + "\n";
                    secondIndex++;
                    if (secondIndex == 12)
                    {
                        secondIndex = 0;
                        firstIndex++;
                    }
                }
            }

            return result;
        }

        public static int getLastMonth(int index) {
            int month = 11;

            for (int i = 11; i >= 0; i--) {
                if (!string.IsNullOrEmpty(Storage.array[index, i])) {
                    month = i;
                    goto after_loop;
                }
            }
            after_loop:

            return month;
        }
    }
}
