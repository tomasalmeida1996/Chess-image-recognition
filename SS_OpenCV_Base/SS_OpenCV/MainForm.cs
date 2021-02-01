using System;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;


namespace SS_OpenCV
{
    public partial class MainForm : Form
    {
        Image<Bgr, Byte> img = null; // working image
        Image<Bgr, Byte> imgUndo = null; // undo backup image - UNDO
        string title_bak = "";

        public MainForm()
        {
            InitializeComponent();
            title_bak = Text;
        }

        /// <summary>
        /// Opens a new image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                img = new Image<Bgr, byte>(openFileDialog1.FileName);
                Text = title_bak + " [" +
                        openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf("\\") + 1) +
                        "]";
                imgUndo = img.Copy();
                ImageViewer.Image = img.Bitmap;
                ImageViewer.Refresh();
            }
        }

        /// <summary>
        /// Saves an image with a new name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImageViewer.Image.Save(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// restore last undo copy of the working image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgUndo == null) // verify if the image is already opened
                return; 
            Cursor = Cursors.WaitCursor;
            img = imgUndo.Copy();

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        /// <summary>
        /// Chaneg visualization mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // zoom
            if (autoZoomToolStripMenuItem.Checked)
            {
                ImageViewer.SizeMode = PictureBoxSizeMode.Zoom;
                ImageViewer.Dock = DockStyle.Fill;
            }
            else // with scroll bars
            {
                ImageViewer.Dock = DockStyle.None;
                ImageViewer.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        /// <summary>
        /// Show authors form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthorsForm form = new AuthorsForm();
            form.ShowDialog();
        }

        /// <summary>
        /// Calculate the image negative
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Negative(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void evalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EvalForm eval = new EvalForm();
            eval.ShowDialog();
        }

        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.ConvertToGray(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void redChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.RedChannel(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void brightnessContrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBox form = new InputBox("brilho?");
            form.ShowDialog();
            int bright = Convert.ToInt32(form.ValueTextBox.Text);

            InputBox form2 = new InputBox("contraste?");
            form2.ShowDialog();
            double contrast = Convert.ToDouble(form2.ValueTextBox.Text);

            ImageClass.BrightContrast(img, bright, contrast);
        }

        private void ImageViewer_Click(object sender, EventArgs e)
        {

        }

        private void translationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form = new InputBox("Coordenada x");
            form.ShowDialog();
            int dx = Convert.ToInt32(form.ValueTextBox.Text);

            InputBox form1 = new InputBox("Coordenada y");
            form1.ShowDialog();
            int dy = Convert.ToInt32(form1.ValueTextBox.Text);

            ImageClass.Translation(img, img, dx, dy);
        }

        private void rotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form = new InputBox("Anglo");
            form.ShowDialog();
            float angle = Convert.ToSingle(form.ValueTextBox.Text);

            ImageClass.Rotation(img, img, angle);
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void scaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form = new InputBox("Escala");
            form.ShowDialog();
            float scaleFactor = Convert.ToSingle(form.ValueTextBox.Text);


            ImageClass.Scale(img, img, scaleFactor);
        }

        //create mouse variables
        int mouseX, mouseY;
        bool mouseFlag = false;

        

        private void scalePointxyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //get mouse coordinates using mouseClick event

            mouseFlag = true;
            while (mouseFlag) // wait for mouseClick event
                Application.DoEvents();

            InputBox form = new InputBox("Escala");
            form.ShowDialog();
            float scaleFactor = Convert.ToSingle(form.ValueTextBox.Text);

            ImageClass.Scale_point_xy(img, img, scaleFactor, mouseX, mouseY);
        }

        

        private void ImageViewer_MouseClick(object sender, MouseEventArgs e)
        {

            if(mouseFlag)
            {
                mouseX = e.X;  // get mouse coordinates
                mouseY = e.Y;

                mouseFlag = false; //unlock (while mouseFlag)

            }

        }

        private void nonUniformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Weight_Matrix Matrix = new Weight_Matrix();
            Matrix.ShowDialog();

            

            ImageClass.NonUniform(img, img, Matrix.matrix, Matrix.matrixWeight);
            
        }

        private void hIstogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] vec = ImageClass.Histogram_Gray(img);

            Form hist = new Form1(vec);
            hist.ShowDialog();
            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen
        }

        private void chessRecognitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            Rectangle rectangulo;
            string angle;
            string[,] Pieces;
            ImageClass.Chess_Recognition(img, img, out rectangulo, out angle, out Pieces);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void bWOtsuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            //ImageClass.ConvertToBW_Otsu(img);
            ImageClass.ConvertToBW(img, 50);
            ImageClass.Negative(img);


            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void meanToolStripMenuItem_Click(object sender, EventArgs e)
        {
        

        }
    }
}