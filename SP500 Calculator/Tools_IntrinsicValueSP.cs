using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SP500_Calculator
{
    class Tools_IntrinsicValueSP
    {
        public static Form1 form;
        public static String[] array = new string[2];
        public static int numberOfMonths = 0;

        public static void calculate() {
            if (form.checkIfEnableCalculateButton(new Object[] { form.comboBox8, form.comboBox7, form.comboBox6, form.comboBox5, form.textBox14 }))
            {
                calc();
            }
        }

        public static void calc() {
            //CLEAR
            numberOfMonths = 0;
            array[0] = "";
            array[1] = "";

            Methods.getMonth(form.comboBox7, form.comboBox8, form.comboBox5, form.comboBox6);

            //GET NUMBER OF MONTHS
            numberOfMonths = Methods.array[4];
            int firstIndex = Methods.array[5];
            int secondIndex = Methods.array[6];

            //GET AVERAGE GROWTH
            Double averagePercent = Math.Pow(Methods.percentToNum(Double.Parse(form.textBox14.Text.Replace(".", ",").Replace("%", ""))), 1.0 / 12);
            
            //GET AVERAGE GRAPH
            double averageGraph = 1.0;

            for (int i = 0; i < numberOfMonths; i++) {
                averageGraph *= averagePercent;
                array[0] += averageGraph + (i == numberOfMonths - 1 ? "" : "\n");
            }

            //GET REAL GRAPH
            double realGraph = 1.0;

            firstIndex = form.comboBox7.SelectedIndex;
            secondIndex = form.comboBox8.SelectedIndex;

            for (int i = 0; i < numberOfMonths; i++)
            {
                realGraph *= Methods.percentToNum(Double.Parse(Storage.array[firstIndex, secondIndex]));
                array[1] += realGraph + (i == numberOfMonths - 1 ? "" : "\n");

                secondIndex++;
                if (secondIndex == 12)
                {
                    secondIndex = 0;
                    firstIndex++;
                }
            }
            
            form.richTextBox4.Text = array[0];
            form.richTextBox5.Text = array[1];
        }
    }
}
