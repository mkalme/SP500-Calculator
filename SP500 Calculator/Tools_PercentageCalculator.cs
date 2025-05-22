using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP500_Calculator
{
    class Tools_PercentageCalculator
    {
        public static Form1 form;

        public static void calculate() {
            if (form.checkIfEnableCalculateButton(new Object[] { form.textBox10, form.textBox9 }))
            {
                calc();
            }
        }

        public static void calc() {
            double percent = Double.Parse(form.textBox10.Text.Replace(".", ",").Replace("%", ""));
            int rootOf = Int32.Parse(form.textBox9.Text);

            form.textBox6.Text = Methods.numToPercent(Math.Pow(Methods.percentToNum(percent), 1.0 / rootOf)).ToString();
        }
    }
}
