using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        TextBox textBox1 = new TextBox();
        TextBox textBox3 = new TextBox();
        Label label1 = new Label();
        Label label2 = new Label();
        Button[] button = new Button[24];
        Button[] buttonM = new Button[5];
        Calculate c = new Calculate();
        bool isOperation, isEqual = false;
        double secondV = 0, memory;

        private void MainText()
        {
            textBox1.Dispose();
            textBox1 = new TextBox();
            textBox1.Location = new Point(1, 75);
            textBox1.Size = new Size(243, 50);
            textBox1.Font = new Font("Arial", 20);
            textBox1.Text = "0";
            this.Controls.Add(textBox1);
        }
        private void SecondaryText()
        {
            label2.Dispose();
            label2 = new Label();
            label2.Location = new Point(1, 55);
            label2.Size = new Size(243, 20);
            label2.Font = new Font("Arial", 10);
            this.Controls.Add(label2);
        }
        private void MemoryText()
        {
            textBox3.Dispose();
            textBox3 = new TextBox();
            textBox3.Location = new Point(1, 15);
            textBox3.Size = new Size(243, 50);
            textBox3.Font = new Font("Arial", 10);
            textBox3.ReadOnly = true;
            this.Controls.Add(textBox3);
        }

        public Form1()
        {
            InitializeComponent();

            label1.Text = "Memory";
            label1.Size = new Size(100, 14);
            label1.Location = new Point(1, 1);
            this.Controls.Add(label1);

            string[] characters = { "%", "1/x", "x*x", "sqrt", "C", "CE", "<-", "/", "7", "8", "9", "*", "4", "5", "6", "-", "1", "2", "3", "+", "+/-", "0", ".", "=" };
            for (int i = 0; i < 24; i++)
            {
                int j = i / 4;
                int ii = i % 4;

                button[i] = new Button();
                button[i].Location = new Point(61 * ii + 1, 167 + 51 * j);
                button[i].Size = new Size(60, 50);
                button[i].Text = characters[i];
                button[i].UseVisualStyleBackColor = true;
                this.Controls.Add(button[i]);

                if (button[i].Text == "<-")
                {
                    button[i].Click += new EventHandler(BackButton_Click);
                    continue;
                }

                if (button[i].Text == ".")
                {
                    button[i].Click += new EventHandler(DotButton_Click);
                    continue;
                }

                if (button[i].Text == "C" || button[i].Text == "CE")
                {
                    button[i].Click += new EventHandler(ClearButtons_Click);
                    continue;
                }

                if (button[i].Text == "+/-")
                {
                    button[i].Click += new EventHandler(NegateButton_Click);
                    continue;
                }

                if (button[i].Text == "+" || button[i].Text == "-" || button[i].Text == "*" || button[i].Text == "/")
                {
                    button[i].Click += new EventHandler(OperationButtons_Click);
                    continue;
                }

                if (button[i].Text == "x*x" || button[i].Text == "sqrt" || button[i].Text == "1/x" || button[i].Text == "%")
                {
                    button[i].Click += new EventHandler(SingleOperation_Click);
                    continue;
                }

                if (Char.IsDigit(Convert.ToChar(button[i].Text)))
                {

                    button[i].Click += new EventHandler(DigitButtons_Click);
                    continue;
                }

                if (button[i].Text == "=")
                {
                    button[i].Click += new EventHandler(EqualButton_Click);
                    continue;
                }
            }

            string[] charactersMemory = { "MS", "MR", "MC", "M+", "M-" };
            for (int i = 0; i < 5; i++)
            {
                buttonM[i] = new Button();
                buttonM[i].Location = new Point(48 * i + 3, 116);
                buttonM[i].Size = new Size(47, 50);
                buttonM[i].Text = charactersMemory[i];
                buttonM[i].UseVisualStyleBackColor = true;
                this.Controls.Add(buttonM[i]);
                buttonM[i].Click += new EventHandler(MemoryButtons_Click);
            }


        } //Concstructor
        private void ClearButtons_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Text == "CE" || b.Text == "C")
            {
                MainText();
                if (b.Text == "C")
                {
                    label2.Dispose();
                    SecondaryText();
                    c = new Calculate();
                    isEqual = false;
                }
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.TextLength - 1);
                if (textBox1.Text == "" || textBox1.Text == "-")
                    textBox1.Text = "0";
            }
        }

        private void DigitButtons_Click(object sender, EventArgs e)//TODO is equal add
        {
            Button b = (Button)sender;
            if (textBox1.Text == "0" || textBox1.Text == "∞" || isEqual)
            {
                textBox1.Text = b.Text;
                isEqual = false;
            }
            else
            {
                textBox1.Text += b.Text;
            }
        }

        private void DotButton_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text.IndexOf(",") == -1) && !(textBox1.Text == ""))
                textBox1.Text += ",";
        }

        private void NegateButton_Click(object sender, EventArgs e)
        {
            double number = Convert.ToDouble(textBox1.Text);
            textBox1.Text = Convert.ToString(-number);

        }

        private void OperationButtons_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (isOperation)
            {
                EqualButton_Click(null, null);
                isOperation = true;
                isEqual = false;
            }
            c.Operation = b.Text;
            c.FirstValue = Convert.ToDouble(textBox1.Text);
            SecondaryText();
            MainText();
            label2.Text = c.FirstValue + c.Operation;
            isOperation = true;
        }

        private void SingleOperation_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            double number = Convert.ToDouble(textBox1.Text);
            switch (b.Text)
            {
                case "x*x":
                    textBox1.Text = Convert.ToString(c.Power(number));
                    break;
                case "1/x":
                    if (number != 0)
                        textBox1.Text = Convert.ToString(c.Inverse(number));
                    else
                    {
                        MessageBox.Show("На ноль делить нельзя");
                        MainText();
                        SecondaryText();
                    }
                    break;
                case "%":
                    if (textBox1.Text != "0")
                    {
                        textBox1.Text = Convert.ToString(c.Percent(number));
                    }
                    break;
                case "sqrt":
                    textBox1.Text = Convert.ToString(c.Sqrt(number));
                    break;
            }
        }

        private void EqualButton_Click(object sender, EventArgs e)
        {
            if (isOperation)
                secondV = Convert.ToDouble(textBox1.Text);
            else
            {
                label2.Text = textBox1.Text;
                isEqual = true;
                return;
            }  

            label2.Text = c.FirstValue + c.Operation + Convert.ToString(secondV);
            isEqual = true;
            isOperation = false;

            switch (c.Operation)
            {
                case "+":
                    textBox1.Text = Convert.ToString(c.Plus(secondV));
                    break;
                case "-":
                    textBox1.Text = Convert.ToString(c.Minus(secondV));
                    break;
                case "*":
                    textBox1.Text = Convert.ToString(c.Multiply(secondV));
                    break;
                case "/":
                    
                    if (secondV != 0)
                    {
                        textBox1.Text = Convert.ToString(c.Divide(secondV));
                    }
                    else
                    {
                        MessageBox.Show("На ноль делить нельзя");
                        isEqual = false;
                        MainText();
                        SecondaryText();
                        c = new Calculate();
                    }
                    break;
            }
        }

        private void MemoryButtons_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            switch (b.Text)
            {
                case "MC":
                    memory = 0;
                    MemoryText();
                    break;
                case "MR":
                    textBox1.Text = Convert.ToString(memory);
                    break;
                case "MS":
                    memory = Convert.ToDouble(textBox1.Text);
                    textBox3.Text = textBox1.Text;
                    MainText();
                    break;
                case "M+":
                    memory += Convert.ToDouble(textBox1.Text);
                    textBox3.Text = Convert.ToString(memory);
                    MainText();
                    break;
                case "M-":
                    memory -= Convert.ToDouble(textBox1.Text);
                    textBox3.Text = Convert.ToString(memory);
                    MainText();
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MainText();
            SecondaryText();
            MemoryText();
        }
    }
}
