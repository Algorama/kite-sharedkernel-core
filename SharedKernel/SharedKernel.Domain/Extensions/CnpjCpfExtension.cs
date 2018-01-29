using System;
using System.Linq;

namespace SharedKernel.Domain.Extensions
{
    public static class CnpjCpfExtension
    {
        public static bool ValidarCnpjCpf(this string cnpjCpf)
        {
            if (string.IsNullOrEmpty(cnpjCpf))                     return false;
            if (!cnpjCpf.All(char.IsDigit))                        return false;
            if (new string(cnpjCpf[0], cnpjCpf.Length) == cnpjCpf) return false;

            var d = new int[14];
            var v = new int[2];
            int j, i, soma;

            //validação de CPF
            switch (cnpjCpf.Length)
            {
                case 11:
                    for (i = 0; i <= 10; i++) d[i] = Convert.ToInt32(cnpjCpf.Substring(i, 1));
                    for (i = 0; i <= 1; i++)
                    {
                        soma = 0;
                        for (j = 0; j <= 8 + i; j++) soma += d[j] * (10 + i - j);

                        v[i] = (soma * 10) % 11;
                        if (v[i] == 10) v[i] = 0;
                    }
                    return (v[0] == d[9] & v[1] == d[10]);
                case 14:
                    var Sequencia = "6543298765432";
                    for (i = 0; i <= 13; i++) d[i] = Convert.ToInt32(cnpjCpf.Substring(i, 1));
                    for (i = 0; i <= 1; i++)
                    {
                        soma = 0;
                        for (j = 0; j <= 11 + i; j++)
                            soma += d[j] * Convert.ToInt32(Sequencia.Substring(j + 1 - i, 1));

                        v[i] = (soma * 10) % 11;
                        if (v[i] == 10) v[i] = 0;
                    }
                    return (v[0] == d[12] & v[1] == d[13]);
            }
            return false;
        }
    }
}
