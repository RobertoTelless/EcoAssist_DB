using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;

namespace ModelServices.Interfaces.EntitiesServices
{
    public interface IAtendimentoService : IServiceBase<TICKET_ATENDIMENTO>
    {
        List<TICKET_ATENDIMENTO> GetAllItens();
        List<TICKET_ATENDIMENTO> GetByData(DateTime data);
        TICKET_ATENDIMENTO GetItemById(Int32 id);
        List<TICKET_ATENDIMENTO> ExecuteFilter(Int32? categoria, Int32? cliente, Int32? ordem, Int32? status, String numero, String assunto, DateTime? inicio, DateTime? prevista);

        List<CATEGORIA_ATENDIMENTO> GetAllCats();
        List<DEPARTAMENTO> GetAllDeptos();
        List<CLIENTE> GetAllClientes();
        List<ORDEM_SERVICO> GetAllOrdensServico();
        List<USUARIO_SUGESTAO> GetAllUsuarios();
        List<ATENDIMENTO_STATUS> GetAllStatus();

    }
}
