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
    public class AtendimentoStatusRepository : RepositoryBase<ATENDIMENTO_STATUS>, IAtendimentoStatusRepository
    {
        public List<ATENDIMENTO_STATUS> GetAllItens()
        {
            IQueryable<ATENDIMENTO_STATUS> query = Db.ATENDIMENTO_STATUS;
            query = query.Where(p => p.ATST_IN_ATIVO == 1);
            return query.ToList();
        }
    }
}
 