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
    public class ParceiroRepository : RepositoryBase<PARCEIRO>, IParceiroRepository
    {
        public List<PARCEIRO> GetAllItens()
        {
            IQueryable<PARCEIRO> query = Db.PARCEIRO;
            query = query.Where(p => p.PARC_IN_ATIVO == 1);
            return query.ToList();
        }
    }
}
 