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
    public partial class client : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Livraria;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        int ID = 0;

        public client()
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
                adapt = new SqlDataAdapter("SELECT * FROM dbo.Clientes", con);
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
            txtEmail.Text = "";
            txtEndereco.Text = "";
            txtCidade.Text = "";
            txtEstado.Text = "";
            txtTelefone.Text = "";
            ID = 0;

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtEmail.Text = "";
            txtEndereco.Text = "";
            txtCidade.Text = "";
            txtEstado.Text = "";
            txtTelefone.Text = "";
            txtNome.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && txtEmail.Text != "" && txtEndereco.Text != "" && txtCidade.Text != "" && txtEstado.Text != "" && txtTelefone.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("INSERT INTO dbo.Clientes (Nome_do_Cliente,Email_do_Cliente,Endereco_do_Cliente,Cidade_do_Cliente,Estado_do_Cliente, Telefone_do_Cliente) VALUES (@nome,@email,@endereco,@cidade,@estado,@telefone)", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@cidade", txtCidade.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@estado", txtEstado.Text.ToLower());
                    cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text.ToLower());
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
                txtEmail.Text = dgvDados.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtEndereco.Text = dgvDados.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtCidade.Text = dgvDados.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtEstado.Text = dgvDados.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtTelefone.Text = dgvDados.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
            catch { }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if ((txtNome.Text != "" && txtEmail.Text != "" && txtEndereco.Text != "" && txtCidade.Text != "" && txtEstado.Text != "" && txtTelefone.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("UPDATE dbo.Clientes SET Nome_do_Cliente,Email_do_Cliente,Endereco_do_Cliente,Cidade_do_Cliente,Estado_do_Cliente, Telefone_do_Cliente) VALUES (@nome,@email,@endereco,@cidade,@estado,@telefone WHERE Id_Clientes=@id", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@cidade", txtCidade.Text.ToUpper());
                    cmd.Parameters.AddWithValue("@estado", txtEstado.Text.ToLower());
                    cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text.ToLower());
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
                        cmd = new SqlCommand("DELETE dbo.Clientes WHERE Id_Clientes=@id", con);
                        con.Open();
                        cmd.Parameters.AddWithValue("@id", ID);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Deletado com sucesso...!");
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
