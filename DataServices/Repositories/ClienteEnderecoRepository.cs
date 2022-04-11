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
    public class ClienteEnderecoRepository : RepositoryBase<CLIENTE_ENDERECO>, IClienteEnderecoRepository
    {
        public List<CLIENTE_ENDERECO> GetAllItens()
        {
            IQueryable<CLIENTE_ENDERECO> query = Db.CLIENTE_ENDERECO;
            return query.ToList();
        }
    }
}
 