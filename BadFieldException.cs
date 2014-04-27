using System;

namespace ClassLibrary1
{
    /// <summary>
    /// Исключение, которое генерируется, если поле подстановки было построено неверно
    /// </summary>
    public class BadFieldException : ApplicationException
    {
        private string message;
        /// <summary>
        /// Сообщение, демонстрируемое пользователю
        /// </summary>
        public override string Message
        {
            get
            {
                return message;
            }
        }

        /// <summary>
        /// Констуктор исключения
        /// </summary>
        /// <param name="badField">Неприемлимое поле</param>
        public BadFieldException(string badField, string cause)
        {
            message = "Поле подстановки ??" + badField + "?? не является верным. Причина: " + cause;
        }
    }
}
