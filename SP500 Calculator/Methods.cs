using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace SP500_Calculator
{
    class Methods
    {
        public static int[] array = new int[7];

        public static void getMonth(ComboBox yearComboBox, ComboBox monthComboBox, ComboBox endYearComboBox, ComboBox endMonthComboBox) {
            array[0] = Int32.Parse(yearComboBox.Text);
            array[1] = monthComboBox.SelectedIndex; ;

            array[2] = Int32.Parse(endYearComboBox.Text);
            array[3] = endMonthComboBox.SelectedIndex;

            //GET NUMBER OF MONTHS
            DateTime start = new DateTime(array[0], array[1] + 1, 1);
            DateTime end = new DateTime(array[2], array[3] + 1, 1);
            array[4] = 1 + ((end.Year - start.Year) * 12) + end.Month - start.Month;

            array[5] = yearComboBox.SelectedIndex;
            array[6] = monthComboBox.SelectedIndex;
        }

        public static Double floorNumber(Double number, int decimalPlaces)
        {
            return Math.Floor(number * Math.Pow(10, decimalPlaces)) / Math.Pow(10, decimalPlaces);
        }

        public static Double numToPercent(Double number)
        {
            return (number - 1) * 100;
        }

        public static Double percentToNum(Double percent)
        {
            return (percent / 100) + 1;
        }

        public static String split(double am)
        {
            var f = new NumberFormatInfo { NumberGroupSeparator = " " };

            var s = am.ToString("n", f); // 2 000 000.00

            return s;
        }
    }
}
