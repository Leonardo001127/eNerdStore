using FluentValidation;
using NSE.Core.Message;
using System;

namespace NSE.Clientes.API.Application.Commands
{
    public class RegistrarClienteCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public RegistrarClienteCommand(string _nome, string _email, string _cpf, Guid _id)
        {
            AggregateId = Id = _id;
            Nome = _nome;
            Email = _email;
            Cpf = _cpf;

        }
        public override bool IsValid()
        {
            ValidationResult = new RegistrarClienteValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
    public class RegistrarClienteValidation : AbstractValidator<RegistrarClienteCommand>
    {
        public RegistrarClienteValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O Id não pode ser nulo");

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O nome não pode ser nulo");

            RuleFor(c => c.Email)
                .Must(EmailValido)
                .WithMessage("O formato do e-mail está inválido");

            RuleFor(c => c.Cpf)
                .Must(CpfValido)
                .WithMessage("O formato do cpf está inválido");

        }
        protected static bool CpfValido(string cpf)
        {
            return Core.DomainObjects.Cpf.Validar(cpf);
        }

        protected static bool EmailValido(string email)
        {
            return Core.DomainObjects.Email.Validar(email);
        }
    }
}
