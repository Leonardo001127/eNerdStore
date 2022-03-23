using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSE.Core.Message
{
    public  class Event : Message, INotification, IRequest<ValidationResult>
    {
        
        public DateTime TimeStamp { get; set; }

        public ValidationResult ValidationResult { get; set; }

        protected Event()
        {
            TimeStamp = DateTime.Now;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }

    }
}   
