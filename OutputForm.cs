using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ClassLibrary1;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Окно с готовым вопросом
    /// </summary>
    public partial class OutputForm : Form
    {    
        /// <summary>
        /// Список переменных полей подстановок
        /// </summary>
        List<Variable> _v;
                
        //static int _n;
        ///// <summary>
        ///// Щито?
        ///// </summary>
        //public static int N
        //{
        //    get { return Form2._n; }
        //    set { Form2._n = value; }
        //}

        /// <summary>
        /// Форма, содержащая вопрос, созданный по фаблону
        /// </summary>
        /// <param name="i">Номер вопроса</param>
        /// <param name="code">Шаблон</param>
        /// <param name="right">Правильный ответ</param>
        /// <param name="wrong">Неправильные ответы</param>
        public OutputForm(int i, string[] code, string right, params string[] wrong)
        {
            _i = i;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            saveAll.Enabled = InputForm.Questions.Length != 1; //"Сохранить всё" можно, только если вопросов несколько
            this.Text = "Сгенерированное задание №" + i;
            bool flag=true; // Успешно созданы разные ответы
            string[ ] question, answers;
            int failCount=0; // Количество неудачных попыток создания ответов
            do
            {
                _v=new List<Variable>();
                question = PrepareQuestion(code); // Подготовить вопрос
                answers = PrepareAnswers(right, wrong, ref flag, ref failCount); // Подготовить ответы
            } while (!flag);
            Display(question, answers); // Записать ответы в форму
            SetButtons(i); // Настроить кнопки
        }

        /// <summary>
        /// Настраивает кнопки так, чтобы можно было двигаться только в допустимых направлениях
        /// </summary>
        /// <param name="i"></param>
        private void SetButtons(int i)
        {
            if (i == 0)
                prevButton.Enabled = false; //Если вопрос нулевой, то назад двигаться нельзя
            if (i == InputForm.N - 1)
                nextButton.Enabled = false; //Если вопрос последний, то вперед двигаться нельзя
        }

        /// <summary>
        /// Записывает в форму ответы
        /// </summary>
        ///<param name="question">Вопрос</param>
        /// <param name="answers">Ответы</param>
        private void Display(string[] question, string[] answers)
        {
            if (answers.Length != 1)
            {
                answers[0] += " (V)"; // отметить правильный ответ
                //Array.Sort(answers); // сортировка ответов
                List<string> answersList = new List<string>();
                foreach (string answer in answers)
                    answersList.Add(answer);
                answersList.Sort(delegate(string a, string b)
                {
                    bool aBool, bBool;
                    double aDouble, bDouble;
                    aBool = double.TryParse(a, out aDouble);
                    bBool = double.TryParse(b, out bDouble);
                    if (aBool && !bBool)
                        return 1;
                    if (!aBool && bBool)
                        return -1;
                    if (aBool && bBool)
                        return aDouble.CompareTo(bDouble);
                    if (!aBool && !bBool)
                        return a.CompareTo(b);
                    return 0;
                });
                answers = answersList.ToArray();
            }
            const int additionalLines = 2;//пустая строка и строка с надписью "варианты ответов"/"правильный ответ"
            string[] lines = new string[question.Length + additionalLines + answers.Length];
            Array.Copy(question, lines, question.Length);
            lines[question.Length + 1] = answers.Length != 1 ? "Варианты ответов: " : "Правильный ответ: ";
            Array.Copy(answers, 0, lines, question.Length + additionalLines, answers.Length);
            task.Lines = lines;
        }

        /// <summary>
        /// Подготавливает ответы
        /// </summary>
        /// <param name="right">Правильный ответ</param>
        /// <param name="wrong">Неправильные ответы</param>
        /// <param name="flag">Успех</param>
        /// <param name="failCount">Счетчик провалов</param>
        /// <returns></returns>
        private string[] PrepareAnswers(string right, string[] wrong, ref bool flag, ref int failCount)
        {
            string[] answers;
            answers = Developer.CreateAnswers(right, wrong, _i, _v.ToArray());
            // Проверка того, что ответы разные
            for (int j = 0; j < answers.Length - 1; j++)
                for (int k = j + 1; k < answers.Length; k++)
                    if (answers[j] == answers[k])
                    {
                        flag = false;
                        failCount++;
                    }
            if (failCount >= 10) //10 попыток достаточно
                throw new TimeoutException("Невозможно получить разные ответы");
            return answers;
        }
        
        /// <summary>
        /// Составить вопрос
        /// </summary>
        /// <param name="code">Код, полученный из шаблона</param>
        private string[] PrepareQuestion(string[] code)
        {
            string[] question = new string[code.Length];
            
            for (int i=0; i<code.Length;i++)
            {
                string str = code[i];
                question[i] += str;
                MatchCollection lookup_fields = Regex.Matches(str, @"\?\?.+\?\?");
                
                foreach (Match m in lookup_fields)
                {
                    System.Threading.Thread.Sleep(1);
                    string val = m.Value.Trim('?', ' ');
                    _v.Add(new Variable(val));
                    for (int j = 0; j < _v.Count - 1; j++)
                        if (_v[j].Name == _v[_v.Count - 1].Name)
                            throw new BadFieldException(_v[_v.Count - 1].Type + " " + _v[_v.Count - 1].Name,
                                "Переменная с таким именем уже существует");
                    string mValue = m.Value;
                    mValue = Regex.Replace(mValue, @"\?", @"\?");
                    
                    
                    question[i] = Regex.Replace(question[i], mValue, _v[_v.Count - 1].Value);
                }                
            }
            return question; // Запись вопроса в форму
        }
        
        /// <summary>
        /// Номер окна
        /// </summary>
        int _i;

        /// <summary>
        /// Переход к следующему вопросу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextButton_Click(object sender, EventArgs e)
        {
            task.ReadOnly = true;
            ChangeButton(_bigFont, "Редактировать");
            this.Hide();
            InputForm.Questions[_i + 1].Show();
        }

        /// <summary>
        /// Возвращение к предыдущему вопросу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrevButton_Click(object sender, EventArgs e)
        {
            task.ReadOnly = true;
            ChangeButton(_bigFont, "Редактировать");
            this.Hide();
            InputForm.Questions[_i - 1].Show();
        }

        private void save_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.InitialDirectory = Environment.CurrentDirectory + "\\Задания";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.FileName);
                sw.Write(task.Text);
                sw.Flush();
            }
        }

        private void saveAll_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "";
            sfd.InitialDirectory = Environment.CurrentDirectory + "\\Задания";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Directory.CreateDirectory(sfd.FileName);
                for (int i = 0; i < InputForm.Questions.Length; i++)
                {
                    StreamWriter sw = new StreamWriter(sfd.FileName + "\\" + i.ToString() + ".rtf");
                    sw.Write(InputForm.Questions[i].task.Text);
                    sw.Flush();
                    
                }
            }
        }

        const float _bigFont = 15.75f;
        const float _smallFont = 12f;
                
        private void editButton_Click(object sender, EventArgs e)
        {
            task.ReadOnly = !task.ReadOnly;
            
            if (task.ReadOnly)
            {
                ChangeButton(_bigFont, "Редактировать");
            }
            else
            {
                ChangeButton(_smallFont,"Закончить редактирование");
            }
        }

        private void ChangeButton(float fontSize, string text)
        {
            editButton.Font = new System.Drawing.Font("Consolas", fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            editButton.Text = text;
        }
    }
}
