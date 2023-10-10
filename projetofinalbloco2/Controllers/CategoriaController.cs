﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using projetofinalbloco2.Model;
using projetofinalbloco2.Service;

namespace projetofinalbloco2.Controllers
{
    [Route("~/categorias")] // Rota feita na URL
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategotiaService _categoriaService;
        private readonly IValidator<Categoria> _categoriaValidator;

        public CategoriaController(
           ICategotiaService categoriaService,
           IValidator<Categoria> categoriaValidator
           )
        {
            _categoriaService = categoriaService;
            _categoriaValidator = categoriaValidator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _categoriaService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _categoriaService.GetById(id);

            if (Resposta is null)
            {
                return NotFound("Categoria não encontrado!");
            }

            return Ok(Resposta);
        }

        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult> GetByTipo(string tipo)
        {
            return Ok(await _categoriaService.GetByTipo(tipo));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Categoria categoria)
        {
            var validarCategoria = await _categoriaValidator.ValidateAsync(categoria);

            if (!validarCategoria.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarCategoria);

            var Resposta = await _categoriaService.Create(categoria);
            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Categoria categoria)
        {
            if (categoria.Id == 0)
            {
                return BadRequest("Id da categoria é inválido");
            }

            var validarCategoria = await _categoriaValidator.ValidateAsync(categoria);

            if (!validarCategoria.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarCategoria);
            }

            var Resposta = await _categoriaService.Update(categoria);

            if (Resposta is null)
            {
                return NotFound("Categoria não encontrada!");
            }
            return Ok(Resposta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var BuscaCategoria = await _categoriaService.GetById(id);

            if (BuscaCategoria is null)
                return NotFound("Categoria não encontrado!");

            await _categoriaService.Delete(BuscaCategoria);

            return NoContent();

        }
    }
}
