using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IOrdemServicoRepository : IRepositoryBase<ORDEM_SERVICO>
    {
        List<ORDEM_SERVICO> GetAllItens();
        ORDEM_SERVICO GetItemById(Int32 id);
        List<ORDEM_SERVICO> ExecuteFilter(Int32? tipo, Int32? cliente, Int32? status, String numero, DateTime? data, DateTime? agendamento, Int32? prestador);
    }
}
