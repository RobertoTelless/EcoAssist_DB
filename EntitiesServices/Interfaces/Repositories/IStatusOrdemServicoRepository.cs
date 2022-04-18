using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IStatusOrdemServicoRepository : IRepositoryBase<STATUS_ORDEM_SERVICO>
    {
        List<STATUS_ORDEM_SERVICO> GetAllItens();
}
}
