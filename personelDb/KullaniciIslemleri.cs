using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace personelDb
{
    public partial class KullaniciIslemleri : Form
    {
        public KullaniciIslemleri()
        {
            InitializeComponent();
        }

        public KullaniciIslemleri(bool adminMi)         
                                                
        {
            InitializeComponent();

            if (adminMi == false) 
            {
                btnguncelle.Visible = false;
                btnkaydet.Visible = false;
                btnsil.Visible = false;
            }
        }

        Personel ornek = new Personel();


        SqlConnection baglanti = new SqlConnection();

        List<Personel> liste = new List<Personel>();


        public void temizle() 
        {
            txtid.Text = "";
            
            txtad.Clear();
            txtsoyad.Clear();
        }

       

        public void listele()
        {
            liste.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand();
            komut.CommandText="select * from personel";
            komut.Connection = baglanti;
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                ornek.kisi_id = Convert.ToInt32(dr["id"]);
                ornek.ad =(dr["ad"].ToString());
                ornek.soyad = (dr["soyad"].ToString());
                ornek.kulad = (dr["kulad"].ToString());
                ornek.sifre = (dr["sifre"].ToString());
                ornek.rolid=(dr["rolid"].ToString());

                liste.Add(ornek);

            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = liste;
            baglanti.Close();
        }


        private void btnkaydet_Click(object sender, EventArgs e)
        {
              
            


                if (txtad.Text != "" && txtsoyad.Text != "" &&txtKulad.Text!= "" && txtsifre.Text!= "" &&txtrolid.Text!= "")
                {
                    baglanti.Open();

                    
                    Personel yenikisi = new Personel();
                    yenikisi.ad = txtad.Text;                
                    yenikisi.soyad = txtsoyad.Text;
                    yenikisi.kulad = txtKulad.Text;
                    yenikisi.sifre=txtsifre.Text;
                    yenikisi.rolid=txtrolid.Text;
                                    
                    liste.Add(yenikisi);


                    SqlCommand komut = new SqlCommand();
                    komut.CommandText = "insert into personel values(@p1,@p2,@p3,@p4,@p5)";
                    komut.Parameters.AddWithValue("@p1", yenikisi.ad);
                    komut.Parameters.AddWithValue("@p2", yenikisi.soyad);
                    komut.Parameters.AddWithValue("@p3", yenikisi.kulad);
                    komut.Parameters.AddWithValue("@p4", yenikisi.sifre);
                    komut.Parameters.AddWithValue("@p5", yenikisi.rolid);


                    komut.Connection = baglanti;

                    komut.ExecuteNonQuery();
                    MessageBox.Show("kayıt eklendi");

                }                                  

            baglanti.Close();

        }

        private void KullaniciIslemleri_Load(object sender, EventArgs e)
        {
            baglanti.ConnectionString = ConfigurationManager.ConnectionStrings["connect"].ConnectionString; 
        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                Personel yenikisi = (Personel)dataGridView1.SelectedRows[0].DataBoundItem; 

                baglanti.Open();
                SqlCommand komut = new SqlCommand();
                komut.CommandText = "delete from personel where id=" + yenikisi.kisi_id;

                komut.Connection = baglanti;

                komut.ExecuteNonQuery();

            }
            else
            {
                MessageBox.Show("bir adet kayıt seçmelisiniz");
            }

            baglanti.Close();

            listele(); 

            temizle();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            if (txtid.Text != ""  && txtad.Text != "" && txtsoyad.Text != "" && txtKulad.Text != "" && txtsifre.Text != "" && txtrolid.Text != "")
            {
                Personel yenikisi = new Personel();
                yenikisi.kisi_id = int.Parse(txtid.Text);                 
                yenikisi.ad = txtad.Text;
                yenikisi.soyad = txtsoyad.Text;


                baglanti.Open();

                SqlCommand komut = new SqlCommand();

                komut.CommandText = "update personel set  ad=@p2, soyad=@p3, kulad=@p5, sifre= @p6 where id=@p4";
                               
                komut.Parameters.AddWithValue("@p2", yenikisi.ad);
                komut.Parameters.AddWithValue("@p3", yenikisi.soyad);
                komut.Parameters.AddWithValue("@p4", yenikisi.kisi_id);
                komut.Parameters.AddWithValue("@p5", yenikisi.kulad);
                komut.Parameters.AddWithValue("@p6", yenikisi.sifre);

                komut.Connection = baglanti;
                komut.ExecuteNonQuery();
                MessageBox.Show("kayıt güncellendi");

            }
            else
            {
                MessageBox.Show("Güncellemek için kayıt seçmelisiniz");
            }

            baglanti.Close();
            listele();


            temizle();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count!=0)
            {
                Personel p = (Personel)dataGridView1.SelectedRows[0].DataBoundItem;
                txtad.Text = p.ad;
                txtsoyad.Text = p.soyad;
                txtKulad.Text = p.kulad;
                txtsifre.Text = p.sifre;
                txtrolid.Text = p.rolid;

            }

        }
        // personelDb tamamlandı
    }
}
