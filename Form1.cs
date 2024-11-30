//using FaceRecognition;

using FaceRecognitionv3;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TCC_DO_MANO
{
    public partial class Form1 : Form
    {
        public class ComboboxItem
        {
            public string Text { get; set; }
            public long Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        public Form1()
        {
            InitializeComponent();

            // param1: Camera Ao Vivo - param2: Foto
            //faceRec.openCamera(pictureBox2, pictureBox1);
        }

        FaceRec faceRec = new FaceRec();
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //faceRec.Save_IMAGE(txtRA.Text);
            //MessageBox.Show("Imagem Armazenada Com Sucesso");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (tabControl1.SelectedIndex == 0)
                faceRec.Took_PICTURE(pictureBox3, idAula.Text);
            else if (tabControl1.SelectedIndex == 1)
                faceRec.Took_PICTURE(pictureBox3, txtIdAula.Text);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;";

            // Crie a conexão com o banco de dados
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Defina o comando SQL para o UPDATE
                    string sql = "INSERT INTO Aula (DataAula, Materia, StatusAula) VALUES (@valor1, @valor2, @valor3); SELECT LAST_INSERT_ID();";

                    // Crie o objeto de comando
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    // Adicione os parâmetros
                    cmd.Parameters.AddWithValue("@valor1", dateTimePicker1.Value.ToShortDateString());
                    cmd.Parameters.AddWithValue("@valor2", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@valor3", 1);


                    // Execute o comando
                    var insertedId = Convert.ToInt32(cmd.ExecuteScalar());

                    idAula.Text = insertedId.ToString();

                    MessageBox.Show("Aula inicializada com sucesso!");

                    groupBox1.Enabled = true;
                    tabControl1.Enabled = false;

                    faceRec.openCamera(pictureBox2, pictureBox3);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocorreu um erro: {ex.Message}", "Erro ao Inicializar Aula");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<Aluno> alunosSemPresenca = new List<Aluno>();
            using (var connection = new MySqlConnection("Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;"))
            {
                connection.Open();

                var query = @"SELECT a.RA, a.Nome
                              FROM Aluno a
                              LEFT JOIN chamada p ON a.RA = p.RA AND p.IDAula = @idAula
                              WHERE p.RA IS NULL;";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idAula", (tabControl1.SelectedIndex == 0 ? idAula.Text : txtIdAula.Text));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var aluno = new Aluno
                            {
                                RA = reader.GetInt64("RA"),
                                Nome = reader.GetString("Nome")
                            };
                            alunosSemPresenca.Add(aluno);
                        }
                    }
                }

                connection.Close();
            }

            if (alunosSemPresenca.Count > 0)
            {
                foreach (var item in alunosSemPresenca)
                {
                    string connectionString = "Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;";

                    // Crie a conexão com o banco de dados
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();

                            // Defina o comando SQL para o UPDATE
                            string sql = "INSERT INTO CHAMADA (RA, IDAula, Presente) VALUES (@valor1, @valor2, @valor3);";

                            // Crie o objeto de comando
                            MySqlCommand cmd = new MySqlCommand(sql, conn);

                            // Adicione os parâmetros
                            cmd.Parameters.AddWithValue("@valor1", item.RA);
                            cmd.Parameters.AddWithValue("@valor2", (tabControl1.SelectedIndex == 0 ? idAula.Text : txtIdAula.Text));
                            cmd.Parameters.AddWithValue("@valor3", 0);

                            // Execute o comando
                            cmd.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ocorreu um erro: {ex.Message}", "Erro ao Inicializar Aula");
                        }
                        finally { conn.Close(); }
                    }
                }
            }

            string connectionString2 = "Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;";

            // Crie a conexão com o banco de dados
            using (MySqlConnection conn = new MySqlConnection(connectionString2))
            {
                try
                {
                    conn.Open();

                    // Defina o comando SQL para o UPDATE
                    string sql = "UPDATE aula SET StatusAula = 0 WHERE IDAula = " + (tabControl1.SelectedIndex == 0 ? idAula.Text : txtIdAula.Text) +"";

                    // Crie o objeto de comando
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Execute o comando
                    cmd.ExecuteNonQuery();

                    MessageBox.Show($"Chamada finalizada com sucesso"); 

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocorreu um erro: {ex.Message}", "Erro ao finalizar aula");
                }
                finally { conn.Close(); }
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    comboBox2.Text = "";
                    txtIdAula.Text = "";


                    break;

                case 1:

                    comboBox2.Items.Clear();

                    using (var connection = new MySqlConnection("Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;"))
                    {
                        connection.Open();

                        var query = "SELECT IDAula, Materia FROM Aula WHERE StatusAula = 1;";

                        using (var command = new MySqlCommand(query, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var item = new ComboboxItem
                                    {
                                        Text = reader.GetString("Materia"),
                                        Value = reader.GetInt64("IDAula")
                                    };
                                    comboBox2.Items.Add(item);
                                }
                            }
                        }
                    }

                    break;

                default:
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = (ComboboxItem)comboBox2.SelectedItem;
            txtIdAula.Text = selectedItem.Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            tabControl1.Enabled = false;

            faceRec.openCamera(pictureBox2, pictureBox3);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            faceRec.CloseCamera();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
