using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StoreBooks
{
    public partial class frmbook : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Livraria;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        int ID = 0;

        public frmbook()
        {
            InitializeComponent();
            ExibirDados();
        }

        private void ExibirDados()
        {
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                adapt = new SqlDataAdapter("SELECT * FROM  dbo.Livrarias", con);
                adapt.Fill(dt);
                dgvDados.DataSource = dt;
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        private void LimparDados()
        {
            txtNome.Text = "";
            txtAutor.Text = "";
            txtAno.Text = "";
            txtGenero.Text = "";
            txtEditora.Text = "";
            ID = 0;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && txtAutor.Text != "" && txtAno.Text != "" && txtGenero.Text != "" && txtEditora.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("INSERT INTO dbo.Livrarias(Nome,Autor,Ano,Genero,Editora) VALUES(@nome,@autor,@ano,@genero,@editora)", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@autor", txtAutor.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@ano", txtAno.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@genero", txtGenero.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@editora", txtEditora.Text.ToLower());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro incluído com sucesso...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                }
                finally
                {
                    con.Close();
                    ExibirDados();
                    LimparDados();
                }
            }
            else
            {
                MessageBox.Show("Informe todos os dados requeridos");
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && txtAutor.Text != "" && txtAno.Text != "" && txtGenero.Text != "" && txtEditora.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("UPDATE dbo.Livrarias SET Nome=@nome, Autor=@autor, Ano=@Ano ,Genero=@genero, Editora=@editora WHERE Id_livros=@id", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@autor", txtAutor.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@ano", txtAno.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@genero", txtGenero.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@editora", txtEditora.Text.ToLower());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro atualizado com sucesso...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro : " + ex.Message);
                }
                finally
                {
                    con.Close();
                    ExibirDados();
                    LimparDados();
                }
            }
            else
            {
                MessageBox.Show("Informe todos os dados requeridos");
            }
        }

        private void dgvDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ID = Convert.ToInt32(dgvDados.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtNome.Text = dgvDados.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtAutor.Text = dgvDados.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtAno.Text = dgvDados.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtGenero.Text = dgvDados.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtEditora.Text = dgvDados.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            catch { }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtAutor.Text = "";
            txtAno.Text = "";
            txtGenero.Text = "";
            txtEditora.Text = "";
            txtNome.Focus();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                if (MessageBox.Show("Deseja Deletar este registro ?", "Livraria", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        cmd = new SqlCommand("DELETE dbo.Livrarias WHERE Id_livros=@id", con);
                        con.Open();
                        cmd.Parameters.AddWithValue("@id", ID);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("registro deletado com sucesso...!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro : " + ex.Message);
                    }
                    finally
                    {
                        con.Close();
                        ExibirDados();
                        LimparDados();
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um registro para deletar");
            }
        }

        private void book_Load(object sender, EventArgs e)
        {

        }
    }
}














      