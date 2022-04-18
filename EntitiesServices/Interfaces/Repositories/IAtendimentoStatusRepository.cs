using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IAtendimentoStatusRepository : IRepositoryBase<ATENDIMENTO_STATUS>
    {
        List<ATENDIMENTO_STATUS> GetAllItens();
}
}
