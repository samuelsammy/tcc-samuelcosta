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
    public partial class ConsultarPresenca : Form
    {
        public ConsultarPresenca()
        {
            InitializeComponent();
        }

        private int ID_Chamada = 0;

        private void ConsultarPresenca_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            using (var connection = new MySqlConnection("Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;"))
            {
                connection.Open();

                var query = "SELECT IDAula, Materia FROM Aula;";

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
                            comboBox1.Items.Add(item);
                        }
                    }
                }

                connection.Close();
            }

            if (Globals.CargoUser == 1)
            {
                using (var connection = new MySqlConnection("Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;"))
                {
                    connection.Open();

                    var query = @"
                                SELECT 
                                    al.RA, 
                                    al.Nome, 
                                    au.Materia, 
                                    au.DataAula, 
                                    au.IDAula,
                                    p.ID_Chamada,
                                    CASE 
                                        WHEN p.Presente = 1 THEN 'Presente'
                                        ELSE 'Falta'
                                    END AS StatusPresenca
                                FROM 
                                    Aluno al
                                LEFT JOIN 
                                    chamada p ON al.RA = p.RA
                                LEFT JOIN 
                                    Aula au ON au.IDAula = p.IDAula;";

                    using (var adapter = new MySqlDataAdapter(query, connection))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;

                        dataGridView1.Columns["ID_Chamada"].Visible = false;
                    }
                }
            }
            else if (Globals.CargoUser == 0)
            {
                txtRA.Text = Globals.RA.ToString();
                txtRA.Enabled = false;

                using (var connection = new MySqlConnection("Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;"))
                {
                    connection.Open();

                    var query = @"
                                SELECT 
                                    al.RA, 
                                    al.Nome, 
                                    au.Materia, 
                                    au.DataAula, 
                                    au.IDAula,
                                    p.ID_Chamada,
                                    CASE 
                                        WHEN p.Presente = 1 THEN 'Presente'
                                        ELSE 'Falta'
                                    END AS StatusPresenca
                                FROM 
                                    Aluno al
                                LEFT JOIN 
                                    chamada p ON al.RA = p.RA
                                LEFT JOIN 
                                    Aula au ON au.IDAula = p.IDAula
                                WHERE
                                    al.RA = " + Globals.RA + ";";





                    using (var adapter = new MySqlDataAdapter(query, connection))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;

                        dataGridView1.Columns["ID_Chamada"].Visible = false;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var connection = new MySqlConnection("Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;"))
            {
                connection.Open();

                var query = new StringBuilder(@"
                                                 SELECT 
                                                     al.RA, 
                                                     al.Nome, 
                                                     au.Materia, 
                                                     au.DataAula, 
                                                     au.IDAula,
                                                     CASE 
                                                         WHEN p.Presente = 1 THEN 'Presente'
                                                         ELSE 'Falta'
                                                     END AS StatusPresenca
                                                 FROM 
                                                     Aluno al
                                                 LEFT JOIN 
                                                     chamada p ON al.RA = p.RA
                                                 LEFT JOIN 
                                                     Aula au ON au.IDAula = p.IDAula");

                // Construir a cláusula WHERE dinamicamente
                var whereClause = new List<string>();

                if (!string.IsNullOrEmpty(txtRA.Text))
                {
                    whereClause.Add("al.RA = @RA");
                }

                if (!string.IsNullOrEmpty(comboBox1.Text))
                {
                    whereClause.Add("au.IDAula LIKE @IDAula");
                }

                if (!string.IsNullOrEmpty(dateTimePicker1.Text))
                {
                    whereClause.Add("au.DataAula LIKE @DataAula");
                }

                // Se houver filtros, adiciona a cláusula WHERE
                if (whereClause.Count > 0)
                {
                    query.Append(" WHERE " + string.Join(" AND ", whereClause));
                }

                using (var command = new MySqlCommand(query.ToString(), connection))
                {
                    // Adicionar parâmetros aos filtros
                    if (!string.IsNullOrEmpty(txtRA.Text))
                    {
                        command.Parameters.AddWithValue("@RA", txtRA.Text);
                    }

                    if (!string.IsNullOrEmpty(comboBox1.Text))
                    {
                        var selectedItem = (ComboboxItem)comboBox1.SelectedItem;
                        var idAula = selectedItem.Value.ToString();

                        command.Parameters.AddWithValue("@IDAula", "%" + idAula + "%");
                    }

                    if (dateTimePicker1.Value != null)
                    {
                        command.Parameters.AddWithValue("@DataAula", dateTimePicker1.Value.ToShortDateString());
                    }

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Globals.CargoUser != 1)
                return;

            if (e.RowIndex >= 0)
            {
                // Obtém a linha clicada
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Obtém o ID ou outra chave primária do registro
                long RA = Convert.ToInt64(row.Cells["RA"].Value); // Altere "id" para o nome da sua coluna de chave primária
                string presente = row.Cells["StatusPresenca"].Value.ToString();
                int idAula = Convert.ToInt32(row.Cells["IDAula"].Value);
                string materia = row.Cells["Materia"].Value.ToString();
                ID_Chamada = Convert.ToInt32(row.Cells["ID_Chamada"].Value);

                groupBox2.Enabled = true;

                txtRA2.Text = RA.ToString();
                txtMateria.Text = materia.ToString();
                checkBox1.Checked = presente.Equals("Presente");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;";

            // Crie a conexão com o banco de dados
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Defina o comando SQL para o UPDATE
                    string sql = "UPDATE Chamada SET Presente = @valor1  WHERE ID_Chamada = @condicao";

                    // Crie o objeto de comando
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    // Adicione os parâmetros
                    cmd.Parameters.AddWithValue("@valor1", checkBox1.Checked);


                    cmd.Parameters.AddWithValue("@condicao", $"{ID_Chamada}");

                    // Execute o comando
                    int rowsAffected = cmd.ExecuteNonQuery();

                    MessageBox.Show("Presença atualizada com sucesso!");
                    ID_Chamada = 0;

                    txtRA2.Text = "";
                    txtMateria.Text = "";
                    checkBox1.Checked = false;

                    groupBox2.Enabled = false;
                    
                    LoadDataGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocorreu um erro: {ex.Message}", "Erro Atualização Cadastral");
                }
            }
        }

        public void LoadDataGrid()
        {
            comboBox1.Items.Clear();

            using (var connection = new MySqlConnection("Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;"))
            {
                connection.Open();

                var query = "SELECT IDAula, Materia FROM Aula;";

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
                            comboBox1.Items.Add(item);
                        }
                    }
                }

                connection.Close();
            }

            if (Globals.CargoUser == 1)
            {
                using (var connection = new MySqlConnection("Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;"))
                {
                    connection.Open();

                    var query = @"
                                SELECT 
                                    al.RA, 
                                    al.Nome, 
                                    au.Materia, 
                                    au.DataAula, 
                                    au.IDAula,
                                    p.ID_Chamada,
                                    CASE 
                                        WHEN p.Presente = 1 THEN 'Presente'
                                        ELSE 'Falta'
                                    END AS StatusPresenca
                                FROM 
                                    Aluno al
                                LEFT JOIN 
                                    chamada p ON al.RA = p.RA
                                LEFT JOIN 
                                    Aula au ON au.IDAula = p.IDAula;";

                    using (var adapter = new MySqlDataAdapter(query, connection))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;

                        dataGridView1.Columns["ID_Chamada"].Visible = false;
                    }
                }
            }
            else if (Globals.CargoUser == 0)
            {
                txtRA.Text = Globals.RA.ToString();
                txtRA.Enabled = false;

                using (var connection = new MySqlConnection("Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;"))
                {
                    connection.Open();

                    var query = @"
                                SELECT 
                                    al.RA, 
                                    al.Nome, 
                                    au.Materia, 
                                    au.DataAula, 
                                    au.IDAula,
                                    p.ID_Chamada,
                                    CASE 
                                        WHEN p.Presente = 1 THEN 'Presente'
                                        ELSE 'Falta'
                                    END AS StatusPresenca
                                FROM 
                                    Aluno al
                                LEFT JOIN 
                                    chamada p ON al.RA = p.RA
                                LEFT JOIN 
                                    Aula au ON au.IDAula = p.IDAula
                                WHERE
                                    al.RA = " + Globals.RA + ";";





                    using (var adapter = new MySqlDataAdapter(query, connection))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;

                        dataGridView1.Columns["ID_Chamada"].Visible = false;
                    }
                }
            }
        }
    }
}
