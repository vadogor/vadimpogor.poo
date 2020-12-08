using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Excel
{
    public class Base
    {
        public _Application App;
        public _Workbook Book;
        public Workbooks Books;
        public _Worksheet Sheet;
        public Range rng;

        public Base()
        {
            App = new Application();
            App.Visible = true;
            App.DisplayAlerts = false;
            Books = App.Workbooks;
            Book = Books.Add(Missing.Value);
            Sheet = Book.ActiveSheet;
        }

        public void Quit()
        {
            Book.Close();
            App.Quit();
            Marshal.ReleaseComObject(Book);
            Marshal.ReleaseComObject(Books);
            Book = null;
            Books = null;
            rng = null;
            App = null;
            Sheet = null;
            GC.Collect();
        }
    }
}
