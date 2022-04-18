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
    public class CategoriaAtendimentoRepository : RepositoryBase<CATEGORIA_ATENDIMENTO>, ICategoriaAtendimentoRepository
    {
        public List<CATEGORIA_ATENDIMENTO> GetAllItens()
        {
            IQueryable<CATEGORIA_ATENDIMENTO> query = Db.CATEGORIA_ATENDIMENTO;
            query = query.Where(p => p.CAAT_IN_ATIVO == 1);
            return query.ToList();
        }
    }
}
 