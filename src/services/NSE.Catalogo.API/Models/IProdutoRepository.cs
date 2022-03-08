using NSE.Core.DomainObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.Catalogo.API.Models
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutos();

        Task<Produto> ObterProduto(Guid produto);

        void Adicionar(Produto produto);

        void Atualizar(Produto produto);
    }
}
