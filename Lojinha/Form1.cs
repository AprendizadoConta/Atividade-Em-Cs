using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lojinha
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Cliente client = new Cliente();
            List<Cliente> clientes = client.listacliente();
            dgvClientes.DataSource = clientes;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Cliente client = new Cliente();
            if(string.IsNullOrEmpty(txtNome.Text) || string.IsNullOrEmpty(txtCelular.Text)||string.IsNullOrEmpty(txtCidade.Text)||string.IsNullOrEmpty(txtCpf.Text)|| string.IsNullOrEmpty(txtDataNas.Text))
            {
                MessageBox.Show("CAMPOS VAZIOS!!! Por Favor preencha-os!!!");
            }
            else
            {
                if (client.RegistroRepetido(txtNome.Text, txtCelular.Text) == true)
                {
                    MessageBox.Show("Cliente já Cadastrado em Nossa Loja!!!");
                    txtNome.Text = "";
                    txtCelular.Text = "";
                }
                else
                {
                    client.Inserir(txtNome.Text, txtCelular.Text, txtCidade.Text, txtDataNas.Text, txtCpf.Text);
                    MessageBox.Show("Cliente Cadastrado com Sucesso!!!");
                    List<Cliente> clientes = client.listacliente();
                    txtNome.Text = "";
                    txtCelular.Text = "";
                    txtCidade.Text = "";
                    txtDataNas.Text = "";
                    txtCpf.Text = "";
                }
            }
            
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text.Trim());// Usa-se o Trim() para deletar os espaços em branco no início ou no final da string Ex: ___2___ ele remove os "_"
            if (txtNome.Text != null)
            {
                btnEditar.Enabled = true;
                btnExcluir.Enabled = true;
            }
            Cliente client = new Cliente();
            client.Localizar(id);
            txtNome.Text = client.nome;
            txtCelular.Text = client.celular;
            txtCidade.Text = client.cidade;
            txtDataNas.Text = client.dataNasc;
            txtCpf.Text = client.cpf;

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text.Trim());
            Cliente client = new Cliente();
            client.Atualizar(id, txtNome.Text, txtCelular.Text, txtCidade.Text, txtDataNas.Text, txtCpf.Text);
            MessageBox.Show("Dados do Cliente ATUALIZADOS com Sucesso!!!");
            List<Cliente> clientes = client.listacliente();
            txtNome.Text = "";
            txtCelular.Text = "";
            txtCidade.Text = "";
            txtDataNas.Text = "";
            txtCpf.Text = "";
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtID.Text.Trim());
            Cliente client = new Cliente();
            client.Excluir(id);
            MessageBox.Show("Pessoa EXLCUÍDA com Sucesso!!!");
            List<Cliente> clientes = client.listacliente();
            txtNome.Text = "";
            txtCelular.Text = "";
            txtCidade.Text = "";
            txtDataNas.Text = "";
            txtCpf.Text = "";
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >=0)
            {
                DataGridViewRow row = this.dgvClientes.Rows[e.RowIndex];
                this.dgvClientes.Rows[e.RowIndex].Selected = true;
                txtID.Text = row.Cells[0].Value.ToString();
                txtNome.Text = row.Cells[1].Value.ToString();
                txtCelular.Text = row.Cells[2].Value.ToString();
                txtCidade.Text = row.Cells[3].Value.ToString();
                txtDataNas.Text = row.Cells[4].Value.ToString();
                txtCpf.Text = row.Cells[5].Value.ToString();
            }
            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
        }
    }
}
