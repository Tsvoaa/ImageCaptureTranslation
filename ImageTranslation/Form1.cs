using Gma.System.MouseKeyHook;
using IronOcr;
using System.Runtime.InteropServices;

namespace ImageTranslation
{
    public partial class Form1 : Form
    {
        
        private IKeyboardMouseEvents globalHook;

        public Form1()
        {
            InitializeComponent();

            globalHook = Hook.GlobalEvents();

            globalHook.MouseDown += globalHook_MouseDown;
            globalHook.MouseUp += globalHook_MouseUp;

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

        bool globalHook_MouseDown_Switch = false;
        bool globalHook_MouseUp_Switch = false;

        int mouseDownX = 0;
        int mouseDownY = 0;
        int mouseUpX = 0;
        int mouseUpY = 0;

        private void globalHook_MouseDown(object sender, MouseEventArgs e)
        {
            if(globalHook_MouseDown_Switch)
            {
                mouseDownX = int.Parse(Cursor.Position.X.ToString());
                mouseDownY = int.Parse(Cursor.Position.Y.ToString());

                globalHook_MouseDown_Switch = false;
            }
        }

        static int count = 0;
        string path = "D:\\cap\\" + count + "aaa.png";

        private void globalHook_MouseUp(object sender, MouseEventArgs e)
        {
            if(globalHook_MouseUp_Switch)
            {
                mouseUpX = int.Parse(Cursor.Position.X.ToString());
                mouseUpY = int.Parse(Cursor.Position.Y.ToString());

                path = "D:\\cap\\" + count + "aaa.png";

                count++;

                ImgCapture imgCapture = new ImgCapture(mouseDownX, mouseDownY, mouseUpX - mouseDownX, mouseUpY - mouseDownY);

                imgCapture.SetPath(path);
                imgCapture.DoCaptureImage();

                this.pbCapture.Image = Bitmap.FromFile(path);
                this.pbCapture.SizeMode = PictureBoxSizeMode.StretchImage;

                ImageOCR();



                globalHook_MouseUp_Switch = false;
            }
        }

        private void TranslationLanguage()
        {




            this.txtTranslationTo.Text = "";
        }

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



        private void btnCapture_Click(object sender, EventArgs e)
        {
            globalHook_MouseDown_Switch = true;
            globalHook_MouseUp_Switch = true;

        }

   
    }
}