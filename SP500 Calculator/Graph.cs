using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP500_Calculator
{
    class Graph
    {
        public static Form1 form;
        public static int numberOfMonths = 0;

        public static void calculate()
        {
            if (form.checkIfEnableCalculateButton(new Object[] { form.monthComboBox3, form.yearComboBox3, form.endMonthComboBox3, form.endYearComboBox3 }))
            {
                calc();
            }
        }

        public static void calc() {
            form.richTextBox1.Text = Graph.getGraph()[1];
            form.richTextBox2.Text = Graph.getGraph()[0];
        }

        public static String[] getGraph()
        {
            //CLEAR
            numberOfMonths = 0;

            Methods.getMonth(form.yearComboBox3, form.monthComboBox3, form.endYearComboBox3, form.endMonthComboBox3);

            //GET NUMBER OF MONTHS
            numberOfMonths = Methods.array[4];
            int firstIndex = Methods.array[5];
            int secondIndex = Methods.array[6];

            Double number = 1.0;
            String[] textArray = new string[2];

            for (int i = 0; i < numberOfMonths; i++)
            {
                number *= Methods.percentToNum(Double.Parse(Storage.array[firstIndex, secondIndex]));
                textArray[0] += number + (i == numberOfMonths - 1 ? "" : "\n");
                textArray[1] += Storage.array[firstIndex, secondIndex] + (i == numberOfMonths - 1 ? "" : "\n");

                secondIndex++;
                if (secondIndex == 12)
                {
                    secondIndex = 0;
                    firstIndex++;
                }
            }

            return textArray;
        }
    }
}
