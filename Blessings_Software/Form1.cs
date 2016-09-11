using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using System.IO;

namespace Blessings_Software
{
    public partial class Form1 : Form
    {
        public string imagepath = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private async Task<Emotion[]> UploadAndDetectEmotions(string imageFilePath)
        {
            string subscriptionKey = "c1535e7f2cb74b998131a4ed71ac36b5";
            Console.WriteLine("EmotionServiceClient is created");
            EmotionServiceClient emotionServiceClient = new EmotionServiceClient(subscriptionKey);
            Console.WriteLine("Calling EmotionServiceClient.RecognizeAsync()...");
            try
            {
                Emotion[] emotionResult;
                using (Stream imageFileStream = File.OpenRead(imageFilePath))
                {
                    emotionResult = await emotionServiceClient.RecognizeAsync(imageFileStream);
                    return emotionResult;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                return null;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.jpg|*.jpg";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != null)
            {
                imagepath = openFileDialog1.FileName;
                pictureBox1.ImageLocation = imagepath;
                Emotion[] emotionResult = await UploadAndDetectEmotions(imagepath);
                LogEmotionResult(emotionResult);
            }
            
        }
        public void LogEmotionResult(Emotion[] emotionResult)
        {
            int emotionResultCount = 0;
            if (emotionResult != null && emotionResult.Length > 0)
            {
                foreach (Emotion emotion in emotionResult)
                {
                    listBox1.Items.Add("Emotion[" + emotionResultCount + "]");
                    listBox1.Items.Add("  .FaceRectangle = left: " + emotion.FaceRectangle.Left
                             + ", top: " + emotion.FaceRectangle.Top
                             + ", width: " + emotion.FaceRectangle.Width
                             + ", height: " + emotion.FaceRectangle.Height);

                    listBox1.Items.Add("  Anger    : " + emotion.Scores.Anger.ToString());
                    listBox1.Items.Add("  Contempt : " + emotion.Scores.Contempt.ToString());
                    listBox1.Items.Add("  Disgust  : " + emotion.Scores.Disgust.ToString());
                    listBox1.Items.Add("  Fear     : " + emotion.Scores.Fear.ToString());
                    listBox1.Items.Add("  Happiness: " + emotion.Scores.Happiness.ToString());
                    listBox1.Items.Add("  Neutral  : " + emotion.Scores.Neutral.ToString());
                    listBox1.Items.Add("  Sadness  : " + emotion.Scores.Sadness.ToString());
                    listBox1.Items.Add("  Surprise  : " + emotion.Scores.Surprise.ToString());
                    listBox1.Items.Add("");
                    emotionResultCount++;
                }
            }
            else
            {
                listBox1.Items.Add("No emotion is detected. This might be due to:\n" +
                    "    image is too small to detect faces\n" +
                    "    no faces are in the images\n" +
                    "    faces poses make it difficult to detect emotions\n" +
                    "    or other factors");
            }
        }
    }
}
