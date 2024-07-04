using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Veritabani12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=MAMI;Initial Catalog=TelefonKayıtları;Integrated Security=True;TrustServerCertificate=True");
        void listele()
        {

            DataTable dt = new DataTable("");
            SqlDataAdapter adtr = new SqlDataAdapter("Select * from Personel", baglanti);
            //    Kayıtları Datatable aktarıldı.daha sonra veriler datagridviewde gösterildi.
            adtr.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
        listele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("INSERT INTO Personel(AdiSoyadi,Telefon,Maas,Adress,Tarih) values('" + txtAdsoyad.Text+"','"+txtTelefon.Text+"',@Maas,'"+txtAdress.Text+"',@Tarih)", baglanti);
            komut.Parameters.AddWithValue("@Maas",decimal.Parse(txtMaas.Text));
            komut.Parameters.AddWithValue("@Tarih", DateTime.Now);

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Başarılı.");
            listele();
            Temizle();
        }
        void Temizle()
        {
            txtId.Text = string.Empty;
            txtAdsoyad.Text = string.Empty;
            txtTelefon.Text= string.Empty;
            txtMaas.Text = string.Empty;
            txtAdress.Text = string.Empty;

        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {

            baglanti.Open();

            SqlCommand komut = new SqlCommand("UPDATE Personel SET AdiSoyadi='" + txtAdsoyad.Text + "', Telefon='" + txtTelefon.Text + "', Maas=@Maas, Adress='" + txtAdress.Text + "' WHERE Id='" + txtId.Text + "'", baglanti);
            komut.Parameters.AddWithValue("@Maas", decimal.Parse("txtMaas.Text"));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Düzenleme Başarılı.");
            listele();
            Temizle();



        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
            txtAdsoyad.Text = dataGridView1.CurrentRow.Cells["AdiSoyadi"].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells["Telefon"].Value.ToString();
            txtMaas.Text = dataGridView1.CurrentRow.Cells["Maas"].Value.ToString();
            txtAdress.Text = dataGridView1.CurrentRow.Cells["Adress"].Value.ToString();

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Kayıt silinecek.Onaylıyor musun?","UYARI",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
             baglanti.Open();

             SqlCommand komut = new SqlCommand("DELETE FROM Personel WHERE Id='" + dataGridView1.CurrentRow.Cells["Id"].Value.ToString() + "'", baglanti);
             komut.ExecuteNonQuery();
             baglanti.Close();
             MessageBox.Show("Kayıt silinecek.");
             listele();
             Temizle();

            }
            
        }
    }
}
