using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCC_DO_MANO
{
    public class Comparadores
    {
        /*
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

                // Lista de fotos do banco de dados
                List<FotoAlunoVO> base64Images = GetBase64ImagesFromDatabase();

                // Cria listas para armazenar as imagens e labels
                List<Image<Gray, byte>> trainedFaces = new List<Image<Gray, byte>>();
                List<int> labels = new List<int>();
                List<string> names = new List<string>();

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
                    num++;
                }

                // Cria o reconhecedor de faces e treina com as imagens do banco de dados
                var recognizer = new EigenFaceRecognizer(trainedFaces.Count, double.PositiveInfinity);
                recognizer.Train(trainedFaces.ToArray(), labels.ToArray());


                LBPHFaceRecognizer lBPHFaceRecognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);

                lBPHFaceRecognizer.Train(trainedFaces.ToArray(), labels.ToArray());

                var predictLB = lBPHFaceRecognizer.Predict(grayFrameImage);

                // Prediz o rosto no frame
                var prediction = recognizer.Predict(grayFrameImage);

                // Verifica se há correspondência e se a distância é aceitável
                if (prediction.Label != -1 && ((prediction.Distance < 5000 && predictLB.Distance > 50) || (predictLB.Distance > 80 && predictLB.Distance <= 100 && prediction.Distance < 10000))) // Ajuste o valor da distância conforme necessário
                {
                    MessageBox.Show($"Correspondência encontrada! Nome: {names[prediction.Label]}");
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

        */
    }
}
