using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TextEditor
{
    public partial class Form1 : Form
    {
        string filename, path, spath;


        public Form1()
        {
            InitializeComponent();
            path = "";

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = null;
            filename = null;
            path = "";
            this.Text = "New file*";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files (*.txt) | *.txt";
            if (ofd.ShowDialog() == DialogResult.Cancel)
                return;
            filename = ofd.SafeFileName;
            path = ofd.FileName;
            richTextBox1.Text = File.ReadAllText(path);
            this.Text = filename;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (path != "")
                File.WriteAllText(path, richTextBox1.Text);
            else
                saveAsToolStripMenuItem_Click(null, null);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Text files (*.txt) | *.txt";
            if (sfd.ShowDialog() == DialogResult.Cancel)
                return;
            spath = sfd.FileName;
            File.WriteAllText(spath, richTextBox1.Text);
        }

        private void BoldToolButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont.Bold)
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
            else
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
        }

        private void ItalicToolButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont.Italic)
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
            else
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Italic);
        }

        private void UnderlineToolButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont.Underline)
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
            else
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Underline);
        }

        private void LeftAllign_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void CenterAllign_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void RightAllign_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }
        //TODO:: Justify align
        private void ColorToolButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            else
                richTextBox1.SelectionColor = colorDialog1.Color;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = Clipboard.GetText();
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(MousePosition, ToolStripDropDownDirection.Right);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(richTextBox1.SelectedText != "")
                Clipboard.SetText(richTextBox1.SelectedText);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.SelectedText); 
            richTextBox1.SelectedText = "";
        }

        //TODO: Add context menu, to be showed
    }
}
