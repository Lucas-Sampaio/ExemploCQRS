using System;

namespace Utils
{
    public static class DateTimeExtensions
    {
        public static int CalcularIdade(this DateTime dataNascimento)
        {
            var idade = DateTime.Now.Year - dataNascimento.Year;

            if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
                idade--;

            return idade;
        }
    }
}
