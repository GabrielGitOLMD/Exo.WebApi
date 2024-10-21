using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;



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
        //[HttpPost]
        //  public IActionResult Cadastrar(Usuario usuario)
        //  {
        //   _usuarioRepository.Cadastrar(usuario);
        //    return StatusCode(201);
        // }


        //Novo codigo Post para auxxiliar o método de Login.
        public IActionResult Post(Usuario usuario)
        {
            Usuario usuarioBuscado = _usuarioRepository.Login(usuario.Email, usuario.Senha);
            if (usuarioBuscado == null)
            {
                return NotFound("email ou senha inválidos!");

            }
            //se o usuario for encontrado , segue a criaçao do token.

            // define os dados que serão fornecidos no token - Payload.
            var Claims = new[]
            {
                //Armazena na claim o email usuário autenticado.
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                //armazena na claim o email usuario autenticado.
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString()),
            };
                // define a chave de acesso ao token.
                var key = new
                SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chave-autenticacao"));

            // define as credenciais do token.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Gera o token.
            var token = new JwtSecurityToken(
                issuer: "exoapi.WebApi", //emissor do token.
                audience: "exoapi.webapi", //destinatario do token.
                claims: Claims, //dados definidos acima.
                expires: DataTime.Now.AddMinutes(30), // tempo de expiraçao.
                signingCredentials: creds // Credenciais do token.
            );
            // retorna ok com o token.
            return Ok(
                new { token = new JwtSecurityTokenHandler().WriteToken(token) }
            );

        }
            // fim do novo codigo Post para auxiliar o metodo de login.
        

        


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
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Usuario usuario)
        {
            _usuarioRepository.Atualizar(id, usuario);
            return StatusCode(204);
        }
        //delete -> /api/usuario/{id}
        [Authorize]
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