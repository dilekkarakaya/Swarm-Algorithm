using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Swarm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string dizii;
        string satir;
        string best;
        int buton1 = 0;
        int iter= 0;
        double w1,c1,c2 = 0;
       
        private void button1_Click(object sender, EventArgs e)
        {
            c1 = Convert.ToDouble(textBox2.Text);
            c2 = Convert.ToDouble(textBox3.Text);
            w1 = Convert.ToDouble(textBox1.Text);
            iter = Convert.ToInt32(textBox4.Text);
            double amacson = 0;
            label6.Text = "Minumum Bulunuyor...";
            best = "";
            listBox1.Items.Clear();
           
            double[,] dizi = new double[10, 2];
            double[,] hiz = new double[10, 2];
            double[] amac1 = new double[10];
            double[,] parcacik_en_iyi_pozisyon = new double[10, 2];
           
            double[] parcacik_en_iyi_deger = new double[10];
            Random rnd = new Random();
            Random rastgele = new Random();
            Random rastgele2 = new Random();
            
            listBox1.Items.Add("Olusturulan İlk Sürü");

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    dizi[i, j] = (rnd.NextDouble() * rnd.Next(-10, 10));
                    dizii += (dizi[i, j].ToString("0.0000\t"));
                }
                listBox1.Items.Add(dizii);
                dizii = "";
            }
            listBox1.Items.Add("--------------------------");
            Array.Copy(dizi, 0, parcacik_en_iyi_pozisyon, 0, dizi.Length);

            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                  
                    amac1[j] = ((-1) * Math.Cos(dizi[j, i])) * Math.Cos(dizi[j, i + 1]) * Math.Exp((-1) * 
                        Math.Pow((dizi[j, i] - Math.PI), 2) - Math.Pow((dizi[j, i + 1] - Math.PI), 2));
                   
                }

            }
            Array.Copy(amac1, parcacik_en_iyi_deger, amac1.Length);
            ///////////////////////////////////////////////////////////
            double sürüenkucuk = amac1.Min();

            double[,] swarm_eniyipozisyon = dizi.Clone() as double[,];
            int index = Array.IndexOf(amac1, sürüenkucuk);
            for (int i = 0; i < 2; i++)
            {
                swarm_eniyipozisyon[1, i] = dizi[index, i];
                best += swarm_eniyipozisyon[1, i].ToString("0.000\t");
            }

            for (int iterasyon = 1; iterasyon <= iter; iterasyon++)
            {
                 listBox1.Items.Add(iterasyon.ToString() + ". ITERASYON");
                ///HİZ HESAPLA
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        hiz[i, j] = w1 * hiz[i, j] + c1 * rastgele.NextDouble() * (parcacik_en_iyi_pozisyon[i, j] - dizi[i, j]) + c2 * rastgele2.NextDouble() * (swarm_eniyipozisyon[1, j] - dizi[i, j]);

                        if (hiz[i, j] > 10)
                        {
                            hiz[i, j] = 10;
                        }
                        else if (hiz[i, j] < -10)
                        {
                            hiz[i, j] = -10;
                        }
                        dizi[i, j] += hiz[i, j];
                        if (dizi[i, j] > 10)
                        {
                            dizi[i, j] = 10;
                        }
                        else if (dizi[i, j] < -10)
                        {
                            dizi[i, j] = -10;
                        }
                        satir += dizi[i, j].ToString("0.0000\t");
                    }

                    listBox1.Items.Add(satir);

                    satir = "";
                }
                listBox1.Items.Add("------------");

                for (int i = 0; i < 1; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        
                        amac1[j] = (-1) * Math.Cos(dizi[j, i]) * Math.Cos(dizi[j, i + 1]) * Math.Exp((-1) * 
                            Math.Pow((dizi[j, i] - Math.PI), 2) - Math.Pow((dizi[j, i + 1] - Math.PI), 2));

                    }

                }

                ////////////////PARÇACIK EN İYİSİNİ GÜNCELLE
                for (int r = 0; r < 10; r++)
                {
                    if (amac1[r] < parcacik_en_iyi_deger[r])
                    {
                        parcacik_en_iyi_deger[r] = amac1[r];
                        amacson = amac1[r];
                        for (int i = 0; i < 2; i++)
                        {
                            parcacik_en_iyi_pozisyon[r, i] = dizi[r, i];

                        }
                    }

                }

                ////////////////SÜRÜ EN İYİSİNİ GÜNCELLE

                if (amac1.Min() < sürüenkucuk)
                {
                    best = "";
                    sürüenkucuk = amac1.Min();
                    index = Array.IndexOf(amac1, sürüenkucuk);
                    for (int i = 0; i < 2; i++)
                    {
                        swarm_eniyipozisyon[1, i] = dizi[index, i];
                        best += swarm_eniyipozisyon[1, i].ToString("0.000\t");
                    }

                }

            }
            buton1 ++;
            listBox2.Items.Add(buton1.ToString()+". Çalısmanın Sonucu(Minumum)");
            listBox2.Items.Add(best + "" + "f(x)="+amacson.ToString("0.000\t"));

        }
        ///
        private void button2_Click(object sender, EventArgs e)
        {
            double amacson = 0;
            label6.Text = "Maximum Bulunuyor...";
            c1 = Convert.ToDouble(textBox2.Text);
            c2 = Convert.ToDouble(textBox3.Text);
            w1 = Convert.ToDouble(textBox1.Text);
            iter = Convert.ToInt32(textBox4.Text);

            best = "";
            listBox1.Items.Clear();
        
            double[,] dizi = new double[10, 2];
            double[,] hiz = new double[10, 2];
            double[] amac1 = new double[10];
            double[,] parcacik_en_iyi_pozisyon = new double[10, 2];
         
            double[] parcacik_en_iyi_deger = new double[10];
            Random rnd = new Random();
            Random rastgele = new Random();
            Random rastgele2 = new Random();
           
            listBox1.Items.Add("Olusturulan İlk Sürü");

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    dizi[i, j] = (rnd.NextDouble() * rnd.Next(-10, 10));
                    dizii += (dizi[i, j].ToString("0.0000\t"));
                }

                listBox1.Items.Add(dizii);

                dizii = "";

            }
            listBox1.Items.Add("--------------------------");
            Array.Copy(dizi, 0, parcacik_en_iyi_pozisyon, 0, dizi.Length);

            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    
                    amac1[j] = ((-1) * Math.Cos(dizi[j, i])) * Math.Cos(dizi[j, i + 1]) * Math.Exp((-1) *
                        Math.Pow((dizi[j, i] - Math.PI), 2) - Math.Pow((dizi[j, i + 1] - Math.PI), 2));
                    


                }

            }
            Array.Copy(amac1, parcacik_en_iyi_deger, amac1.Length);
            ///////////////////////////////////////////////////////////
            double suruenbuyuk = amac1.Max();

            double[,] swarm_eniyipozisyon = dizi.Clone() as double[,];
            int index = Array.IndexOf(amac1, suruenbuyuk);
            for (int i = 0; i < 2; i++)
            {
                swarm_eniyipozisyon[1, i] = dizi[index, i];
                best += swarm_eniyipozisyon[1, i].ToString("0.000\t");
            }


            for (int iterasyon = 1; iterasyon <= iter; iterasyon++)
            {
                listBox1.Items.Add(iterasyon.ToString() + ". ITERASYON");
                ///HİZ HESAPLA
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        hiz[i, j] = w1 * hiz[i, j] + c1 * rastgele.NextDouble() * (parcacik_en_iyi_pozisyon[i, j] - dizi[i, j]) + c2 * rastgele2.NextDouble() * (swarm_eniyipozisyon[1, j] - dizi[i, j]);

                        if (hiz[i, j] > 10)
                        {
                            hiz[i, j] = 10;
                        }
                        else if (hiz[i, j] < -10)
                        {
                            hiz[i, j] = -10;
                        }
                        dizi[i, j] += hiz[i, j];
                        if (dizi[i, j] > 10)
                        {
                            dizi[i, j] = 10;
                        }
                        else if (dizi[i, j] < -10)
                        {
                            dizi[i, j] = -10;
                        }
                        satir += dizi[i, j].ToString("0.0000\t");
                    }

                    listBox1.Items.Add(satir);

                    satir = "";
                }
                listBox1.Items.Add("------------");





                for (int i = 0; i < 1; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        
                        amac1[j] = (-1) * Math.Cos(dizi[j, i]) * Math.Cos(dizi[j, i + 1]) * Math.Exp((-1) *
                            Math.Pow((dizi[j, i] - Math.PI), 2) - Math.Pow((dizi[j, i + 1] - Math.PI), 2));

                    }

                }

                ////////////////PARÇACIK EN İYİSİNİ GÜNCELLE

                for (int r = 0; r < 10; r++)
                {
                    if (amac1[r] > parcacik_en_iyi_deger[r])
                    {
                        parcacik_en_iyi_deger[r] = amac1[r];
                        amacson = amac1[r];
                        for (int i = 0; i < 2; i++)
                        {
                            parcacik_en_iyi_pozisyon[r, i] = dizi[r, i];

                        }
                    }

                }

                ////////////////SÜRÜ EN İYİSİNİ GÜNCELLE

                if (amac1.Max() > suruenbuyuk)
                {
                    best = "";
                    suruenbuyuk = amac1.Max();
                    index = Array.IndexOf(amac1, suruenbuyuk);
                    for (int i = 0; i < 2; i++)
                    {
                        swarm_eniyipozisyon[1, i] = dizi[index, i];
                        best += swarm_eniyipozisyon[1, i].ToString("0.000\t");
                    }

                }

            }
            buton1++;
            listBox2.Items.Add(buton1.ToString() + ". Çalısmanın Sonucu(Maximum)");
            listBox2.Items.Add(best + "" + "f(x)=" + amacson.ToString("0.000\t"));

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

       
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip Aciklama = new ToolTip();
            Aciklama.SetToolTip(label1, "W:Eylemsizlik Katsayısı");
            ToolTip Aciklama2 = new ToolTip();
            Aciklama2.SetToolTip(label2, "C1:Bilişsel Katsayı");
            ToolTip Aciklama3 = new ToolTip();
            Aciklama3.SetToolTip(label3, "C2:Sosyal Katsayı");
        }
    }
   
}


        
    
        
    

