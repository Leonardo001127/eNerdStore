using NSE.Core;
using System;

namespace NSE.Cliente.API.Models
{
    public class Endereco : Entity
    {

        //apenas para o tratamento do entity framework
        public Guid ClienteId { get; private set; }
        public Cliente Cliente { get; private set; }


        public string Logradouro { get; private set; }

        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Cep { get; private set; }
        public string Estado { get; private set; }

        protected Endereco() { }
        public Endereco(string bairro, string cidade, string numero, string complemento, string cep, string estado)
        {
            Bairro = bairro;
            Cidade = cidade;     
            Numero = numero;
            Complemento = complemento;
            Cep = cep;
            Estado = estado; 

            
        }
    }
}
