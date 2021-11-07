using System;
using System.Linq;

namespace Utils
{
    public static class StringExtensions
    {
        /// <summary>
        /// retorna apenas numeros de uma string
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static string ApenasNumeros(this string valor)
        {
            return new string(valor.Where(char.IsDigit).ToArray());
        }
        /// <summary>
        /// converte a string para numero
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="valorPadrao">valor de retorno padrão caso string seja vazia</param>
        /// <returns></returns>
        public static int ToInt32(this string valor, int valorPadrao = 0)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return valorPadrao;

            return Convert.ToInt32(valor);
        }
    }
}
