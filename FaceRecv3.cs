extern alias CV;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using MySql.Data.MySqlClient;
using TCC_DO_MANO;
namespace FaceRecognitionv3
{
    public class FaceRec : Form
    {
        private double distance = 1E+19;

        private CascadeClassifier cascadeClassifier = new CascadeClassifier(Environment.CurrentDirectory + "/Haarcascade/haarcascade_frontalface_alt.xml");

        private Image<Bgr, byte> Frame = null;

        private Capture camera;

        private Mat mat = new Mat();

        private List<Image<Gray, byte>> trainedFaces = new List<Image<Gray, byte>>();

        private List<int> PersonLabs = new List<int>();

        private bool isEnable_SaveImage = false;
        private bool isEnable_TookPicture = false;

        private PictureBox pictureBoxTookPick;

        private TextBox txtRA = null;
        private string idAula = null;

        private string ImageName;

        private PictureBox PictureBox_Frame;

        private PictureBox PictureBox_smallFrame;

        private string setPersonName;

        public bool isTrained = false;

        public bool salvandoFoto = false;

        public string bitmapfoto = null;

        private List<string> Names = new List<string>();

        private EigenFaceRecognizer eigenFaceRecognizer;
        private FisherFaceRecognizer fisherFaceRecognizer;
        private LBPHFaceRecognizer lBPHFaceRecognizer;

        private IContainer components = null;

        private string nomeDoArrombado = string.Empty;

        private List<string> imageBase64Array = new List<string>();

        // Adicione um contador para os frames
        private int frameCount = 0;
        private const int maxFrames = 10;

