using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IkariamZid
{
    public partial class About : Form
    {
        public About()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(IkariamZid.Properties.Settings.Default.Language);
            InitializeComponent();
        }
    }
}
