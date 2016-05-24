using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform;
using Vector;
namespace diplom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // инициализация для работы с openGL 
            AnT.InitializeContexts();
        }

        // массив вершин создаваемого геометрического объекта 
        private float[,] GeomObject = new float[32, 3];
        // счетчик его вершин 
        private int count_elements = 0;

        // событие загрузки формы окна 
        private void Form1_Load(object sender, EventArgs e)
        {
            // инициализация OpenGL, много раз комментированная ранее 
            // инициализация библиотеки glut 
            Glut.glutInit();
            // инициализация режима экрана 
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);

            // установка цвета очистки экрана (RGBA) 
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода 
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);

            // активация проекционной матрицы 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // очистка матрицы 
            Gl.glLoadIdentity();

            Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 0.1, 200);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glEnable(Gl.GL_DEPTH_TEST);

            // пирамида для визуализации (4 точки) 

            GeomObject[0, 0] = -0.7f;
            GeomObject[0, 1] = 0;
            GeomObject[0, 2] = 0;

            GeomObject[1, 0] = 0.7f;
            GeomObject[1, 1] = 0;
            GeomObject[1, 2] = 0;

            GeomObject[2, 0] = 0.0f;
            GeomObject[2, 1] = 0;
            GeomObject[2, 2] = 1.0f;

            GeomObject[3, 0] = 0;
            GeomObject[3, 1] = 0.7f;
            GeomObject[3, 2] = 0.3f;

            // количество вершин рассматриваемого геометрического объекта 
            count_elements = 4;
            // устанавливаем ось X по умолчанию 
            comboBox1.SelectedIndex = 0;

            // начало визуализации (активируем таймер) 
            RenderTimer.Start();

        }

        private void AnT_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void AnT_KeyDown_1(object sender, KeyEventArgs e)
        {
         /*   // Z и X отвечают за масштабирование 
            if (e.KeyCode == Keys.Z)
            {
                // вызов функции, в которой мы реализуем масштабирование - передаем коэффициент масштабирования и выбранную ось в окне программы 
                CreateZoom(1.05f, comboBox1.SelectedIndex);
            }
            if (e.KeyCode == Keys.X)
            {
                // вызов функции, в которой мы реализуем масштабирование - передаем коэффициент масштабирования и выбранную ось в окне программы 
                CreateZoom(0.95f, comboBox1.SelectedIndex);
            }

            // W и S отвечают за перенос 
            if (e.KeyCode == Keys.W)
            {
                // вызов функции, в которой мы реализуем перенос - передаем значение перемещения и выбранную ось в окне программы 
                CreateTranslate(0.05f, comboBox1.SelectedIndex);
            }
            if (e.KeyCode == Keys.S)
            {
                // вызов функции, в которой мы реализуем перенос - передаем значение перемещения и выбранную ось в окне программы 
                CreateTranslate(-0.05f, comboBox1.SelectedIndex);

            } // A и D отвечают за поворот 
            if (e.KeyCode == Keys.A)
            {
                // вызов функции, в которой мы реализуем поворот - передаем значение для поворота и выбранную ось 
                CreateRotate(0.05f, comboBox1.SelectedIndex);
            }
            if (e.KeyCode == Keys.D)
            {
                CreateRotate(-0.05f, comboBox1.SelectedIndex);
            }*/

        }
        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            // обработка "тика" таймера - вызов функции отрисовки 
            Render();
        }
        private void Render()
        {
            // очистка буфера цвета и буфера глубины 
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor(255, 255, 255, 1);
            // очищение текущей матрицы 
            Gl.glLoadIdentity();

            // установка черного цвета 
            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Glu.GLUnurbs surface;
            float[] vknots = { 0.5f, 0.0f, 0.1f };
            float[] tknots = { 0.1f, 0.4f, 0.2f };
            surface = Glu.gluNewNurbsRenderer();
            float[,] points = {{-1.0f,-1.0f,-1.0f},{-1.0f,-1.0f,1.0f},{-1.0f,1.0f,1.0f},
                                {-1.0f,1.0f,-1.0f},{1.0f,1.0f,-1.0f},{1.0f,1.0f,1.0f},
                                {1.0f,-1.0f,1.0f},{1.0f,-1.0f,1.0f},{1.0f,-1.0f,-1.0f}};
            Glu.gluBeginSurface(surface);
            Glu.gluNurbsSurface(surface, 3, vknots, 3, tknots, 2, 1, points, 4, 4, Gl.GL_MAP2_VERTEX_4);

            Glu.gluEndSurface(surface);
            Glu.gluLookAt(3.0f, 3.0f, 3.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f);
            // отрисовываем геометрию 
            Gl.glFlush();

            // обновляем состояние элемента 
            AnT.Invalidate();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // устанавливаем фокус в AnT 
            AnT.Focus();
        }

        // функция масштабирования 
        private void CreateZoom(float coef, int os)
        {
            // создаем матрицу 
            float[,] Zoom3D = new float[3, 3]; Zoom3D[0, 0] = 1;
            Zoom3D[1, 0] = 0;
            Zoom3D[2, 0] = 0;

            Zoom3D[0, 1] = 0;
            Zoom3D[1, 1] = 1;
            Zoom3D[2, 1] = 0;

            Zoom3D[0, 2] = 0;
            Zoom3D[1, 2] = 0;
            Zoom3D[2, 2] = 1;

            // устанавливаем коэффициент масштабирования для необходимой (выбранной и переданной в качестве параметра) оси 
            Zoom3D[os, os] = coef;

            // вызываем функцию для выполнения умножения матриц, представляющих собой координаты вершин геометрического объекта 
            // на созданную в данной функции матрицу 
            multiply(GeomObject, Zoom3D);
        }

        // перенос 
        private void CreateTranslate(float translate, int os)
        {
            // в виду простоты данного алгоритма, мы упростили его обработку - 
            // достаточно прибавить изменение (перенос) в координатах объекта по выбранной и переданной оси 
            for (int ax = 0; ax < count_elements; ax++)
            {
                // обновление координат (для выбранной оси) 
                GeomObject[ax, os] += translate;
            }
        }

        // реализация поворота 
        private void CreateRotate(float angle, int os)
        {
            // массив, который будет содержать матрицу 
            float[,] Rotate3D = new float[3, 3];
            // в зависимости от оси, матрицы будут кардинально различаться, 
            // поэтому создаем необходимую матрицу в зависимости от оси, используя switch 
            switch (os)
            {
                case 0: // вокруг оси Х 
                    {
                        Rotate3D[0, 0] = 1;
                        Rotate3D[1, 0] = 0;
                        Rotate3D[2, 0] = 0;

                        Rotate3D[0, 1] = 0;
                        Rotate3D[1, 1] = (float)Math.Cos(angle);
                        Rotate3D[2, 1] = (float)-Math.Sin(angle);

                        Rotate3D[0, 2] = 0;
                        Rotate3D[1, 2] = (float)Math.Sin(angle);
                        Rotate3D[2, 2] = (float)Math.Cos(angle);
                        break;
                    }
                case 1: // вокруг оси Y 
                    {
                        Rotate3D[0, 0] = (float)Math.Cos(angle);
                        Rotate3D[1, 0] = 0;
                        Rotate3D[2, 0] = (float)Math.Sin(angle);
                        Rotate3D[0, 1] = 0;
                        Rotate3D[1, 1] = 1;
                        Rotate3D[2, 1] = 0;
                        Rotate3D[0, 2] = (float)-Math.Sin(angle);
                        Rotate3D[1, 2] = 0;
                        Rotate3D[2, 2] = (float)Math.Cos(angle);
                        break;

                    }
                case 2: // вокруг оси Z 
                    {
                        Rotate3D[0, 0] = (float)Math.Cos(angle);
                        Rotate3D[1, 0] = (float)-Math.Sin(angle);
                        Rotate3D[2, 0] = 0;

                        Rotate3D[0, 1] = (float)Math.Sin(angle);
                        Rotate3D[1, 1] = (float)Math.Cos(angle);
                        Rotate3D[2, 1] = 0;

                        Rotate3D[0, 2] = 0;
                        Rotate3D[1, 2] = 0;
                        Rotate3D[2, 2] = 1;
                        break;
                    }

            }

            // вызываем функцию для выполнения умножения матриц, представляющих собой координаты вершин геометрического объекта 
            // на созданную в данной функции матрицу 
            multiply(GeomObject, Rotate3D);
        }

        // функция умножения матриц 
        private void multiply(float[,] obj, float[,] matrix)
        {
            // временные переменные 
            float res_1, res_2, res_3;

            // проходим циклом по всем координатам (представляющие собой матрицу A [x,y,z]) 
            // и умножаем каждую матрицу на матрицу B (переданную) 
            // результат сразу заносим в массив геометрии 

            for (int ax = 0; ax < count_elements; ax++)
            {
                res_1 = (obj[ax, 0] * matrix[0, 0] + obj[ax, 1] * matrix[0, 1] + obj[ax, 2] * matrix[0, 2]);
                res_2 = (obj[ax, 0] * matrix[1, 0] + obj[ax, 1] * matrix[1, 1] + obj[ax, 2] * matrix[1, 2]);
                res_3 = (obj[ax, 0] * matrix[2, 0] + obj[ax, 1] * matrix[2, 1] + obj[ax, 2] * matrix[2, 2]);

                obj[ax, 0] = res_1;
                obj[ax, 1] = res_2;
                obj[ax, 2] = res_3;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Vector2D A = new Vector2D(4, 9);
            textBox1.Text = A.ToString();

        }

    }
    
}
