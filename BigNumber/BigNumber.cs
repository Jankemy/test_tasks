using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    /* Задача:
     * Необходимо реализовать класс BigNumber для работы с длинными числами:
     * - конструктор
     * - преобразование в строку
     * - оператор сложения

     * !Нельзя использовать готовые реализации длинных чисел

     * Требования к длинному числу:
     * - целое
     * - положительное
     * - произвольное число разрядов (может быть больше, чем допускает long)
     * Ограничения на строку - параметр конструктора BigNumber:
     * - содержит только цифры
     * - отсутствуют ведущие нули
     * 
     * Пример использования:
     * var a = new BigNumber("175872");
     * var b = new BigNumber("1234567890123456789012345678901234567890");
     * var r = a + b;
     * 
     * Для проверки решения необходимо запустить тесты.
     */

    public class BigNumber
    {
        // лучше хранить в обратном порядке
        private IList<int> digits;

        public BigNumber(string x)
        {
            var digitList = new List<int>();

            if (x[0] == '0') 
                throw new ArgumentException("The number must not start with 0");

            for (var i = x.Length-1; i >= 0; i--)
            {
                digitList.Add(int.Parse(x[i].ToString()));
            }

            this.digits = digitList;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var i = digits.Count() - 1; i >= 0; i--)
            {
                sb.Append(digits[i]);
            }

            return sb.ToString();
        }

        public static BigNumber operator +(BigNumber a, BigNumber b)
        {
            var max = a.digits.Count() >= b.digits.Count() ? a : b;
            var min = a.digits.Count() >= b.digits.Count() ? b : a;

            int? dec = null;
            var resultSb = new StringBuilder();

            for (var i = 0; i <= max.digits.Count() - 1; i++) 
            {
                if (min.digits.Count() - 1 >= i)
                {
                    var maxI = max.digits[i];
                    var minI = min.digits[i];

                    var cI = (maxI + minI + (dec.HasValue ? dec.Value : 0)).ToString();

                    if (cI.Length < 2)
                    {
                        resultSb.Append(cI);
                        dec = null;
                    }
                    else
                    {
                        resultSb.Append(cI[1]);
                        dec = int.Parse(cI[0].ToString());
                    }
                }
                else 
                {
                    var maxI = max.digits[i];
                    var cI = (maxI + (dec.HasValue ? dec.Value : 0)).ToString();

                    if (cI.Length < 2)
                    {
                        resultSb.Append(cI);
                        dec = null;
                    }
                    else
                    {
                        resultSb.Append(cI[1]);
                        dec = int.Parse(cI[0].ToString());
                    }
                }
            }

            if (dec.HasValue && dec.Value > 0) resultSb.Append(dec.Value); 

            var charArray = resultSb.ToString().ToCharArray();
            Array.Reverse(charArray);

            return new BigNumber(new string(charArray));
        }
    }
}