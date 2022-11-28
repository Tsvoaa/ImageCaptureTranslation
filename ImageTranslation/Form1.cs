namespace ImageTranslation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

                    using()

                }
            }

        }
        private void btnCapture_Click(object sender, EventArgs e)
        {

        }
    }
}