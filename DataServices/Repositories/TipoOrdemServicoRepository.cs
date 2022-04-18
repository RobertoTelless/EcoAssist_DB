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
    public class TipoOrdemServicoRepository : RepositoryBase<TIPO_ORDEM_SERVICO>, ITipoOrdemServicoRepository
    {
        public List<TIPO_ORDEM_SERVICO> GetAllItens()
        {
            IQueryable<TIPO_ORDEM_SERVICO> query = Db.TIPO_ORDEM_SERVICO;
            query = query.Where(p => p.TIOS_IN_ATIVO == 1);
            return query.ToList();
        }
    }
}
 