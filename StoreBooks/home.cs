using System;
using System.Windows.Forms;

namespace StoreBooks
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }
             
        private void tsbtnClientes_Click_1(object sender, EventArgs e)
        {
            frmclient Client = new frmclient();
            Client.ShowDialog();
        }

        private void tsbtnLivros_Click_1(object sender, EventArgs e)
        {
            frmbook Book = new frmbook();
            Book.ShowDialog();
        }

        private void tsbtnSair_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja sair do sistema?", "Mensagem do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
        

