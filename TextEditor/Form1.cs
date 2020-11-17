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
        string path = "";
        string filter = "Text files (*.txt) |*.txt| Rich text files (*.rtf) |*.rtf";


        public Form1()
        {
            InitializeComponent();

        }

        #region File

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = null;
            path = "";
            this.Text = "New file*";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = filter;

            if (ofd.ShowDialog() == DialogResult.Cancel)
                return;

            path = ofd.FileName;
            if (path.IndexOf(".txt") == -1)
                richTextBox1.LoadFile(path, RichTextBoxStreamType.RichText);
            else
                richTextBox1.Text = File.ReadAllText(path);
            this.Text = ofd.SafeFileName;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (path != "")
            {
                if (path.IndexOf(".txt") == -1)
                    richTextBox1.SaveFile(path, RichTextBoxStreamType.RichText);
                else
                    File.WriteAllText(path, richTextBox1.Text);
            } 
            else
                saveAsToolStripMenuItem_Click(null, null);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = filter;

            if (sfd.ShowDialog() == DialogResult.Cancel)
                return;

            path = sfd.FileName;
            if (path.IndexOf(".txt") == -1)
                richTextBox1.SaveFile(path, RichTextBoxStreamType.RichText);
            else
                File.WriteAllText(path, richTextBox1.Text);
            this.Text = sfd.FileName.Substring(sfd.FileName.LastIndexOf('\\') + 1);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        #endregion

        #region Edit

        #region BUIC

        private void BoldToolButton_Click(object sender, EventArgs e)
        {

            if (richTextBox1.SelectionFont.Bold)
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, richTextBox1.SelectionFont.Style & ~FontStyle.Bold);
            else
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, richTextBox1.SelectionFont.Style | FontStyle.Bold);
        }

        private void ItalicToolButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont.Italic)
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, richTextBox1.SelectionFont.Style & ~FontStyle.Italic);
            else
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, richTextBox1.SelectionFont.Style | FontStyle.Italic);
        }

        private void UnderlineToolButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont.Underline)
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, richTextBox1.SelectionFont.Style & ~FontStyle.Underline);
            else
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, richTextBox1.SelectionFont.Style | FontStyle.Underline);
        }
        private void ColorToolButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            else
                richTextBox1.SelectionColor = colorDialog1.Color;
        }

        #endregion

        #region Align

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

        #endregion

        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(FRForm findForm = new FRForm())
            {
                findForm.ShowDialog();
            }
        }

        #endregion

        #region CopyCutPaste(Context)

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != "")
                Clipboard.SetText(richTextBox1.SelectedText);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != "")
            {
                Clipboard.SetText(richTextBox1.SelectedText);
                richTextBox1.SelectedText = "";
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = Clipboard.GetText();
        }

        #endregion
        //TODO: Find and replace
    }
}
