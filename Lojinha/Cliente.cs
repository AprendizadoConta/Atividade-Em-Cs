using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Lojinha
{
    class Cliente
    {
         public int id { get; set; }
         public string nome { get; set; }
         public string celular { get; set; }
         public string cpf  { get; set; }
         public string cidade { get; set; }
         public string dataNasc { get; set; }

         SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Programação\Modulo 2\Emerson\Lojinha\DBClientes.mdf"";Integrated Security=True");

        public List<Cliente> listacliente()
        {
            List<Cliente> li = new List<Cliente>();
            string sql = "Select * From Cliente";
            if (con.State== ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) 
            { 
                Cliente cli = new Cliente();
                cli.id = (int)dr["ID"];
                cli.nome = dr["nome"].ToString();
                cli.celular = dr["celular"].ToString();
                cli.cidade = dr["cidade"].ToString();
                cli.dataNasc = dr["data_nascimento"].ToString();
                cli.cpf = dr["cpf"].ToString();
                li.Add(cli);
            }
            return li;
        }
        public void Inserir(string nome, string celular, string cidade, string dataNasc, string cpf)
        {
            string sql = "INSERT INTO Cliente(nome,celular,cidade,data_nascimento,cpf) VALUES('" + nome + "','" + celular + "','" + cidade + "','" + dataNasc + "','" + cpf + "')";
            if (con.State ==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand (sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void Localizar(int Id)
        {
            string sql = "SELECT * FROM Cliente WHERE Id = '" + Id + "'";
            if (con.State ==ConnectionState.Open) { 
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader ();
            while (dr.Read()) {
                nome = dr["Nome"].ToString();
                celular = dr["celular"].ToString();
                cidade = dr["cidade"].ToString();
                dataNasc = dr["data_nascimento"].ToString();
                cpf = dr["cpf"].ToString();
            }
            con.Close();

        }
        public void Atualizar (int Id, string nome, string celular,  string cidade, string DataNasc, string cpf)
        {
            string sql = "UPDATE Cliente SET nome = '" + nome + "', celular = '" + celular + "',cidade ='" + cidade + "',data_nascimento ='" + DataNasc + "',cpf ='" + cpf + "'WHERE Id="+Id;
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public void Excluir(int Id)
        {
            string sql = "DELETE FROM Cliente WHERE Id= '" + Id + "' ";
            if (con.State == ConnectionState.Open) 
            { 
                con.Close(); 
            } 
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public bool RegistroRepetido(string nome, string celular)
        {
            string sql = "SELECT * FROM Cliente WHERE nome='" + nome + "'AND celular='" + celular + "'";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                return (int)result > 0;
            }
            con.Close();
            return false;
        }
    }
}
