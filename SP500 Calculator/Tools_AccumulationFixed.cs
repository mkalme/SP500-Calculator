using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SP500_Calculator
{
    class Tools_AccumulationFixed
    {
        public static Form1 form;
        public static String text = "";

        public static void calculate() {
            if(form.checkIfEnableCalculateButton(new Object[] { form.textBox1, form.textBox2, form.textBox3, form.textBox4 }))
            {
                cal();
            }
        }

        public static void cal() {
            //CLEAR
            text = "";

            double deposit = Double.Parse(form.textBox1.Text);
            double percentage = Methods.percentToNum(Double.Parse(form.textBox2.Text));
            double everyDeposit = Double.Parse(form.textBox3.Text);
            int counter = Int32.Parse(form.textBox4.Text);

            double results = deposit;

            for (int i = 0; i < counter; i++)
            {
                if (form.checkBox1.Checked)
                {
                    results = (results * percentage) + everyDeposit;
                }
                else
                {
                    results = (results + everyDeposit) * percentage;
                }

                text += results + (i == counter - 1 ? "" : "\n");
            }

            form.textBox5.Text = Methods.split(results);
        }
    }
}
