using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;

namespace ModelServices.Interfaces.EntitiesServices
{
    public interface IOrdemServicoService : IServiceBase<ORDEM_SERVICO>
    {
        List<ORDEM_SERVICO> GetAllItens();
        ORDEM_SERVICO GetItemById(Int32 id);
        List<ORDEM_SERVICO> ExecuteFilter(Int32? tipo, Int32? cliente, Int32? status, String numero, DateTime? data, DateTime? agendamento, Int32? prestador);

        List<TIPO_ORDEM_SERVICO> GetAllTipos();
        List<PARCEIRO> GetAllParceiros();
        List<CLIENTE> GetAllClientes();
        List<STATUS_ORDEM_SERVICO> GetAllStatus();
        List<USUARIO_SUGESTAO> GetAllUsuarios();
    }
}
