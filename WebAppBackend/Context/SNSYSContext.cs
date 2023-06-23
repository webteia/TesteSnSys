using Microsoft.EntityFrameworkCore;
using System.Text;
using WebAppBackend.Entidades;
using WebAppBackend.Enum;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using WebAppBackend.Mapping;

namespace WebAppBackend.Context
{
    public class SNSYSContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public SNSYSContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SNSYSContext()
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Acesso> Acessos { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=changeme;Host=localhost;Port=5432;Database=myDataBase;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddMapping(new ClienteMapping());

            modelBuilder.Entity<Usuario>()
                .HasMany(b => b.Acessos)
                .WithOne(a => a.Usuario)
                .OnDelete(DeleteBehavior.Cascade);

            List<Usuario> usuarios = new List<Usuario>();

            #region Criando Usuario Adm
            var senhaAdm = "12345";
            byte[] convertSenhaAdm = Encoding.ASCII.GetBytes(senhaAdm);
            string hashedAdm = Convert.ToBase64String(KeyDerivation.Pbkdf2(senhaAdm, convertSenhaAdm, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));

            var usuarioADM = new Usuario { Id = 1, DataCadastro = DateTime.Now, Login = "admin", Password = hashedAdm };
            usuarios.Add(usuarioADM);
            #endregion

            #region Criando Usuario Guess
            var senhaGuess = "123";
            byte[] convertSenhaGuess = Encoding.ASCII.GetBytes(senhaGuess);
            string hashedGuess = Convert.ToBase64String(KeyDerivation.Pbkdf2(senhaGuess, convertSenhaGuess, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));
            var usuarioGuess = new Usuario { Id = 2, DataCadastro = DateTime.Now, Login = "guest", Password = hashedGuess };
            usuarios.Add(usuarioGuess);
            #endregion

            usuarios.ForEach(usu => modelBuilder.Entity<Usuario>().HasData(usu));

            List<Acesso> acessos = new List<Acesso>();
            acessos.Add(new Acesso { Id = 1, DataCadastro = DateTime.Now, Permissao = AcessosCRUD.Leitura, UsuarioId = 2 });
            acessos.Add(new Acesso { Id = 2, DataCadastro = DateTime.Now, Permissao = AcessosCRUD.Input, UsuarioId = 1 });
            acessos.Add(new Acesso { Id = 3, DataCadastro = DateTime.Now, Permissao = AcessosCRUD.Delete, UsuarioId = 1 });
            acessos.Add(new Acesso { Id = 4, DataCadastro = DateTime.Now, Permissao = AcessosCRUD.Atualizacao, UsuarioId = 1 });

            acessos.ForEach(ace => modelBuilder.Entity<Acesso>().HasData(ace));
        }
    }
}
