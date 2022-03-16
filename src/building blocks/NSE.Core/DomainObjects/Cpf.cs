using NSE.Core.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSE.Core.DomainObjects
{
    public class Cpf
    {
        public const int CpfLength = 11;
        public string Numero { get; set; }
        protected Cpf() { }

        public Cpf(string cpf)
        {
            if (!Validar(cpf)) throw new DomainException("CPF inválido");
            Numero = cpf;
        }
        public static bool Validar(string cpf)
        {
            cpf = cpf.ApenasNumeros();

            if (cpf.Length > CpfLength) return false;

            while (cpf.Length < CpfLength)
                cpf = string.Concat("0",cpf);

            var igual = true;

            for (int i = 1; i < CpfLength && igual; i++)
            {
                if (cpf[i] != cpf[0])
                    igual = false;
            }

            if (igual || cpf == "12345678909") return false;

            var nums = new int[CpfLength];

            for (int i = 0; i < CpfLength; i++) 
                nums[i] = int.Parse(cpf[i].ToString());

            var soma = 0;
            for (int i = 0; i < CpfLength - 2; i++)
                soma += (10 - i) * nums[i];
            
            var resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (nums[9] != 0)
                    return false;
            }
            else if(nums[9] != 11 - resultado)
            {
                return false;
            }

            soma = 0;
            for (int i = 0; i < CpfLength - 1; i++)
                soma += (10 - i) * nums[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (nums[10] != 0)
                    return false;
            }
            else if (nums[10] != 11 - resultado)
            {
                return false;
            }

            return true;
        }
    }

     
}
