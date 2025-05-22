using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SP500_Calculator
{
    class Accumulation
    {
        public static Form1 form;
        public static int numberOfMonths = 0;
        public static String growthMonth = "";

        public static void calculate()
        {
            if (form.checkIfEnableCalculateButton(new Object[] { form.monthComboBox2, form.yearComboBox2, form.endMonthComboBox2, form.endYearComboBox2, form.originalDepositTextBox, form.monthlyDepositTextBox }))
            {
                calc();
            }
        }

        public static void calc() {
            form.amountTextBox2.Text = Accumulation.calculateAmount();
        }

        public static String calculateAmount(){
            //CLEAR
            growthMonth = "";
            numberOfMonths = 0;

            Methods.getMonth(form.yearComboBox2, form.monthComboBox2, form.endYearComboBox2, form.endMonthComboBox2);

            //GET NUMBER OF MONTHS
            numberOfMonths = Methods.array[4];
            int firstIndex = Methods.array[5];
            int secondIndex = Methods.array[6];

            Double originalDeposit = Double.Parse(form.originalDepositTextBox.Text.Replace(" ", "").Replace(".", ","));
            Double monthlyDeposit = Double.Parse(form.monthlyDepositTextBox.Text.Replace(" ", "").Replace(".", ","));

            Double sum = originalDeposit;

            for (int i = 0; i < numberOfMonths; i++)
            {
                if (form.percentageCheckBox.Checked == false)
                {
                    sum = (sum + monthlyDeposit) * Methods.percentToNum(Double.Parse(Storage.array[firstIndex, secondIndex]));
                }
                else {
                    sum = (sum * Methods.percentToNum(Double.Parse(Storage.array[firstIndex, secondIndex]))) + monthlyDeposit;
                }

                growthMonth += sum + (i == numberOfMonths - 1 ? "" : "\n");

                secondIndex++;
                if (secondIndex == 12)
                {
                    secondIndex = 0;
                    firstIndex++;
                }
            }

            return Methods.split(sum);
        }
    }
}
