using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text;
using WebAppBackend.Data.Interfaces;
using WebAppBackend.Entidades;
using WebAppBackend.Services.Interfaces;
using WebAppBackend.Validacoes;

namespace WebAppBackend.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UsuarioValidacao _usuarioValidacao;
        private readonly IMapper _mapper;

        public LoginService(IUsuarioRepository usuarioRepository, UsuarioValidacao usuarioValidacao)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioValidacao = usuarioValidacao;
        }

        private string CriarHash(string senha)
        {
            byte[] salt = Encoding.ASCII.GetBytes(senha);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(senha, salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));

            return hashed;
        }

        public Usuario Login(string? username, string? password)
        {
            Usuario usuario = new Usuario { Login = username, Password = password };
            try
            {

                _usuarioValidacao.ValidarAcesso(usuario);
                if (usuario.ValidationResult != null && usuario.ValidationResult.Errors.Any())
                    return usuario;

                usuario.Password = CriarHash(usuario.Password);
                usuario = _usuarioRepository.Login(usuario.Login, usuario.Password);
            }catch(Exception ex)
            {
                usuario.Password = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }

            return usuario;
        }
    }
}
