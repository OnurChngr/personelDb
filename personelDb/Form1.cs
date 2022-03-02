using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace personelDb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGiriş_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
            con.Open();
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "select * from personel where kulad='" + txtkuladi.Text + "' and sifre='" + txtsifre.Text + "'";
            komut.Connection = con;
            SqlDataReader dr = komut.ExecuteReader();

            if (dr.HasRows)
            {
                bool adminMi;
                if (txtkuladi.Text == "admin" && txtsifre.Text == "1234")
                {
                    adminMi = true;

                }
                else
                {
                    adminMi = false;
                }

                KullaniciIslemleri form = new KullaniciIslemleri(adminMi);
                form.ShowDialog();
                

            }
            else
            {
                MessageBox.Show("Kullanıcı Adı veya Şifre Yanlış.Lütfen Tekrar Deneyiniz");
            }

            con.Close();
        }
    }
}
