using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface IClienteAppService : IAppServiceBase<CLIENTE>
    {
        List<CLIENTE> GetAllItens();
        List<CLIENTE> GetByNome(String nome);
        Int32 ExecuteFilter(Int32? tipo, Int32? origem, String nome, String razao, Int32? pessoa, String cpf, String cnpj, String cidade, Int32? uf, out List<CLIENTE> objeto);
        List<TIPO_CLIENTE> GetAllTipos();
        List<ORIGEM_CLIENTE> GetAllOrigens();
    }
}
