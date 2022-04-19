using System;
using System.Collections.Generic;

namespace NSE.Carrinho.API.Model
{
    public class CarrinhoCliente
    {
        public CarrinhoCliente(Guid clientid)
        {
            Id = new Guid();
            ClienteId = clientid;
        }
        public CarrinhoCliente() { }
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public string Nome { get; set; } 
        public double ValorTotal { get; set; }
        public List<CarrinhoItem> Itens { get; set; } = new List<CarrinhoItem>();
    }
}
