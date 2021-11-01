using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace Mahjong
{
    public partial class Form1 : Form
    {
        enum Tiklamalar
        {
            ilkTiklama,ikinciTiklama
        }
        #region GenelDeğişkenler
        Tiklamalar tiklama = Tiklamalar.ilkTiklama;
        PictureBox oncekiResim;
        int kalan;
        int ciftSay;
        #endregion
        public Form1()
        {
            InitializeComponent();
        }
        void ResimleriGizle()
        {
            foreach (PictureBox x in panel1.Controls) x.Image = imageList1.Images[0];
        }
        void ResimleriGoster()
        {
            foreach (PictureBox x in panel1.Controls) x.Image = imageList1.Images[(int)x.Tag];
        }
        void ResimleriDoldur()
        {
            ArrayList Tagler = new ArrayList();
            for (int i = 0; i < (ciftSay) * 2; i++) Tagler.Add((i % ciftSay) + 1);
            //listBox1.Items.AddRange(Tagler.ToArray());
            foreach(PictureBox x in panel1.Controls)
            {
                int sansli = new Random().Next(Tagler.Count);
                
                x.Tag = Tagler[sansli];
                x.Show();
                Tagler.RemoveAt(sansli);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            YenidenBaşlat();
        }
        private void YenidenBaşlat()
        {
            ciftSay = imageList1.Images.Count - 1;
            kalan = ciftSay;
            label2.Text = kalan.ToString();
            ResimleriGizle();
            ResimleriDoldur();
            oncekiResim = null;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox simdikiResim = sender as PictureBox;
            if(oncekiResim == simdikiResim)
            {
                MessageBox.Show("Hatalı Seçim");
                return;
            }
            simdikiResim.Image = imageList1.Images[(int)simdikiResim.Tag];
            panel1.Refresh();
            switch (tiklama)
            {
                case Tiklamalar.ilkTiklama:
                    oncekiResim = simdikiResim;
                    tiklama = Tiklamalar.ikinciTiklama;
                    break;
                case Tiklamalar.ikinciTiklama:
                    Thread.Sleep(500);
                    if(oncekiResim.Tag.ToString() == simdikiResim.Tag.ToString())
                    {
                        oncekiResim.Hide();
                        simdikiResim.Hide();
                        label2.Text = (--kalan).ToString();
                        if(kalan == 0)
                        {
                            MessageBox.Show("Oyun Bitti");
                            YenidenBaşlat();
                        }
                    }
                    ResimleriGizle();
                    {
                        tiklama = Tiklamalar.ilkTiklama;
                        break;
                    }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            YenidenBaşlat();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResimleriGoster();
            panel1.Refresh();
            Thread.Sleep(500);
            ResimleriGizle();
        }
    }
}
