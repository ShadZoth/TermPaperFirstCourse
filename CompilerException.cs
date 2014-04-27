using System;
using System.CodeDom.Compiler;

namespace ClassLibrary1
{
    /// <summary>
    /// Исключение, возникающее при ошибке компиляции ответа
    /// </summary>
    public class CompilerException : ApplicationException
    {
        private string message;
        public override string Message
        {
            get
            {
                return message;
            }
        }

        /// <summary>
        /// Создает исключение, основываясь на ошибках компилятора
        /// </summary>
        /// <param name="cec">Ошибки компилятора</param>
        public CompilerException(CompilerErrorCollection cec)
        {
            message = "При компиляции ответа произошл";
            message += (cec.Count == 1) ? "а следующая ошибка: " : "и следующие ошибки:\n";
            foreach (CompilerError ce in cec)
            {
                message += String.Format("  {0}\n", ce.ToString());
            }
        }
    }
}
