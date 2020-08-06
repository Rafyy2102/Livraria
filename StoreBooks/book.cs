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

namespace StoreBooks
{
    public partial class book : Form
    {        
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Livraria;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        int ID = 0;

        public book()
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
                adapt = new SqlDataAdapter("SELECT * FROM dbo.Livrarias", con);
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

        private void book_Load(object sender, EventArgs e)
        {

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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && txtAutor.Text != "" && txtAno.Text != "" && txtGenero.Text != "" && txtEditora.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("INSERT INTO dbo.Livrarias (Nome_do_livro,Autor_do_livro,Ano_do_livro,Genero_do_livro,Editora_do_livro) VALUES (@nome,@autor,@ano,@genero,@editora)", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@autor", txtAutor.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@ano", txtAno.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@genero", txtGenero.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@editora", txtEditora.Text.ToLower());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registrado com sucesso...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro :" + ex.Message);
                }
                finally
                {
                    con.Close();
                    ExibirDados();
                    LimparDados();

                }
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

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && txtAutor.Text != "" && txtAno.Text != "" && txtGenero.Text != "" && txtEditora.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("UPDATE dbo.Livrarias SET Nome_do_livro=@nome, Autor_do_livro=@autor,  Ano_do_livro=@ano, Genero_do_livro=@genero, Editora_do_livro=@editora WHERE Id_livros=@id", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@autor", txtAutor.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@ano", txtAno.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@genero", txtGenero.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@editora", txtEditora.Text.ToLower());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Atualizado com sucesso...");
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                if (MessageBox.Show("Deseja Deletar este registro ?", "Mensagem do sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja Sair do programa ?", "Mensagem do sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                txtNome.Focus();
            }
        }
    }
}
