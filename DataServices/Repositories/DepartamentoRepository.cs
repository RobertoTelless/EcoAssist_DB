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
    public class DepartamentoRepository : RepositoryBase<DEPARTAMENTO>, IDepartamentoRepository
    {
        public List<DEPARTAMENTO> GetAllItens()
        {
            IQueryable<DEPARTAMENTO> query = Db.DEPARTAMENTO;
            query = query.Where(p => p.DEPT_IN_ATIVO == 1);
            return query.ToList();
        }
    }
}
 