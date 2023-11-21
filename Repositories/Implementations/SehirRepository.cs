using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class SehirRepository : RepositoryBase<Sehirler>, ISehirRepository
    {
        public SehirRepository(DataContext context) : base(context)
        {
        }
    }
}
