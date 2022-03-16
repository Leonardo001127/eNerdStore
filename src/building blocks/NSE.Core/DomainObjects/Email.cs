using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NSE.Core.DomainObjects
{
    public  class Email
    {
        public const int MaxLengthEmail = 254;
        public const int MinLengthEmail = 5;
        public string _email { get; private set; }

        protected Email() { }

        public Email(string email)
        {
            if (!Validar(email)) throw new DomainException("E-mail inválido");
            _email = email;
        }
        public bool Validar(string email)
        {
            var regexEmail = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

            return regexEmail.IsMatch(_email);
        }
    }
}
