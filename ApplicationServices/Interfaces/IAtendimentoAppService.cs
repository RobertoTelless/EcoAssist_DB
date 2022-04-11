using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface IAtendimentoAppService : IAppServiceBase<TICKET_ATENDIMENTO>
    {
        List<TICKET_ATENDIMENTO> GetAllItens();
        List<TICKET_ATENDIMENTO> GetByData(DateTime data);
        Int32 ExecuteFilter(Int32? categoria, Int32? cliente, Int32? ordem, Int32? status, String numero, String assunto, DateTime? inicio, DateTime? prevista, out List<TICKET_ATENDIMENTO> objeto);
    }
}
