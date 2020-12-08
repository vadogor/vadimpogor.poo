using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Excel
{
    public partial class Form1 : Form
    {
        ImportData import = new ImportData();
        Validator validate = new Validator();
        CalcFunc calcFunc = new CalcFunc();
        public Form1()
        {
            InitializeComponent();
            Application.ApplicationExit += new EventHandler(ExitExcel);
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.RestoreDirectory = true;
            ofd.Filter = "TXT files(*.txt)|*.txt|JSON files(*.json)|*.json|XML files(*.xml)|*.xml";

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                Function[] func, validateFunc;

                if (ofd.FileName.IndexOf(".txt") != -1)
                    func = import.ImportTXT(ofd.FileName);
                else if (ofd.FileName.IndexOf(".json") != -1)
                    func = import.ImportJSON(ofd.FileName);
                else
                    func = import.ImportXML(ofd.FileName);

                validateFunc = validate.Validate(func);

                for(int i = 0; i < func.Length; i++)
                    if(func[i].valid)
                        listBox1.Items.Add(validateFunc[i].function);
            }
        }

        private void CalcButton_Click(object sender, EventArgs e)
        {
                label2.Text = Convert.ToString(calcFunc.Calculate(Convert.ToString(listBox1.SelectedItem)));
        }
        
        private void ExitExcel(object sender, EventArgs e)
        {
            validate.Quit();
            calcFunc.Quit();
        }
            
    }
}
