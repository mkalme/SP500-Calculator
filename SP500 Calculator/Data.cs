using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SP500_Calculator
{
    class Data
    {
        public static Form1 form; 
        public static int numberOfMonths = 0;

        public static String[] listArray = new String[3];

        public static void calculate()
        {
            if (form.checkIfEnableCalculateButton(new Object[] { form.monthComboBox1, form.yearComboBox1, form.endMonthComboBox1, form.endYearComboBox1 }))
            {
                calc();
            }
        }

        public static void calc() {
            Double percentage = getPercent();
            Double annualPercentage = getAnnualPercent();

            form.overallTextBox.Text = Methods.floorNumber(Methods.numToPercent(percentage), 3).ToString() + "%";
            form.annualTextBox.Text = Methods.floorNumber(Methods.numToPercent(annualPercentage), 3) + "%";
            form.averageTextBox.Text = Methods.floorNumber(Methods.numToPercent(Math.Pow(percentage, 1.0 / numberOfMonths)), 3).ToString() + "%";
        }

        public static Double getAnnualPercent()
        {
            int firstIndex = form.yearComboBox1.SelectedIndex;
            Double annualPercentage = 1.0;
            for (int i = 0; i < Int32.Parse(form.endYearComboBox1.Text) - Int32.Parse(form.yearComboBox1.Text) + 1; i++)
            {
                for (int b = 0; b < 12; b++)
                {
                    if (!string.IsNullOrEmpty(Storage.array[firstIndex, b]))
                    {
                        annualPercentage *= Methods.percentToNum(Double.Parse(Storage.array[firstIndex, b]));
                        listArray[1] += Methods.numToPercent(Math.Pow(annualPercentage, 1.0 / ((i * 12) + b + 1))) + "\n";
                    }
                }
                listArray[2] += Methods.numToPercent(Math.Pow(annualPercentage, 1.0 / (i + 1))) + "\n";
                firstIndex++;
            }
            return Math.Pow(annualPercentage, 1.0 / (Int32.Parse(form.endYearComboBox1.Text) - Int32.Parse(form.yearComboBox1.Text) + 1));
        }

        public static Double getPercent()
        {
            //CLEAR
            numberOfMonths = 0;
            listArray[0] = "";
            listArray[1] = "";
            listArray[2] = "";

            Methods.getMonth(form.yearComboBox1, form.monthComboBox1, form.endYearComboBox1, form.endMonthComboBox1);

            //GET NUMBER OF MONTHS
            numberOfMonths = Methods.array[4];
            int firstIndex = Methods.array[5];
            int secondIndex = Methods.array[6];

            Double percentage = 1.0;

            for (int i = 0; i < numberOfMonths; i++)
            {
                percentage *= Methods.percentToNum(Double.Parse(Storage.array[firstIndex, secondIndex]));
                listArray[0] += Methods.numToPercent(percentage) + "\n";
                secondIndex++;
                if (secondIndex == 12)
                {
                    secondIndex = 0;
                    firstIndex++;
                }
            }

            return percentage;
        }
    }
}
