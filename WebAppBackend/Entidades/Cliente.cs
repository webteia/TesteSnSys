namespace WebAppBackend.Entidades
{
    public class Cliente : BaseEntidade
    {
        public DateTime? DataAtualizacao { get; set; }
        public int? UsuarioInclusaoId { get; set; }
        public int? UsuarioAtualizacaoId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
