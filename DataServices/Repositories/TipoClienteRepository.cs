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
    public class TipoClienteRepository : RepositoryBase<TIPO_CLIENTE>, ITipoClienteRepository
    {
        public List<TIPO_CLIENTE> GetAllItens()
        {
            IQueryable<TIPO_CLIENTE> query = Db.TIPO_CLIENTE;
            query = query.Where(p => p.TICL_IN_ATIVO == 1);
            return query.ToList();
        }
    }
}
 