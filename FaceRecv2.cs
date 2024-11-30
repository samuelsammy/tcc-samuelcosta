using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FaceRecognitionExample
{
    public class FaceRec
    {
        private readonly CascadeClassifier _cascadeClassifier;
        private readonly EigenFaceRecognizer _eigenFaceRecognizer;
        private readonly List<Image<Gray, byte>> _trainedFaces = new List<Image<Gray, byte>>();
        private readonly List<int> _personLabels = new List<int>();
        private readonly List<string> _names = new List<string>();
        private double _recognitionThreshold = 5000;
        private bool _isTrained = false;
        private Capture _camera;
        private string _imageName;
        private bool _isEnableSaveImage = false;

        public FaceRec()
        {
            _cascadeClassifier = new CascadeClassifier("C:/Users/Samuel Costa/Desktop/TCC_Final/TCC_DO_MANO/TCC_DO_MANO/bin/Debug/Haarcascade/haarcascade_frontalface_alt.xml");
            _eigenFaceRecognizer = new EigenFaceRecognizer();
        }

        public void StartCamera(PictureBox pictureBoxFrame, PictureBox pictureBoxSmallFrame)
        {
            _camera = new Capture();
            _camera.ImageGrabbed += (sender, e) => ProcessFrame(pictureBoxFrame, pictureBoxSmallFrame);
            _camera.Start();
        }

        public void SaveImage(string imageName)
        {
            _imageName = imageName;
            _isEnableSaveImage = true;
        }

        private void ProcessFrame(PictureBox pictureBoxFrame, PictureBox pictureBoxSmallFrame)
        {
            var mat = new Mat();
            _camera.Retrieve(mat);
            var frame = mat.ToImage<Bgr, byte>().Resize(pictureBoxFrame.Width, pictureBoxFrame.Height, Inter.Cubic);
            DetectFace(frame, pictureBoxSmallFrame);
            pictureBoxFrame.Image = frame.Bitmap;
        }

        private void DetectFace(Image<Bgr, byte> frame, PictureBox pictureBoxSmallFrame)
        {
            var grayFrame = frame.Convert<Gray, byte>();
            CvInvoke.EqualizeHist(grayFrame, grayFrame);
            var faces = _cascadeClassifier.DetectMultiScale(grayFrame, 1.1, 10, new Size(20, 20));

            foreach (var face in faces)
            {
                CvInvoke.Rectangle(frame, face, new Bgr(Color.LimeGreen).MCvScalar, 2);
                if (_isEnableSaveImage)
                {
                    SaveDetectedFace(frame, face);
                    _isEnableSaveImage = false;
                }
                if (_isTrained)
                {
                    RecognizeFace(grayFrame, face, pictureBoxSmallFrame);
                }
            }
        }

        private void SaveDetectedFace(Image<Bgr, byte> frame, Rectangle face)
        {
            var faceImage = frame.Copy(face).Convert<Gray, byte>().Resize(100, 100, Inter.Cubic);
            faceImage.Save(Path.Combine("Image", $"{_imageName}.jpg"));
            TrainRecognizer();
        }

        private void TrainRecognizer()
        {
            _trainedFaces.Clear();
            _personLabels.Clear();
            _names.Clear();

            var files = Directory.GetFiles("Image", "*.jpg", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                _trainedFaces.Add(new Image<Gray, byte>(files[i]));
                _personLabels.Add(i);
                _names.Add(Path.GetFileNameWithoutExtension(files[i]));
            }

            _eigenFaceRecognizer.Train(_trainedFaces.ToArray(), _personLabels.ToArray());
            _isTrained = true;
        }

        private void RecognizeFace(Image<Gray, byte> grayFrame, Rectangle face, PictureBox pictureBoxSmallFrame)
        {
            var detectedFace = grayFrame.Copy(face).Resize(100, 100, Inter.Cubic);
            CvInvoke.EqualizeHist(detectedFace, detectedFace);

            var result = _eigenFaceRecognizer.Predict(detectedFace);
            if (result.Label != -1 && result.Distance < _recognitionThreshold)
            {
                pictureBoxSmallFrame.Image = _trainedFaces[result.Label].Bitmap;
                CvInvoke.PutText(grayFrame, _names[result.Label], new Point(face.X - 2, face.Y - 2), FontFace.HersheyPlain, 1.0, new Bgr(Color.LimeGreen).MCvScalar);
            }
            else
            {
                CvInvoke.PutText(grayFrame, "Unknown", new Point(face.X - 2, face.Y - 2), FontFace.HersheyPlain, 1.0, new Bgr(Color.OrangeRed).MCvScalar);
            }
        }

        public void DetectAndLabelFace(string imagePath, PictureBox pictureBox)
        {
            try
            {
                // Carrega a imagem a partir do caminho fornecido
                Image<Bgr, byte> image = new Image<Bgr, byte>(imagePath);

                // Converte a imagem para tons de cinza
                Image<Gray, byte> grayImage = image.Convert<Gray, byte>();

                // Equaliza o histograma para melhorar o contraste
                CvInvoke.EqualizeHist(grayImage, grayImage);

                // Detecta rostos na imagem
                Rectangle[] faces = _cascadeClassifier.DetectMultiScale(grayImage, 1.1, 10, new Size(20, 20));

                foreach (var face in faces)
                {
                    // Desenha um retângulo ao redor do rosto detectado
                    CvInvoke.Rectangle(image, face, new Bgr(Color.LimeGreen).MCvScalar, 2);

                    // Tenta reconhecer o rosto
                    if (_isTrained)
                    {
                        Image<Gray, byte> detectedFace = grayImage.Copy(face).Resize(100, 100, Inter.Cubic);
                        CvInvoke.EqualizeHist(detectedFace, detectedFace);

                        var result = _eigenFaceRecognizer.Predict(detectedFace);

                        if (result.Label != -1 && result.Distance < _recognitionThreshold)
                        {
                            // Desenha o nome da pessoa reconhecida acima do retângulo
                            string name = _names[result.Label];
                            CvInvoke.PutText(image, name, new Point(face.X, face.Y - 10), FontFace.HersheyPlain, 1.0, new Bgr(Color.LimeGreen).MCvScalar);
                        }
                        else
                        {
                            // Se não for reconhecido, coloca "Unknown"
                            CvInvoke.PutText(image, "Unknown", new Point(face.X, face.Y - 10), FontFace.HersheyPlain, 1.0, new Bgr(Color.OrangeRed).MCvScalar);
                        }
                    }
                }

                // Mostra a imagem processada no PictureBox
                pictureBox.Image = image.Bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao detectar e rotular rosto: {ex.Message}");
            }
        }
    }
}
