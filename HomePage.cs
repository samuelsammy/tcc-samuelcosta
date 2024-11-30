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
using static TCC_DO_MANO.Form1;

namespace TCC_DO_MANO
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
            timer1.Start();
            radioButton1.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 cadastrar_ = new Form1();

            cadastrar_.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cadastrar_Rosto cadastrar = new Cadastrar_Rosto();

            cadastrar.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CadastrarAluno cad = new CadastrarAluno();
            cad.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDataHora.Text = "Data e Hora: " + DateTime.Now.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ConsultarPresenca consultar = new ConsultarPresenca();
            consultar.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                using (var connection = new MySqlConnection("Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;"))
                {
                    connection.Open();

                    var query = "SELECT RA FROM Aluno Where RA = " + Convert.ToInt64(textBox1.Text) + ";";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                lblFuncao.Text = "Função: Aluno";
                                
                                identificacao.Enabled = false;
                                gerenciador.Enabled = true;

                                btnConsultarPresenca.Enabled = true;
                                btnConfirmarPresenca.Enabled = false;

                                btnCadastrarAluno.Enabled = false;
                                btnAtualizarAluno.Enabled = false;

                                Globals.CargoUser = 0;
                                Globals.UserName = "Aluno";
                                Globals.RA = Convert.ToInt64(textBox1.Text);

                                textBox1.Text = "";
                            }
                            else
                            {
                                MessageBox.Show("RA não encontrada na base.", "Falha na autenticação");
                                textBox1.Text = "";
                            }
                        }
                    }

                    connection.Close();
                }
            }
            else if (radioButton2.Checked)
            {

                if (textBox1.Text == "Admin@1234")
                {
                    gerenciador.Enabled = true;
                    textBox1.Text = "";
                    lblFuncao.Text = "Função: Secretaria";
                    identificacao.Enabled = false;

                    btnConsultarPresenca.Enabled = false;
                    btnConfirmarPresenca.Enabled = false;

                    btnCadastrarAluno.Enabled = true;
                    btnAtualizarAluno.Enabled = true;

                    Globals.CargoUser = 2;
                    Globals.UserName = "Secreteria";
                }
                else
                {
                    MessageBox.Show("Senh Incorreta !", "Falha na autenticação");
                    textBox1.Text = "";
                }
            }
            else if (radioButton3.Checked)
            {
                if (textBox1.Text == "Prof@1234")
                {
                    gerenciador.Enabled = true;
                    textBox1.Text = "";
                    lblFuncao.Text = "Função: Professor";
                    identificacao.Enabled = false;

                    btnConsultarPresenca.Enabled = true;
                    btnConfirmarPresenca.Enabled = true;

                    btnCadastrarAluno.Enabled = false;
                    btnAtualizarAluno.Enabled = false;

                    Globals.CargoUser = 1;
                    Globals.UserName = "Professor";
                }
                else
                {
                    MessageBox.Show("Senh Incorreta !", "Falha na autenticação");
                    textBox1.Text = "";
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox1.Text = "";
                textBox1.PasswordChar = '*';
            }
            else
            {
                textBox1.Text = "";
                textBox1.PasswordChar = '\0';
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                textBox1.Text = "";
                textBox1.PasswordChar = '*';
            }
            else
            {
                textBox1.Text = "";
                textBox1.PasswordChar = '\0';
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            gerenciador.Enabled = false;
            lblFuncao.Text = string.Empty;
            identificacao.Enabled = true;
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            label2.MouseHover += new EventHandler(Label2_MouseHover);
            label2.MouseLeave += new EventHandler(Label2_MouseLeave);
        }

        private void Label2_MouseHover(object sender, EventArgs e)
        {
            // Alterar o cursor quando o mouse estiver sobre o Label
            label2.Cursor = Cursors.Hand;
        }

        private void Label2_MouseLeave(object sender, EventArgs e)
        {
            // Voltar ao cursor padrão quando o mouse sair do Label
            label2.Cursor = Cursors.Default;
        }
    }
}
