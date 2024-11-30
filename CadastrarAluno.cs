
using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;

using System.Drawing;
using System.IO;

using System.Windows.Forms;
using FaceRecognitionv3;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TCC_DO_MANO
{
    public partial class CadastrarAluno : Form
    {
        public string fotoBitMap = null;
        FaceRec faceRec = new FaceRec(); //abre o cmd ai como admin pera ai

        public CadastrarAluno()
        {
            InitializeComponent();

            faceRec.openCamera(pictureBox2, pictureBox1);
        }


        private void btnFoto_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            pictureBox1.Visible = true;
            faceRec.salvandoFoto = true;
            faceRec.Save_IMAGE("", out fotoBitMap);
        }


        private void CadastrarAluno_Load(object sender, EventArgs e)
        {

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(faceRec.bitmapfoto))
            {
                string connectionString = "Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;";

                // Crie a conexão com o banco de dados
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // Defina o comando SQL para o UPDATE
                        string sql = "INSERT INTO Aluno (RA, NOME, SOBRENOME, TELEFONE, CELULAR, CPF, RG, EMAIL, FOTO) VALUES (@valor1, @valor2, @valor3, @valor4, @valor5, @valor6, @valor7, @valor8, @valor9); SELECT LAST_INSERT_ID();";

                        // Crie o objeto de comando
                        MySqlCommand cmd = new MySqlCommand(sql, conn);

                        // Adicione os parâmetros
                        cmd.Parameters.AddWithValue("@valor1", Convert.ToInt64(txtRA.Text));
                        cmd.Parameters.AddWithValue("@valor2", txtNome.Text);
                        cmd.Parameters.AddWithValue("@valor3", txtSobrenome.Text);
                        cmd.Parameters.AddWithValue("@valor4", txtTelefone.Text);
                        cmd.Parameters.AddWithValue("@valor5", txtCelular.Text);
                        cmd.Parameters.AddWithValue("@valor6", txtCPF.Text);
                        cmd.Parameters.AddWithValue("@valor7", txtRG.Text);
                        cmd.Parameters.AddWithValue("@valor8", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@valor9", faceRec.bitmapfoto);

                        // Execute o comando
                        var insertedId = Convert.ToInt32(cmd.ExecuteScalar());

                        //idAula.Text = insertedId.ToString();

                        //MessageBox.Show("Aula icializada com sucesso!");

                        //groupBox1.Enabled = true;
                        //tabControl1.Enabled = false;

                        //faceRec.openCamera(pictureBox2, pictureBox3);

                        txtRA.Text = "";
                        txtNome.Text = "";
                        txtSobrenome.Text = "";
                        txtTelefone.Text = "";
                        txtCelular.Text = "";
                        txtCPF.Text = "";
                        txtRG.Text = "";
                        txtEmail.Text = "";
                        pictureBox1.Image = null;
                        faceRec.bitmapfoto = "";

                        MessageBox.Show($"Aluno cadastrado com sucesso", "Cadastro Aluno");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocorreu um erro: {ex.Message}", "Erro ao Inicializar Aula");
                    }
                }
            }
        }

        private void btnFecharAttCadastral_Click(object sender, EventArgs e)
        {
            faceRec.CloseCamera();
            this.Close();

            faceRec.Dispose();
        }
    }
}
