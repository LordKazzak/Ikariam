using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace IkariamTrain
{
    public partial class FormTraining : Form
    {
        int[] nether = null;
        List<string> tmpUnits;
        List<int> values;
        int numOfTownsBarracks, numOfTownsShips;
        int mainBarracks;
        int barracks;
        int mainShip;
        int ship;
        
        /*
         * 
         * Form
         * 
         */

        public FormTraining()
        {
            InitializeComponent();
            try
            {
                string[] data = System.IO.File.ReadAllLines("razmerja.ini");
                IEnumerable<TextBox> boxes = getBoxes<TextBox>(this);
                int cnt = 1;
                foreach (TextBox box in boxes)
                {
                    if (box.Name.Length >= 4 && "per" == box.Name.Remove(3))
                    {
                        box.Text = data[data.Length - cnt];
                        cnt++;
                    }
                }

                data = System.IO.File.ReadAllLines("lvls.ini");
                for (int i = 0; i < data.Count(); i++)
                {
                    try //remove comments, if any
                    {
                        data[i] = data[i].Remove(data[i].IndexOf(" "));
                    }
                    catch { }
                }
                Int32.TryParse(data[0], out numOfTownsBarracks);
                Int32.TryParse(data[1], out numOfTownsShips);
                Int32.TryParse(data[2], out mainBarracks);
                Int32.TryParse(data[3], out barracks);
                Int32.TryParse(data[4], out mainShip);
                Int32.TryParse(data[5], out ship);
                textBoxDiscountLadje.Text = data[6];
                textBoxDiscountKopenske.Text = data[7];
            }
            catch(Exception e)
            {
                MessageBox.Show("Configuration files \"lvls.ini\" or \"razmerja.ini\" not found or corrupt. Training times might not be shown properly.\nTo fix this issue add a file named \"lvls.ini\" with 8"+
                                "numbers in 8 lines. First number should depict the number of towns in which units will be trained, the second number of towns in which ships will be trained, third and fourth"+
                                "the maximum lvl of barracks and the average lvl of other barracks. Next 2 numbers should be the same, except that they are for shipyards. The last numbers define training"+
                                "discount for ships and infantry.\nIf only \"razmerja.ini\" is missing you may ignore the error.\n\nError message:\n" + e.Message);
                numOfTownsBarracks = 1;
                numOfTownsShips = 1;
                mainBarracks = 1;
                barracks = 1;
                mainShip = 1;
                ship = 1;
            }

            values = new List<int>();
            tmpUnits = new List<string>();
        }

        private void textBoxNumTurns_TextChanged(object sender, EventArgs e)
        {
            int r = 0;
            try
            {
                r = Int32.Parse(textBoxNumTurns.Text);
                if (r > 100)
                {
                    r = 100;
                    textBoxNumTurns.Text = r.ToString();
                }
            }
            catch { return; }

            textBoxGenerali.Text = ""; //da se ne sproži posodobitev vsakič, ko spremeni vrednost

            numericUpDownMetalciPlamena.Value = Math.Round((decimal)((84 + r * 42) * Double.Parse(perMP.Text)));
            numericUpDownParniRami.Value = Math.Round((decimal)((35 + r * 18) * Double.Parse(perPR.Text)));
            numericUpDownRami.Value = 36 + r * 18;
            numericUpDownBaliste.Value = 0;
            numericUpDownLadjeKatapulti.Value = 0;
            numericUpDownLadjeMortarji.Value = Math.Round((decimal)(42 + (r - 5) * 8.4));
            numericUpDownPodmornice.Value = Math.Round((decimal)((25 + r * 4) * Double.Parse(perPod.Text)));
            numericUpDownRaketne.Value = Math.Round((decimal)((15 + r * 4) * Double.Parse(perRak.Text)));
            numericUpDownNosilciBalonov.Value = 6 + r * 2;
            numericUpDownHitriParniki.Value = 30 + (r - 5) * 6;
            numericUpDownServisniDoki.Value = 0;
            numericUpDownSulicarji.Value = Math.Round((decimal)((350 + r * 150) * Double.Parse(perSul.Text)));
            numericUpDownParniVelikani.Value = Math.Round((decimal)((112 + r * 50) * Double.Parse(perPV.Text)));
            numericUpDownMetalciKopja.Value = 0;
            numericUpDownMecevalci.Value = 240 + r * 120;
            numericUpDownPracarji.Value = 0;
            numericUpDownLokostrelci.Value = 0;
            numericUpDownMusketirji.Value = 84 + (r - 3) * 28;
            numericUpDownOvni.Value = Math.Round((decimal)(r * 4 * Double.Parse(perOvn.Text)));
            numericUpDownKatapulti.Value = Math.Round((decimal)(r * 6 * Double.Parse(perKat.Text)));
            numericUpDownMortarji.Value = Math.Round((decimal)((30 + r * 8) * Double.Parse(perMor.Text)));
            numericUpDownBombniki.Value = 30 + r * 24;
            numericUpDownGirokopterji.Value = 60 + (r - 4) * 15;
            numericUpDownKuharji.Value = 0;
            numericUpDownZdravniki.Value = 0;

            zlato_generali(checkBoxLadje.Checked, checkBoxKopenske.Checked);
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if("" != textBoxGenerali.Text)
                zlato_generali(checkBoxLadje.Checked, checkBoxKopenske.Checked);
        }

        private void checkBoxLadje_CheckedChanged(object sender, EventArgs e)
        {
            IEnumerable<GroupBox> boxes = groupBoxLadje.Controls.OfType<GroupBox>();
            bool tmpVar;
            if (checkBoxLadje.Checked)
                tmpVar = true;
            else
                tmpVar = false;

            foreach (GroupBox box in boxes)
            {
                var list = box.Controls.OfType<NumericUpDown>();
                foreach (NumericUpDown control in list)
                {
                    //control.Value = 0;
                    control.Enabled = tmpVar;
                }
            }

            zlato_generali(checkBoxLadje.Checked, checkBoxKopenske.Checked);
        }

        private void checkBoxKopenske_CheckedChanged(object sender, EventArgs e)
        {
            IEnumerable<GroupBox> boxes = groupBoxKopenske.Controls.OfType<GroupBox>();
            bool tmpVar;
            if (checkBoxKopenske.Checked)
                tmpVar = true;
            else
                tmpVar = false;

            foreach (GroupBox box in boxes)
            {
                var list = box.Controls.OfType<NumericUpDown>();
                foreach (NumericUpDown control in list)
                {
                    //control.Value = 0;
                    control.Enabled = tmpVar;
                }
            }

            zlato_generali(checkBoxLadje.Checked, checkBoxKopenske.Checked);
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            List<string> data = new List<string>();

            /*data.Add("Skupaj");

            data.AddRange(full(false));*/

            data.Add("----------Main1----------");

            data.AddRange(zdruzi(true));

            data.Add("--------Other5i|6s--------"); //5 infantry, 6 ships

            data.AddRange(zdruzi(false));

            data.Add("----------Nether1----------");

            if (checkBoxLadje.Checked)
            {
                data.Add("Rami\t\t" + (nether[0] + nether[2]));
                data.Add("Podmornice\t" + nether[1]);
                //values.Clear();
                values.Add(nether[1]);
                values.Add(nether[0] + nether[2]);
                List<TimeSpan> lts = new List<TimeSpan>(casTreninga(values, false, true));
                textBoxTreningNether.Text = lts[0].Days + "D " + lts[0].Hours + "H " + lts[0].Minutes + "M " + lts[0].Seconds + "S";
                data.Add(lts[0].Days + "D " + lts[0].Hours + "H " + lts[0].Minutes + "M " + lts[0].Seconds + "S");
                data.Add("\n");
            }
            else
                textBoxTreningNether.Text = "0D 0H 0M 0S";

            nether = null;
            System.IO.File.WriteAllLines("data.txt", data);

            data.Clear();

            IEnumerable<TextBox> boxes = getBoxes<TextBox>(this);

            foreach (TextBox box in boxes)
            {
                if (box.Name.Length >= 4 && "per" == box.Name.Remove(3))
                    data.Add(box.Text);
            }
            System.IO.File.WriteAllLines("razmerja.ini", obrni(data));

            values = new List<int>();
            tmpUnits = new List<string>();
        }

        /*
         * 
         * End Form
         * 
         */

        /*
         * 
         * Custom
         * 
         */

        private IEnumerable<T> getBoxes<T>(Control control)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => getBoxes<T>(ctrl).OfType<Control>())
                                  .Concat(controls)
                                  .OfType<T>();
        }

        private void zlato_generali(bool ships, bool land)
        {
            double tmpGen, tmpZlato, generali, zlato;
            if (ships)
            {
                tmpGen =
                (double)numericUpDownMetalciPlamena.Value * 6.2 +
                (double)numericUpDownParniRami.Value * 24 +
                (double)numericUpDownRami.Value * 5 +
                (double)numericUpDownLadjeKatapulti.Value * 6.4 +
                (double)numericUpDownBaliste.Value * 6.8 +
                (double)numericUpDownLadjeMortarji.Value * 22.4 +
                (double)numericUpDownRaketne.Value * 28 +
                (double)numericUpDownPodmornice.Value * 20.2 +
                (double)numericUpDownHitriParniki.Value * 6.4 +
                (double)numericUpDownNosilciBalonov.Value * 28 +
                (double)numericUpDownServisniDoki.Value * 16;
                tmpZlato =
                ((double)numericUpDownMetalciPlamena.Value * 25 +
                (double)numericUpDownParniRami.Value * 45 +
                (double)numericUpDownRami.Value * 15 +
                (double)numericUpDownLadjeKatapulti.Value * 35 +
                (double)numericUpDownBaliste.Value * 20 +
                (double)numericUpDownLadjeMortarji.Value * 50 +
                (double)numericUpDownRaketne.Value * 55 +
                (double)numericUpDownPodmornice.Value * 50 +
                (double)numericUpDownHitriParniki.Value * 5 +
                (double)numericUpDownNosilciBalonov.Value * 100 +
                (double)numericUpDownServisniDoki.Value * 100) *
                (1.0 - Double.Parse(textBoxDiscountLadje.Text) / 100);
            }
            else
                tmpGen = tmpZlato = 0;
            generali = tmpGen;
            zlato = tmpZlato;
            if(land)
            {
                tmpGen =
                (double)numericUpDownSulicarji.Value * 1.4 +
                (double)numericUpDownParniVelikani.Value * 6.2 +
                (double)numericUpDownMetalciKopja.Value * 0.6 +
                (double)numericUpDownMecevalci.Value * 1.2 +
                (double)numericUpDownPracarji.Value * 0.4 +
                (double)numericUpDownLokostrelci.Value * 1.1 +
                (double)numericUpDownMusketirji.Value * 4 +
                (double)numericUpDownOvni.Value * 4.4 +
                (double)numericUpDownKatapulti.Value * 11.2 +
                (double)numericUpDownMortarji.Value * 31 +
                (double)numericUpDownGirokopterji.Value * 2.5 +
                (double)numericUpDownBombniki.Value * 5.8 +
                (double)numericUpDownKuharji.Value * 4 +
                (double)numericUpDownZdravniki.Value * 10;
                tmpZlato =
                ((double)numericUpDownSulicarji.Value * 3 +
                (double)numericUpDownParniVelikani.Value * 12 +
                (double)numericUpDownMetalciKopja.Value +
                (double)numericUpDownMecevalci.Value * 4 +
                (double)numericUpDownPracarji.Value * 2 +
                (double)numericUpDownLokostrelci.Value * 4 +
                (double)numericUpDownMusketirji.Value * 3 +
                (double)numericUpDownOvni.Value * 15 +
                (double)numericUpDownKatapulti.Value * 25 +
                (double)numericUpDownMortarji.Value * 30 +
                (double)numericUpDownGirokopterji.Value * 15 +
                (double)numericUpDownBombniki.Value * 45 +
                (double)numericUpDownKuharji.Value * 10 +
                (double)numericUpDownZdravniki.Value * 20) *
                (1.0 - Double.Parse(textBoxDiscountKopenske.Text) / 100);
            }
            else
                tmpGen = tmpZlato = 0;

            generali += tmpGen;
            zlato += tmpZlato;

            textBoxGenerali.Text = generali.ToString();  
            textBoxZlato.Text = zlato.ToString();
        }

        private List<TimeSpan> casTreninga(List<int> values, bool mainTown, bool crystal)
        {
            int kopenske, ladje;

            if (mainTown)
            {
                kopenske = mainBarracks;
                ladje = mainShip;
            }
            else
            {
                kopenske = barracks;
                ladje = ship;
            }

            CasTreninga t = new CasTreninga();
            double tmpSecL = 0.0, tmpSecK = 0.0;
            bool soLadje = false; //kadar se ne nastavi vmes je vseeno
            int tp=0;
            if (!checkBoxLadje.Checked)
                tp = 11;

            List<string> tmp = new List<string>();
            for (int i = 0; i < values.Count; i++)
            {
                double timeUnit; //čas treninga za eno enoto
                if (!crystal)
                {
                    if (i < 11+tp && checkBoxLadje.Checked) //ladje
                    {
                        timeUnit = Math.Pow(0.95, ladje - t.trainData[i, 0]) * t.trainData[i, 1];
                        soLadje = true;
                        /*Debug.WriteLine("Ladje");
                        Debug.WriteLine("i: " + i);
                        Debug.WriteLine("timeUnit: " + timeUnit);
                        Debug.WriteLine("\n");*/
                    }
                    else if (i >= 11-tp && checkBoxKopenske.Checked) //kopenske
                    {
                        timeUnit = Math.Pow(0.95, kopenske - t.trainData[i+tp, 0]) * t.trainData[i+tp, 1];
                        soLadje = false;
                        /*Debug.WriteLine("Enote");
                        Debug.WriteLine("i: " + i);
                        Debug.WriteLine("timeUnit: " + timeUnit);
                        Debug.WriteLine("\n");*/
                    }
                    else
                        timeUnit = 0;
                }
                else if (checkBoxLadje.Checked)
                {
                    timeUnit = Math.Pow(0.95, ladje - t.netherData[i, 0]) * t.netherData[i, 1];
                    soLadje = true;
                    /*Debug.WriteLine("Nether");
                    Debug.WriteLine("i: " + i);
                    Debug.WriteLine("timeUnit: " + timeUnit);
                    Debug.WriteLine("\n");*/
                }
                else
                    timeUnit = 0;

                if (soLadje)
                    tmpSecL += Math.Round(timeUnit * values[i] * 60); //prišteje čas treninga enot k skupnemu treningu
                else
                    tmpSecK += Math.Round(timeUnit * values[i] * 60); //prišteje čas treninga enot k skupnemu treningu

                /*string tmpish;
                if (tmpUnits[i].Length < 8)
                    tmpish = "\t\t";
                else
                    tmpish = "\t";
                tmp.Add(tmpUnits[i].ToString() + tmpish + (timeUnit * values[i]).ToString());*/
            }
            //System.IO.File.AppendAllLines("tmp.txt", tmp);


            List<TimeSpan> lts = new List<TimeSpan>();

            lts.Add(TimeSpan.FromSeconds(tmpSecL));
            lts.Add(TimeSpan.FromSeconds(tmpSecK));
            
            values.Clear(); //ko sprocesira ostale iz nekega razloga ne izbriše...
            //MessageBox.Show(values.Count.ToString());
            return lts;
        }

        int naMesto(int value, int numOfTowns, int mod, bool mainTown)
        {
            int fixInt = 0;
            int extra = value % numOfTowns; //ostanek
            if(extra > 0.9 * (value / numOfTowns) && value > numOfTowns+1 && value / numOfTowns + extra >= numOfTowns - 1) //če je preostanek za več kot polovico večji od vrednosti na mesto popravi končno vrednost in če se vrednost da razdeliti
                fixInt = 1; //vzame enote glavnemu mestu in jih razdeli ostalim

            //if(value >= extra)
            value -= extra; //odšteješ ostanek, da je deljivo

            if (mainTown)
            {
                //MessageBox.Show("extra: " + extra + "\nvalue: " + (value / numOfTowns + extra - fixInt * (numOfTowns - 1)));
                return value / numOfTowns + extra - fixInt * (numOfTowns - 1);
            }
            else
            {   
                //MessageBox.Show("extra: " + extra + "\nvalue: " + (value / numOfTowns + fixInt));
                return value / numOfTowns + fixInt;
            }
        }

        private List<string> dodaj(IEnumerable<GroupBox> boxes, int numOfTowns, bool mainTown)
        {
            List<string> data = new List<string>();
            foreach (GroupBox box in boxes)
            {
                var list = box.Controls.OfType<NumericUpDown>();
                foreach (NumericUpDown control in list)
                {
                    string tmp = Regex.Replace(control.Name, "numericUpDown", "");
                    //MessageBox.Show(tmp);
                    int value = Convert.ToInt32(control.Value);
                    tmpUnits.Add(tmp);
                    //tmpUnits.Insert(0, tmp);

                    if (("Rami" == tmp || "Podmornice" == tmp))
                    {
                        if (null == nether) //če prvič pride do tega
                        {
                            nether = new int[3]; //rami in podmornice imajo svoje mesto
                            nether[0] = nether[1] = -1;
                        }

                        if ("Rami" == tmp)
                        {
                            if (-1 == nether[0])
                            {
                                nether[0] = Convert.ToInt32(control.Value);
                                nether[2] = nether[0] % 2;
                                nether[0] /= 2;
                            }
                            value = nether[0];
                        }
                        else// if ("Podmornice" == tmp)
                        {
                            if (-1 == nether[1])
                            {
                                nether[1] = Convert.ToInt32(control.Value);
                            }
                            value = 0;
                        }
                    }

                    if (tmp.Length < 8)
                        tmp += "\t\t";
                    else
                        tmp += "\t";

                    if(0 != value)
                        value = naMesto(value, numOfTowns, 1, mainTown);

                    if (value < 0)
                        MessageBox.Show("funkcija naMesto(...) je za " + tmp + " vrnila vrednost " + value, "Zaznana napaka v delovanju programa", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    values.Add(value);
                    data.Add(tmp + value);
                }
            }
            return data;
        }

        private List<string> obrni(List<string> list)
        {
            list.Reverse();
            return list;
        }

        private List<string> zdruzi(bool mainTown)
        {
            IEnumerable<GroupBox> boxL = groupBoxLadje.Controls.OfType<GroupBox>();
            IEnumerable<GroupBox> boxK = groupBoxKopenske.Controls.OfType<GroupBox>();
            List<string> data = new List<string>();
            List<string> tmpInf = null; //da lahko izpišem čas za vsakega posebej
            List<string> tmpShip = null;

            if (checkBoxLadje.Checked)
                tmpShip = obrni(dodaj(boxL, numOfTownsShips, mainTown));

            if (checkBoxKopenske.Checked)
                tmpInf = obrni(dodaj(boxK, numOfTownsBarracks, mainTown));

            List<TimeSpan> lts = new List<TimeSpan>(casTreninga(values, mainTown, false));

            if (checkBoxLadje.Checked)
            {
                data.AddRange(tmpShip);
                data.Add(lts[0].Days + "D " + lts[0].Hours + "H " + lts[0].Minutes + "M " + lts[0].Seconds + "S");
                data.Add("\n");
            }

            if (checkBoxKopenske.Checked)
            {
                data.AddRange(tmpInf);
                data.Add(lts[1].Days + "D " + lts[1].Hours + "H " + lts[1].Minutes + "M " + lts[1].Seconds + "S");
                data.Add("\n");
            }

            if (mainTown)
            {
                textBoxTreningGlavnoL.Text = lts[0].Days + "D " + lts[0].Hours + "H " + lts[0].Minutes + "M " + lts[0].Seconds + "S";
                textBoxTreningGlavnoK.Text = lts[1].Days + "D " + lts[1].Hours + "H " + lts[1].Minutes + "M " + lts[1].Seconds + "S";
            }
            else
            {
                textBoxTreningOstalaL.Text = lts[0].Days + "D " + lts[0].Hours + "H " + lts[0].Minutes + "M " + lts[0].Seconds + "S";
                textBoxTreningOstalaK.Text = lts[1].Days + "D " + lts[1].Hours + "H " + lts[1].Minutes + "M " + lts[1].Seconds + "S";
            }

            return data;
        }

        private void perMP_TextChanged(object sender, EventArgs e)
        {
            double tmp;
            if (Double.TryParse(perMP.Text, out tmp) && tmp <= 1)
                perPR.Text = (1 - Double.Parse(perMP.Text)).ToString();
        }

        private void perPR_TextChanged(object sender, EventArgs e)
        {
            double tmp;
            if (Double.TryParse(perPR.Text, out tmp) && tmp <= 1)
                perMP.Text = (1 - Double.Parse(perPR.Text)).ToString();
        }

        private void perPod_TextChanged(object sender, EventArgs e)
        {
            double tmp;
            if (Double.TryParse(perPod.Text, out tmp) && tmp <= 1)
                perRak.Text = (1 - Double.Parse(perPod.Text)).ToString();
        }

        private void perRak_TextChanged(object sender, EventArgs e)
        {
            double tmp;
            if (Double.TryParse(perRak.Text, out tmp) && tmp <= 1)
                perPod.Text = (1 - Double.Parse(perRak.Text)).ToString();
        }

        private void perSul_TextChanged(object sender, EventArgs e)
        {
            double tmp;
            if (Double.TryParse(perSul.Text, out tmp) && tmp <= 1)
                perPV.Text = (1 - Double.Parse(perSul.Text)).ToString();
        }

        private void perPV_TextChanged(object sender, EventArgs e)
        {
            double tmp;
            if (Double.TryParse(perPV.Text, out tmp) && tmp <= 1)
                perSul.Text = (1 - Double.Parse(perPV.Text)).ToString();
        }

        private void perMor_TextChanged(object sender, EventArgs e)
        {
            double tmp;
            if (Double.TryParse(perMor.Text, out tmp) && tmp <= 1)
                perOvn.Text = (1 - Double.Parse(perMor.Text)).ToString();
        }

        private void perKat_TextChanged(object sender, EventArgs e)
        {
            double tmp;
            if (Double.TryParse(perKat.Text, out tmp) && tmp <= 1)
                perMor.Text = (1 - Double.Parse(perKat.Text)).ToString();
        }

        private void perOvn_TextChanged(object sender, EventArgs e)
        {
            double tmp;
            if (Double.TryParse(perOvn.Text, out tmp) && tmp <= 1)
            {
                perMor.Text = (1 - Double.Parse(perOvn.Text) - Double.Parse(perKat.Text)).ToString();
            }
        }

        /*
         * 
         * End Custom
         * 
         */
    }
}