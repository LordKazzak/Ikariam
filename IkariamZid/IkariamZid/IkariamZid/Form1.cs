using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Resources;

namespace IkariamZid
{
    public partial class FormIkariamZid : Form
    {
        CultureInfo cul;

        public FormIkariamZid()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(IkariamZid.Properties.Settings.Default.Language);
            InitializeComponent();
            if ("en-GB" == IkariamZid.Properties.Settings.Default.Language)
                englishToolStripMenuItem.Checked = true;
            else
                sloveneToolStripMenuItem.Checked = true;
            cul = new CultureInfo(IkariamZid.Properties.Settings.Default.Language);
            comboBoxOrozje.SelectedIndex = 0;
        }

        string lang(string slo, string en)
        {
            if ("en-GB" == cul.ToString())
                return en;
            else
                return slo;
        }

        void izracunaj()
        {
            int hitPoints = 100;
            if (upDownStopnjaZidu.Value == 0)
            {
                textBoxStEnot.Text = lang("Ni zidu, za napad ni potrebna artilerija.", "The town has no wall. Artillery is not needed.");
                return;
            }
            hitPoints += (int)upDownStopnjaZidu.Value * 50; //osnovni - 50 + ...
            int oklep = (int)upDownStopnjaZidu.Value * 4;
            int napadEnot;

            if (comboBoxOrozje.Text == lang("Oven", "Ram"))
                napadEnot = 80;
            else if (comboBoxOrozje.Text == lang("Katapult","Catapult"))
                napadEnot = 133;
            else
                napadEnot = 270;

            napadEnot += (int)upDownNadgradnja.Value;

            if (napadEnot <= oklep)
            {
                textBoxStEnot.Text = lang("Enote imajo premajhno napadalno moč, da bi poškodovale zid.", "Selected unit does not have enough attack strength to damage the wall.");
                return;
            }

            double stEnot = (double)hitPoints / ((double)napadEnot - (double)oklep);
            int tmp = (int)stEnot;
            double ostanek = stEnot - tmp;
            if (ostanek > 0)
                tmp++;

            string glagol = "";
            switch (tmp)
            {
                case 1:
                    glagol = lang("je potrebna "+ tmp.ToString() + " enota.", tmp.ToString() + " unit is needed");
                    break;

                case 2:
                    glagol = lang("sta potrebni " + tmp.ToString() + " enoti.", tmp.ToString() + " units are needed");
                    break;

                case 3:
                    glagol = lang("so potrebne " + tmp.ToString() + " enote.", tmp.ToString() + " units are needed");
                    break;

                case 4:
                    glagol = lang("so potrebne " + tmp.ToString() + " enote.", tmp.ToString() + " units are needed");
                    break;

                default:
                    glagol = lang("je potrebnih " + tmp.ToString() + " enot.", tmp.ToString() + " units are needed");
                    break;
            }

            textBoxStEnot.Text = lang("Za preboj zidu v enem krogu " + glagol, glagol + " to breach the wall in one turn.");
        }

        private void buttonIzracunaj_Click(object sender, EventArgs e)
        {
            izracunaj();
        }

        private void comboBoxOrozje_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)0;
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            About wnd = new About();
            wnd.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!englishToolStripMenuItem.Checked)
            {
                englishToolStripMenuItem.Checked = true;
                sloveneToolStripMenuItem.Checked = false;
                cul = CultureInfo.CreateSpecificCulture(Properties.Settings.Default.Language = "en-GB");
                Application.CurrentCulture = cul;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Upgrade();
                Application.Restart();
            }
        }

        private void sloveneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!sloveneToolStripMenuItem.Checked)
            {
                englishToolStripMenuItem.Checked = false;
                sloveneToolStripMenuItem.Checked = true;
                cul = CultureInfo.CreateSpecificCulture(Properties.Settings.Default.Language = "sl-SI");
                Application.CurrentCulture = cul;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Upgrade();
                Application.Restart();
            }
        }
    }
}
