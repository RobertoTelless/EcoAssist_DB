using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IOrigemClienteRepository : IRepositoryBase<ORIGEM_CLIENTE>
    {
        List<ORIGEM_CLIENTE> GetAllItens();
}
}
