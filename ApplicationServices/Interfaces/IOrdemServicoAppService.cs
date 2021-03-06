using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface IOrdemServicoAppService : IAppServiceBase<ORDEM_SERVICO>
    {
        List<ORDEM_SERVICO> GetAllItens();
        ORDEM_SERVICO GetItemById(Int32 id);
        Int32 ExecuteFilter(Int32? tipo, Int32? cliente, Int32? status, String numero, DateTime? data, DateTime? agendamento, Int32? prestador, out List<ORDEM_SERVICO> objeto);

        List<TIPO_ORDEM_SERVICO> GetAllTipos();
        List<PARCEIRO> GetAllParceiros();
        List<CLIENTE> GetAllClientes();
        List<STATUS_ORDEM_SERVICO> GetAllStatus();
        List<USUARIO_SUGESTAO> GetAllUsuarios();
    }
}
