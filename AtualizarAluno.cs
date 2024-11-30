using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCC_DO_MANO
{
    public partial class Cadastrar_Rosto : Form
    {
        public Cadastrar_Rosto()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;";

            // Cria uma conexão
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Abre a conexão
                    connection.Open();
                    Console.WriteLine("Conexão aberta com sucesso.");

                    // Cria um comando para executar uma consulta
                    string query = $"SELECT * FROM aluno where RA = {Convert.ToInt64(txtRA.Text)};";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            groupBox1.Enabled = true;

                            while (reader.Read())
                            {
                                txtNome.Text = reader["NOME"].ToString();
                                txtSobrenome.Text = reader["SOBRENOME"].ToString();
                                txtTelefone.Text = reader["TELEFONE"].ToString();
                                txtCelular.Text = reader["CELULAR"].ToString();
                                txtCPF.Text = reader["CPF"].ToString();
                                txtRG.Text = reader["RG"].ToString();
                                txtEmail.Text = reader["EMAIL"].ToString();
                                pictureBox1.Image = Base64ToBitmap(reader["FOTO"].ToString().Split(',')[0]);
                            }

                            txtRA.Enabled = false;
                            btnPesquisar.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show($"Não existe Aluno com RA igual a {txtRA.Text}");
                            txtRA.Text = "";
                        }
                    }

                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
                finally
                {
                    // Fecha a conexão
                    connection.Close();
                    Console.WriteLine("Conexão fechada.");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtSobrenome.Text = "";
            txtTelefone.Text = "";
            txtCelular.Text = "";
            txtCPF.Text = "";
            txtRG.Text = "";
            txtEmail.Text = "";
            pictureBox1.Image = null;

            txtRA.Text = "";

            groupBox1.Enabled = false;

            txtRA.Enabled = true;
            btnPesquisar.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;";

            // Crie a conexão com o banco de dados
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Defina o comando SQL para o UPDATE
                    string sql = "UPDATE aluno SET NOME = @valor1, SOBRENOME = @valor2, TELEFONE = @valor3, CELULAR = @valor4, RG = @valor5, CPF = @valor6, EMAIL = @valor7  WHERE RA = @condicao";

                    // Crie o objeto de comando
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    // Adicione os parâmetros
                    cmd.Parameters.AddWithValue("@valor1", txtNome.Text);
                    cmd.Parameters.AddWithValue("@valor2", txtSobrenome.Text);
                    cmd.Parameters.AddWithValue("@valor3", txtTelefone.Text);
                    cmd.Parameters.AddWithValue("@valor4", txtCelular.Text);
                    cmd.Parameters.AddWithValue("@valor5", txtCPF.Text);
                    cmd.Parameters.AddWithValue("@valor6", txtRG.Text);
                    cmd.Parameters.AddWithValue("@valor7", txtEmail.Text);

                    cmd.Parameters.AddWithValue("@condicao", $"{Convert.ToInt64(txtRA.Text)}");

                    // Execute o comando
                    int rowsAffected = cmd.ExecuteNonQuery();

                    MessageBox.Show("Cadastro atualizado com sucesso!");

                    txtNome.Text = "";
                    txtSobrenome.Text = "";
                    txtTelefone.Text = "";
                    txtCelular.Text = "";
                    txtCPF.Text = "";
                    txtRG.Text = "";
                    txtEmail.Text = "";
                    pictureBox1.Image = null;

                    txtRA.Text = "";

                    groupBox1.Enabled = false;

                    txtRA.Enabled = true;
                    btnPesquisar.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocorreu um erro: {ex.Message}", "Erro Atualização Cadastral");
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public Bitmap Base64ToBitmap(string base64String)
        {
            // Converte a string Base64 para um array de bytes
            byte[] imageBytes = Convert.FromBase64String(base64String);

            // Cria um MemoryStream a partir do array de bytes
            using (MemoryStream memoryStream = new MemoryStream(imageBytes))
            {
                // Cria um Bitmap a partir do MemoryStream
                Bitmap bitmap = new Bitmap(memoryStream);
                return bitmap;
            }
        }
    }
}
