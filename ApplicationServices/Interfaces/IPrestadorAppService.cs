using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ApplicationServices.Interfaces
{
    public interface IPrestadorAppService : IAppServiceBase<PRESTADOR>
    {
        List<PRESTADOR> GetAllItens();
        List<PRESTADOR> GetByNome(String nome);
        Int32 ExecuteFilter(String nome, String razao, String cnpj, String cidade, Int32? uf, out List<PRESTADOR> objeto);
    }
}
