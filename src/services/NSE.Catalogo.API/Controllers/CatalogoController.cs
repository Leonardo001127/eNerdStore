using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Models;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.Identidade;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSE.Catalogo.API.Controllers
{
    [Route("catalogo")]
    [ApiController]
    [Authorize]
    public class CatalogoController : MainController
    {
        private readonly IProdutoRepository produtoRepository;

        public CatalogoController(IProdutoRepository produtoRepository)
        {
            this.produtoRepository = produtoRepository;
        }

        [AllowAnonymous]
        [HttpGet("produtos")]
        public async Task<IEnumerable<Produto>> Index()
         => await produtoRepository.ObterProdutos();


        [ClaimsAuthorize("Catalogo","Ler")]
        [HttpGet("produtos/{Id}")]
        public async Task<Produto> ProdutosDetalhe(Guid Id)
         => await produtoRepository.ObterProduto(Id);

    }
}
