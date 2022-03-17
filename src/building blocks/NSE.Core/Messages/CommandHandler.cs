using FluentValidation.Results;
using NSE.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        protected async Task<ValidationResult> Persist(IUnitOfWork work)
        {
            if(!await work.Commit())
                AddError("Erro ao inserir persistir informações na base de dados");

            return ValidationResult;
        }

    }
}