using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Drawing;
using System.IO;
using System.Linq;

namespace SS_OpenCV
{
    class ImageClass
    {

        /// <summary>
        /// Image Negative using EmguCV library
        /// Slower method
        /// </summary>
        /// <param name="img">Image</param>
        public static void Negative(Image<Bgr, byte> img)
        {

            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //obtém as 3 componentes
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];


                            // store in the image
                            dataPtr[0] = (byte)(255 - (int)blue);
                            dataPtr[1] = (byte)(255 - (int)green);
                            dataPtr[2] = (byte)(255 - (int)red);

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }

        }
        public static void RedChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //obtém as 3 componentes
                            red = dataPtr[2];

                            // store in the image
                            dataPtr[0] = red;
                            dataPtr[1] = red;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }

        }

        public static void BrightContrast(Image<Bgr, byte> img, int bright, double contrast)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            // guardar contrast * dataPtr[0] + bright numa variavel double e depois limitar de 0 a 255
                            // e guardar no ptr
                            // store in the image
                            dataPtr[0] = Convert.ToByte(contrast * dataPtr[0] + bright);
                            dataPtr[1] = Convert.ToByte(contrast * dataPtr[1] + bright);
                            dataPtr[2] = Convert.ToByte(contrast * dataPtr[2] + bright);
                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }



        }


        /// <summary>
        /// Convert to gray
        /// Direct access to memory
        /// </summary>
        /// <param name="img">image</param>
        public static void ConvertToGray(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //obtém as 3 componentes
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to gray
                            gray = (byte)(((int)blue + green + red) / 3);

                            // store in the image
                            dataPtr[0] = gray;
                            dataPtr[1] = gray;
                            dataPtr[2] = gray;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }
        /// <summary>
        /// Translation
        /// Direct access to memory
        /// </summary>
        /// <param> (Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int dx, int dy)

        public static void Translation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int dx, int dy)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                MIplImage mOrig = imgCopy.MIplImage;
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y, xo, yo;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            xo = x - dx;
                            yo = y - dy;

                            if (xo >= width || yo >= height || xo < 0 || yo < 0)
                            {

                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;

                            }
                            else
                            {

                                dataPtrAux = dataPtrOrig + yo * m.widthStep + xo * nChan;

                                dataPtr[0] = dataPtrAux[0];
                                dataPtr[1] = dataPtrAux[1];
                                dataPtr[2] = dataPtrAux[2];


                            }
                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void Rotation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float angle)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                MIplImage mOrig = imgCopy.MIplImage;
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y, xo, yo;
                double w2 = width / 2.0;
                double h2 = height / 2.0;
                double cos = Math.Cos(angle);
                double sen = Math.Sin(angle);

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            xo = (int)Math.Round((x - w2) * cos - (h2 - y) * sen + w2);
                            yo = (int)Math.Round(h2 - (x - w2) * sen - (h2 - y) * cos);

                            if (xo >= width || yo >= height || xo < 0 || yo < 0)
                            {
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;

                            }
                            else
                            {

                                dataPtrAux = dataPtrOrig + yo * m.widthStep + xo * nChan;

                                dataPtr[0] = dataPtrAux[0];
                                dataPtr[1] = dataPtrAux[1];
                                dataPtr[2] = dataPtrAux[2];


                            }
                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }
                }
            }


        }

        public static void Scale(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor)
        {

            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                MIplImage mOrig = imgCopy.MIplImage;
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y, xo, yo;


                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            xo = (int)(x / scaleFactor);
                            yo = (int)(y / scaleFactor);

                            if (xo >= width || yo >= height || xo < 0 || yo < 0)
                            {
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;

                            }
                            else
                            {

                                dataPtrAux = dataPtrOrig + yo * m.widthStep + xo * nChan;

                                dataPtr[0] = dataPtrAux[0];
                                dataPtr[1] = dataPtrAux[1];
                                dataPtr[2] = dataPtrAux[2];


                            }
                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }
                }
            }

        }

        public static void Scale_point_xy(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor, int centerX, int centerY)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                MIplImage mOrig = imgCopy.MIplImage;
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrAux;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y, xo, yo;
                double w2 = width / 2.0;
                double h2 = height / 2.0;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            xo = (int)Math.Round(x / scaleFactor - w2 / scaleFactor + centerX);
                            yo = (int)Math.Round(y / scaleFactor - h2 / scaleFactor + centerY);

                            if (xo >= width || yo >= height || xo < 0 || yo < 0)
                            {
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;

                            }
                            else
                            {

                                dataPtrAux = dataPtrOrig + yo * m.widthStep + xo * nChan;

                                dataPtr[0] = dataPtrAux[0];
                                dataPtr[1] = dataPtrAux[1];
                                dataPtr[2] = dataPtrAux[2];


                            }
                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }
                }
            }


        }

        public static void Mean(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                MIplImage mOrig = imgCopy.MIplImage;
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrMid, dataPtrBot, dataPtrTop, ptrAux, ptrAux1;
                int x = 0, y = 0, media0 = 0, media1 = 0, media2 = 0;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int widthStep = m.widthStep;
                int nChan_menos_widthS = nChan - widthStep; //assim conseguimos evitar 4 somas por componente do pixel
                int nChan_mais_widthS = nChan + widthStep;  //logo 4*3(RGB)*526*700 = 4.418.400 somas

                if (nChan == 3) // image in RGB
                {
                    dataPtr += widthStep + nChan;
                    dataPtrOrig += widthStep + nChan;


                    for (y = 1; y < height - 1; y++)
                    {
                        for (x = 1; x < width - 1; x++)
                        {

                            dataPtr[0] = (byte)Math.Round(((dataPtrOrig - nChan_menos_widthS)[0] + (dataPtrOrig - widthStep)[0] + (dataPtrOrig + nChan_menos_widthS)[0] + (dataPtrOrig - nChan)[0] + (dataPtrOrig)[0] + (dataPtrOrig + nChan)[0] + (dataPtrOrig - nChan_mais_widthS)[0] + (dataPtrOrig + widthStep)[0] + (dataPtrOrig + nChan_mais_widthS)[0]) / 9.0);
                            dataPtr[1] = (byte)Math.Round(((dataPtrOrig - nChan_menos_widthS)[1] + (dataPtrOrig - widthStep)[1] + (dataPtrOrig + nChan_menos_widthS)[1] + (dataPtrOrig - nChan)[1] + (dataPtrOrig)[1] + (dataPtrOrig + nChan)[1] + (dataPtrOrig - nChan_mais_widthS)[1] + (dataPtrOrig + widthStep)[1] + (dataPtrOrig + nChan_mais_widthS)[1]) / 9.0);
                            dataPtr[2] = (byte)Math.Round(((dataPtrOrig - nChan_menos_widthS)[2] + (dataPtrOrig - widthStep)[2] + (dataPtrOrig + nChan_menos_widthS)[2] + (dataPtrOrig - nChan)[2] + (dataPtrOrig)[2] + (dataPtrOrig + nChan)[2] + (dataPtrOrig - nChan_mais_widthS)[2] + (dataPtrOrig + widthStep)[2] + (dataPtrOrig + nChan_mais_widthS)[2]) / 9.0);



                            dataPtr += nChan;
                            dataPtrOrig += nChan;
                        }
                        dataPtr += padding + (2 * nChan);
                        dataPtrOrig += padding + (2 * nChan);

                    }

                    dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();

                    //primeiro canto; P(x,y) * 4 + P(x+1,y) * 2 + P(x,y+1) *2 + P(x+1,y+1)
                    dataPtr[0] = (byte)Math.Round((dataPtrOrig[0] * 4 + (dataPtrOrig + nChan)[0] * 2 + (dataPtrOrig + widthStep)[0] * 2 + (dataPtrOrig + widthStep + nChan)[0]) / 9.0);
                    dataPtr[1] = (byte)Math.Round((dataPtrOrig[1] * 4 + (dataPtrOrig + nChan)[1] * 2 + (dataPtrOrig + widthStep)[1] * 2 + (dataPtrOrig + widthStep + nChan)[1]) / 9.0);
                    dataPtr[2] = (byte)Math.Round((dataPtrOrig[2] * 4 + (dataPtrOrig + nChan)[2] * 2 + (dataPtrOrig + widthStep)[2] * 2 + (dataPtrOrig + widthStep + nChan)[2]) / 9.0);


                    //terá a haver com a saturaçao em 255?? mas nao faz sentido
                    //faz a linha y=0
                    dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();
                    for (int a = 1; a < width - 1; a++)
                    {
                        dataPtr += nChan; //passa para o proximo pixel 

                        dataPtrOrig += nChan;
                        dataPtr[0] = (byte)Math.Round((((dataPtrOrig - nChan)[0] * 2) + (dataPtrOrig[0] * 2) + ((dataPtrOrig + nChan)[0] * 2) + (dataPtrOrig - nChan + widthStep)[0] + (dataPtrOrig + widthStep)[0] + (dataPtrOrig + nChan + widthStep)[0]) / 9.0);
                        dataPtr[1] = (byte)Math.Round((((dataPtrOrig - nChan)[1] * 2) + (dataPtrOrig[1] * 2) + ((dataPtrOrig + nChan)[1] * 2) + (dataPtrOrig - nChan + widthStep)[1] + (dataPtrOrig + widthStep)[1] + (dataPtrOrig + nChan + widthStep)[1]) / 9.0);
                        dataPtr[2] = (byte)Math.Round((((dataPtrOrig - nChan)[2] * 2) + (dataPtrOrig[2] * 2) + ((dataPtrOrig + nChan)[2] * 2) + (dataPtrOrig - nChan + widthStep)[2] + (dataPtrOrig + widthStep)[2] + (dataPtrOrig + nChan + widthStep)[2]) / 9.0);
                    }
                    //dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    //dataPtrOrig = (byte*)mOrig.imageData.ToPointer();
                    dataPtrOrig += nChan;
                    dataPtr += nChan;
                    //dataPtr += (width - 1) * nChan; //avança o dataPtr para o segundo canto
                    //segundo canto; P(x,y) * 4 + P(x-1,y) * 2 + P(x,y+1) *2 + P(x-1,y+1)
                    dataPtr[0] = (byte)Math.Round(((dataPtrOrig[0] * 4 + (dataPtrOrig - nChan)[0] * 2 + (dataPtrOrig + widthStep)[0] * 2 + (dataPtrOrig + widthStep - nChan)[0])) / 9.0);
                    dataPtr[1] = (byte)Math.Round(((dataPtrOrig[1] * 4 + (dataPtrOrig - nChan)[1] * 2 + (dataPtrOrig + widthStep)[1] * 2 + (dataPtrOrig + widthStep - nChan)[1])) / 9.0);
                    dataPtr[2] = (byte)Math.Round(((dataPtrOrig[2] * 4 + (dataPtrOrig - nChan)[2] * 2 + (dataPtrOrig + widthStep)[2] * 2 + (dataPtrOrig + widthStep - nChan)[2])) / 9.0);

                    //passa para o canto inferior esquerdo
                    dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();

                    dataPtr += widthStep * (height - 1);
                    dataPtrOrig += widthStep * (height - 1);

                    //canto inferior esquerdo
                    dataPtr[0] = (byte)Math.Round((dataPtrOrig[0] * 4 + (dataPtrOrig + nChan)[0] * 2 + (dataPtrOrig - widthStep)[0] * 2 + (dataPtrOrig - widthStep + nChan)[0]) / 9.0);
                    dataPtr[1] = (byte)Math.Round((dataPtrOrig[1] * 4 + (dataPtrOrig + nChan)[1] * 2 + (dataPtrOrig - widthStep)[1] * 2 + (dataPtrOrig - widthStep + nChan)[1]) / 9.0);
                    dataPtr[2] = (byte)Math.Round((dataPtrOrig[2] * 4 + (dataPtrOrig + nChan)[2] * 2 + (dataPtrOrig - widthStep)[2] * 2 + (dataPtrOrig - widthStep + nChan)[2]) / 9.0);

                    //faz a linha y=heigth
                    for (int a = 1; a < width - 1; a++)
                    {
                        dataPtr += nChan; //passa para o proximo pixel para fazer a linha de baixo (y = heigth)

                        dataPtrOrig += nChan;
                        dataPtr[0] = (byte)Math.Round(((dataPtrOrig - nChan)[0] * 2 + dataPtrOrig[0] * 2 + (dataPtrOrig + nChan)[0] * 2 + (dataPtrOrig - nChan - widthStep)[0] + (dataPtrOrig - widthStep)[0] + (dataPtrOrig + nChan - widthStep)[0]) / 9.0);
                        dataPtr[1] = (byte)Math.Round(((dataPtrOrig - nChan)[1] * 2 + dataPtrOrig[1] * 2 + (dataPtrOrig + nChan)[1] * 2 + (dataPtrOrig - nChan - widthStep)[1] + (dataPtrOrig - widthStep)[1] + (dataPtrOrig + nChan - widthStep)[1]) / 9.0);
                        dataPtr[2] = (byte)Math.Round(((dataPtrOrig - nChan)[2] * 2 + dataPtrOrig[2] * 2 + (dataPtrOrig + nChan)[2] * 2 + (dataPtrOrig - nChan - widthStep)[2] + (dataPtrOrig - widthStep)[2] + (dataPtrOrig + nChan - widthStep)[2]) / 9.0);

                    }

                    //canto inferior direito
                    dataPtr += nChan; //avança o dataPtr para o canto inferior direito
                    dataPtrOrig += nChan;
                    //segundo canto; P(x,y) * 4 + P(x-1,y) * 2 + P(x,y-1) *2 + P(x-1,y-1)
                    dataPtr[0] = (byte)Math.Round((dataPtrOrig[0] * 4 + (dataPtrOrig - nChan)[0] * 2 + (dataPtrOrig - widthStep)[0] * 2 + (dataPtrOrig - widthStep - nChan)[0]) / 9.0);
                    dataPtr[1] = (byte)Math.Round((dataPtrOrig[1] * 4 + (dataPtrOrig - nChan)[1] * 2 + (dataPtrOrig - widthStep)[1] * 2 + (dataPtrOrig - widthStep - nChan)[1]) / 9.0);
                    dataPtr[2] = (byte)Math.Round((dataPtrOrig[2] * 4 + (dataPtrOrig - nChan)[2] * 2 + (dataPtrOrig - widthStep)[2] * 2 + (dataPtrOrig - widthStep - nChan)[2]) / 9.0);

                    dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();
                    //faz a linha x=0
                    //dataPtr += widthStep;//fica em P(0,1) e faz a linha x = 0
                    for (int a = 1; a < height - 1; a++)
                    {
                        dataPtr += widthStep; //passa para o pixel de baixo (em x = 0)

                        dataPtrOrig += widthStep;
                        //ptrAux = dataPtrOrig + (widthStep * a);
                        dataPtr[0] = (byte)Math.Round(((dataPtrOrig - widthStep)[0] * 2 + dataPtrOrig[0] * 2 + (dataPtrOrig + widthStep)[0] * 2 + (dataPtrOrig + nChan - widthStep)[0] + (dataPtrOrig + nChan)[0] + (dataPtrOrig + nChan + widthStep)[0]) / 9.0);
                        dataPtr[1] = (byte)Math.Round(((dataPtrOrig - widthStep)[1] * 2 + dataPtrOrig[1] * 2 + (dataPtrOrig + widthStep)[1] * 2 + (dataPtrOrig + nChan - widthStep)[1] + (dataPtrOrig + nChan)[1] + (dataPtrOrig + nChan + widthStep)[1]) / 9.0);
                        dataPtr[2] = (byte)Math.Round(((dataPtrOrig - widthStep)[2] * 2 + dataPtrOrig[2] * 2 + (dataPtrOrig + widthStep)[2] * 2 + (dataPtrOrig + nChan - widthStep)[2] + (dataPtrOrig + nChan)[2] + (dataPtrOrig + nChan + widthStep)[2]) / 9.0);

                    }

                    dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();
                    dataPtr += ((width - 1) * nChan) + widthStep;//fica em P(width,1) e faz a linha x = width
                    dataPtrOrig += ((width - 1) * nChan) + widthStep;//fica em P(width,1) e faz a linha x = width

                    //faz a linha x = width
                    for (int a = 1; a < height - 1; a++)
                    {
                        dataPtr[0] = (byte)Math.Round(((dataPtrOrig - widthStep)[0] * 2 + dataPtrOrig[0] * 2 + (dataPtrOrig + widthStep)[0] * 2 + (dataPtrOrig - nChan - widthStep)[0] + (dataPtrOrig - nChan)[0] + (dataPtrOrig - nChan + widthStep)[0]) / 9.0);
                        dataPtr[1] = (byte)Math.Round(((dataPtrOrig - widthStep)[1] * 2 + dataPtrOrig[1] * 2 + (dataPtrOrig + widthStep)[1] * 2 + (dataPtrOrig - nChan - widthStep)[1] + (dataPtrOrig - nChan)[1] + (dataPtrOrig - nChan + widthStep)[1]) / 9.0);
                        dataPtr[2] = (byte)Math.Round(((dataPtrOrig - widthStep)[2] * 2 + dataPtrOrig[2] * 2 + (dataPtrOrig + widthStep)[2] * 2 + (dataPtrOrig - nChan - widthStep)[2] + (dataPtrOrig - nChan)[2] + (dataPtrOrig - nChan + widthStep)[2]) / 9.0);

                        dataPtr += widthStep; //passa para o pixel de baixo (em x = width)

                        dataPtrOrig += widthStep;

                    }


                }
            }

        }

        public static void NonUniform(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float[,] matrix, float matrixWeight)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                MIplImage mOrig = imgCopy.MIplImage;
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer(); // Pointer to the image
                int x = 0, y = 0;
                double num0, num1, num2;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int widthStep = m.widthStep;
                int nChan_menos_widthS = nChan - widthStep; //assim conseguimos evitar 4 somas por componente do pixel
                int nChan_mais_widthS = nChan + widthStep;  //logo 4*3(RGB)*526*700 = 4.418.400 somas

                if (nChan == 3) // image in RGB
                {
                    dataPtr += widthStep + nChan;
                    dataPtrOrig += widthStep + nChan;

                    for (y = 1; y < height - 1; y++)
                    {
                        for (x = 1; x < width - 1; x++)
                        {
                            // PARA OS PIXEIS NÃO CANTOS
                            num0 = Math.Round(((dataPtrOrig - nChan_menos_widthS)[0] * matrix[0, 0] + (dataPtrOrig - widthStep)[0] * matrix[1, 0] + (dataPtrOrig + nChan_menos_widthS)[0] * matrix[2, 0] + (dataPtrOrig - nChan)[0] * matrix[0, 1] + (dataPtrOrig)[0] * matrix[1, 1] + (dataPtrOrig + nChan)[0] * matrix[2, 1] + (dataPtrOrig - nChan_mais_widthS)[0] * matrix[0, 2] + (dataPtrOrig + widthStep)[0] * matrix[1, 2] + (dataPtrOrig + nChan_mais_widthS)[0] * matrix[2, 2]) / matrixWeight);
                            num1 = Math.Round(((dataPtrOrig - nChan_menos_widthS)[1] * matrix[0, 0] + (dataPtrOrig - widthStep)[1] * matrix[1, 0] + (dataPtrOrig + nChan_menos_widthS)[1] * matrix[2, 0] + (dataPtrOrig - nChan)[1] * matrix[0, 1] + (dataPtrOrig)[1] * matrix[1, 1] + (dataPtrOrig + nChan)[1] * matrix[2, 1] + (dataPtrOrig - nChan_mais_widthS)[1] * matrix[0, 2] + (dataPtrOrig + widthStep)[1] * matrix[1, 2] + (dataPtrOrig + nChan_mais_widthS)[1] * matrix[2, 2]) / matrixWeight);
                            num2 = Math.Round(((dataPtrOrig - nChan_menos_widthS)[2] * matrix[0, 0] + (dataPtrOrig - widthStep)[2] * matrix[1, 0] + (dataPtrOrig + nChan_menos_widthS)[2] * matrix[2, 0] + (dataPtrOrig - nChan)[2] * matrix[0, 1] + (dataPtrOrig)[2] * matrix[1, 1] + (dataPtrOrig + nChan)[2] * matrix[2, 1] + (dataPtrOrig - nChan_mais_widthS)[2] * matrix[0, 2] + (dataPtrOrig + widthStep)[2] * matrix[1, 2] + (dataPtrOrig + nChan_mais_widthS)[2] * matrix[2, 2]) / matrixWeight);

                            if (num0 < 0)
                            {
                                dataPtr[0] = 0;
                            }

                            else if (num0 > 255)
                            {
                                dataPtr[0] = 255;
                            }
                            else dataPtr[0] = (byte)num0;


                            if (num1 < 0)
                            {
                                dataPtr[1] = 0;
                            }

                            else if (num1 > 255)
                            {
                                dataPtr[1] = 255;
                            }
                            else dataPtr[1] = (byte)num1;

                            if (num2 < 0)
                            {
                                dataPtr[2] = 0;
                            }

                            else if (num2 > 255)
                            {
                                dataPtr[2] = 255;
                            }
                            else dataPtr[2] = (byte)num2;


                            dataPtr += nChan;
                            dataPtrOrig += nChan;
                        }
                        dataPtr += padding + (2 * nChan);
                        dataPtrOrig += padding + (2 * nChan);

                    }

                    dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();

                    //primeiro canto; P(x,y) * 4 + P(x+1,y) * 2 + P(x,y+1) *2 + P(x+1,y+1)
                    num0 = Math.Round((dataPtrOrig[0] * (matrix[1, 1] + matrix[0, 0] + matrix[1, 0] + matrix[0, 1]) + (dataPtrOrig + nChan)[0] * (matrix[2, 1] + matrix[2, 0]) + (dataPtrOrig + widthStep)[0] * (matrix[0, 2] + matrix[1, 2]) + (dataPtrOrig + widthStep + nChan)[0] * matrix[2, 2]) / matrixWeight);
                    num1 = Math.Round((dataPtrOrig[1] * (matrix[1, 1] + matrix[0, 0] + matrix[1, 0] + matrix[0, 1]) + (dataPtrOrig + nChan)[1] * (matrix[2, 1] + matrix[2, 0]) + (dataPtrOrig + widthStep)[1] * (matrix[0, 2] + matrix[1, 2]) + (dataPtrOrig + widthStep + nChan)[1] * matrix[2, 2]) / matrixWeight);
                    num2 = Math.Round((dataPtrOrig[2] * (matrix[1, 1] + matrix[0, 0] + matrix[1, 0] + matrix[0, 1]) + (dataPtrOrig + nChan)[2] * (matrix[2, 1] + matrix[2, 0]) + (dataPtrOrig + widthStep)[2] * (matrix[0, 2] + matrix[1, 2]) + (dataPtrOrig + widthStep + nChan)[2] * matrix[2, 2]) / matrixWeight);

                    if (num0 < 0)
                    {
                        dataPtr[0] = 0;
                    }

                    else if (num0 > 255)
                    {
                        dataPtr[0] = 255;
                    }
                    else dataPtr[0] = (byte)num0;


                    if (num1 < 0)
                    {
                        dataPtr[1] = 0;
                    }

                    else if (num1 > 255)
                    {
                        dataPtr[1] = 255;
                    }
                    else dataPtr[1] = (byte)num1;

                    if (num2 < 0)
                    {
                        dataPtr[2] = 0;
                    }

                    else if (num2 > 255)
                    {
                        dataPtr[2] = 255;
                    }
                    else dataPtr[2] = (byte)num2;


                    //terá a haver com a saturaçao em 255?? mas nao faz sentido
                    //faz a linha y=0
                    dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();
                    for (int a = 1; a < width - 1; a++)
                    {
                        dataPtr += nChan; //passa para o proximo pixel 

                        dataPtrOrig += nChan;
                        num0 = Math.Round(((dataPtrOrig - nChan)[0] * (matrix[0, 0] + matrix[0, 1]) + dataPtrOrig[0] * (matrix[1, 1] + matrix[0, 1]) + (dataPtrOrig + nChan)[0] * (matrix[2, 0] + matrix[2, 1]) + (dataPtrOrig - nChan + widthStep)[0] * matrix[0, 2] + (dataPtrOrig + widthStep)[0] * matrix[1, 2] + (dataPtrOrig + nChan + widthStep)[0] * matrix[2, 2]) / matrixWeight);
                        num1 = Math.Round(((dataPtrOrig - nChan)[1] * (matrix[0, 0] + matrix[0, 1]) + dataPtrOrig[1] * (matrix[1, 1] + matrix[0, 1]) + (dataPtrOrig + nChan)[1] * (matrix[2, 0] + matrix[2, 1]) + (dataPtrOrig - nChan + widthStep)[1] * matrix[0, 2] + (dataPtrOrig + widthStep)[1] * matrix[1, 2] + (dataPtrOrig + nChan + widthStep)[1] * matrix[2, 2]) / matrixWeight);
                        num2 = Math.Round(((dataPtrOrig - nChan)[2] * (matrix[0, 0] + matrix[0, 1]) + dataPtrOrig[2] * (matrix[1, 1] + matrix[0, 1]) + (dataPtrOrig + nChan)[2] * (matrix[2, 0] + matrix[2, 1]) + (dataPtrOrig - nChan + widthStep)[2] * matrix[0, 2] + (dataPtrOrig + widthStep)[2] * matrix[1, 2] + (dataPtrOrig + nChan + widthStep)[2] * matrix[2, 2]) / matrixWeight);

                        if (num0 < 0)
                        {
                            dataPtr[0] = 0;
                        }

                        else if (num0 > 255)
                        {
                            dataPtr[0] = 255;
                        }
                        else dataPtr[0] = (byte)num0;


                        if (num1 < 0)
                        {
                            dataPtr[1] = 0;
                        }

                        else if (num1 > 255)
                        {
                            dataPtr[1] = 255;
                        }
                        else dataPtr[1] = (byte)num1;

                        if (num2 < 0)
                        {
                            dataPtr[2] = 0;
                        }

                        else if (num2 > 255)
                        {
                            dataPtr[2] = 255;
                        }
                        else dataPtr[2] = (byte)num2;
                    }
                    //dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    //dataPtrOrig = (byte*)mOrig.imageData.ToPointer();
                    dataPtrOrig += nChan;
                    dataPtr += nChan;
                    //dataPtr += (width - 1) * nChan; //avança o dataPtr para o segundo canto
                    //segundo canto; P(x,y) * 4 + P(x-1,y) * 2 + P(x,y+1) *2 + P(x-1,y+1)
                    num0 = Math.Round(((dataPtrOrig[0] * (matrix[1, 1] + matrix[1, 0] + matrix[2, 0] + matrix[2, 1]) + (dataPtrOrig - nChan)[0] * (matrix[1, 0] + matrix[0, 0]) + (dataPtrOrig + widthStep)[0] * (matrix[2, 1] + matrix[2, 2]) + (dataPtrOrig + widthStep - nChan)[0] * matrix[0, 2])) / matrixWeight);
                    num1 = Math.Round(((dataPtrOrig[1] * (matrix[1, 1] + matrix[1, 0] + matrix[2, 0] + matrix[2, 1]) + (dataPtrOrig - nChan)[1] * (matrix[1, 0] + matrix[0, 0]) + (dataPtrOrig + widthStep)[1] * (matrix[2, 1] + matrix[2, 2]) + (dataPtrOrig + widthStep - nChan)[1] * matrix[0, 2])) / matrixWeight);
                    num2 = Math.Round(((dataPtrOrig[2] * (matrix[1, 1] + matrix[1, 0] + matrix[2, 0] + matrix[2, 1]) + (dataPtrOrig - nChan)[2] * (matrix[1, 0] + matrix[0, 0]) + (dataPtrOrig + widthStep)[2] * (matrix[2, 1] + matrix[2, 2]) + (dataPtrOrig + widthStep - nChan)[2] * matrix[0, 2])) / matrixWeight);

                    if (num0 < 0)
                    {
                        dataPtr[0] = 0;
                    }

                    else if (num0 > 255)
                    {
                        dataPtr[0] = 255;
                    }
                    else dataPtr[0] = (byte)num0;


                    if (num1 < 0)
                    {
                        dataPtr[1] = 0;
                    }

                    else if (num1 > 255)
                    {
                        dataPtr[1] = 255;
                    }
                    else dataPtr[1] = (byte)num1;

                    if (num2 < 0)
                    {
                        dataPtr[2] = 0;
                    }

                    else if (num2 > 255)
                    {
                        dataPtr[2] = 255;
                    }
                    else dataPtr[2] = (byte)num2;

                    //passa para o canto inferior esquerdo
                    dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();

                    dataPtr += widthStep * (height - 1);
                    dataPtrOrig += widthStep * (height - 1);

                    //canto inferior esquerdo
                    num0 = Math.Round((dataPtrOrig[0] * (matrix[1, 1] + matrix[1, 2] + matrix[0, 2] + matrix[0, 1]) + (dataPtrOrig + nChan)[0] * (matrix[2, 1] + matrix[2, 2]) + (dataPtrOrig - widthStep)[0] * (matrix[1, 0] + matrix[0, 0]) + (dataPtrOrig - widthStep + nChan)[0] * matrix[2, 0]) / matrixWeight);
                    num1 = Math.Round((dataPtrOrig[1] * (matrix[1, 1] + matrix[1, 2] + matrix[0, 2] + matrix[0, 1]) + (dataPtrOrig + nChan)[1] * (matrix[2, 1] + matrix[2, 2]) + (dataPtrOrig - widthStep)[1] * (matrix[1, 0] + matrix[0, 0]) + (dataPtrOrig - widthStep + nChan)[1] * matrix[2, 0]) / matrixWeight);
                    num2 = Math.Round((dataPtrOrig[2] * (matrix[1, 1] + matrix[1, 2] + matrix[0, 2] + matrix[0, 1]) + (dataPtrOrig + nChan)[2] * (matrix[2, 1] + matrix[2, 2]) + (dataPtrOrig - widthStep)[2] * (matrix[1, 0] + matrix[0, 0]) + (dataPtrOrig - widthStep + nChan)[2] * matrix[2, 0]) / matrixWeight);

                    if (num0 < 0)
                    {
                        dataPtr[0] = 0;
                    }

                    else if (num0 > 255)
                    {
                        dataPtr[0] = 255;
                    }
                    else dataPtr[0] = (byte)num0;


                    if (num1 < 0)
                    {
                        dataPtr[1] = 0;
                    }

                    else if (num1 > 255)
                    {
                        dataPtr[1] = 255;
                    }
                    else dataPtr[1] = (byte)num1;

                    if (num2 < 0)
                    {
                        dataPtr[2] = 0;
                    }

                    else if (num2 > 255)
                    {
                        dataPtr[2] = 255;
                    }
                    else dataPtr[2] = (byte)num2;


                    //faz a linha y=heigth
                    for (int a = 1; a < width - 1; a++)
                    {
                        dataPtr += nChan; //passa para o proximo pixel para fazer a linha de baixo (y = heigth)

                        dataPtrOrig += nChan;
                        num0 = Math.Round(((dataPtrOrig - nChan)[0] * (matrix[0, 1] + matrix[0, 2]) + dataPtrOrig[0] * (matrix[1, 1] + matrix[1, 2]) + (dataPtrOrig + nChan)[0] * (matrix[2, 1] + matrix[2, 2]) + (dataPtrOrig - nChan - widthStep)[0] * matrix[0, 0] + (dataPtrOrig - widthStep)[0] * matrix[1, 0] + (dataPtrOrig + nChan - widthStep)[0] * matrix[2, 0]) / matrixWeight);
                        num1 = Math.Round(((dataPtrOrig - nChan)[1] * (matrix[0, 1] + matrix[0, 2]) + dataPtrOrig[1] * (matrix[1, 1] + matrix[1, 2]) + (dataPtrOrig + nChan)[1] * (matrix[2, 1] + matrix[2, 2]) + (dataPtrOrig - nChan - widthStep)[1] * matrix[0, 0] + (dataPtrOrig - widthStep)[1] * matrix[1, 0] + (dataPtrOrig + nChan - widthStep)[1] * matrix[2, 0]) / matrixWeight);
                        num2 = Math.Round(((dataPtrOrig - nChan)[2] * (matrix[0, 1] + matrix[0, 2]) + dataPtrOrig[2] * (matrix[1, 1] + matrix[1, 2]) + (dataPtrOrig + nChan)[2] * (matrix[2, 1] + matrix[2, 2]) + (dataPtrOrig - nChan - widthStep)[2] * matrix[0, 0] + (dataPtrOrig - widthStep)[2] * matrix[1, 0] + (dataPtrOrig + nChan - widthStep)[2] * matrix[2, 0]) / matrixWeight);

                        if (num0 < 0)
                        {
                            dataPtr[0] = 0;
                        }

                        else if (num0 > 255)
                        {
                            dataPtr[0] = 255;
                        }
                        else dataPtr[0] = (byte)num0;


                        if (num1 < 0)
                        {
                            dataPtr[1] = 0;
                        }

                        else if (num1 > 255)
                        {
                            dataPtr[1] = 255;
                        }
                        else dataPtr[1] = (byte)num1;

                        if (num2 < 0)
                        {
                            dataPtr[2] = 0;
                        }

                        else if (num2 > 255)
                        {
                            dataPtr[2] = 255;
                        }
                        else dataPtr[2] = (byte)num2;
                    }

                    //canto inferior direito
                    dataPtr += nChan; //avança o dataPtr para o canto inferior direito
                    dataPtrOrig += nChan;
                    //segundo canto; P(x,y) * 4 + P(x-1,y) * 2 + P(x,y-1) *2 + P(x-1,y-1)
                    num0 = Math.Round((dataPtrOrig[0] * (matrix[2, 2] + matrix[2, 1] + matrix[1, 2] + matrix[1, 1]) + (dataPtrOrig - nChan)[0] * (matrix[0, 1] + matrix[0, 2]) + (dataPtrOrig - widthStep)[0] * (matrix[2, 0] + matrix[1, 0]) + (dataPtrOrig - widthStep - nChan)[0] * matrix[0, 0]) / matrixWeight);
                    num1 = Math.Round((dataPtrOrig[1] * (matrix[2, 2] + matrix[2, 1] + matrix[1, 2] + matrix[1, 1]) + (dataPtrOrig - nChan)[1] * (matrix[0, 1] + matrix[0, 2]) + (dataPtrOrig - widthStep)[1] * (matrix[2, 0] + matrix[1, 0]) + (dataPtrOrig - widthStep - nChan)[1] * matrix[0, 0]) / matrixWeight);
                    num2 = Math.Round((dataPtrOrig[2] * (matrix[2, 2] + matrix[2, 1] + matrix[1, 2] + matrix[1, 1]) + (dataPtrOrig - nChan)[2] * (matrix[0, 1] + matrix[0, 2]) + (dataPtrOrig - widthStep)[2] * (matrix[2, 0] + matrix[1, 0]) + (dataPtrOrig - widthStep - nChan)[2] * matrix[0, 0]) / matrixWeight);

                    if (num0 < 0)
                    {
                        dataPtr[0] = 0;
                    }

                    else if (num0 > 255)
                    {
                        dataPtr[0] = 255;
                    }
                    else dataPtr[0] = (byte)num0;


                    if (num1 < 0)
                    {
                        dataPtr[1] = 0;
                    }

                    else if (num1 > 255)
                    {
                        dataPtr[1] = 255;
                    }
                    else dataPtr[1] = (byte)num1;

                    if (num2 < 0)
                    {
                        dataPtr[2] = 0;
                    }

                    else if (num2 > 255)
                    {
                        dataPtr[2] = 255;
                    }
                    else dataPtr[2] = (byte)num2;


                    dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();
                    //faz a linha x=0
                    //dataPtr += widthStep;//fica em P(0,1) e faz a linha x = 0
                    for (int a = 1; a < height - 1; a++)
                    {
                        dataPtr += widthStep; //passa para o pixel de baixo (em x = 0)

                        dataPtrOrig += widthStep;
                        //ptrAux = dataPtrOrig + (widthStep * a);
                        num0 = Math.Round(((dataPtrOrig - widthStep)[0] * (matrix[0, 0] + matrix[1, 0]) + dataPtrOrig[0] * (matrix[0, 1] + matrix[1, 1]) + (dataPtrOrig + widthStep)[0] * (matrix[0, 2] + matrix[1, 2]) + (dataPtrOrig + nChan - widthStep)[0] * matrix[2, 0] + (dataPtrOrig + nChan)[0] * matrix[2, 1] + (dataPtrOrig + nChan + widthStep)[0] * matrix[2, 2]) / matrixWeight);
                        num1 = Math.Round(((dataPtrOrig - widthStep)[1] * (matrix[0, 0] + matrix[1, 0]) + dataPtrOrig[1] * (matrix[0, 1] + matrix[1, 1]) + (dataPtrOrig + widthStep)[1] * (matrix[0, 2] + matrix[1, 2]) + (dataPtrOrig + nChan - widthStep)[1] * matrix[2, 0] + (dataPtrOrig + nChan)[1] * matrix[2, 1] + (dataPtrOrig + nChan + widthStep)[1] * matrix[2, 2]) / matrixWeight);
                        num2 = Math.Round(((dataPtrOrig - widthStep)[2] * (matrix[0, 0] + matrix[1, 0]) + dataPtrOrig[2] * (matrix[0, 1] + matrix[1, 1]) + (dataPtrOrig + widthStep)[2] * (matrix[0, 2] + matrix[1, 2]) + (dataPtrOrig + nChan - widthStep)[2] * matrix[2, 0] + (dataPtrOrig + nChan)[2] * matrix[2, 1] + (dataPtrOrig + nChan + widthStep)[2] * matrix[2, 2]) / matrixWeight);

                        if (num0 < 0)
                        {
                            dataPtr[0] = 0;
                        }

                        else if (num0 > 255)
                        {
                            dataPtr[0] = 255;
                        }
                        else dataPtr[0] = (byte)num0;


                        if (num1 < 0)
                        {
                            dataPtr[1] = 0;
                        }

                        else if (num1 > 255)
                        {
                            dataPtr[1] = 255;
                        }
                        else dataPtr[1] = (byte)num1;

                        if (num2 < 0)
                        {
                            dataPtr[2] = 0;
                        }

                        else if (num2 > 255)
                        {
                            dataPtr[2] = 255;
                        }
                        else dataPtr[2] = (byte)num2;

                    }

                    dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();
                    dataPtr += ((width - 1) * nChan) + widthStep;//fica em P(width,1) e faz a linha x = width
                    dataPtrOrig += ((width - 1) * nChan) + widthStep;//fica em P(width,1) e faz a linha x = width

                    //faz a linha x = width
                    for (int a = 1; a < height - 1; a++)
                    {
                        num0 = Math.Round(((dataPtrOrig - widthStep)[0] * (matrix[1, 0] + matrix[2, 0]) + dataPtrOrig[0] * (matrix[1, 1] + matrix[2, 1]) + (dataPtrOrig + widthStep)[0] * (matrix[1, 2] + matrix[2, 2]) + (dataPtrOrig - nChan - widthStep)[0] * matrix[0, 0] + (dataPtrOrig - nChan)[0] * matrix[0, 1] + (dataPtrOrig - nChan + widthStep)[0] * matrix[0, 2]) / matrixWeight);
                        num1 = Math.Round(((dataPtrOrig - widthStep)[1] * (matrix[1, 0] + matrix[2, 0]) + dataPtrOrig[1] * (matrix[1, 1] + matrix[2, 1]) + (dataPtrOrig + widthStep)[1] * (matrix[1, 2] + matrix[2, 2]) + (dataPtrOrig - nChan - widthStep)[1] * matrix[0, 0] + (dataPtrOrig - nChan)[1] * matrix[0, 1] + (dataPtrOrig - nChan + widthStep)[1] * matrix[0, 2]) / matrixWeight);
                        num2 = Math.Round(((dataPtrOrig - widthStep)[2] * (matrix[1, 0] + matrix[2, 0]) + dataPtrOrig[2] * (matrix[1, 1] + matrix[2, 1]) + (dataPtrOrig + widthStep)[2] * (matrix[1, 2] + matrix[2, 2]) + (dataPtrOrig - nChan - widthStep)[2] * matrix[0, 0] + (dataPtrOrig - nChan)[2] * matrix[0, 1] + (dataPtrOrig - nChan + widthStep)[2] * matrix[0, 2]) / matrixWeight);

                        if (num0 < 0)
                        {
                            dataPtr[0] = 0;
                        }

                        else if (num0 > 255)
                        {
                            dataPtr[0] = 255;
                        }
                        else dataPtr[0] = (byte)num0;


                        if (num1 < 0)
                        {
                            dataPtr[1] = 0;
                        }

                        else if (num1 > 255)
                        {
                            dataPtr[1] = 255;
                        }
                        else dataPtr[1] = (byte)num1;

                        if (num2 < 0)
                        {
                            dataPtr[2] = 0;
                        }

                        else if (num2 > 255)
                        {
                            dataPtr[2] = 255;
                        }
                        else dataPtr[2] = (byte)num2;

                        dataPtr += widthStep; //passa para o pixel de baixo (em x = width)

                        dataPtrOrig += widthStep;

                    }
                }
            }
        }



        public static void Sobel(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)//poderia ter sido utilizado um kernel de sobel mas a funcao Non-uniform nao estava pronta
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                MIplImage mOrig = imgCopy.MIplImage;
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer(); // Pointer to the image
                int x = 0, y = 0;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int widthStep = m.widthStep;
                int nChan_menos_widthS = nChan - widthStep;
                int nChan_mais_widthS = nChan + widthStep;
                int r, g, b;

                if (nChan == 3) // image in RGB
                {
                    dataPtr += widthStep + nChan;
                    dataPtrOrig += widthStep + nChan;

                    //ConvertToGray(img);
                    for (y = 1; y < height - 1; y++)
                    {
                        for (x = 1; x < width - 1; x++)
                        {
                            r = (Math.Abs((
                                (dataPtrOrig - nChan_mais_widthS)[0] +
                                (2 * (dataPtrOrig - nChan)[0]) +
                                (dataPtrOrig - nChan_menos_widthS)[0]
                                ) - (
                                (dataPtrOrig + nChan - widthStep)[0] +
                                (2 * (dataPtrOrig + nChan)[0]) +
                                (dataPtrOrig + nChan_mais_widthS)[0])
                                ) + Math.Abs((
                                (dataPtrOrig - nChan_menos_widthS)[0] +
                                (2 * (dataPtrOrig + widthStep)[0]) +
                                (dataPtrOrig + nChan_mais_widthS)[0]
                                ) - (
                                (dataPtrOrig - nChan_mais_widthS)[0] +
                                (2 * (dataPtrOrig - widthStep)[0]) +
                                (dataPtrOrig + nChan - widthStep)[0])));
                            if (r > 255)
                                dataPtr[0] = 255;
                            else if (r < 0)
                                dataPtr[0] = (byte)Math.Abs(r);
                            else
                                dataPtr[0] = (byte)r;

                            g = (Math.Abs((
                                (dataPtrOrig - nChan_mais_widthS)[1] +
                                (2 * (dataPtrOrig - nChan)[1]) +
                                (dataPtrOrig - nChan_menos_widthS)[1]
                                ) - (
                                (dataPtrOrig + nChan - widthStep)[1] +
                                (2 * (dataPtrOrig + nChan)[1]) +
                                (dataPtrOrig + nChan_mais_widthS)[1])
                                ) + Math.Abs((
                                (dataPtrOrig - nChan_menos_widthS)[1] +
                                (2 * (dataPtrOrig + widthStep)[1]) +
                                (dataPtrOrig + nChan_mais_widthS)[1]
                                ) - (
                                (dataPtrOrig - nChan_mais_widthS)[1] +
                                (2 * (dataPtrOrig - widthStep)[1]) +
                                (dataPtrOrig + nChan - widthStep)[1])));
                            if (g > 255)
                                dataPtr[1] = 255;
                            else if (g < 0)
                                dataPtr[1] = (byte)Math.Abs(g);
                            else
                                dataPtr[1] = (byte)g;

                            b = (Math.Abs((
                                (dataPtrOrig - nChan_mais_widthS)[2] +
                                (2 * (dataPtrOrig - nChan)[2]) +
                                (dataPtrOrig - nChan_menos_widthS)[2]
                                ) - (
                                (dataPtrOrig + nChan - widthStep)[2] +
                                (2 * (dataPtrOrig + nChan)[2]) +
                                (dataPtrOrig + nChan_mais_widthS)[2])
                                ) + Math.Abs((
                                (dataPtrOrig - nChan_menos_widthS)[2] +
                                (2 * (dataPtrOrig + widthStep)[2]) +
                                (dataPtrOrig + nChan_mais_widthS)[2]
                                ) - (
                                (dataPtrOrig - nChan_mais_widthS)[2] +
                                (2 * (dataPtrOrig - widthStep)[2]) +
                                (dataPtrOrig + nChan - widthStep)[2])));
                            if (b > 255)
                                dataPtr[2] = 255;
                            else if (b < 0)
                                dataPtr[2] = (byte)Math.Abs(b);
                            else
                                dataPtr[2] = (byte)b;
                            dataPtr += nChan;
                            dataPtrOrig += nChan;
                        }
                        dataPtr += padding + (2 * nChan);
                        dataPtrOrig += padding + (2 * nChan);

                    }

                    dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();

                    //primeiro canto; P(x,y) * 4 + P(x+1,y) * 2 + P(x,y+1) *2 + P(x+1,y+1)
                    r = (Math.Abs(((3 * (dataPtrOrig)[0]) + (dataPtrOrig + widthStep)[0]) - ((3 * (dataPtrOrig + nChan)[0]) + (dataPtrOrig + nChan_mais_widthS)[0])) + Math.Abs(((3 * (dataPtrOrig + widthStep)[0]) + (dataPtrOrig + nChan_mais_widthS)[0]) - ((3 * dataPtrOrig[0]) + (dataPtrOrig + nChan)[0])));
                    if (r > 255)
                        dataPtr[0] = 255;
                    else if (r < 0)
                        dataPtr[0] = (byte)Math.Abs(r);
                    else
                        dataPtr[0] = (byte)r;

                    g = (Math.Abs(((3 * (dataPtrOrig)[1]) + (dataPtrOrig + widthStep)[1]) - ((3 * (dataPtrOrig + nChan)[1]) + (dataPtrOrig + nChan_mais_widthS)[1])) + Math.Abs(((3 * (dataPtrOrig + widthStep)[1]) + (dataPtrOrig + nChan_mais_widthS)[1]) - ((3 * dataPtrOrig[1]) + (dataPtrOrig + nChan)[1])));
                    if (g > 255)
                        dataPtr[1] = 255;
                    else if (g < 0)
                        dataPtr[1] = (byte)Math.Abs(g);
                    else
                        dataPtr[1] = (byte)g;

                    b = (Math.Abs(((3 * (dataPtrOrig)[2]) + (dataPtrOrig + widthStep)[2]) - ((3 * (dataPtrOrig + nChan)[2]) + (dataPtrOrig + nChan_mais_widthS)[2])) + Math.Abs(((3 * (dataPtrOrig + widthStep)[2]) + (dataPtrOrig + nChan_mais_widthS)[2]) - ((3 * dataPtrOrig[2]) + (dataPtrOrig + nChan)[2])));
                    if (b > 255)
                        dataPtr[2] = 255;
                    else if (b < 0)
                        dataPtr[2] = (byte)Math.Abs(b);
                    else
                        dataPtr[2] = (byte)b;


                    //faz a linha y=0
                    dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();
                    for (int a = 1; a < width - 1; a++)
                    {
                        dataPtr += nChan; //passa para o proximo pixel 

                        dataPtrOrig += nChan;
                        r = (Math.Abs(((3 * (dataPtrOrig - nChan)[0]) + (dataPtrOrig + widthStep - nChan)[0]) - ((3 * (dataPtrOrig + nChan)[0]) + (dataPtrOrig + nChan_mais_widthS)[0])) + Math.Abs((((dataPtrOrig + widthStep - nChan)[0] + (2 * (dataPtrOrig + widthStep)[0])) + (dataPtrOrig + nChan_mais_widthS)[0]) - (((dataPtrOrig - nChan)[0] + (2 * (dataPtrOrig)[0])) + (dataPtrOrig + nChan)[0])));
                        if (r > 255)
                            dataPtr[0] = 255;
                        else if (r < 0)
                            dataPtr[0] = (byte)Math.Abs(r);
                        else
                            dataPtr[0] = (byte)r;

                        g = (Math.Abs(((3 * (dataPtrOrig - nChan)[1]) + (dataPtrOrig + widthStep - nChan)[1]) - ((3 * (dataPtrOrig + nChan)[1]) + (dataPtrOrig + nChan_mais_widthS)[1])) + Math.Abs((((dataPtrOrig + widthStep - nChan)[1] + (2 * (dataPtrOrig + widthStep)[1])) + (dataPtrOrig + nChan_mais_widthS)[1]) - (((dataPtrOrig - nChan)[1] + (2 * (dataPtrOrig)[1])) + (dataPtrOrig + nChan)[1])));
                        if (g > 255)
                            dataPtr[1] = 255;
                        else if (g < 0)
                            dataPtr[1] = (byte)Math.Abs(g);
                        else
                            dataPtr[1] = (byte)g;

                        b = (Math.Abs(((3 * (dataPtrOrig - nChan)[2]) + (dataPtrOrig + widthStep - nChan)[2]) - ((3 * (dataPtrOrig + nChan)[2]) + (dataPtrOrig + nChan_mais_widthS)[2])) + Math.Abs((((dataPtrOrig + widthStep - nChan)[2] + (2 * (dataPtrOrig + widthStep)[2])) + (dataPtrOrig + nChan_mais_widthS)[2]) - (((dataPtrOrig - nChan)[2] + (2 * (dataPtrOrig)[2])) + (dataPtrOrig + nChan)[2])));
                        if (b > 255)
                            dataPtr[2] = 255;
                        else if (b < 0)
                            dataPtr[2] = (byte)Math.Abs(b);
                        else
                            dataPtr[2] = (byte)b;
                    }


                    dataPtrOrig += nChan;
                    dataPtr += nChan;
                    //dataPtr += (width - 1) * nChan; //avança o dataPtr para o segundo canto
                    //segundo canto; P(x,y) * 4 + P(x-1,y) * 2 + P(x,y+1) *2 + P(x-1,y+1)
                    r = (Math.Abs(((3 * (dataPtrOrig - nChan)[0]) + (dataPtrOrig + widthStep - nChan)[0]) - ((3 * (dataPtrOrig)[0]) + (dataPtrOrig + widthStep)[0])) + Math.Abs((((dataPtrOrig + widthStep - nChan)[0] + (3 * (dataPtrOrig + widthStep)[0])) - (((dataPtrOrig - nChan)[0] + (3 * (dataPtrOrig)[0]))))));
                    if (r > 255)
                        dataPtr[0] = 255;
                    else if (r < 0)
                        dataPtr[0] = (byte)Math.Abs(r);
                    else
                        dataPtr[0] = (byte)r;

                    g = (Math.Abs(((3 * (dataPtrOrig - nChan)[1]) + (dataPtrOrig + widthStep - nChan)[1]) - ((3 * (dataPtrOrig)[1]) + (dataPtrOrig + widthStep)[1])) + Math.Abs((((dataPtrOrig + widthStep - nChan)[1] + (3 * (dataPtrOrig + widthStep)[1])) - (((dataPtrOrig - nChan)[1] + (3 * (dataPtrOrig)[1]))))));
                    if (g > 255)
                        dataPtr[1] = 255;
                    else if (g < 0)
                        dataPtr[1] = (byte)Math.Abs(g);
                    else
                        dataPtr[1] = (byte)g;

                    b = (Math.Abs(((3 * (dataPtrOrig - nChan)[2]) + (dataPtrOrig + widthStep - nChan)[2]) - ((3 * (dataPtrOrig)[2]) + (dataPtrOrig + widthStep)[2])) + Math.Abs((((dataPtrOrig + widthStep - nChan)[2] + (3 * (dataPtrOrig + widthStep)[2])) - (((dataPtrOrig - nChan)[2] + (3 * (dataPtrOrig)[2]))))));
                    if (b > 255)
                        dataPtr[2] = 255;
                    else if (b < 0)
                        dataPtr[2] = (byte)Math.Abs(b);
                    else
                        dataPtr[2] = (byte)b;

                    //passa para o canto inferior esquerdo
                    dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();

                    dataPtr += widthStep * (height - 1);
                    dataPtrOrig += widthStep * (height - 1);

                    //canto inferior esquerdo
                    r = (Math.Abs(((dataPtrOrig - widthStep)[0] + (3 * dataPtrOrig[0])) - ((dataPtrOrig + nChan - widthStep)[0] + (3 * (dataPtrOrig + nChan)[0]))) + Math.Abs(((3 * dataPtrOrig[0]) + (dataPtrOrig + nChan)[0]) - ((3 * (dataPtrOrig - widthStep)[0]) + (dataPtrOrig + nChan - widthStep)[0])));
                    if (r > 255)
                        dataPtr[0] = 255;
                    else if (r < 0)
                        dataPtr[0] = (byte)Math.Abs(r);
                    else
                        dataPtr[0] = (byte)r;

                    g = (Math.Abs(((dataPtrOrig - widthStep)[1] + (3 * dataPtrOrig[1])) - ((dataPtrOrig + nChan - widthStep)[1] + (3 * (dataPtrOrig + nChan)[1]))) + Math.Abs(((3 * dataPtrOrig[1]) + (dataPtrOrig + nChan)[1]) - ((3 * (dataPtrOrig - widthStep)[1]) + (dataPtrOrig + nChan - widthStep)[1])));
                    if (g > 255)
                        dataPtr[1] = 255;
                    else if (g < 0)
                        dataPtr[1] = (byte)Math.Abs(g);
                    else
                        dataPtr[1] = (byte)g;

                    b = (Math.Abs(((dataPtrOrig - widthStep)[2] + (3 * dataPtrOrig[2])) - ((dataPtrOrig + nChan - widthStep)[2] + (3 * (dataPtrOrig + nChan)[2]))) + Math.Abs(((3 * dataPtrOrig[2]) + (dataPtrOrig + nChan)[2]) - ((3 * (dataPtrOrig - widthStep)[2]) + (dataPtrOrig + nChan - widthStep)[2])));
                    if (b > 255)
                        dataPtr[2] = 255;
                    else if (b < 0)
                        dataPtr[2] = (byte)Math.Abs(b);
                    else
                        dataPtr[2] = (byte)b;

                    //faz a linha y=heigth  //ja esta feito!!!!!
                    for (int a = 1; a < width - 1; a++)
                    {
                        dataPtr += nChan; //passa para o proximo pixel para fazer a linha de baixo (y = heigth)

                        dataPtrOrig += nChan;
                        r = (Math.Abs(((3 * (dataPtrOrig - nChan)[0]) + (dataPtrOrig - widthStep - nChan)[0]) - ((3 * (dataPtrOrig + nChan)[0]) + (dataPtrOrig + nChan_menos_widthS)[0])) + Math.Abs((((dataPtrOrig - nChan)[0] + (2 * (dataPtrOrig)[0])) + (dataPtrOrig + nChan)[0]) - (((dataPtrOrig - nChan - widthStep)[0] + (2 * (dataPtrOrig - widthStep)[0])) + (dataPtrOrig + nChan - widthStep)[0])));
                        if (r > 255)
                            dataPtr[0] = 255;
                        else if (r < 0)
                            dataPtr[0] = (byte)Math.Abs(r);
                        else
                            dataPtr[0] = (byte)r;

                        g = (Math.Abs(((3 * (dataPtrOrig - nChan)[1]) + (dataPtrOrig - widthStep - nChan)[1]) - ((3 * (dataPtrOrig + nChan)[1]) + (dataPtrOrig + nChan_menos_widthS)[1])) + Math.Abs((((dataPtrOrig - nChan)[1] + (2 * (dataPtrOrig)[1])) + (dataPtrOrig + nChan)[1]) - (((dataPtrOrig - nChan - widthStep)[1] + (2 * (dataPtrOrig - widthStep)[1])) + (dataPtrOrig + nChan - widthStep)[1])));
                        if (g > 255)
                            dataPtr[1] = 255;
                        else if (g < 0)
                            dataPtr[1] = (byte)Math.Abs(g);
                        else
                            dataPtr[1] = (byte)g;

                        b = (Math.Abs(((3 * (dataPtrOrig - nChan)[2]) + (dataPtrOrig - widthStep - nChan)[2]) - ((3 * (dataPtrOrig + nChan)[2]) + (dataPtrOrig + nChan_menos_widthS)[2])) + Math.Abs((((dataPtrOrig - nChan)[2] + (2 * (dataPtrOrig)[2])) + (dataPtrOrig + nChan)[2]) - (((dataPtrOrig - nChan - widthStep)[2] + (2 * (dataPtrOrig - widthStep)[2])) + (dataPtrOrig + nChan - widthStep)[2])));
                        if (b > 255)
                            dataPtr[2] = 255;
                        else if (b < 0)
                            dataPtr[2] = (byte)Math.Abs(b);
                        else
                            dataPtr[2] = (byte)b;

                    }


                    //canto inferior direito
                    dataPtr += nChan; //avança o dataPtr para o canto inferior direito
                    dataPtrOrig += nChan;
                    //segundo canto; P(x,y) * 4 + P(x-1,y) * 2 + P(x,y-1) *2 + P(x-1,y-1)
                    r = (Math.Abs(((dataPtrOrig - widthStep - nChan)[0] + (3 * (dataPtrOrig - nChan)[0])) - ((dataPtrOrig - widthStep)[0] + (3 * (dataPtrOrig)[0]))) + Math.Abs(((dataPtrOrig - nChan)[0] + (3 * dataPtrOrig[0])) - ((dataPtrOrig - nChan - widthStep)[0] + (3 * (dataPtrOrig - widthStep)[0]))));
                    if (r > 255)
                        dataPtr[0] = 255;
                    else if (r < 0)
                        dataPtr[0] = (byte)Math.Abs(r);
                    else
                        dataPtr[0] = (byte)r;

                    g = (Math.Abs(((dataPtrOrig - widthStep - nChan)[1] + (3 * (dataPtrOrig - nChan)[1])) - ((dataPtrOrig - widthStep)[1] + (3 * (dataPtrOrig)[1]))) + Math.Abs(((dataPtrOrig - nChan)[1] + (3 * dataPtrOrig[1])) - ((dataPtrOrig - nChan - widthStep)[1] + (3 * (dataPtrOrig - widthStep)[1]))));
                    if (g > 255)
                        dataPtr[1] = 255;
                    else if (g < 0)
                        dataPtr[1] = (byte)Math.Abs(g);
                    else
                        dataPtr[1] = (byte)g;

                    b = (Math.Abs(((dataPtrOrig - widthStep - nChan)[2] + (3 * (dataPtrOrig - nChan)[2])) - ((dataPtrOrig - widthStep)[2] + (3 * (dataPtrOrig)[2]))) + Math.Abs(((dataPtrOrig - nChan)[2] + (3 * dataPtrOrig[2])) - ((dataPtrOrig - nChan - widthStep)[2] + (3 * (dataPtrOrig - widthStep)[2]))));
                    if (b > 255)
                        dataPtr[2] = 255;
                    else if (b < 0)
                        dataPtr[2] = (byte)Math.Abs(b);
                    else
                        dataPtr[2] = (byte)b;


                    dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();
                    //faz a linha x=0
                    //dataPtr += widthStep;//fica em P(0,1) e faz a linha x = 0
                    for (int a = 1; a < height - 1; a++)
                    {
                        dataPtr += widthStep; //passa para o pixel de baixo (em x = 0)
                        dataPtrOrig += widthStep;

                        r = (Math.Abs(((dataPtrOrig - widthStep)[0] + (2 * dataPtrOrig[0]) + (dataPtrOrig + widthStep)[0]) - ((dataPtrOrig + nChan - widthStep)[0] + (2 * (dataPtrOrig + nChan)[0]) + (dataPtrOrig + nChan_mais_widthS)[0])) + Math.Abs(((3 * (dataPtrOrig + widthStep)[0]) + (dataPtrOrig + nChan_mais_widthS)[0]) - (((3 * (dataPtrOrig - widthStep)[0])) + (dataPtrOrig + nChan - widthStep)[0])));
                        if (r > 255)
                            dataPtr[0] = 255;
                        else if (r < 0)
                            dataPtr[0] = (byte)Math.Abs(r);
                        else
                            dataPtr[0] = (byte)r;

                        g = (Math.Abs(((dataPtrOrig - widthStep)[1] + (2 * dataPtrOrig[1]) + (dataPtrOrig + widthStep)[1]) - ((dataPtrOrig + nChan - widthStep)[1] + (2 * (dataPtrOrig + nChan)[1]) + (dataPtrOrig + nChan_mais_widthS)[1])) + Math.Abs(((3 * (dataPtrOrig + widthStep)[1]) + (dataPtrOrig + nChan_mais_widthS)[1]) - (((3 * (dataPtrOrig - widthStep)[1])) + (dataPtrOrig + nChan - widthStep)[1])));
                        if (g > 255)
                            dataPtr[1] = 255;
                        else if (g < 0)
                            dataPtr[1] = (byte)Math.Abs(g);
                        else
                            dataPtr[1] = (byte)g;

                        b = (Math.Abs(((dataPtrOrig - widthStep)[2] + (2 * dataPtrOrig[2]) + (dataPtrOrig + widthStep)[2]) - ((dataPtrOrig + nChan - widthStep)[2] + (2 * (dataPtrOrig + nChan)[2]) + (dataPtrOrig + nChan_mais_widthS)[2])) + Math.Abs(((3 * (dataPtrOrig + widthStep)[2]) + (dataPtrOrig + nChan_mais_widthS)[2]) - (((3 * (dataPtrOrig - widthStep)[2])) + (dataPtrOrig + nChan - widthStep)[2])));
                        if (b > 255)
                            dataPtr[2] = 255;
                        else if (b < 0)
                            dataPtr[2] = (byte)Math.Abs(b);
                        else
                            dataPtr[2] = (byte)b;
                    }

                    dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();
                    dataPtr += ((width - 1) * nChan) + widthStep;//fica em P(width,1) e faz a linha x = width
                    dataPtrOrig += ((width - 1) * nChan) + widthStep;//fica em P(width,1) e faz a linha x = width

                    //faz a linha x = width
                    for (int a = 1; a < height - 1; a++)
                    {
                        r = (Math.Abs(((dataPtrOrig - widthStep - nChan)[0] + (2 * (dataPtrOrig - nChan)[0]) + (dataPtrOrig + widthStep - nChan)[0]) - ((dataPtrOrig - widthStep)[0] + (2 * (dataPtrOrig)[0]) + (dataPtrOrig + widthStep)[0])) + Math.Abs(((3 * (dataPtrOrig + widthStep)[0]) + (dataPtrOrig + widthStep - nChan)[0]) - (((3 * (dataPtrOrig - widthStep)[0])) + (dataPtrOrig - nChan - widthStep)[0])));
                        if (r > 255)
                            dataPtr[0] = 255;
                        else if (r < 0)
                            dataPtr[0] = (byte)Math.Abs(r);
                        else
                            dataPtr[0] = (byte)r;

                        g = (Math.Abs(((dataPtrOrig - widthStep - nChan)[1] + (2 * (dataPtrOrig - nChan)[1]) + (dataPtrOrig + widthStep - nChan)[1]) - ((dataPtrOrig - widthStep)[1] + (2 * (dataPtrOrig)[1]) + (dataPtrOrig + widthStep)[1])) + Math.Abs(((3 * (dataPtrOrig + widthStep)[1]) + (dataPtrOrig + widthStep - nChan)[1]) - (((3 * (dataPtrOrig - widthStep)[1])) + (dataPtrOrig - nChan - widthStep)[1])));
                        if (g > 255)
                            dataPtr[1] = 255;
                        else if (g < 0)
                            dataPtr[1] = (byte)Math.Abs(g);
                        else
                            dataPtr[1] = (byte)g;

                        b = (Math.Abs(((dataPtrOrig - widthStep - nChan)[2] + (2 * (dataPtrOrig - nChan)[2]) + (dataPtrOrig + widthStep - nChan)[2]) - ((dataPtrOrig - widthStep)[2] + (2 * (dataPtrOrig)[2]) + (dataPtrOrig + widthStep)[2])) + Math.Abs(((3 * (dataPtrOrig + widthStep)[2]) + (dataPtrOrig + widthStep - nChan)[2]) - (((3 * (dataPtrOrig - widthStep)[2])) + (dataPtrOrig - nChan - widthStep)[2])));
                        if (b > 255)
                            dataPtr[2] = 255;
                        else if (b < 0)
                            dataPtr[2] = (byte)Math.Abs(b);
                        else
                            dataPtr[2] = (byte)b;

                        dataPtr += widthStep; //passa para o pixel de baixo (em x = width)
                        dataPtrOrig += widthStep;
                    }
                }
            }

        }

        public static void Diferentiation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                MIplImage mOrig = imgCopy.MIplImage;
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer(); // Pointer to the image
                int x = 0, y = 0;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int widthStep = m.widthStep;
                int r, g, b;


                if (nChan == 3) // image in RGB
                {
                    //dataPtr += widthStep + nChan;
                    //dataPtrOrig += widthStep + nChan;

                    for (y = 0; y < height - 1; y++)
                    {
                        for (x = 0; x < width - 1; x++)
                        {
                            r = (Math.Abs(dataPtrOrig[0] - (dataPtrOrig + nChan)[0]) + Math.Abs(dataPtrOrig[0] - (dataPtrOrig + widthStep)[0]));
                            if (r > 255)
                                dataPtr[0] = 255;
                            else if (r < 0)
                                dataPtr[0] = (byte)Math.Abs(r);
                            else
                                dataPtr[0] = (byte)r;

                            g = (Math.Abs(dataPtrOrig[1] - (dataPtrOrig + nChan)[1]) + Math.Abs(dataPtrOrig[1] - (dataPtrOrig + widthStep)[1]));
                            if (g > 255)
                                dataPtr[1] = 255;
                            else if (g < 0)
                                dataPtr[1] = (byte)Math.Abs(g);
                            else
                                dataPtr[1] = (byte)g;

                            b = (Math.Abs(dataPtrOrig[2] - (dataPtrOrig + nChan)[2]) + Math.Abs(dataPtrOrig[2] - (dataPtrOrig + widthStep)[2]));
                            if (b > 255)
                                dataPtr[2] = 255;
                            else if (b < 0)
                                dataPtr[2] = (byte)Math.Abs(b);
                            else
                                dataPtr[2] = (byte)b;


                            dataPtr += nChan;
                            dataPtrOrig += nChan;
                        }
                        dataPtr += padding + nChan;// (2 * nChan);
                        dataPtrOrig += padding + nChan;// (2 * nChan);

                    }

                    //faz a linha x=width
                    dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();

                    dataPtr += ((width - 1) * nChan);//fica em P(width,1) e faz a linha x = width
                    dataPtrOrig += ((width - 1) * nChan);//fica em P(width,1) e faz a linha x = width

                    //faz a linha x = width
                    for (int a = 0; a < height - 1; a++)
                    {
                        r = (byte)(0 + Math.Abs(dataPtrOrig[0] - (dataPtrOrig + widthStep)[0])); //o primeiro é zero pois subtraimos por ele proprio
                        if (r > 255)
                            dataPtr[0] = 255;
                        else if (r < 0)
                            dataPtr[0] = (byte)Math.Abs(r);
                        else
                            dataPtr[0] = (byte)r;

                        g = (byte)(0 + Math.Abs(dataPtrOrig[1] - (dataPtrOrig + widthStep)[1]));
                        if (g > 255)
                            dataPtr[1] = 255;
                        else if (g < 0)
                            dataPtr[1] = (byte)Math.Abs(g);
                        else
                            dataPtr[1] = (byte)g;

                        b = (byte)(0 + Math.Abs(dataPtrOrig[2] - (dataPtrOrig + widthStep)[2]));
                        if (b > 255)
                            dataPtr[2] = 255;
                        else if (b < 0)
                            dataPtr[2] = (byte)Math.Abs(b);
                        else
                            dataPtr[2] = (byte)b;


                        dataPtr += widthStep; //passa para o pixel de baixo (em x = width)
                        dataPtrOrig += widthStep;

                    }
                    dataPtr += padding - nChan - widthStep;//fica em P(0,height) e faz a linha y = height
                    dataPtrOrig += padding - nChan - widthStep;//fica em P(0,height) e faz a linha y = height

                    //faz a linha y=heigth
                    for (int a = 0; a < width; a++)
                    {
                        dataPtr += nChan; //passa para o proximo pixel para fazer a linha de baixo (y = heigth)
                        dataPtrOrig += nChan;

                        r = (byte)(Math.Abs(dataPtrOrig[0] - (dataPtrOrig + nChan)[0])); //o primeiro é zero pois subtraimos por ele proprio
                        if (r > 255)
                            dataPtr[0] = 255;
                        else if (r < 0)
                            dataPtr[0] = (byte)Math.Abs(r);
                        else
                            dataPtr[0] = (byte)r;

                        g = (byte)(Math.Abs(dataPtrOrig[1] - (dataPtrOrig + nChan)[1]));
                        if (g > 255)
                            dataPtr[1] = 255;
                        else if (g < 0)
                            dataPtr[1] = (byte)Math.Abs(g);
                        else
                            dataPtr[1] = (byte)g;

                        b = (byte)(Math.Abs(dataPtrOrig[2] - (dataPtrOrig + nChan)[2]));
                        if (b > 255)
                            dataPtr[2] = 255;
                        else if (b < 0)
                            dataPtr[2] = (byte)Math.Abs(b);
                        else
                            dataPtr[2] = (byte)b;
                    }
                    dataPtr += nChan;
                    dataPtrOrig += nChan;
                    //canto inferior direito
                    dataPtr[0] = 0;
                    dataPtr[1] = 0;
                    dataPtr[2] = 0;

                }
            }

        }

        public static int[] Histogram_Gray(Emgu.CV.Image<Bgr, byte> img)
        {

            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                int x = 0, y = 0, i=0;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int widthStep = m.widthStep;

                int[] vec = new int[256];

                //limpar o vector 

                for (i = 0; i < 255; i++) 
                    vec[i] = 0;                                         


                if (nChan == 3) // image in RGB
                {
                    dataPtr += widthStep + nChan;                    

                    //ConvertToGray(img);
                    for (y = 1; y < height - 1; y++)
                    {
                        for (x = 1; x < width - 1; x++)
                        {

                            int gray_level = ((int)Math.Round((dataPtr[0] + dataPtr[1] + dataPtr[2]) / 3.0));
                            vec[gray_level]++; //niveis de cinzento
                            
                            dataPtr += nChan;
                        }
                        dataPtr += padding;
                    }
                }
                return vec;
            }
            
        }

        public static void ConvertToBW(Emgu.CV.Image<Bgr, byte> img, int threshold)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                int x = 0, y = 0, i = 0;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int widthStep = m.widthStep;
                int gray;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            // convert to gray
                            gray = (int)Math.Round((dataPtr[0] + dataPtr[1] + dataPtr[2]) / 3.0);

                            if (gray <= threshold)
                            {
                                // store in the image
                                dataPtr[0] = 0;
                                dataPtr[1] = 0;
                                dataPtr[2] = 0;
                            }
                            else
                            {
                                // store in the image
                                dataPtr[0] = 255;
                                dataPtr[1] = 255;
                                dataPtr[2] = 255;
                            }

                            dataPtr += nChan;
                        }

                        dataPtr += padding;
                    }
                }
            }
        }

        public static void ConvertToBW_Otsu(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int widthStep = m.widthStep;

                int[] hist = Histogram_Gray(img);//array para guardar valores do histograma
                double pixtot = width * height;
                double somatotal = 0;
                double soma1 = 0, q1 = 0, q2 = 0, variancia = 0, var_max = 0;
                double u1 = 0, u2 = 0;
                int niveis = 255;
                int threshold = 0;

                for (int i = 0; i <= niveis; i++)
                {
                    somatotal += i * hist[i];
                }

                for(int t = 0; t <= niveis; t++)
                {
                    q1 += hist[t];
                    //if (q1 == 0)
                    //    continue;
                    q2 = pixtot - q1;

                    //if (q2 == 0)    // nothing on the right side - finish
                    //    break;

                    soma1 += t * hist[t];
                    u1 = soma1 / (q1 * 1.0);
                    u2 = (somatotal - soma1) / (q2 * 1.0);

                    variancia = q1 * q2 * (int)Math.Pow((u1 - u2), 2);
                    if(variancia > var_max)
                    {
                        threshold = t;
                        var_max = variancia;
                        
                    }
                }//Console.WriteLine("o threshold é: " + threshold);
                ConvertToBW(img, threshold);
            }

        }
        public static void HitNmiss(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int[] mat)
        {

        }

        public static void Chess_Recognition(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, out Rectangle BD_Location, out string Angle, out string[,] Pieces)
        {
            unsafe
            {
                
                Sobel(img, imgCopy);               
                ConvertToBW_Otsu(img);
                // direcion top left -> bottom right
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int widthstep = m.widthStep;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y, x_pos = -1, y_pos = -1, width_pos = -1, height_pos = -1;
                int preto = 0, branco = 255;
                int[] projecaoX = new int[width]; //array com projecao de X
                int[] projecaoY = new int[height]; //array com projecao de X
                int count = 0, b = 0;
                int passoux = -1;
                int passouy = -1;
                
                //encontra o x e o width do rectangulo
                for (x = 0; x < width; x++)
                {
                    for (y = 0; y < height; y++)
                    {
                        if (dataPtr[0] == branco)//podia ser o dataPtr[1] ou dataPtr[2] visto que é imagem binaria
                        {
                            projecaoX[x]++;
                        }
                        dataPtr += widthstep;

                        if (projecaoX[x] >= (height))   //apenas pra nao demorar 
                            break;
                    }
                    dataPtr = (byte*)m.imageData.ToPointer(); // volta a por no inicio da imagem pois é mais facil
                    dataPtr += (x + 1) * nChan;   //avança na posiçao x

                }

                dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image

                //encontra o y e o heigth do rectangulo
                for (y = 0; y < height; y++)
                {
                    for (x = 0; x < width; x++)
                    {
                        if (dataPtr[0] == branco)//podia ser o dataPtr[1] ou dataPtr[2] visto que é imagem binaria
                        {
                            projecaoY[y]++;
                        }
                        dataPtr += nChan;

                        if (projecaoY[y] >= (width))   //apenas pra nao demorar 
                            break;
                    }
                    dataPtr += padding;
                }

                //-------------------------------
                while (passoux != 0 && passouy != 0)
                {
                    passoux = 0;
                    passouy = 0;
                    for (x = 0; x < width; x++)
                    {
                        if (projecaoX[x] > 0 && projecaoX[x] < 10)  //10 foi o numero encontrado que mais equilibrava os resultados
                        {
                            projecaoX[x] = 0;   //tem poucos pixeis a um muito provavelmente é ruido
                            passoux++;
                        }

                        else if (projecaoX[x] >= 10)
                            count++;    //conta o numero de linhas com mais de 10 pixeis a branco até encontrar outra linha sem pixeis a branco
                        else//se for zero
                        {
                            if (count < (height / 5) && count > 0) //height/5 pois a largura do tabuleiro nunca será menor do que 1/5 da largura da imagem 
                            {
                                for (int a = count; a > 0; a--)
                                {
                                    projecaoX[x - a] = 0;
                                    passoux++;
                                }
                                count = 0;
                            }
                            else count = 0;
                        }
                        b = x;
                    }
                    if (count < (height / 5) && count > 0) //height/5 pois a largura do tabuleiro nunca será menor do que 1/5 da largura da imagem 
                    {
                        for (int a = count; a > 0; a--)
                        {
                            projecaoX[x - a] = 0;
                            passoux++;
                        }
                    }
                    count = 0;
                    for (y = 0; y < height; y++)
                    {
                        if (projecaoY[y] > 0 && projecaoY[y] < 10)
                        {
                            projecaoY[y] = 0;   //tem poucos pixeis a um muito provavelmente é ruido
                            passouy++;
                        }

                        else if (projecaoY[y] >= 10)
                            count++;    //conta o numero de linhas com mais de 2 pixeis a branco até encontrar outra linha sem pixeis a branco

                        else//se for zero
                        {
                            if (count < (width / 5) && count > 0) //width/5 pois a largura do tabuleiro nunca será menos do que 1/5 da largura da imagem 
                            {
                                for (int a = count; a > 0; a--)
                                {
                                    projecaoY[y - a] = 0;
                                    passouy++;
                                }
                                count = 0;
                            }
                            else count = 0;
                        }
                    }
                    if (count < (width / 5) && count > 0) //width/5 pois a largura do tabuleiro nunca será menos do que 1/5 da largura da imagem 
                    {
                        for (int a = count; a > 0; a--)
                        {
                            projecaoY[y - a] = 0;
                            passouy++;
                        }
                    }
                    count = 0;
                    Console.WriteLine(passoux.ToString());
                    Console.WriteLine(passouy.ToString() + "\n");
                }
                //----------------------

                //para calcular o eixo maior verificar quando virmos de cima pra baixo esquerda para a direita qual o primeiro pixel a branco 
                //depois vir de baixo para cima da esquerda para a direita ou da direita para a esquerda e calcular o eixo consoante a posicao dos dois pixeis na imagem

                //ter em atençao os radianos
                double x1 = 0, x2 = 0, y1 = 0, y2 = 0;
                //vem de cima para baixo da direita para a esquerda
                int control = -1;
                dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                dataPtr += ((width - 1) * nChan);//fica em P(width,0)

                //UMA MANEIRA DE CORRIGIR O PROBLEMA DO ANGULO É QUANDO ENCONTRAR O SUPOSTO PONTO
                //VERIFICAR SE AS PROJECCOES CORRESPONDENTES AO X E AO Y ESTAO != 0 SE SIM ENTAO SAO PONTOS DO ANGULO
                for (y = 0; y < height; y++)
                {
                    for (x = width; x > 0; x--)
                    {
                        if (dataPtr[0] == branco)
                        {
                            //encontrou o primeiro pixel branco  
                            if (projecaoX[x - 1] != 0 && projecaoY[y] != 0)
                            {
                                x2 = x;
                                y2 = y;
                                control = 1;
                                break;
                            }
                        }

                        dataPtr -= nChan;
                    }
                    if (control == 1)
                        break;

                    dataPtr = dataPtr + (2 * widthstep) - padding;
                }
                //Console.WriteLine("x1 = " + Convert.ToString(x1) + "y1 = " + Convert.ToString(y1) + "x2 = " + Convert.ToString(x2) + "y2 = " + Convert.ToString(y2));


                dataPtr = (byte*)m.imageData.ToPointer(); //volta a meter o ptr no principio da imagem
                dataPtr += widthstep * (height - 1);

                control = -1;
                for (y = height; y > 0; y--)
                {
                    for (x = 0; x < width; x++)
                    {
                        if (dataPtr[0] == branco)
                        {
                            //encontrou o primeiro pixel branco
                            if (projecaoX[x] != 0 && projecaoY[y - 1] != 0)
                            {
                                x1 = x;
                                y1 = y;
                                control = 1;
                                break;
                            }
                        }

                        dataPtr += nChan;
                    }
                    if (control == 1)
                        break;

                    dataPtr = dataPtr - (2 * widthstep) + padding;  //2*widthStep porque senão voltava para a mesma linha
                }
                //inclinaçao em graus
                double inclinacao = Math.Round(Math.Atan((y2 - y1) / ((x2 - x1) * 1.0)), 3) * (-1);    //se for negativo adicionar 180 graus
                inclinacao = Math.Round(inclinacao * (180 / Math.PI));
                if (inclinacao < 0)
                    inclinacao += 180;

                //Console.WriteLine("x1 = " + Convert.ToString(x1) + "y1 = " + Convert.ToString(y1) + "x2 = " + Convert.ToString(x2) + "y2 = " + Convert.ToString(y2) + "inclinaçao = " + Convert.ToString(inclinacao));
                Angle = inclinacao.ToString();
                //if (inclinacao != 45)
                //{
                //    int ajuste = (int)(45.0 - inclinacao);
                //    Console.WriteLine("ajuste = " + Convert.ToString(ajuste));
                //    Rotation(img, img, ajuste);
                //}

                //-----------------------outra vez
                passoux = -1;   //para entrar no ciclo
                passouy = -1;
                while (passoux != 0 && passouy != 0)
                {
                    passoux = 0;
                    passouy = 0;
                    for (x = 0; x < width; x++)
                    {
                        if (projecaoX[x] > 0 && projecaoX[x] < 3)
                        {
                            projecaoX[x] = 0;   //tem poucos pixeis a um muito provavelmente é ruido
                            passoux++;
                        }

                        else if (projecaoX[x] >= 3)
                            count++;    //conta o numero de linhas com mais de 2 pixeis a branco até encontrar outra linha sem pixeis a branco
                        else//se for zero
                        {
                            if (count < (height / 5) && count > 0) //width/5 pois a largura do tabuleiro nunca será menos do que 1/5 da largura da imagem 
                            {
                                for (int a = count; a > 0; a--)
                                {
                                    projecaoX[x - a] = 0;
                                    passoux++;
                                }
                                count = 0;
                            }
                            else count = 0;
                        }
                        b = x;
                    }
                    if (count < (height / 5) && count > 0) //width/5 pois a largura do tabuleiro nunca será menos do que 1/5 da largura da imagem 
                    {
                        for (int a = count; a > 0; a--)
                        {
                            projecaoX[x - a] = 0;
                            passoux++;
                        }
                    }
                    count = 0;
                    for (y = 0; y < height; y++)
                    {
                        if (projecaoY[y] > 0 && projecaoY[y] < 3)
                        {
                            projecaoY[y] = 0;   //tem poucos pixeis a um muito provavelmente é ruido
                            passouy++;
                        }

                        else if (projecaoY[y] >= 3)
                            count++;    //conta o numero de linhas com mais de 2 pixeis a branco até encontrar outra linha sem pixeis a branco

                        else//se for zero
                        {
                            if (count < (width / 5) && count > 0) //width/5 pois a largura do tabuleiro nunca será menos do que 1/5 da largura da imagem 
                            {
                                for (int a = count; a > 0; a--)
                                {
                                    projecaoY[y - a] = 0;
                                    passouy++;
                                }
                                count = 0;
                            }
                            else count = 0;
                        }
                    }
                    if (count < (width / 5) && count > 0) //width/5 pois a largura do tabuleiro nunca será menos do que 1/5 da largura da imagem 
                    {
                        for (int a = count; a > 0; a--)
                        {
                            projecaoY[y - a] = 0;
                            passouy++;
                        }
                    }
                    count = 0;
                    Console.WriteLine(passoux.ToString());
                    Console.WriteLine(passouy.ToString() + "\n");
                }
                //--------------------------------

                int j = 0;
                //ja tem as projecoes feitas
                while (projecaoX[j] == 0 && j < (width - 1))
                {
                    j++;
                }
                x_pos = j;

                while (projecaoX[j] != 0 && j < (width - 1))
                {
                    j++;
                }
                width_pos = j;

                j = 0;
                //ja tem as projecoes feitas
                while (projecaoY[j] == 0 && j < (height - 1))
                {
                    j++;
                }
                y_pos = j;

                while (projecaoY[j] != 0 && j < (height - 1))
                {
                    j++;
                }
                height_pos = j;

                int largura = width_pos - x_pos;
                int altura = height_pos - y_pos;
                
                //para ir buscar as imagens das peças vai ao mainform e vê o codigo da opçao openToolStripMenu para retirar para uma imagem
                string[,] tmp = new string[8, 8];
                float y_tab = y_pos;
                float x_tab = x_pos;

                int countAux = -1;


                
                Image<Bgr, Byte> cavaloBranco = new Image<Bgr, byte>("C:\\SistemasSensoriais_2017\\Trabalho 1\\BDChess\\cavaloBranco.png");
                Image<Bgr, Byte> cavaloPreto = new Image<Bgr, byte>("C:\\SistemasSensoriais_2017\\Trabalho 1\\BDChess\\cavaloPreto.PNG");
                Image<Bgr, Byte> bispoBranco = new Image<Bgr, byte>("C:\\SistemasSensoriais_2017\\Trabalho 1\\BDChess\\bispoBranco.PNG");
                Image<Bgr, Byte> bispoPreto = new Image<Bgr, byte>("C:\\SistemasSensoriais_2017\\Trabalho 1\\BDChess\\bispoPreto.PNG");
                Image<Bgr, Byte> rainhaBranco = new Image<Bgr, byte>("C:\\SistemasSensoriais_2017\\Trabalho 1\\BDChess\\rainhaBranco.PNG");
                Image<Bgr, Byte> rainhaPreto = new Image<Bgr, byte>("C:\\SistemasSensoriais_2017\\Trabalho 1\\BDChess\\rainhaPreto.PNG");
                Image<Bgr, Byte> reiBranco = new Image<Bgr, byte>("C:\\SistemasSensoriais_2017\\Trabalho 1\\BDChess\\reiBranco.PNG");
                Image<Bgr, Byte> reiPreto = new Image<Bgr, byte>("C:\\SistemasSensoriais_2017\\Trabalho 1\\BDChess\\reiPreto.PNG");

                
                int x_temp = (int)Math.Round(x_tab + (double)3.0, 0);   //para ficar mais dentro do quadrado e evitar as bordas
                int y_temp = (int)Math.Round(y_tab + (double)3.0, 0);


                Image<Bgr, Byte> tabuleiro = img.Copy(new Rectangle(x_pos, y_pos, largura, altura));

                countAux = -1;
                MIplImage n = tabuleiro.MIplImage;
                byte* dataPtrAux = (byte*)n.imageData.ToPointer(); // Pointer to the image

                int widthAux = tabuleiro.Width;
                int heightAux = tabuleiro.Height;
                int widthstepAux = n.widthStep;
                int nChanAux = n.nChannels; // number of channels - 3
                int paddingAux = n.widthStep - n.nChannels * n.width; // alinhament bytes (padding)                                                                  
                //para calcular dimensoes do tabuleiro e de cada quadrado 
                int width_cadaPos = (int)(widthAux / 8.0);
                int height_cadaPos = (int)(heightAux / 8.0);

                //---------------------------peaoPreto--------------------//
                /*
                Image<Bgr, Byte> peaoPreto = new Image<Bgr, byte>("C:\\SistemasSensoriais_2017\\Trabalho 1\\BDChess\\peaoPreto.png");

                
                peaoPreto = peaoPreto.Resize(width_cadaPos, height_cadaPos, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                

                //Negative(peaoPreto);
                //Sobel(peaoPreto, peaoPreto);

                //ConvertToBW_Otsu(peaoPreto);
                ConvertToBW(peaoPreto, 242);
                Negative(peaoPreto);
                
                // direcion top left -> bottom right
                n = peaoPreto.MIplImage;
                dataPtrAux = (byte*)n.imageData.ToPointer(); // Pointer to the image

                widthAux = peaoPreto.Width;
                heightAux = peaoPreto.Height;
                widthstepAux = n.widthStep;
                nChanAux = n.nChannels; // number of channels - 3
                paddingAux = n.widthStep - n.nChannels * n.width; // alinhament bytes (padding)
                long peaoPretoFactor = -1;
                control = -1;
                int countof = 0;
                for (y = 3; y < heightAux - 2; y++)
                {
                    for (x = 2; x < widthAux - 2; x++)
                    {
                        dataPtrAux = (byte*)n.imageData.ToPointer();
                        dataPtrAux += (nChanAux * (x)) + (widthstepAux * (y));
                        //Console.WriteLine(dataPtrAux[0]);

                        if (dataPtrAux[0] == branco)
                        {
                            countof++;
                            //encontrou o primeiro pixel branco                              
                            control = 1;

                            break;
                        }
                        dataPtrAux += nChanAux;
                    }

                    if (control == 1)
                        break;
                    dataPtrAux += paddingAux;
                }
                Console.WriteLine("ponto = " + x + ", " + y);
                //Console.WriteLine("count = " + countof);
                //temos a posiçao do primeiro pixel em dataPtrAux

                //-----------------chain code-------------------//
                int[] chainCode = new int[16000]; //array com chain code 
                int[] codigo = { nChanAux, (-widthstepAux), (-nChanAux), widthstepAux };
                //0-1-2-3- respetivamente (conetividade 4)
                chainCode[0] = 0;
                byte* ptrSPerimeter = dataPtrAux; // Pointer to the image
                int controlvar = 0;
                int code_length = 1;
                
                while (dataPtrAux != ptrSPerimeter || controlvar == 0)  //faz o chain code do objeto (com conetividade 4)
                {
                    controlvar++;

                    //se a ultima direçao for 0
                    if(chainCode[code_length - 1] == 0)
                    {
                        //Console.WriteLine("entrou em x=0");
                        if ((dataPtrAux - widthstepAux)[0] == branco)
                            chainCode[code_length] = 1;
                        else if ((dataPtrAux + nChanAux)[0] == branco)
                            chainCode[code_length] = 0;
                        else if ((dataPtrAux + widthstepAux)[0] == branco)
                            chainCode[code_length] = 3;
                        else 
                            chainCode[code_length] = 2;

                    }

                    //se a ultima direçao for 1
                    else if (chainCode[code_length - 1] == 1)
                    {
                        //Console.WriteLine("entro em x=1");
                        if ((dataPtrAux - nChanAux)[0] == branco)
                            chainCode[code_length] = 2;
                        else if ((dataPtrAux - widthstepAux)[0] == branco)
                            chainCode[code_length] = 1;
                        else if ((dataPtrAux + nChanAux)[0] == branco)
                            chainCode[code_length] = 0;
                        else
                            chainCode[code_length] = 3;

                    }

                    //se a ultima direçao for 2
                    else if (chainCode[code_length - 1] == 2)
                    {
                        //Console.WriteLine("entro em x=2");

                        if ((dataPtrAux + widthstepAux)[0] == branco)
                            chainCode[code_length] = 3;
                        else if ((dataPtrAux - nChanAux)[0] == branco)
                            chainCode[code_length] = 2;
                        else if ((dataPtrAux - widthstepAux)[0] == branco)
                            chainCode[code_length] = 1;
                        else
                            chainCode[code_length] = 0;

                    }

                    //se a ultima direçao for 3
                    else
                    {
                        //Console.WriteLine("entro em x=3");
                        if ((dataPtrAux + nChanAux)[0] == branco)
                            chainCode[code_length] = 0;
                        else if ((dataPtrAux + widthstepAux)[0] == branco)
                            chainCode[code_length] = 3;
                        else if ((dataPtrAux - nChanAux)[0] == branco)
                            chainCode[code_length] = 2;
                        else
                            chainCode[code_length] = 1;

                    }

                    dataPtrAux += codigo[chainCode[code_length]]; //mete o dataPtrAux a apontar para a proxima posiçao
                    code_length++;
                    
                }
                
                int[] PPchainCode =chainCode;
                Console.WriteLine("É agora");
                //Console.Write(chainCode[0].ToString());
                //Array.ForEach(chainCode, d => Console.Write(d));
                
                int mudou = 0;
                for (int d = 0; d < code_length; d++)
                {
                    if (d != 0 && chainCode[d] != chainCode[d - 1])
                        mudou++;
                    //Console.WriteLine(Convert.ToString(chainCode[d]));

                }
               // Console.WriteLine("Mudanças de direção = " + mudou + "\n" + "Total de dígitos do Chain Code  = " + code_length);
                //foreach (var item in chainCode)
                //{
                //    Console.Write(item);
                //}
                //Console.WriteLine("chain code length = " + PPchainCode.Length);
                //Console.WriteLine("controlvar = " + controlvar);
                countAux = -1;
                */
                //---------------------------peaoBranco--------------------//
                /*
                Image<Bgr, Byte> peaoBranco = new Image<Bgr, byte>("C:\\SistemasSensoriais_2017\\Trabalho 1\\BDChess\\peaoBranco.png");

                peaoBranco = peaoBranco.Resize(width_cadaPos, height_cadaPos, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);

                //ConvertToBW_Otsu(peaoPreto);
                ConvertToBW(peaoBranco, 242);
                Negative(peaoBranco);

                // direcion top left -> bottom right
                n = peaoBranco.MIplImage;
                dataPtrAux = (byte*)n.imageData.ToPointer(); // Pointer to the image

                widthAux = peaoBranco.Width;
                heightAux = peaoBranco.Height;
                widthstepAux = n.widthStep;
                nChanAux = n.nChannels; // number of channels - 3
                paddingAux = n.widthStep - n.nChannels * n.width; // alinhament bytes (padding)
                control = -1;
                int countof = 0;
                for (y = 3; y < heightAux - 2; y++)
                {
                    for (x = 2; x < widthAux - 2; x++)
                    {
                        dataPtrAux = (byte*)n.imageData.ToPointer();
                        dataPtrAux += (nChanAux * (x)) + (widthstepAux * (y));
                        //Console.WriteLine(dataPtrAux[0]);

                        if (dataPtrAux[0] == branco)
                        {
                            countof++;
                            //encontrou o primeiro pixel branco                              
                            control = 1;

                            break;
                        }
                        dataPtrAux += nChanAux;
                    }

                    if (control == 1)
                        break;
                    dataPtrAux += paddingAux;
                }
                Console.WriteLine("ponto = " + x + ", " + y);
                //Console.WriteLine("count = " + countof);
                //temos a posiçao do primeiro pixel em dataPtrAux

                //-----------------chain code-------------------//
                int[] chainCode = new int[16000]; //array com chain code 
                int[] codigo = { nChanAux, (-widthstepAux), (-nChanAux), widthstepAux };
                //0-1-2-3- respetivamente (conetividade 4)
                chainCode[0] = 0;
                byte* ptrSPerimeter = dataPtrAux; // Pointer to the image
                int controlvar = 0;
                int code_length = 1;

                while (dataPtrAux != ptrSPerimeter || controlvar == 0)  //faz o chain code do objeto (com conetividade 4)
                {
                    controlvar++;

                    //se a ultima direçao for 0
                    if (chainCode[code_length - 1] == 0)
                    {
                        //Console.WriteLine("entrou em x=0");
                        if ((dataPtrAux - widthstepAux)[0] == branco)
                            chainCode[code_length] = 1;
                        else if ((dataPtrAux + nChanAux)[0] == branco)
                            chainCode[code_length] = 0;
                        else if ((dataPtrAux + widthstepAux)[0] == branco)
                            chainCode[code_length] = 3;
                        else
                            chainCode[code_length] = 2;

                    }

                    //se a ultima direçao for 1
                    else if (chainCode[code_length - 1] == 1)
                    {
                        //Console.WriteLine("entro em x=1");
                        if ((dataPtrAux - nChanAux)[0] == branco)
                            chainCode[code_length] = 2;
                        else if ((dataPtrAux - widthstepAux)[0] == branco)
                            chainCode[code_length] = 1;
                        else if ((dataPtrAux + nChanAux)[0] == branco)
                            chainCode[code_length] = 0;
                        else
                            chainCode[code_length] = 3;

                    }

                    //se a ultima direçao for 2
                    else if (chainCode[code_length - 1] == 2)
                    {
                        //Console.WriteLine("entro em x=2");

                        if ((dataPtrAux + widthstepAux)[0] == branco)
                            chainCode[code_length] = 3;
                        else if ((dataPtrAux - nChanAux)[0] == branco)
                            chainCode[code_length] = 2;
                        else if ((dataPtrAux - widthstepAux)[0] == branco)
                            chainCode[code_length] = 1;
                        else
                            chainCode[code_length] = 0;

                    }

                    //se a ultima direçao for 3
                    else
                    {
                        //Console.WriteLine("entro em x=3");
                        if ((dataPtrAux + nChanAux)[0] == branco)
                            chainCode[code_length] = 0;
                        else if ((dataPtrAux + widthstepAux)[0] == branco)
                            chainCode[code_length] = 3;
                        else if ((dataPtrAux - nChanAux)[0] == branco)
                            chainCode[code_length] = 2;
                        else
                            chainCode[code_length] = 1;

                    }

                    dataPtrAux += codigo[chainCode[code_length]]; //mete o dataPtrAux a apontar para a proxima posiçao
                    code_length++;

                }

                int[] TPchainCode = chainCode;
                Console.WriteLine("É agora");
                //Console.Write(chainCode[0].ToString());
                //Array.ForEach(chainCode, d => Console.Write(d));

                int mudou = 0;
                for (int d = 0; d < code_length; d++)
                {
                    if (d != 0 && chainCode[d] != chainCode[d - 1])
                        mudou++;
                    Console.WriteLine(Convert.ToString(chainCode[d]));

                }
                Console.WriteLine("Mudanças de direção = " + mudou + "\n" + "Total de dígitos do Chain Code  = " + code_length);
                */
                //---------------------------torrePreto--------------------//
                /*
                Image<Bgr, Byte> torrePreto = new Image<Bgr, byte>("C:\\SistemasSensoriais_2017\\Trabalho 1\\BDChess\\torrePreto.PNG");

                torrePreto = torrePreto.Resize(width_cadaPos, height_cadaPos, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                
                //Sobel(peaoPreto, peaoPreto);

                //ConvertToBW_Otsu(peaoPreto);
                ConvertToBW(torrePreto, 220);
                Negative(torrePreto);

                // direcion top left -> bottom right
                n = torrePreto.MIplImage;
                dataPtrAux = (byte*)n.imageData.ToPointer(); // Pointer to the image

                widthAux = torrePreto.Width;
                heightAux = torrePreto.Height;
                widthstepAux = n.widthStep;
                nChanAux = n.nChannels; // number of channels - 3
                paddingAux = n.widthStep - n.nChannels * n.width; // alinhament bytes (padding)
                control = -1;
                int countof = 0;
                for (y = 3; y < heightAux - 2; y++)
                {
                    for (x = 2; x < widthAux - 2; x++)
                    {
                        dataPtrAux = (byte*)n.imageData.ToPointer();
                        dataPtrAux += (nChanAux * (x)) + (widthstepAux * (y));
                        //Console.WriteLine(dataPtrAux[0]);

                        if (dataPtrAux[0] == branco)
                        {
                            countof++;
                            //encontrou o primeiro pixel branco                              
                            control = 1;

                            break;
                        }
                        dataPtrAux += nChanAux;
                    }

                    if (control == 1)
                        break;
                    dataPtrAux += paddingAux;
                }
                Console.WriteLine("ponto = " + x + ", " + y);
                //Console.WriteLine("count = " + countof);
                //temos a posiçao do primeiro pixel em dataPtrAux

                //-----------------chain code-------------------//
                int[] chainCode = new int[16000]; //array com chain code 
                int[] codigo = { nChanAux, (-widthstepAux), (-nChanAux), widthstepAux };
                //0-1-2-3- respetivamente (conetividade 4)
                chainCode[0] = 0;
                byte* ptrSPerimeter = dataPtrAux; // Pointer to the image
                int controlvar = 0;
                int code_length = 1;

                while (dataPtrAux != ptrSPerimeter || controlvar == 0)  //faz o chain code do objeto (com conetividade 4)
                {
                    controlvar++;

                    //se a ultima direçao for 0
                    if (chainCode[code_length - 1] == 0)
                    {
                        //Console.WriteLine("entrou em x=0");
                        if ((dataPtrAux - widthstepAux)[0] == branco)
                            chainCode[code_length] = 1;
                        else if ((dataPtrAux + nChanAux)[0] == branco)
                            chainCode[code_length] = 0;
                        else if ((dataPtrAux + widthstepAux)[0] == branco)
                            chainCode[code_length] = 3;
                        else
                            chainCode[code_length] = 2;

                    }

                    //se a ultima direçao for 1
                    else if (chainCode[code_length - 1] == 1)
                    {
                        //Console.WriteLine("entro em x=1");
                        if ((dataPtrAux - nChanAux)[0] == branco)
                            chainCode[code_length] = 2;
                        else if ((dataPtrAux - widthstepAux)[0] == branco)
                            chainCode[code_length] = 1;
                        else if ((dataPtrAux + nChanAux)[0] == branco)
                            chainCode[code_length] = 0;
                        else
                            chainCode[code_length] = 3;

                    }

                    //se a ultima direçao for 2
                    else if (chainCode[code_length - 1] == 2)
                    {
                        //Console.WriteLine("entro em x=2");

                        if ((dataPtrAux + widthstepAux)[0] == branco)
                            chainCode[code_length] = 3;
                        else if ((dataPtrAux - nChanAux)[0] == branco)
                            chainCode[code_length] = 2;
                        else if ((dataPtrAux - widthstepAux)[0] == branco)
                            chainCode[code_length] = 1;
                        else
                            chainCode[code_length] = 0;

                    }

                    //se a ultima direçao for 3
                    else
                    {
                        //Console.WriteLine("entro em x=3");
                        if ((dataPtrAux + nChanAux)[0] == branco)
                            chainCode[code_length] = 0;
                        else if ((dataPtrAux + widthstepAux)[0] == branco)
                            chainCode[code_length] = 3;
                        else if ((dataPtrAux - nChanAux)[0] == branco)
                            chainCode[code_length] = 2;
                        else
                            chainCode[code_length] = 1;

                    }

                    dataPtrAux += codigo[chainCode[code_length]]; //mete o dataPtrAux a apontar para a proxima posiçao
                    code_length++;

                }

                int[] TPchainCode = chainCode;
                Console.WriteLine("É agora");
                //Console.Write(chainCode[0].ToString());
                //Array.ForEach(chainCode, d => Console.Write(d));

                int mudou = 0;
                for (int d = 0; d < code_length; d++)
                {
                    if (d != 0 && chainCode[d] != chainCode[d - 1])
                        mudou++;
                    Console.WriteLine(Convert.ToString(chainCode[d]));

                }
                Console.WriteLine("Mudanças de direção = " + mudou + "\n" + "Total de dígitos do Chain Code  = " + code_length);
                */

                //---------------------------torreBranco--------------------//

                Image<Bgr, Byte> torreBranco = new Image<Bgr, byte>("C:\\SistemasSensoriais_2017\\Trabalho 1\\BDChess\\torreBranco.PNG");

                torreBranco = torreBranco.Resize(width_cadaPos, height_cadaPos, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);

                ConvertToBW(torreBranco, 50);
                Negative(torreBranco);

                // direcion top left -> bottom right
                n = torreBranco.MIplImage;
                dataPtrAux = (byte*)n.imageData.ToPointer(); // Pointer to the image

                widthAux = torreBranco.Width;
                heightAux = torreBranco.Height;
                widthstepAux = n.widthStep;
                nChanAux = n.nChannels; // number of channels - 3
                paddingAux = n.widthStep - n.nChannels * n.width; // alinhament bytes (padding)
                control = -1;
                int countof = 0;
                for (y = 3; y < heightAux - 2; y++)
                {
                    for (x = 2; x < widthAux - 2; x++)
                    {
                        dataPtrAux = (byte*)n.imageData.ToPointer();
                        dataPtrAux += (nChanAux * (x)) + (widthstepAux * (y));
                        //Console.WriteLine(dataPtrAux[0]);

                        if (dataPtrAux[0] == branco)
                        {
                            countof++;
                            //encontrou o primeiro pixel branco                              
                            control = 1;

                            break;
                        }
                        dataPtrAux += nChanAux;
                    }

                    if (control == 1)
                        break;
                    dataPtrAux += paddingAux;
                }
                Console.WriteLine("ponto = " + x + ", " + y);
                //Console.WriteLine("count = " + countof);
                //temos a posiçao do primeiro pixel em dataPtrAux

                //-----------------chain code-------------------//
                int[] chainCode = new int[16000]; //array com chain code 
                int[] codigo = { nChanAux, (-widthstepAux), (-nChanAux), widthstepAux };
                //0-1-2-3- respetivamente (conetividade 4)
                chainCode[0] = 0;
                byte* ptrSPerimeter = dataPtrAux; // Pointer to the image
                int controlvar = 0;
                int code_length = 1;

                while (dataPtrAux != ptrSPerimeter || controlvar == 0)  //faz o chain code do objeto (com conetividade 4)
                {
                    controlvar++;

                    //se a ultima direçao for 0
                    if (chainCode[code_length - 1] == 0)
                    {
                        //Console.WriteLine("entrou em x=0");
                        if ((dataPtrAux - widthstepAux)[0] == branco)
                            chainCode[code_length] = 1;
                        else if ((dataPtrAux + nChanAux)[0] == branco)
                            chainCode[code_length] = 0;
                        else if ((dataPtrAux + widthstepAux)[0] == branco)
                            chainCode[code_length] = 3;
                        else
                            chainCode[code_length] = 2;

                    }

                    //se a ultima direçao for 1
                    else if (chainCode[code_length - 1] == 1)
                    {
                        //Console.WriteLine("entro em x=1");
                        if ((dataPtrAux - nChanAux)[0] == branco)
                            chainCode[code_length] = 2;
                        else if ((dataPtrAux - widthstepAux)[0] == branco)
                            chainCode[code_length] = 1;
                        else if ((dataPtrAux + nChanAux)[0] == branco)
                            chainCode[code_length] = 0;
                        else
                            chainCode[code_length] = 3;

                    }

                    //se a ultima direçao for 2
                    else if (chainCode[code_length - 1] == 2)
                    {
                        //Console.WriteLine("entro em x=2");

                        if ((dataPtrAux + widthstepAux)[0] == branco)
                            chainCode[code_length] = 3;
                        else if ((dataPtrAux - nChanAux)[0] == branco)
                            chainCode[code_length] = 2;
                        else if ((dataPtrAux - widthstepAux)[0] == branco)
                            chainCode[code_length] = 1;
                        else
                            chainCode[code_length] = 0;

                    }

                    //se a ultima direçao for 3
                    else
                    {
                        //Console.WriteLine("entro em x=3");
                        if ((dataPtrAux + nChanAux)[0] == branco)
                            chainCode[code_length] = 0;
                        else if ((dataPtrAux + widthstepAux)[0] == branco)
                            chainCode[code_length] = 3;
                        else if ((dataPtrAux - nChanAux)[0] == branco)
                            chainCode[code_length] = 2;
                        else
                            chainCode[code_length] = 1;

                    }

                    dataPtrAux += codigo[chainCode[code_length]]; //mete o dataPtrAux a apontar para a proxima posiçao
                    code_length++;

                }

                int[] TBchainCode = chainCode;
                Console.WriteLine("É agora");
                //Console.Write(chainCode[0].ToString());
                //Array.ForEach(chainCode, d => Console.Write(d));

                int mudou = 0;
                for (int d = 0; d < code_length; d++)
                {
                    if (d != 0 && chainCode[d] != chainCode[d - 1])
                        mudou++;
                    Console.WriteLine(Convert.ToString(chainCode[d]));

                }
                Console.WriteLine("Mudanças de direção = " + mudou + "\n" + "Total de dígitos do Chain Code = " + code_length);

                //----------------------------------------------------------------------
                //por outra vez na imagem do tabuleiro
                countAux = -1;
                n = tabuleiro.MIplImage;
                dataPtrAux = (byte*)n.imageData.ToPointer(); // Pointer to the image

                widthAux = tabuleiro.Width;
                heightAux = tabuleiro.Height;
                widthstepAux = n.widthStep;
                nChanAux = n.nChannels; // number of channels - 3
                paddingAux = n.widthStep - n.nChannels * n.width; // alinhament bytes (padding)

                //Console.WriteLine("x_pos = "+ Convert.ToString(x_pos)+ " y_pos = " + Convert.ToString(y_pos) + " width_pos = " + Convert.ToString(width_pos) +" height_pos = " + Convert.ToString(height_pos));

                //Console.WriteLine("width_cadaPos = " + width_cadaPos);
                //Console.WriteLine("height_cadaPos = " + height_cadaPos);
                //Console.WriteLine("width_Tabuleiro = " + widthAux);
                //Console.WriteLine("width_Tabuleiro = " + heightAux);
                int percent = (int)Math.Round(width_cadaPos * height_cadaPos * 0.1);

                //imgCopy = img.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);

                for (int i = 0; i < 8; i++)
                {
                    for (j = 0; j < 8; j++)    //corre as posiçoes da estrutura das peças
                    {
                        //para cada posiçao fazer operaçao para a peça mas primeiro verificar se posiçao está vazia
                        for (y = 0; y < height_cadaPos - 1; y++)
                        {
                            for (x = 0; x < width_cadaPos - 1; x++)
                            {
                                dataPtrAux = (byte*)n.imageData.ToPointer();
                                dataPtrAux += (nChanAux * (x)) + ((nChanAux * j * (width_cadaPos - 1))) + (widthstepAux * (y)) + ((widthstepAux * i * (height_cadaPos - 1)));

                                if (dataPtrAux[0] == branco)
                                    countAux++;
                                //dataPtrAux += nChanAux;
                            }
                            //dataPtrAux += paddingAux;
                        }
                        if(countAux < percent)//se for menor que 10% dos pixeis da imagem
                            tmp[j, i] = "E_p";  //E_p = casa vazia;

                        //if else
                        else
                        {
                            tmp[j, i] = "P_w";  
                        }
                        //Console.WriteLine("count" + "  " + j + ", " + i + " = " + countAux);
                        countAux = -1;

                    }
                }
                BD_Location = new Rectangle(x_pos, y_pos, largura, altura);

                Pieces = tmp;
            }
        }

    }
}
