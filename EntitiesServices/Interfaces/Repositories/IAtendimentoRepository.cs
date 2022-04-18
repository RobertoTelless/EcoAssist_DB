using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface IAtendimentoRepository : IRepositoryBase<TICKET_ATENDIMENTO>
    {
        List<TICKET_ATENDIMENTO> GetAllItens();
        List<TICKET_ATENDIMENTO> GetByData(DateTime data);
        TICKET_ATENDIMENTO GetItemById(Int32 id);
        List<TICKET_ATENDIMENTO> ExecuteFilter(Int32? categoria, Int32? cliente, Int32? ordem, Int32? status, String numero, String assunto, DateTime? inicio, DateTime? prevista);
    }
}
