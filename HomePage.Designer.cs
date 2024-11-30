namespace TCC_DO_MANO
{
    partial class HomePage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomePage));
            this.btnAtualizarAluno = new System.Windows.Forms.Button();
            this.btnConfirmarPresenca = new System.Windows.Forms.Button();
            this.btnConsultarPresenca = new System.Windows.Forms.Button();
            this.btnCadastrarAluno = new System.Windows.Forms.Button();
            this.lblDataHora = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gerenciador = new System.Windows.Forms.GroupBox();
            this.lblFuncao = new System.Windows.Forms.Label();
            this.identificacao = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gerenciador.SuspendLayout();
            this.identificacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAtualizarAluno
            // 
            this.btnAtualizarAluno.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizarAluno.Location = new System.Drawing.Point(5, 304);
            this.btnAtualizarAluno.Name = "btnAtualizarAluno";
            this.btnAtualizarAluno.Size = new System.Drawing.Size(148, 63);
            this.btnAtualizarAluno.TabIndex = 0;
            this.btnAtualizarAluno.Text = "Atualização Cadastral";
            this.btnAtualizarAluno.UseVisualStyleBackColor = true;
            this.btnAtualizarAluno.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnConfirmarPresenca
            // 
            this.btnConfirmarPresenca.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmarPresenca.Location = new System.Drawing.Point(646, 304);
            this.btnConfirmarPresenca.Name = "btnConfirmarPresenca";
            this.btnConfirmarPresenca.Size = new System.Drawing.Size(148, 63);
            this.btnConfirmarPresenca.TabIndex = 1;
            this.btnConfirmarPresenca.Text = "Confirmar Presença";
            this.btnConfirmarPresenca.UseVisualStyleBackColor = true;
            this.btnConfirmarPresenca.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnConsultarPresenca
            // 
            this.btnConsultarPresenca.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultarPresenca.Location = new System.Drawing.Point(646, 235);
            this.btnConsultarPresenca.Name = "btnConsultarPresenca";
            this.btnConsultarPresenca.Size = new System.Drawing.Size(148, 63);
            this.btnConsultarPresenca.TabIndex = 2;
            this.btnConsultarPresenca.Text = "Consultar Presença";
            this.btnConsultarPresenca.UseVisualStyleBackColor = true;
            this.btnConsultarPresenca.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnCadastrarAluno
            // 
            this.btnCadastrarAluno.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadastrarAluno.Location = new System.Drawing.Point(6, 235);
            this.btnCadastrarAluno.Name = "btnCadastrarAluno";
            this.btnCadastrarAluno.Size = new System.Drawing.Size(148, 63);
            this.btnCadastrarAluno.TabIndex = 4;
            this.btnCadastrarAluno.Text = "Cadastrar Aluno";
            this.btnCadastrarAluno.UseVisualStyleBackColor = true;
            this.btnCadastrarAluno.Click += new System.EventHandler(this.button4_Click);
            // 
            // lblDataHora
            // 
            this.lblDataHora.AutoSize = true;
            this.lblDataHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataHora.Location = new System.Drawing.Point(12, 9);
            this.lblDataHora.Name = "lblDataHora";
            this.lblDataHora.Size = new System.Drawing.Size(70, 25);
            this.lblDataHora.TabIndex = 5;
            this.lblDataHora.Text = "label1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(251, 50);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(302, 317);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // gerenciador
            // 
            this.gerenciador.Controls.Add(this.label2);
            this.gerenciador.Controls.Add(this.lblFuncao);
            this.gerenciador.Controls.Add(this.btnCadastrarAluno);
            this.gerenciador.Controls.Add(this.pictureBox1);
            this.gerenciador.Controls.Add(this.btnConsultarPresenca);
            this.gerenciador.Controls.Add(this.btnAtualizarAluno);
            this.gerenciador.Controls.Add(this.btnConfirmarPresenca);
            this.gerenciador.Enabled = false;
            this.gerenciador.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gerenciador.Location = new System.Drawing.Point(12, 152);
            this.gerenciador.Name = "gerenciador";
            this.gerenciador.Size = new System.Drawing.Size(800, 378);
            this.gerenciador.TabIndex = 7;
            this.gerenciador.TabStop = false;
            this.gerenciador.Text = "Gerenciador";
            // 
            // lblFuncao
            // 
            this.lblFuncao.AutoSize = true;
            this.lblFuncao.Location = new System.Drawing.Point(10, 39);
            this.lblFuncao.Name = "lblFuncao";
            this.lblFuncao.Size = new System.Drawing.Size(0, 20);
            this.lblFuncao.TabIndex = 7;
            // 
            // identificacao
            // 
            this.identificacao.Controls.Add(this.radioButton3);
            this.identificacao.Controls.Add(this.radioButton2);
            this.identificacao.Controls.Add(this.radioButton1);
            this.identificacao.Controls.Add(this.button5);
            this.identificacao.Controls.Add(this.textBox1);
            this.identificacao.Controls.Add(this.label1);
            this.identificacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.identificacao.Location = new System.Drawing.Point(12, 37);
            this.identificacao.Name = "identificacao";
            this.identificacao.Size = new System.Drawing.Size(800, 117);
            this.identificacao.TabIndex = 8;
            this.identificacao.TabStop = false;
            this.identificacao.Text = "Identificação";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(380, 85);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(95, 24);
            this.radioButton3.TabIndex = 5;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Professor";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(380, 57);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(100, 24);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Secretaria";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(380, 30);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(50, 24);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "RA";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(699, 76);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(95, 33);
            this.button5.TabIndex = 2;
            this.button5.Text = "Validar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(14, 55);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(322, 26);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Digite seu RA ou a Senha de Administração";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(735, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Logout";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // HomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 542);
            this.Controls.Add(this.identificacao);
            this.Controls.Add(this.gerenciador);
            this.Controls.Add(this.lblDataHora);
            this.Name = "HomePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HomePage";
            this.Load += new System.EventHandler(this.HomePage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gerenciador.ResumeLayout(false);
            this.gerenciador.PerformLayout();
            this.identificacao.ResumeLayout(false);
            this.identificacao.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAtualizarAluno;
        private System.Windows.Forms.Button btnConfirmarPresenca;
        private System.Windows.Forms.Button btnConsultarPresenca;
        private System.Windows.Forms.Button btnCadastrarAluno;
        private System.Windows.Forms.Label lblDataHora;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox gerenciador;
        private System.Windows.Forms.GroupBox identificacao;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label lblFuncao;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Label label2;
    }
}