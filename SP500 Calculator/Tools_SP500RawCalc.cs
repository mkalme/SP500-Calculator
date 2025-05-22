using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SP500_Calculator
{
    class Tools_SP500RawCalc
    {
        public static Form1 form;

        public static void calculate() {
            if (form.checkIfEnableCalculateButton(new Object[] { form.textBox8}))
            {
                calc();
            }
        }

        public static void calc() {
            String text = File.ReadAllText(form.textBox8.Text).Replace("%", "").Replace(".", ",");
            double[] array = Array.ConvertAll(text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries), Double.Parse);

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Methods.percentToNum(array[i]);
            }

            Double totalNumber = 1;
            for (int i = 0; i < array.Length; i++)
            {
                totalNumber *= array[i];
            }

            form.textBox7.Text = Math.Round(Methods.numToPercent(totalNumber), 5).ToString() + "%";
        }
    }
}
