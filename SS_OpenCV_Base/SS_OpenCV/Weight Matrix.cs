using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SS_OpenCV
{
    public partial class Weight_Matrix : Form
    {
        public float[,] matrix = new float[3, 3];
        public float matrixWeight = 0;
        public Weight_Matrix()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex;
            

            switch (i)
            {

                case 0:
                    textBox1.Text = "-1";
                    matrix[0, 0] = float.Parse(textBox1.Text);
                    textBox2.Text = "-1";
                    matrix[1, 0] = float.Parse(textBox2.Text);
                    textBox3.Text = "-1";
                    matrix[2, 0] = float.Parse(textBox3.Text);
                    textBox4.Text = "-1";
                    matrix[0, 1] = float.Parse(textBox4.Text);
                    textBox5.Text = "9";
                    matrix[1, 1] = float.Parse(textBox5.Text);
                    textBox6.Text = "-1";
                    matrix[2, 1] = float.Parse(textBox6.Text);
                    textBox7.Text = "-1";
                    matrix[0, 2] = float.Parse(textBox7.Text);
                    textBox8.Text = "-1";
                    matrix[1, 2] = float.Parse(textBox8.Text);
                    textBox9.Text = "-1";
                    matrix[2, 2] = float.Parse(textBox9.Text);

                    matrixWeight = 1;
                    break;


                case 1:
                    textBox1.Text = "1";
                    matrix[0, 0] = float.Parse(textBox1.Text);
                    textBox2.Text = "2";
                    matrix[1, 0] = float.Parse(textBox2.Text);
                    textBox3.Text = "1";
                    matrix[2, 0] = float.Parse(textBox3.Text);
                    textBox4.Text = "2";
                    matrix[0, 1] = float.Parse(textBox4.Text);
                    textBox5.Text = "4";
                    matrix[1, 1] = float.Parse(textBox5.Text);
                    textBox6.Text = "2";
                    matrix[2, 1] = float.Parse(textBox6.Text);
                    textBox7.Text = "1";
                    matrix[0, 2] = float.Parse(textBox7.Text);
                    textBox8.Text = "2";
                    matrix[1, 2] = float.Parse(textBox8.Text);
                    textBox9.Text = "1";
                    matrix[2, 2] = float.Parse(textBox9.Text);

                    matrixWeight = 16;
                    break;

                case 2:
                    textBox1.Text = "1";
                    matrix[0, 0] = float.Parse(textBox1.Text);
                    textBox2.Text = "-2";
                    matrix[1, 0] = float.Parse(textBox2.Text);
                    textBox3.Text = "1";
                    matrix[2, 0] = float.Parse(textBox3.Text);
                    textBox4.Text = "-2";
                    matrix[0, 1] = float.Parse(textBox4.Text);
                    textBox5.Text = "4";
                    matrix[1, 1] = float.Parse(textBox5.Text);
                    textBox6.Text = "-2";
                    matrix[2, 1] = float.Parse(textBox6.Text);
                    textBox7.Text = "1";
                    matrix[0, 2] = float.Parse(textBox7.Text);
                    textBox8.Text = "-2";
                    matrix[1, 2] = float.Parse(textBox8.Text);
                    textBox9.Text = "1";
                    matrix[2, 2] = float.Parse(textBox9.Text);

                    matrixWeight = 1;
                    break;


                case 3:
                    textBox1.Text = "0";
                    matrix[0, 0] = float.Parse(textBox1.Text);
                    textBox2.Text = "0";
                    matrix[1, 0] = float.Parse(textBox2.Text);
                    textBox3.Text = "0";
                    matrix[2, 0] = float.Parse(textBox3.Text);
                    textBox4.Text = "-1";
                    matrix[0, 1] = float.Parse(textBox4.Text);
                    textBox5.Text = "2";
                    matrix[1, 1] = float.Parse(textBox5.Text);
                    textBox6.Text = "-1";
                    matrix[2, 1] = float.Parse(textBox6.Text);
                    textBox7.Text = "0";
                    matrix[0, 2] = float.Parse(textBox7.Text);
                    textBox8.Text = "0";
                    matrix[1, 2] = float.Parse(textBox8.Text);
                    textBox9.Text = "0";
                    matrix[2, 2] = float.Parse(textBox9.Text);

                    matrixWeight = 1;
                    break;

                default: break;


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
