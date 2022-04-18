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
    public class StatusOrdemServicoRepository : RepositoryBase<STATUS_ORDEM_SERVICO>, IStatusOrdemServicoRepository
    {
        public List<STATUS_ORDEM_SERVICO> GetAllItens()
        {
            IQueryable<STATUS_ORDEM_SERVICO> query = Db.STATUS_ORDEM_SERVICO;
            query = query.Where(p => p.STOS_IN_ATIVO == 1);
            return query.ToList();
        }
    }
}
 