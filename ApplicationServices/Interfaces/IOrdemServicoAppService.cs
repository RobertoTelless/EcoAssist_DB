using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.DTO;

namespace ApplicationServices.Interfaces
{
    public interface IOrdemServicoAppService : IAppServiceBase<ORDEM_SERVICO>
    {
        List<ORDEM_SERVICO> GetAllItens();
        Int32 ExecuteFilter(Int32? tipo, Int32? cliente, Int32? status, String numero, DateTime? data, DateTime agendamento, Int32? prestador, out List<ORDEM_SERVICO> objeto);

    }
}
