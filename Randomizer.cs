using System;

namespace ClassLibrary1
{
    public class Randomizer
    {
        #region Допустимые значения переменных
        const int _signedMinValue = -100;
        const int _signedMaxValue = 100;
        const int _unsignedMaxValue = 100;
        const double _realMinValue = -100.00;
        const double _realMaxValue = 100.00;
        const char _symbolMinValue = 'A';
        const char _symbolMaxValue = 'z';
        const int _maxLength = 5;
        #endregion

        static Random rnd = new Random(DateTime.Now.Millisecond * (int)DateTime.Now.Ticks);

        /// <summary>
        /// Создает случайную строку
        /// </summary>
        /// <returns></returns>
        public static string RandomLine()
        {
            int length = rnd.Next(_maxLength - 1) + 1;
            string value = "\"";
            for (int j = 0; j < length; j++)
                value += RandomSymbol().Trim('\'');
            value += "\"";
            return value;
        }

        /// <summary>
        /// Создает случайный символ
        /// </summary>
        /// <returns></returns>
        public static string RandomSymbol()
        {
            char c = (char)rnd.Next(_symbolMinValue, _symbolMaxValue + 1);
            string value;
            if (c == '\\')
            {
                value = "'\\\\'";
            }
            else
            {
                value = "'" + c + "'";
            }
            return value;
        }

        /// <summary>
        /// Создает случайную десятикную дробь
        /// </summary>
        /// <returns></returns>
        public static string RandomReal()
        {
            return (_realMinValue + rnd.NextDouble() * (_realMaxValue - _realMinValue)).ToString("F3").Replace(',', '.');
        }

        /// <summary>
        /// Создает случайное беззнаковое число
        /// </summary>
        /// <returns></returns>
        public static string RandomUnsigned()
        {
            return rnd.Next(_unsignedMaxValue).ToString();
        }

        /// <summary>
        /// Создаёт случайное знаковое число
        /// </summary>
        /// <returns></returns>
        public static string RandomSigned()
        {
            return rnd.Next(_signedMinValue, _signedMaxValue + 1).ToString();
        }

        /// <summary>
        /// Создает случайное логическое значение
        /// </summary>
        /// <returns></returns>
        public static string RandomBoolean()
        {
            return (rnd.Next(1) == 1).ToString();
        }
    }
}
