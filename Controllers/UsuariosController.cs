using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Exo.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[contoller]")]
    [ApiController]
    public class UsuarionController : ControllerBase
    {
        private readonly UsuarioRepositoy _usuarioRepository;
        public UsuarionController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;

        }
        //  get -> / api/usuarios
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_usuarioRepository.Listar());
        }

        //post -> /api/ usuarios
        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            _usuarioRepository.Cadastrar(usuario);
            return StatusCode(201);
        }


        // get -> /api/usuarios/{id}
        [HttpGet("{id}")] //faz a busca pelo ID
        public IActionResult BuscarPorId(int id)
        {
            Usuario usuario = _usuarioRepository.BuscarPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }
        //put -> /api/usuatios/{id}
        // Atualiza.
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Usuario usuario)
        {
            _usuarioRepository.Atualizar(id, usuario);
            return StatusCode(204);
        }
        //delete -> /api/usuario/{id}
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _usuarioRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}