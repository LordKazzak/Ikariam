using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Ikariam
{
    public partial class Okno : Form
    {
        bool test; //če je rast sploh dovolj visoka, da se mesto zapolni

        public Okno()
        {
            InitializeComponent();
            dobiPodatke();
            comboBoxVladavina.SelectedIndex = Int32.Parse(dobiPodatke()[0]); //prvi vnos je vladavina
            textBoxRaziskave.Text = dobiPodatke()[1]; //drugi vnos je bonus skozi raziskave
            textBoxRaziskavePrestolnica.Text = dobiPodatke()[2];
            this.AcceptButton = buttonIzracunaj;
        }

        void spremeniPodatke(int index, string sprememba) //za shranjevanje podatkov v data.ini 
        {
            string[] podatki = File.ReadAllLines("data.ini");
            StreamWriter datoteka = new StreamWriter("data.ini");
            podatki[index] = sprememba; //na mesto index shrani spremenjen podatek
            for (int i = 0; i < podatki.Length; i++ )
                datoteka.WriteLine(podatki[i]);
            datoteka.Close();
        }

        string[] dobiPodatke() //za branje podatkov iz data.ini
        {
            StreamReader datoteka = new StreamReader("data.ini");
            string[] podatki = File.ReadAllLines("data.ini");
            datoteka.Close();
            return podatki;
        }

        int dobiKorupcijo(string vladavina) //izračuna bonus/minus k rasti od vladavine; drugi argument je za primere kjer nastane korupcija
        {
            int zadovoljstvo = 0;
            switch (vladavina)
            {
                case "Aristokracija":
                    if (!checkBoxPrestolnica.Checked)
                        textBoxKorupcija.Text = "3";
                    break;
                case "Demokracija":
                    zadovoljstvo = 75;
                    textBoxKorupcija.Text = "0";
                    break;
                case "Diktatorstvo":
                    zadovoljstvo = -75;
                    textBoxKorupcija.Text = "0";
                    break;
                case "Oligarhija":
                    textBoxKorupcija.Text = "3";
                    break;
                case "Teokracija":
                    textBoxKorupcija.Text = "0";
                    try
                    {
                        if (Double.Parse(textBoxStopnjaVere.Text) >= 75)
                            zadovoljstvo += 150;
                        else
                            zadovoljstvo = Int32.Parse(textBoxStopnjaVere.Text) * 2;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                    break;
                default:
                    textBoxKorupcija.Text = "0";
                    break;
            }
            return zadovoljstvo;
        }

        double stProstihMest() //izračuna število prostih mest
        {
            double korupcija = 0;
            test = true; //za testiranje, če bo mesto polno na koncu
            //double osnovniBonus = 196; //osnovni bonus
            double sestevek = 196;
            /*if (comboBoxVladavina.Text == "Aristokracija")
            {
                if (!checkBoxPrestolnica.Checked)
                    korupcija = 3;
            }
            else if (comboBoxVladavina.Text == "Demokracija")
                sestevek += 75;
            else if (comboBoxVladavina.Text == "Diktatorstvo")
                sestevek -= 75;
            else if (comboBoxVladavina.Text == "Oligarhija")
                korupcija = 3;
            else if (comboBoxVladavina.Text == "Teokracija")
            {
                
            }*/

            const int faktor = 50; //faktor s katerim deliš, da dobiš rast
            double rast = 0, rezultat = 0, hise = 0;
            uint raziskave = 0, vino = 0, muzeji = 0, populacija = 0;
            //korupcija += sestevek; //korupcija od vladavine + osnovni bonus + bonus vladavine
            
            try
            {
                if (Int32.Parse(textBoxVseHise.Text) < Int32.Parse(textBoxZasedeneHise.Text))
                    MessageBox.Show("Več zasedenih kot nezasedenih hiš!", "Napaka!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                else
                    hise = UInt32.Parse(textBoxVseHise.Text) - UInt32.Parse(textBoxZasedeneHise.Text); //število prostih hiš
                if(!checkBoxRast.Checked)
                {
                    raziskave = UInt32.Parse(textBoxRaziskave.Text); //bonus skozi raziskave
                    if (raziskave.ToString() != dobiPodatke()[1]) //če ni enako vnosu v datoteki
                        spremeniPodatke(1, raziskave.ToString());
                    vino = UInt32.Parse(textBoxKrcma.Text) + UInt32.Parse(textBoxStrezba.Text); //bonus z vinom
                    muzeji = UInt32.Parse(textBoxMuzeji.Text) + UInt32.Parse(textBoxSporazumi.Text); //bonus skozi kulturo
                    populacija = UInt32.Parse(textBoxZasedeneHise.Text); //populacija mesta (odštet)
                    if (checkBoxPrestolnica.Checked) //če je prestolnica ima bonus
                    {
                        if (textBoxRaziskavePrestolnica.Text != dobiPodatke()[2]) //če raziskave v prestolnici niso enake vnosu v datoteki
                            spremeniPodatke(2, textBoxRaziskavePrestolnica.Text);
                        sestevek = sestevek + Int32.Parse(textBoxRaziskavePrestolnica.Text);
                    }
                    //korupcija += raziskave + vino + muzeji;
                    //
                    sestevek = sestevek + raziskave + vino + muzeji - populacija;
                    sestevek += dobiKorupcijo(comboBoxVladavina.Text);
                    korupcija = Int32.Parse(textBoxKorupcija.Text);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Neveljaven vnos (dovoljena so le števila).", "Napaka!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            //MessageBox.Show(korupcija.ToString());
            
            //MessageBox.Show(sestevek.ToString());
            korupcija /= 100;
            sestevek *= 1 - korupcija;

            if (!checkBoxRast.Checked)
            {
                rast = sestevek / faktor;
                //MessageBox.Show(sestevek.ToString());
                //MessageBox.Show(sestevek.ToString());
                //MessageBox.Show("!" + rast.ToString());
                textBoxRast.Text = rast.ToString();
            }
            else //če je rast vnešena
            {
                rast = Double.Parse(textBoxRast.Text);
                sestevek = rast * faktor;
                //MessageBox.Show("=" + rast.ToString());
            }
            while (hise > 0) //za vsako uro
            {
                //if(!checkBoxRast.Checked)
                {
                    sestevek = sestevek - rast; //nov seštevek je seštevek - rast (populacija se poveča)
                    rast = sestevek / faktor; //nova rast je nov seštevek / faktor rasti
                }
                /*else
                {
                    //double spremembaPopulacije = Double.Parse(textBoxVseHise.Text) - Double.Parse(textBoxZasedeneHise.Text) - hise;
                    //rast = rast - spremembaPopulacije;
                }*/
                hise = hise - rast; //novo število domov je število prostih - rast
                if (hise < 0)
                    rezultat = rezultat + (hise + rast) / rast; //za preostali delež
                else
                    rezultat++;
                
                if (rast <= 0)
                {
                    test = false;
                    rezultat = Double.Parse(textBoxVseHise.Text) - hise; //koliko meščanov še manjka
                    //MessageBox.Show("Hiše: " + (Int32.Parse(vseHise.Text) - hise).ToString() + "/" + vseHise.Text + "\nSeštevek: " + sestevek + "\nRast: " + rast.ToString() + "\nŠt. meščanov: " + rezultat.ToString());
                    return rezultat;
                }
                //MessageBox.Show("Hiše: " + (Int32.Parse(vseHise.Text) - hise).ToString() + "/" + vseHise.Text + "\nSeštevek: " + sestevek + "\nRast: " + rast.ToString() + "\nPretekel čas: " + rezultat.ToString());
            }
            
            return rezultat; //vrne število prostih mest
        }

        private void izracunaj_Click(object sender, EventArgs e)
        {
            {
                double tmp = stProstihMest();
                if (!test)
                {
                    double populacija = tmp % 1; //dobi decimalni del
                    populacija = tmp - populacija; //dobi celoštevilčni del populacije
                    textBoxPolnoCez.Text = "";
                    MessageBox.Show("Pri sedanjih pogojih mesto ne bo nikoli polno. Rast se bo ustavila pri " + populacija + " (+-2) populacije.", "Napaka!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    double ure = tmp;
                    double minute = ure % 1; //ostanek
                    ure = ure - minute; //ostane samo celi del
                    minute = Math.Round(minute * 100 * 6 / 10);
                    textBoxPolnoCez.Text = ure.ToString() + " ur " + minute.ToString() + " min";
                }
            }
        }

        private void vladavinaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            spremeniPodatke(0, comboBoxVladavina.SelectedIndex.ToString()); //prvi vnos je vladavina
            if (comboBoxVladavina.Text == "Teokracija")
            {
                textBoxStopnjaVere.ReadOnly = false;
                textBoxStopnjaVere.TabStop = true;
                textBoxStopnjaVere.BackColor = SystemColors.Window;
                textBoxStopnjaVere.Text = "0";
            }
            else
            {
                textBoxStopnjaVere.ReadOnly = true;
                textBoxStopnjaVere.TabStop = false;
                textBoxStopnjaVere.BackColor = SystemColors.Control;
                textBoxStopnjaVere.Text = "";
            }
        }

        private void vladavinaComboBox_KeyPress(object sender, KeyPressEventArgs e) //za readonly
        {
            e.KeyChar = (char)0;
        }

        private void checkBoxRast_CheckedChanged(object sender, EventArgs e) //omogoči/onemogoči ustrezne textbox glede na to, ali je za računanje uporabljena rast ali ne
        {
            if (checkBoxRast.Checked)
            {
                textBoxRast.Enabled = true;
                textBoxRast.BackColor = Color.White;
                textBoxKorupcija.Enabled = textBoxKrcma.Enabled = textBoxMuzeji.Enabled = textBoxRaziskave.Enabled = textBoxRaziskavePrestolnica.Enabled = textBoxSporazumi.Enabled = textBoxStrezba.Enabled = textBoxStopnjaVere.Enabled = false;
                textBoxKorupcija.BackColor = textBoxKrcma.BackColor = textBoxMuzeji.BackColor = textBoxRaziskave.BackColor = textBoxRaziskavePrestolnica.BackColor = textBoxStrezba.BackColor = textBoxStopnjaVere.BackColor = SystemColors.Control;

                comboBoxVladavina.Enabled = false;
                comboBoxVladavina.BackColor = SystemColors.Control;
                checkBoxPrestolnica.Enabled = false;
                checkBoxPrestolnica.Checked = false;
            }
            else
            {
                textBoxRast.Enabled = false;
                textBoxRast.BackColor = SystemColors.Control;
                textBoxKorupcija.Enabled = textBoxKrcma.Enabled = textBoxMuzeji.Enabled = textBoxRaziskave.Enabled = textBoxRaziskavePrestolnica.Enabled = textBoxSporazumi.Enabled = textBoxStrezba.Enabled = true;
                textBoxKorupcija.BackColor = textBoxKrcma.BackColor = textBoxMuzeji.BackColor = textBoxRaziskave.BackColor = textBoxRaziskavePrestolnica.BackColor = textBoxSporazumi.BackColor = textBoxStrezba.BackColor = SystemColors.Window;
                if(7 == comboBoxVladavina.SelectedIndex)
                {
                    textBoxStopnjaVere.Enabled = true;
                    textBoxStopnjaVere.BackColor = SystemColors.Window;
                }
                comboBoxVladavina.Enabled = true;
                comboBoxVladavina.BackColor = SystemColors.Window;
                checkBoxPrestolnica.Enabled = true;
                textBoxRaziskavePrestolnica.Enabled = false;
            }
        }

        private void checkBoxPrestolnica_CheckedChanged(object sender, EventArgs e) //omogoči/onemogoči bonus rast, ki velja le za prestolnico
        {
            if (checkBoxPrestolnica.Checked && !checkBoxRast.Checked)
            {
                textBoxRaziskavePrestolnica.Enabled = true;
                textBoxRaziskavePrestolnica.BackColor = SystemColors.Window;
            }
            else
            {
                textBoxRaziskavePrestolnica.Enabled = false;
                textBoxRaziskavePrestolnica.BackColor = SystemColors.Control;
            }
        }
    }
}
