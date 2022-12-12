using Microsoft.EntityFrameworkCore;
using WebAppKey.Models;
using WebAppKey;

namespace WebAppKey.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }

        public DbSet<Unidade> Unidade { get; set;}
        public DbSet<Produto> Produto { get; set;}
        public DbSet<TipoProduto> TipoProduto { get; set; }
        public DbSet<Movimento> Movimento { get; set; }
        public DbSet<MovimentoItem> MovimentoItem { get; set; }
        public DbSet<TipoMovimento> TipoMovimento { get; set; }
        public DbSet<Lavoura> Lavoura { get; set; }
        public DbSet<MovimentoLavoura> MovimentoLavoura { get; set; }

    }
}
