using Entities.Models;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class BolgelerRepository : RepositoryBase<Bolgeler>, IBolgelerRepository
    {
        public BolgelerRepository(DataContext context) : base(context)
        {
        }
    }
}
