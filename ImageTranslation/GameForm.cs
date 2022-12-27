using IronOcr;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageTranslation
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();
        }

        bool startSwitch = false;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        static int count = 0;
        // 캡처한 파일을 저장하는 경로
        string path = "D:\\cap\\" + count + "aaa.png";

        // 네이버 파파고 API를 이용한 번역
        private void Papago()
        {
            // 번역할 텍스트의 길이를 저장
            int TranslationCount = this.txtTransTo.Lines.Length;
            this.textTrans.Text = "";

            // 파파고 api를 사용하기 위한 객체 생성
            WebRequest webRequest = null;
            WebResponse webResponse = null;
            Stream stream = null;
            StreamReader streamReader = null;

            // 파파고 api url
            string url = "https://openapi.naver.com/v1/papago/n2mt";

            // 번역할 언어를 선택하는 함수
            string source = LanguageSelect();

            // 번역할 텍스트의 라인별로 번역
            for (int i = 0; i < TranslationCount; i++)
            {

                if (this.txtTransTo.Lines[i] != "")
                {
                    string param = String.Format("source={0}&target=ko&text={1}", source, this.txtTransTo.Lines[i]);

                    byte[] bytearray = Encoding.UTF8.GetBytes(param);

                    webRequest = WebRequest.Create(url);
                    webRequest.Method = "POST";
                    webRequest.ContentType = "application/x-www-form-urlencoded";
                    webRequest.Headers.Add("X-Naver-Client-Id", "RAsF16FVQVOI6TS7a7Dy");
                    webRequest.Headers.Add("X-Naver-Client-Secret", "VSGwbT9kKl");

                    webRequest.ContentLength = bytearray.Length;

                    stream = webRequest.GetRequestStream();
                    stream.Write(bytearray, 0, bytearray.Length);
                    stream.Close();

                    webResponse = webRequest.GetResponse();
                    stream = webResponse.GetResponseStream();
                    streamReader = new StreamReader(stream);
                    string sReturn = streamReader.ReadToEnd();

                    streamReader.Close();
                    stream.Close();
                    webResponse.Close();

                    JObject jObject = JObject.Parse(sReturn);

                    this.textTrans.Text += jObject["message"]["result"]["translatedText"].ToString() + Environment.NewLine;
                }
                else
                {
                    this.textTrans.Text += Environment.NewLine;
                }
            }
        }

        private string LanguageSelect()
        {
            string lang = this.cbLang.SelectedItem.ToString();
            string result = "";

            switch (lang)
            {
                case "한국어":
                    {
                        result = "ko";
                        break;
                    }
                case "영어":
                    {
                        result = "en";
                        break;
                    }
                case "일본어":
                    {
                        result = "ja";
                        break;
                    }
                case "중국어":
                    {
                        result = "zh-CN";
                        break;
                    }

            }


            return result;
        }

        // 이미지에서 글자를 추출하는 Iron Api 사용
        private void ImageOCR()
        {
            //Bitmap oc = (Bitmap)this.pbCapture.Image;
            var Ocr = new IronTesseract();

            switch (this.cbLang.SelectedItem.ToString())
            {
                case "한국어":
                    Ocr.Language = OcrLanguage.Korean;
                    break;
                case "일본어":
                    Ocr.Language = OcrLanguage.Japanese;
                    break;
                case "영어":
                    Ocr.Language = OcrLanguage.English;
                    break;
            }
            using (var Input = new OcrInput(@path))
            {
                Input.Contrast();
                //Input.Deskew();
                Input.EnhanceResolution();

                var Result = Ocr.Read(Input);
                this.txtTransTo.Text = Result.Text;
            }
        }

        private void txtTransTo_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void btnStart_Click(object sender, EventArgs e)
        {
            startSwitch = true;
            tCheck.Start();
            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            startSwitch = false;
            tCheck.Stop();
        }

        private void tCheck_Tick(object sender, EventArgs e)
        {

        }
    }
}
