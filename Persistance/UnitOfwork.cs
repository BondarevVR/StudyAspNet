using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Persistance
{
    public class UnitOfwork : IUnitOfwork
    {
        private readonly AppDbContext context;

        public UnitOfwork(AppDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
