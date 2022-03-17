using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSE.Core.Message
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }

        //sempre é vinda de uma raiz de agregação - por ser uma mensagem genérica
        public Guid AggregateId { get; protected set; }


        protected Message()
        {
            //Pega o nome da classe que estiver herdando
            MessageType = GetType().Name;
        }

    }
}
