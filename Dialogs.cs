using System.Windows.Forms;

namespace ClassLibrary1
{
    /// <summary>
    /// Библиотечный класс, содержащий методы для работы с диалоговыми окнами
    /// </summary>
    public class Dialogs
    {
        /// <summary>
        /// Запрашивает подтверждение у пользователя
        /// </summary>
        /// <returns></returns>
        public static DialogResult Sure()
        {
            return MessageBox.Show("Вы уверены? Все несохранненные данные будут потеряны", "Подтверждение операции", MessageBoxButtons.YesNo);
        }

        /// <summary>
        /// Устанока параметров для диалога открытия/сохранения файла
        /// </summary>
        /// <param name="dialog">Диалог открытия/сохранения файла</param>
        public static void SetParams(FileDialog dialog)
        {
            dialog.InitialDirectory = System.Environment.CurrentDirectory + "\\Шаблоны";
            dialog.Filter = "txt files (*.txt)|*.txt|Файл шаблона тестового задания(*.shdw)|*.shdw|All files (*.*)|*.*";
            const int allFilesIndex = 3;
            dialog.FilterIndex = allFilesIndex; //All files (*.*)|*.*
        }
    }
}
