using System;

namespace ClassLibrary1
{
    static public class Comparer
    {
        /// <summary>
        /// Указывает, находится ли значение на указанном отрезке
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="min">Левая граница отрезка</param>
        /// <param name="max">Правая граница отрезка</param>
        /// <returns></returns>
        public static bool In(this char value, char min, char max)
        {
            if (min > max)
            {
                throw new ArgumentException("Минимальное значение должно быть больше максимального");
            }
            return (value >= min && value <= max);
        }
    }
}
