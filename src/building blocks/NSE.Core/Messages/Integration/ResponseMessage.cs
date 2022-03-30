using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSE.Core.Messages.Integration
{
  public class ResponseMessage : Message.Message
    {
        public ValidationResult validationResult { get; set; }

        public ResponseMessage(ValidationResult validationResult)
        {
            this.validationResult = validationResult;
        }

    }
}
