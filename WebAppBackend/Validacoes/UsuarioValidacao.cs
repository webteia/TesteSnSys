using FluentValidation;
using WebAppBackend.Entidades;

namespace WebAppBackend.Validacoes
{
    public class UsuarioValidacao : AbstractValidator<Usuario>
    {
        public UsuarioValidacao()
        {

        }

        private void ValidarLogin()
        {
            RuleFor(regra => regra.Login).NotEmpty().WithMessage("O login é obrigatório").WithErrorCode("401");
        }

        private void ValidarSenha()
        {
            RuleFor(regra => regra.Password).NotEmpty().WithMessage("A senha é obrigatória").WithErrorCode("401");
        }
        public void ValidarAcesso(Usuario usuario) {

            ValidarLogin();
            ValidarSenha();

            usuario.ValidationResult = Validate(usuario);
        }
    }
}
