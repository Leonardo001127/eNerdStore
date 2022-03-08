using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.Catalogo.API.Controllers
{
    [Route("catalogo")]
    [ApiController]
    public class CatalogoController :Controller
    {
        private readonly IProdutoRepository produtoRepository;

        public CatalogoController(IProdutoRepository produtoRepository)
        {
            this.produtoRepository = produtoRepository;
        }

        [HttpGet("produtos")]
        public async Task<IEnumerable<Produto>> Index()
         => await produtoRepository.ObterProdutos();


        [HttpGet("produtos/{id}")]
        public async Task<Produto> ProdutosDetalhe(Guid Id)
         => await produtoRepository.ObterProduto(Id);

    }
}
