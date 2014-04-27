using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;

namespace ClassLibrary1
{
    /// <summary>
    /// Класс, содержащий методы создания и работы с процессами, расчитывающими
    /// ответы
    /// </summary>
    public class Developer
    {
        /// <summary>
        /// Создаёт массив ответов, в которых 0ой элемент эвляется верным
        /// </summary>
        ///<param name="rigth">Правильный ответ</param>
        ///<param name="wrong">Неправильные ответы</param>
        ///<param name="i">Номер вопроса</param>
        ///<param name="v">Массив переменных полей подстановки, по которым формируется ответ</param>
        /// <returns></returns>
        public static string[] CreateAnswers(string rigth, string[] wrong, int i, Variable[] v)
        {
            string pathToCs = CreateCs(rigth, wrong, i, v); // Создание файла с кодом
            string pathToExe = CreateExe(pathToCs); // Создание исполняемого файла
            DoExe(pathToExe); // Исполнение
            string[] res = new string[wrong.Length + 1];
            res[0] = File.ReadAllText(System.Environment.CurrentDirectory
                + "\\RightAnswer.shdw");// считывание из файла верного ответа
            for (int j = 1; j < res.Length; j++)
                res[j] = File.ReadAllText(System.Environment.CurrentDirectory
                    + "\\WrongAnswer" + (j - 1).ToString() + ".shdw"); //считывание из файла неверного ответа
            DeleteFiles(wrong.Length, i); //"Уборка"
            return res;
        }

        /// <summary>
        /// Метод, создающий код программы, которая расчитывает ответ,
        /// и возвращающий путь к нему
        /// </summary>
        /// <param name="right">Код правильного ответа</param>
        /// <param name="wrong">Коды неправильных ответов</param>
        /// <param name="i"> Номер вопроса</param>
        ///<param name="v">Массив переменных полей подстановки, 
        ///по которым формируется ответ</param>
        /// <returns></returns>
        static private string CreateCs(string right, string[] wrong, int i, Variable[] v)
        {
            string path = System.Environment.CurrentDirectory + @"\code"
                + i + ".cs"; //Создание файла с кодом новой программы
            if (File.Exists(path)) // Если файл с таким названием уже есть, его следует удалить
                File.Delete(path);

            #region "Шапка"
            File.AppendAllText(path, @"using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main()
        {
       ");
            #endregion
            foreach (Variable var in v)
            {
                File.AppendAllText(path, var.Type + " " + var.Name + "="
                    + var.Value + ";\n"); // объявление переменных
            }
            AddAnswerToCode(right, path, "RightAnswer"); // запись правильного ответа в файл
            for (int j = 0; j < wrong.Length; j++)
            {
                AddAnswerToCode(wrong[j], path, "WrongAnswer" + j.ToString()); // запись неправильных ответов в файл
            }
            File.AppendAllText(path, @"
        }
    }
}");
            return path;
        }

        /// <summary>
        /// Добавляет ответ в код
        /// </summary>
        /// <param name="answer">Ответ</param>
        /// <param name="path">Путь к коду</param>
        /// <param name="name">Название, которое будет иметь файл с результатом
        /// исполнения кода</param>
        private static void AddAnswerToCode(string answer, string path, string name)
        {
            File.AppendAllText(path, "object " + name + ";\n"); //создание объекта ответа
            File.AppendAllText(path, "try{");
            File.AppendAllText(path, name + " = " + answer + ";\n"); // Получение ответа
            #region Ловец исключений
            File.AppendAllText(path, @"}
catch(Exception exception)
{
    " + name + @"=""Программа будет аварийно завершена из-за исключения: "" + exception.Message;
}");
            #endregion
            File.AppendAllText(path, @"string PathTo" + name + @" = System.Environment.CurrentDirectory + @""\" + name + @".shdw"";
            if (File.Exists(PathTo" + name + @"))
                File.Delete(PathTo" + name + @");
            File.AppendAllText(PathTo" + name + @", " + name + ".ToString());"); // запись ответа в файл
        }

        /// <summary>
        /// Удаление временных файлов
        /// <param name="length">Количество файлов с неправильными ответами, 
        /// которые необходимо удалить</param>
        /// <param name="i">Номер вопроса</param>
        /// </summary>
        static private void DeleteFiles(int length, int i)
        {
            File.Delete(System.Environment.CurrentDirectory + @"\code" + i + ".cs"); // Файл с кодом
            File.Delete(System.Environment.CurrentDirectory + @"\code" + i + "_cs.exe"); // Исполняемый файл
            File.Delete(System.Environment.CurrentDirectory + "\\RightAnswer.shdw");
            for (int j = 0; j < length; j++)
            {
                File.Delete(System.Environment.CurrentDirectory + "\\WrongAnswer" + (j).ToString() + ".shdw");
            }// Файлы с ответами
        }

        /// <summary>
        /// Запускает исполняемый файл
        /// </summary>
        /// <param name="pathToExe">Путь к исполняемому файлу</param>
        private static void DoExe(string pathToExe)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = pathToExe;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; // убирает неприятное мелькания консолей
            proc.Start();
            proc.WaitForExit();
        }

        /// <summary>
        /// Компилирует код на языке C# и возвращает адрес исполняемого файла
        /// </summary>
        /// <param name="path">Путь к файлу с кодом</param>
        /// <returns></returns>
        private static string CreateExe(string path)
        {
            FileInfo f = new FileInfo(path);
            CodeDomProvider prov = CodeDomProvider.CreateProvider("CSharp"); // Язык - С#

            String exe = String.Format(@"{0}\{1}.exe",
                    Environment.CurrentDirectory,
                    f.Name.Replace(".", "_")); // Создание пути к исполняемому файлу
            CompilerParameters cp = SetParams(exe); // Установка параметров компиляции            
            CompilerResults res = prov.CompileAssemblyFromFile(cp, path); //Компилирование
            if (res.Errors.Count > 0) //ловец ошибок
            {
                throw new CompilerException(res.Errors);
            }
            return exe;
        }

        /// <summary>
        /// Установка параметров компиляции
        /// </summary>
        /// <param name="pathToExe">Адрес исполняемого файла</param>
        /// <returns></returns>
        private static CompilerParameters SetParams(String pathToExe)
        {
            CompilerParameters cParams = new CompilerParameters();
            cParams.GenerateExecutable = true;
            cParams.OutputAssembly = pathToExe;
            cParams.GenerateInMemory = false;
            cParams.TreatWarningsAsErrors = false;
            return cParams;
        }
    }
}
