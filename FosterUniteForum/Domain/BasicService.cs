using System;
using FosterUniteForum.Data.EntityModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FosterUniteForum.Domain
{
    public class BasicService : IDisposable
    {
        protected ForumDbContext context;
        public BasicService()
        {
            context = new ForumDbContext();
        }
        public BasicService(ForumDbContext context)
        {
            this.context = context;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
