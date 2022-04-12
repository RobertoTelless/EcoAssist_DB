using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using ModelServices.Interfaces.Repositories;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class OrigemClienteRepository : RepositoryBase<ORIGEM_CLIENTE>, IOrigemClienteRepository
    {
        public List<ORIGEM_CLIENTE> GetAllItens()
        {
            IQueryable<ORIGEM_CLIENTE> query = Db.ORIGEM_CLIENTE;
            query = query.Where(p => p.ORCL_IN_ATIVO == 1);
            return query.ToList();
        }
    }
}
 