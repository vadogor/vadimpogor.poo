using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Excel
{
    public class CalcFunc : Base
    {
        public double Calculate(string func)
        {
            double result = double.NaN;
            try
            {
                if (func[0] != '=')
                    Sheet.Cells[1, 1].Formula = "=" + func;
                else
                    Sheet.Cells[1, 1].Formula = func;

                rng = Sheet.Cells[1, 1];
                result = double.Parse(rng.Value2.ToString());
            }
            catch
            {
                 MessageBox.Show("Null reference");
            }
            return result;
        } 
    }
}
