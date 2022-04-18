using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;

namespace ModelServices.Interfaces.EntitiesServices
{
    public interface IPrestadorService : IServiceBase<PRESTADOR>
    {
        List<PRESTADOR> GetAllItens();
        List<PRESTADOR> GetByNome(String nome);
        PRESTADOR GetItemById(Int32 id);
        List<PRESTADOR> ExecuteFilter(String nome, String razao, String cnpj, String cidade, Int32? uf);
    }
}
