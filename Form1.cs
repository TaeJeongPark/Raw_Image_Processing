using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int XSIZE = 512;
        int YSIZE = 512;
        byte[,] inImage;

        Bitmap bitmap;

        void LoadImage(string fileName)
        {
            BinaryReader fp = new BinaryReader(File.Open(fileName, FileMode.Open));
            inImage = new byte[XSIZE, YSIZE];

            for (int i = 0; i < XSIZE; i++)
            {
                for (int j = 0; j < YSIZE; j++)
                {
                    inImage[i, j] = fp.ReadByte();  // 한 바이트씩 읽어서 메모리에 로드
                }
            }
            fp.Close();
        }

        /*
         * 흑백 이미지
         */
        void DisplayImage()
        {
            for (int i = 0; i < XSIZE; i++)
            {
                for (int j = 0; j < YSIZE; j++)
                {
                    byte data = inImage[j, i];
                    Color c = Color.FromArgb(data, data, data);
                    bitmap.SetPixel(i, j, c);
                }
            }
            pictureBox1.Image = bitmap;
        }

        string filePath;

        /*
         * 흑백 이미지 로드
         */
        private void btn_load_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "이미지 파일|*.raw;|모든 파일|*.*";
                // openFileDialog.Filter = "이미지 파일|*.raw;*.png;*.jpg;*.jpeg;*.gif;*.bmp|모든 파일|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 선택한 파일의 경로 가져오기
                    filePath = openFileDialog.FileName;

                    bitmap = new Bitmap(XSIZE, YSIZE);

                    LoadImage(filePath);
                    DisplayImage();
                }
            }
        }

        /*
         * 컬러 이미지 로드
         */
        /*private void btn_load_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "이미지 파일|*.png;*.jpg;*.jpeg;*.gif;*.bmp|모든 파일|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    // 이미지를 PictureBox에 표시
                    loadedImage = Image.FromFile(filePath);
                    pictureBox1.Image = loadedImage;
                }
            }
        }*/

        private void btn_save_Click(object sender, EventArgs e)
        {
            // PictureBox의 이미지를 저장
            SaveImage(filePath);
        }

        private void btn_new_save_Click(object sender, EventArgs e)
        {
            if (filePath != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "이미지 파일|*.raw;|모든 파일|*.*";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        filePath = saveFileDialog.FileName;

                        // PictureBox의 이미지를 저장
                        SaveImage(filePath);
                    }
                }
            }
            else
            {
                MessageBox.Show("이미지가 로드되지 않았습니다.\n먼저 이미지를 열어주세요.");
            }
        }

        private void SaveImage(string filePath)
        {
            if(filePath != null)
            {
                try
                {
                    // PictureBox의 이미지를 파일로 저장
                    pictureBox1.Image.Save(filePath);

                    MessageBox.Show("이미지가 성공적으로 저장되었습니다.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("이미지를 저장하는 중에 오류가 발생했습니다. 오류 메시지: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("이미지가 로드되지 않았습니다.\n먼저 이미지를 열어주세요.");
            }
        }

        private bool drawing = false;
        private Point startPoint;
        private Point endPoint;

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            drawing = true;
            startPoint = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                endPoint = e.Location;

                // PictureBox에 선을 그림
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    using (Pen pen = new Pen(Color.Black, 2))
                    {
                        g.DrawLine(pen, startPoint, endPoint);
                    }
                }

                startPoint = endPoint;
                pictureBox1.Invalidate(); // PictureBox를 다시 그리도록 갱신
            }
        }

        /*int x1, y1, x2, y2;
        Pen pen = new Pen(Color.Black, 1);

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            x2 = e.X;
            y2 = e.Y;

            Graphics g = CreateGraphics();
            g.DrawLine(pen, x1, y1, x2, y2);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            x1 = e.X;
            y1 = e.Y;
        }*/

        private void Form1_Load(object sender, EventArgs e)
        {
            /*this.ClientSize = new System.Drawing.Size(XSIZE, YSIZE);    // 사이즈에 맞게 폼 생성

            pictureBox1.Location = new System.Drawing.Point(0, 0);      // 픽처박스의 좌표 설정
            pictureBox1.Size = new System.Drawing.Size(XSIZE, YSIZE);   // 픽처박스의 사이즈 설정*/
        }

    }
}
