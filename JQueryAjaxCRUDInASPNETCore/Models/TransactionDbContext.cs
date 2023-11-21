using Microsoft.EntityFrameworkCore;

namespace JQueryAjaxCRUDInASPNETCore.Models
{
    public class TransactionDbContext:DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options):base(options)
        {
            
        }
        public  DbSet<TransactionModal> Transactions { get; set; }
    }
}