        public FaceRec()
        {
            InitializeComponent();
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Image"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Image");
            }
        }

        public void getPersonName(Control control)
        {
            Timer timer = new Timer();
            timer.Tick += timer_getPersonName_Tick;
            timer.Interval = 100;
            timer.Start();
            void timer_getPersonName_Tick(object sender, EventArgs e)
            {
                control.Text = setPersonName;
            }
        }

        public void openCamera(PictureBox pictureBox_Camera, PictureBox pictureBox_Trained)
        {
            PictureBox_Frame = pictureBox_Camera;
            PictureBox_smallFrame = pictureBox_Trained;
            camera = new Capture();
            camera.ImageGrabbed += Camera_ImageGrabbed;
            camera.Start();
        }

        public void Save_IMAGE(string imageName, out string bitmapFoto)
        {
            ImageName = imageName;
            isEnable_SaveImage = true;
            bitmapFoto = bitmapfoto;
        }

        public void Took_PICTURE(PictureBox tookPick,string ID_Aula)
        {
            pictureBoxTookPick = tookPick;
            idAula = ID_Aula;
            isEnable_TookPicture = true;
        }

        private void Camera_ImageGrabbed(object sender, EventArgs e)
        {
            camera.Retrieve(mat);
            Frame = mat.ToImage<Bgr, byte>().Resize(PictureBox_Frame.Width, PictureBox_Frame.Height, Inter.Cubic);
            detectFace();
            PictureBox_Frame.Image = Frame.Bitmap;
        }

        private void detectFace_old()
        {
            Image<Gray, byte> image = Frame.Convert<Gray, byte>();
            Mat mat = new Mat();
            CvInvoke.CvtColor(Frame, mat, ColorConversion.Bgr2Gray);
            CvInvoke.EqualizeHist(mat, mat);
            Rectangle[] array = cascadeClassifier.DetectMultiScale(mat, 1.1, 10, Size.Empty);
            if (array.Length != 0)
            {
                Rectangle[] array2 = array;
                foreach (Rectangle rectangle in array2)
                {
                    CvInvoke.Rectangle(Frame, rectangle, new Bgr(Color.LimeGreen).MCvScalar, 2);
                    SaveImage(rectangle);
                    image.ROI = rectangle;
                    trainedIamge();

                    checkName(image, rectangle);
                }
            }
            else
            {
                setPersonName = "";
            }
        }

        private void detectFace()
        {
            Image<Gray, byte> image = Frame.Convert<Gray, byte>();
            Mat mat = new Mat();
            CvInvoke.CvtColor(Frame, mat, ColorConversion.Bgr2Gray);
            CvInvoke.EqualizeHist(mat, mat);
            Rectangle[] array = cascadeClassifier.DetectMultiScale(mat, 1.1, 10, Size.Empty);
            if (array.Length != 0)
            {
                foreach (Rectangle rectangle in array)
                {
                    // Ajuste a porcentagem para aumentar a área do retângulo
                    int increasePercentage = 20;

                    // Calcule o aumento em pixels
                    int increaseWidth = rectangle.Width * increasePercentage / 100;
                    int increaseHeight = rectangle.Height * increasePercentage / 100;

                    // Aumente o tamanho do retângulo
                    Rectangle adjustedRectangle = new Rectangle(
                        Math.Max(0, rectangle.X - increaseWidth / 2),   // Garante que o retângulo não saia dos limites da imagem
                        Math.Max(0, rectangle.Y - increaseHeight / 2),  // Garante que o retângulo não saia dos limites da imagem
                        rectangle.Width + increaseWidth,
                        rectangle.Height + increaseHeight
                    );

                    // Desenhe o retângulo ajustado na imagem
                    CvInvoke.Rectangle(Frame, adjustedRectangle, new Bgr(Color.LimeGreen).MCvScalar, 2);

                    // Salve a imagem usando o retângulo ajustado
                    SaveImage(adjustedRectangle);

                    image.ROI = adjustedRectangle;
                    trainedIamge();

                    checkName(image, adjustedRectangle);
                }
            }
            else
            {
                setPersonName = "";
            }
        }


        //private void SaveImage(Rectangle face)
        //{
        //    if (isEnable_SaveImage)
        //    {
        //        Image<Gray, byte> image = Frame.Convert<Gray, byte>();
        //        image.ROI = face;
        //        image = image.Resize(100, 100, Inter.Cubic);
        //        isEnable_SaveImage = false;
        //        trainedIamge();


        //        var imageBitmap = image.ToBitmap();

        //        var imageBase64 = BitmapToBase64(imageBitmap);
        //    }
        //    if (isEnable_TookPicture)
        //    {
        //        isEnable_TookPicture = false;

        //        Image<Gray, byte> image = Frame.Convert<Gray, byte>();
        //        image.ROI = face;

        //        pictureBoxTookPick.Image = image.ToBitmap();

        //        image.Resize(100, 100, Inter.Cubic);

        //        CompararRostoComBanco(pictureBoxTookPick);
        //    }

        //}

        private void SaveImage(Rectangle face)
        {
            if (isEnable_SaveImage)
            {
                Image<Gray, byte> image = Frame.Convert<Gray, byte>();
                image.ROI = face;
                image = image.Resize(100, 100, Inter.Cubic);

                // Converte o frame para Bitmap
                var imageBitmap = image.ToBitmap();

                // Converte o Bitmap para Base64
                var imageBase64 = BitmapToBase64(imageBitmap);

                // Adiciona a imagem Base64 à lista
                imageBase64Array.Add(imageBase64);
                frameCount++;

                // Se atingiu o número máximo de frames, desabilita a captura
                if (frameCount >= maxFrames)
                {
                    isEnable_SaveImage = false;
                    frameCount = 0; // Reinicia o contador para a próxima captura
                                    // Aqui você pode fazer algo com o array de imagens, como salvar em um banco de dados ou arquivo
                                    // Por exemplo:
                                    // SalvarArrayDeImagens(imageBase64Array);

                    bitmapfoto = String.Join(",", imageBase64Array);
                    imageBase64Array.Clear(); // Limpa a lista após salvar
                }

                if (salvandoFoto)
                {
                    PictureBox_smallFrame.Image = image.ToBitmap();

                    salvandoFoto = false;
                }
            }
            if (isEnable_TookPicture)
            {
                isEnable_TookPicture = false;

                Image<Gray, byte> image = Frame.Convert<Gray, byte>();
                image.ROI = face;

                pictureBoxTookPick.Image = image.ToBitmap();

                image.Resize(100, 100, Inter.Cubic);

                CompararRostoComBanco(pictureBoxTookPick);
            }

        }

        private void trainedIamge_old()
        {
            try
            {
                int num = 0;
                trainedFaces.Clear();
                PersonLabs.Clear();
                Names.Clear();
                string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Image", "*.jpg", SearchOption.AllDirectories);
                string[] array = files;
                foreach (string text in array)
                {
                    Image<Gray, byte> item = new Image<Gray, byte>(text);
                    trainedFaces.Add(item);
                    PersonLabs.Add(num);
                    Names.Add(text);
                    num++;
                }

                //eigenFaceRecognizer = new EigenFaceRecognizer(num, distance);
                //fisherFaceRecognizer = new FisherFaceRecognizer(num, distance);
                //lBPHFaceRecognizer = new LBPHFaceRecognizer(1, 8, 8, 8, distance);

                //eigenFaceRecognizer.Train(trainedFaces.ToArray(), PersonLabs.ToArray());

            }
            catch
            {
            }
        }

        private void trainedIamge()
        {
            try
            {
                int num = 0;
                trainedFaces.Clear();
                PersonLabs.Clear();
                Names.Clear();

                List<FotoAlunoVO> base64Images = GetBase64ImagesFromDatabase();
                List<string> base64Strings = new List<string>();


                foreach (var item in base64Images)
                {
                    var arrayFoto = item.Foto.Split(',');

                    foreach (var itemFoto in arrayFoto)
                    {
                        base64Strings.Add(itemFoto);
                    }


                }

                string[] files = base64Strings.ToArray();
                string[] array = files;
                foreach (string text in array)
                {
                    Image<Gray, byte> item = new Image<Gray, byte>(Base64ToBitmap(text)).Resize(100, 100, Inter.Cubic);
                    trainedFaces.Add(item);
                    PersonLabs.Add(num);
                    Names.Add(base64Images.Find(x => x.Foto == text).Nome);
                    num++;
                }

                eigenFaceRecognizer = new EigenFaceRecognizer(num, distance);
                fisherFaceRecognizer = new FisherFaceRecognizer(num, 300);
                lBPHFaceRecognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);

                if (trainedFaces.Count > 0)
                {
                    eigenFaceRecognizer.Train(trainedFaces.ToArray(), PersonLabs.ToArray());
                    fisherFaceRecognizer.Train(trainedFaces.ToArray(), PersonLabs.ToArray());
                    lBPHFaceRecognizer.Train(trainedFaces.ToArray(), PersonLabs.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); //sera que em outrosarquivo sera que esta falando algum parte de conexao ja que nao percebi duas linhas 
            }
        }

        private void checkName(Image<Gray, byte> resultImage, Rectangle face)
        {
            try
            {
                if (isTrained)
                {
                    Image<Gray, byte> image = resultImage.Convert<Gray, byte>().Resize(100, 100, Inter.Cubic);
                    CvInvoke.EqualizeHist(image, image);
                    FaceRecognizer.PredictionResult predictionResult = eigenFaceRecognizer.Predict(image);
                    if (predictionResult.Label != -1 && predictionResult.Distance < distance)
                    {
                        PictureBox_smallFrame.Image = trainedFaces[predictionResult.Label].Bitmap;
                        setPersonName = Names[predictionResult.Label].Replace(Environment.CurrentDirectory + "\\Image\\", "").Replace(".jpg", "");
                        CvInvoke.PutText(Frame, setPersonName, new Point(face.X - 2, face.Y - 2), FontFace.HersheyPlain, 1.0, new Bgr(Color.LimeGreen).MCvScalar);
                    }
                    else
                    {
                        CvInvoke.PutText(Frame, "Unknown", new Point(face.X - 2, face.Y - 2), FontFace.HersheyPlain, 1.0, new Bgr(Color.OrangeRed).MCvScalar);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(800, 450);
            base.Name = "FaceRec";
            Text = "FaceRec";
            ResumeLayout(false);
        }

        public string BitmapToBase64(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {


                // Salva o bitmap no MemoryStream no formato JPEG (ou outro formato, se preferir)
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                // Converte o conteúdo do MemoryStream em um array de bytes
                byte[] imageBytes = memoryStream.ToArray();

                // Converte o array de bytes em uma string Base64
                string base64String = Convert.ToBase64String(imageBytes);

                return base64String;
            }
        }

        public List<FotoAlunoVO> GetBase64ImagesFromDatabase()
        {
            List<FotoAlunoVO> base64Images = new List<FotoAlunoVO>();

            // Exemplo de busca no banco de dados
            string connectionString = "Server=127.0.0.1;Database=tcc_escola;User ID=root;Password=246810;"; 
            string query = "SELECT FOTO, NOME, RA FROM Aluno"; // Altere 'Base64Image' e 'Imagens' conforme a estrutura do seu banco

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();

                using (MySqlDataReader reader = command.ExecuteReader()) 
                {
                    while (reader.Read())
                    {
                        FotoAlunoVO fotoAlunoVO = new FotoAlunoVO();

                        if (reader["FOTO"].ToString() == "") continue;

                        var foto = reader["FOTO"].ToString();
                        var nome = reader["NOME"].ToString();
                        var ra = reader["RA"].ToString();

                        if (foto.Split(',').Length > 2)
                        {
                            foreach (var item in foto.Split(','))
                            {
                                fotoAlunoVO.Foto = item;
                                fotoAlunoVO.Nome = nome;
                                fotoAlunoVO.RA = ra;

                                base64Images.Add(fotoAlunoVO);
                            }
                        }
                        else
                        {
                            fotoAlunoVO.Foto = foto;
                            fotoAlunoVO.Nome = nome;
                            fotoAlunoVO.RA = ra;

                            base64Images.Add(fotoAlunoVO);
                        }
                    }
                }
            }

            return base64Images;
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

        public void CompararRostoComBanco(PictureBox pictureBox)
        {
            try
            {
                // Captura a imagem atual do PictureBox e a redimensiona
                Image<Bgr, byte> frameImage = new Image<Bgr, byte>((Bitmap)pictureBox.Image).Resize(100, 100, Inter.Cubic);

                // Converte para escala de cinza
                Image<Gray, byte> grayFrameImage = frameImage.Convert<Gray, byte>();

                // Equaliza o histograma para melhorar a detecção de feições
                CvInvoke.EqualizeHist(grayFrameImage, grayFrameImage);

                //Lista de fotos do banco de dados
                List<FotoAlunoVO> base64Images = GetBase64ImagesFromDatabase();

                // Cria listas para armazenar as imagens e labels
                List<Image<Gray, byte>> trainedFaces = new List<Image<Gray, byte>>();
                List<int> labels = new List<int>();
                List<string> names = new List<string>();
                List<string> ra = new List<string>();

                int num = 0;

                foreach (var item in base64Images)
                {
                    // Converte a string Base64 para Bitmap
                    Bitmap bitmapDB = Base64ToBitmap(item.Foto);

                    // Converte o Bitmap para uma imagem EmguCV em escala de cinza e redimensiona
                    Image<Gray, byte> dbImage = new Image<Gray, byte>(bitmapDB).Resize(100, 100, Inter.Cubic);

                    // Equaliza o histograma da imagem do banco de dados
                    CvInvoke.EqualizeHist(dbImage, dbImage);

                    // Adiciona a imagem e o label
                    trainedFaces.Add(dbImage);
                    labels.Add(num);
                    names.Add(item.Nome);
                    ra.Add(item.RA);
                    num++;
                }

                //// Cria o reconhecedor de faces e treina com as imagens do banco de dados
                //var recognizer = new EigenFaceRecognizer(trainedFaces.Count, double.PositiveInfinity);
                //recognizer.Train(trainedFaces.ToArray(), labels.ToArray());


                //LBPHFaceRecognizer lBPHFaceRecognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);

                //var fisherFaceRecognizer = new FisherFaceRecognizer(0, 100);
                //fisherFaceRecognizer.Train(trainedFaces.ToArray(), labels.ToArray());


                //lBPHFaceRecognizer.Train(trainedFaces.ToArray(), labels.ToArray());

                var predictLB = lBPHFaceRecognizer.Predict(grayFrameImage);

                var predictFisher = fisherFaceRecognizer.Predict(grayFrameImage);

                // Prediz o rosto no frame
                var prediction = eigenFaceRecognizer.Predict(grayFrameImage);

                // Verifica se há correspondência e se a distância é aceitável
                //if (prediction.Label != -1 && prediction.Distance < 6000 && predictLB.Distance < 100 && predictFisher.Distance < 300) // Ajuste o valor da distância conforme necessário
                if (predictLB.Label != -1 && predictLB.Distance < 100)
                {
                    var result = MessageBox.Show($"Correspondência encontrada! Nome: {names[predictLB.Label]} é você ?","Confirmação de Aluno", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
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
                                cmd.Parameters.AddWithValue("@valor1", ra[predictLB.Label]);
                                cmd.Parameters.AddWithValue("@valor2", idAula);
                                cmd.Parameters.AddWithValue("@valor3", 1);

                                // Execute o comando
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Presença contabilizada com sucesso!");

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Ocorreu um erro: {ex.Message}", "Erro ao Inicializar Aula");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Ocorreu uma falha na identificação do rosto. Favor comunicar professor", "Falha ao identificar aluno");
                    }
                }
                else
                {
                    MessageBox.Show("Aluno não cadastrado!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CloseCamera()
        {
            if (camera != null)
            {
                camera.Stop();  // Para a captura de vídeo
                camera.Dispose();  // Libera os recursos da câmera
                camera = null;  // Remove a referência para o objeto de captura
            }
        }
    }
}