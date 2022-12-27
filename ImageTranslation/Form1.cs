using Gma.System.MouseKeyHook;
using System.Runtime.InteropServices;
using IronOcr;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;

namespace ImageTranslation
{
    
    public partial class Form1 : Form
    {
        // 폼 영역을 벗어난 운영체제 바탕화면에 도형을 그리기 위해 선언, 후킹
        [DllImport("USER32.DLL")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        
        // 운영체제가 받는 마우스 메시지를 훔쳐오기 위한 훅
        private IKeyboardMouseEvents globalHook;

        public Form1()
        {
            InitializeComponent();

            globalHook = Hook.GlobalEvents();

            // 이벤트를 추가
            globalHook.MouseDown += globalHook_MouseDown;
            globalHook.MouseUp += globalHook_MouseUp;
            globalHook.MouseMove += globalHook_MouseMove;

        }

        public class ImgCapture
        {
            private int refX = 0;
            private int refY = 0;
            private int imgW = 0;
            private int imgH = 0;

            private string filePath = "";

            public ImgCapture(int x, int y, int w, int h)
            {
                refX = x;
                refY = y;
                imgW = w;
                imgH = h;
            }

            public void SetPath(string path)
            {
                filePath = path;
            }

            public void DoCaptureImage()
            {
                if(filePath != "")
                {
                    if (imgW == 0 || imgH == 0)
                    { return; }

                    using (Bitmap bitmap = new Bitmap((int)imgW, (int)imgH))
                    {
                        using(Graphics g = Graphics.FromImage(bitmap))
                        {
                            g.CopyFromScreen(refX, refY, 0, 0, bitmap.Size);
                        }
                        bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                    }

                }
            }

        }

        // 마우스의 움직임을 제어하기 위한 bool자료형
        bool globalHook_MouseDown_Switch = false;
        bool globalHook_MouseUp_Switch = false;
        bool globalHook_MouseMove_Switch = false;

        // 마우스의 좌표값을 저장
        int mouseDownX = 0;
        int mouseDownY = 0;
        int mouseUpX = 0;
        int mouseUpY = 0;

        // 캡처 영역을 표시하는 함수
        private void DrawRect()
        {
            Pen p = new Pen(Color.Red, 1);

            IntPtr desktopPtr = GetDC(IntPtr.Zero);
            using (Graphics g = Graphics.FromHdc(desktopPtr))
            {

                Rectangle rect = new Rectangle(mouseDownX, mouseDownY, int.Parse(Cursor.Position.X.ToString()) - mouseDownX, int.Parse(Cursor.Position.Y.ToString()) - mouseDownY);

                g.DrawRectangle(p, rect);

                

                ReleaseDC(IntPtr.Zero, desktopPtr);

                

            }
           


        }

        // 마우스의 좌표값 및 캡처 영역을 계산
        private void globalHook_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (globalHook_MouseMove_Switch)
            {
                //DrawRect();     

            }

        }

        private void globalHook_MouseDown(object sender, MouseEventArgs e)
        {
            if(globalHook_MouseDown_Switch)
            {
                mouseDownX = int.Parse(Cursor.Position.X.ToString());
                mouseDownY = int.Parse(Cursor.Position.Y.ToString());

                DrawRect();

                globalHook_MouseMove_Switch = true;
                globalHook_MouseUp_Switch = true;
                globalHook_MouseDown_Switch = false;
            }
        }
       
        private void globalHook_MouseUp(object sender, MouseEventArgs e)
        {
            if (globalHook_MouseUp_Switch)
            {
                DrawRect();

                mouseUpX = int.Parse(Cursor.Position.X.ToString());
                mouseUpY = int.Parse(Cursor.Position.Y.ToString());

                globalHook_MouseMove_Switch = false;
                globalHook_MouseUp_Switch = false;

                path = "D:\\cap\\" + count + "aaa.png";

                count++;

                ImgCapture imgCapture = null;

                if (mouseDownX > mouseUpX && mouseDownY > mouseUpY)
                {

                    imgCapture = new ImgCapture(mouseUpX, mouseUpY, mouseDownX - mouseUpX, mouseDownY - mouseUpY);
                }
                else if (mouseDownX < mouseUpX && mouseDownY < mouseUpY)
                {
                    imgCapture = new ImgCapture(mouseDownX, mouseDownY, mouseUpX - mouseDownX, mouseUpY - mouseDownY);
                }
                else if (mouseDownX > mouseUpX && mouseDownY < mouseUpY)
                {
                    imgCapture = new ImgCapture(mouseUpX, mouseDownY, mouseDownX - mouseUpX, mouseUpY - mouseDownY);
                }
                else if (mouseDownX < mouseUpX && mouseDownY > mouseUpY)
                {
                    imgCapture = new ImgCapture(mouseDownX, mouseUpY, mouseUpX - mouseDownX, mouseDownY = mouseUpY);
                }

                imgCapture.SetPath(path);
                imgCapture.DoCaptureImage();

                this.pbCapture.Image = Bitmap.FromFile(path);
                this.pbCapture.SizeMode = PictureBoxSizeMode.StretchImage;

                ImageOCR();

                Papago();


            }
        }


        static int count = 0;
        // 캡처한 파일을 저장하는 경로
        string path = "D:\\cap\\" + count + "aaa.png";

       // 네이버 파파고 API를 이용한 번역
        private void Papago()
        {
            // 번역할 텍스트의 길이를 저장
            int TranslationCount = this.txtTranslation.Lines.Length;
            this.txtTranslationTo.Text = "";

            // 파파고 api를 사용하기 위한 객체 생성
            WebRequest webRequest = null;
            WebResponse webResponse = null;
            Stream stream = null;
            StreamReader streamReader = null;

            // 파파고 api url
            string url = "https://openapi.naver.com/v1/papago/n2mt";

            // 번역할 언어를 선택하는 함수
            string source = LanguageBefore();
            string target = LanguageAfter();

            // 번역할 텍스트의 라인별로 번역
            for (int i = 0; i < TranslationCount; i++)
            {

                if (this.txtTranslation.Lines[i] != "")
                {
                    string param = String.Format("source={0}&target={1}&text={2}", source, target, this.txtTranslation.Lines[i]);

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

                    this.txtTranslationTo.Text += jObject["message"]["result"]["translatedText"].ToString() + Environment.NewLine;
                }
                else
                {
                    this.txtTranslationTo.Text += Environment.NewLine;
                }
            }
        }

        // 이미지에서 글자를 추출하는 Iron Api 사용
        private void ImageOCR()
        {
            //Bitmap oc = (Bitmap)this.pbCapture.Image;
            var Ocr = new IronTesseract();

            switch(this.cbBefore.SelectedItem.ToString())
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
                this.txtTranslation.Text = Result.Text;
            }
        }

        private string LanguageBefore()
        {
            string LBefore = this.cbBefore.SelectedItem.ToString();
            string result = "";

            switch(LBefore)
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

        private string LanguageAfter()
        {
            string LAfter = this.cbAfter.SelectedItem.ToString();
            string result = "";

            switch (LAfter)
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




        private void btnCapture_Click(object sender, EventArgs e)
        {
            if(this.cbBefore.SelectedItem.ToString() == "" && this.cbAfter.SelectedItem.ToString() == "")
            {
                MessageBox.Show("언어를 선택해주세요!!", "오류");
            }
            else
            {
                globalHook_MouseDown_Switch = true;         
            }
        }

        private void btnGameMode_Click(object sender, EventArgs e)
        {

        }
    }
}