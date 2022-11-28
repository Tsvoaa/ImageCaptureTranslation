using Gma.System.MouseKeyHook;
using System.Runtime.InteropServices;

namespace ImageTranslation
{
    public partial class Form1 : Form
    {
        /*

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]

        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;

        private const int MOUSEEVENTF_LEFTUP = 0x04;

        */

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

        int count = 0;

        private void globalHook_MouseUp(object sender, MouseEventArgs e)
        {
            if(globalHook_MouseUp_Switch)
            {
                mouseUpX = int.Parse(Cursor.Position.X.ToString());
                mouseUpY = int.Parse(Cursor.Position.Y.ToString());

                string path = "D:\\cap\\" + count + "aaa.png";

                count++;

                ImgCapture imgCapture = new ImgCapture(mouseDownX, mouseDownY, mouseUpX - mouseDownX, mouseUpY - mouseDownY);

                imgCapture.SetPath(path);
                imgCapture.DoCaptureImage();

                this.pbCapture.Image = Bitmap.FromFile(path);
                this.pbCapture.SizeMode = PictureBoxSizeMode.StretchImage;

                globalHook_MouseUp_Switch = false;
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            globalHook_MouseDown_Switch = true;
            globalHook_MouseUp_Switch = true;

        }

   
    }
}