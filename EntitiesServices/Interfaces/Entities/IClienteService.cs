using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;

namespace ModelServices.Interfaces.EntitiesServices
{
    public interface IClienteService : IServiceBase<CLIENTE>
    {
        List<CLIENTE> GetAllItens();
        List<CLIENTE> GetByNome(String nome);
        CLIENTE GetItemById(Int32 id);
        List<CLIENTE> ExecuteFilter(Int32? tipo, Int32? origem, String nome, String razao, Int32? pessoa, String cpf, String cnpj, String cidade, Int32? uf);
        List<TIPO_CLIENTE> GetAllTipos();
        List<ORIGEM_CLIENTE> GetAllOrigens();
    }
}
