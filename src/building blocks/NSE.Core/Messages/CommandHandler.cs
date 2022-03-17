using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSE.Core.Message
{
    public abstract class CommandHandler
    {
        public ValidationResult ValidationResult { get; set; }

        protected CommandHandler()
        { 
            ValidationResult = new ValidationResult();
        }
        protected void AddError(string Erro)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, Erro));
        }

    }
}