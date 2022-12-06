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
        // �� ������ ��� �ü�� ����ȭ�鿡 ������ �׸��� ���� ����, ��ŷ
        [DllImport("USER32.DLL")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        
        // �ü���� �޴� ���콺 �޽����� ���Ŀ��� ���� ��
        private IKeyboardMouseEvents globalHook;

        public Form1()
        {
            InitializeComponent();

            globalHook = Hook.GlobalEvents();

            // �̺�Ʈ�� �߰�
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

        // ���콺�� �������� �����ϱ� ���� bool�ڷ���
        bool globalHook_MouseDown_Switch = false;
        bool globalHook_MouseUp_Switch = false;
        bool globalHook_MouseMove_Switch = false;

        // ���콺�� ��ǥ���� ����
        int mouseDownX = 0;
        int mouseDownY = 0;
        int mouseUpX = 0;
        int mouseUpY = 0;

        // ĸó ������ ǥ���ϴ� �Լ�
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

        // ���콺�� ��ǥ�� �� ĸó ������ ���
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
        // ĸó�� ������ �����ϴ� ���
        string path = "D:\\cap\\" + count + "aaa.png";

       // ���̹� ���İ� API�� �̿��� ����
        private void Papago()
        {
            // ������ �ؽ�Ʈ�� ���̸� ����
            int TranslationCount = this.txtTranslation.Lines.Length;
            this.txtTranslationTo.Text = "";

            // ���İ� api�� ����ϱ� ���� ��ü ����
            WebRequest webRequest = null;
            WebResponse webResponse = null;
            Stream stream = null;
            StreamReader streamReader = null;

            // ���İ� api url
            string url = "https://openapi.naver.com/v1/papago/n2mt";

            // ������ �� �����ϴ� �Լ�
            string source = LanguageBefore();
            string target = LanguageAfter();

            // ������ �ؽ�Ʈ�� ���κ��� ����
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

        // �̹������� ���ڸ� �����ϴ� Iron Api ���
        private void ImageOCR()
        {
            //Bitmap oc = (Bitmap)this.pbCapture.Image;
            var Ocr = new IronTesseract();
            Ocr.Language = OcrLanguage.Korean;
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
                case "�ѱ���":
                    {
                        result = "ko";
                        break;
                    }
                case "����":
                    {
                        result = "en";
                        break;
                    }
                case "�Ϻ���":
                    {
                        result = "ja";
                        break;
                    }
                case "�߱���":
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
                case "�ѱ���":
                    {
                        result = "ko";
                        break;
                    }
                case "����":
                    {
                        result = "en";
                        break;
                    }
                case "�Ϻ���":
                    {
                        result = "ja";
                        break;
                    }
                case "�߱���":
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
                MessageBox.Show("�� �������ּ���!!", "����");
            }
            else
            {
                globalHook_MouseDown_Switch = true;         
            }
        }


   
    }
}