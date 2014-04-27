using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ClassLibrary1;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Форма, в которой задается фаблон вопроса
    /// </summary>
    public partial class InputForm : Form
    {
        /// <summary>
        /// Конструктор формы без параметров
        /// </summary>
        public InputForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            genButton.Enabled = false;
        }
        
        /// <summary>
        /// Метод проверяющий правильность ввода количества заданий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Nbox_TextChanged(object sender, EventArgs e)
        {
            int n;
            if (int.TryParse(nBox.Text, out n) && (n > 0))
                genButton.Enabled = true; //Если количество заданий введено верно, можно приступать к генерации
            else
                genButton.Enabled = false;
        }

        
        private static OutputForm[] _quest;
        /// <summary>
        /// Массив вопросов
        /// </summary>
        public static OutputForm[] Questions
        {
            get { return _quest; }
            set { _quest = value; }
        }

        /// <summary>
        /// Генерация заданий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Generate(object sender, EventArgs e)
        {
            int n;
            int.TryParse(nBox.Text, out n);
            try
            {
                N = n;
                _quest = new OutputForm[n]; //Создание n окон, в которые будут записаны задания
                genButton.Visible = false; // Убрать кнопку
                progressBar1.Visible = true; // Показать progress bar
                progressBar1.Maximum = n;
                for (int i = 0; i < n; i++, progressBar1.Value = i)
                    if (!open.Checked)
                        _quest[i] = new OutputForm
                            (i, codeBox.Lines, rightBox.Text, wrongBox3.Text,
                            wrongBox4.Text, wrongBox2.Text, wrongBox1.Text); // Создание заданий и запись их в окно
                    else
                        _quest[i] = new OutputForm(i, codeBox.Lines, rightBox.Text);
                _quest[0].Show(); //Показ нулевого задания
            }
            catch (Exception ex) //Ловец исключений
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
            finally // Вне зависимости от успешности создания задания необходимо вернуть кнопку
            {
                progressBar1.Value = 0;
                progressBar1.Visible = false;
                genButton.Visible = true;
            }
        }

        /// <summary>
        /// Нажатие кнопки "Новый вопрос"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newButton_Click(object sender, EventArgs e)
        {
            DialogResult d = Dialogs.Sure(); // Запрос подтверждения
            if (d == DialogResult.Yes)
            {
                Clear(); // Очищение
            }
        }

        /// <summary>
        /// Очищает окно
        /// </summary>
        private void Clear()
        {
            codeBox.Text = "";
            rightBox.Text = "";
            wrongBox3.Text = "";
            wrongBox4.Text = "";
            wrongBox2.Text = "";
            wrongBox1.Text = "";
            nBox.Text = "";
        }
        
        /// <summary>
        /// Нажатие кнопки "Открыть"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadButton_Click(object sender, EventArgs e)
        {
            DialogResult d = Dialogs.Sure(); // Запрос подтверждения
            if (d == DialogResult.Yes)
            {
                Clear(); //Очищение окна
                OpenFileDialog op = new OpenFileDialog();
                Dialogs.SetParams(op); // Установка параметров
                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(op.FileName)) //Проверка существования открываемого файла
                    {
                        StreamReader sr = new StreamReader(op.FileName);

                        //string[] lines = File.ReadAllLines(op.FileName); //Считывание данных из файла
                        //try
                        //{
                        //    int i = 0;
                        List<string> list = new List<string>();
                        string line = sr.ReadLine();
                        while (line != "___")
                        {
                            list.Add(line);
                            line = sr.ReadLine();
                            if (line == null) break;
                        }
                        

                        //    while (lines[i] != "___")
                        //    {
                        //        list.Add(lines[i]); //Запись вопроса
                        //        i++;
                        //    }
                        codeBox.Lines = list.ToArray();
                        string openChecked = sr.ReadLine();
                        open.Checked = (openChecked == "True");
                        rightBox.Text = sr.ReadLine();
                        if (!open.Checked)
                        {
                            wrongBox1.Text = sr.ReadLine();
                            wrongBox2.Text = sr.ReadLine();
                            wrongBox3.Text = sr.ReadLine();
                            wrongBox4.Text = sr.ReadLine();
                        }

                        //    OpenAnswers(lines, i); // Запись ответов
                        //    const int nIndex = 6;
                        //    nBox.Text = lines[i + nIndex]; // Запись количества заданий
                        //}
                        //catch (IndexOutOfRangeException) // ловец исключений
                        //{
                        //    MessageBox.Show("Файл поврежден или не является файлом шаблона", "Невозможно открыть файл"); //Сообщение об ошибке
                        //    Clear(); //Удаление того, что было считано
                        //}
                    }
                }
            }
        }

        ///// <summary>
        ///// Открытие ответов
        /////</summary>
        ///// <param name="lines">Массив строк, в котором содержатся ответы</param>
        ///// <param name="i">Строка, после которой начинаются ответы</param>
        //private void OpenAnswers(string[] lines, int i)
        //{
        //    rightBox.Text = lines[i + 1];
        //    wrongBox1.Text = lines[i + 2];
        //    wrongBox2.Text = lines[i + 3];
        //    wrongBox3.Text = lines[i + 4];
        //    wrongBox4.Text = lines[i + 5];
        //}

        /// <summary>
        /// Нажатие кнопки "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if ((codeBox.Text == "")
                || (rightBox.Text == "")
                || ((!open.Checked) && (wrongBox1.Text == ""))
                || ((!open.Checked) && (wrongBox2.Text == ""))
                || ((!open.Checked) && (wrongBox3.Text == ""))
                || ((!open.Checked) && (wrongBox4.Text == ""))
                || (nBox.Text == ""))
                MessageBox.Show("Не все поля заполнены", "Ошибка"); //Для коррекного работы программы требуется, чтобы все поля были заполнены
            else
            {
                SaveFileDialog save = new SaveFileDialog();
                Dialogs.SetParams(save); // Установка параметров
                if (save.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(save.FileName, false);
                    foreach (string line in codeBox.Lines)
                        sw.WriteLine(line);
                    sw.WriteLine("___");
                    sw.WriteLine(open.Checked);
                    sw.WriteLine(rightBox.Text);
                    if (!open.Checked)
                    {
                        sw.WriteLine(wrongBox1.Text);
                        sw.WriteLine(wrongBox2.Text);
                        sw.WriteLine(wrongBox3.Text);
                        sw.WriteLine(wrongBox4.Text);
                    }
                    sw.Flush();
                }
            }
        }

        ///// <summary>
        ///// Нажатие кнопки "Создать" из меню
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    newButton_Click(sender, e);
        //}

        ///// <summary>
        ///// Нажатие кнопки "Открыть" из меню
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    LoadButton_Click(sender, e);
        //}

        ///// <summary>
        ///// Нажатие кнопки "Сохранить" из меню
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    SaveButton_Click(sender, e);
        //}

        /// <summary>
        /// Нажатие кнопки "Выход"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult d = Dialogs.Sure();
            if (d == DialogResult.Yes)
                this.Close();
        }

        /// <summary>
        /// Нажатие кнопки "О программе"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new AboutBox1().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        /// <summary>
        /// Нажатие кнопки "Справка"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotImplementedException ex = new NotImplementedException();
            MessageBox.Show(ex.Message, ex.GetType().ToString());
        }

        static int _n;
        /// <summary>
        /// Максимальное количество заданий
        /// </summary>
        static public int N
        {
            get { return _n; }
            set { _n = value; }
        }

        private void Open_CheckedChanged(object sender, EventArgs e)
        {
            wrongBox1.Enabled = wrongBox2.Enabled = wrongBox3.Enabled = wrongBox4.Enabled = !open.Checked;
        }
    }
}
