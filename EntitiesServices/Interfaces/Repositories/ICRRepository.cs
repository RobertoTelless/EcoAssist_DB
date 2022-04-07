using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace ModelServices.Interfaces.Repositories
{
    public interface ICRRepository : IRepositoryBase<vwContasAReceber>
    {
        List<vwContasAReceber> GetAllItens();
        List<vwContasAReceber> GetByData(DateTime data);
        List<vwContasAReceber> ExecuteFilter(DateTime? emissaoInicio, DateTime? emissaoFinal, DateTime? vencInicio, DateTime? vencFinal, DateTime? recInicio, DateTime? recFinal, String centroLucro, String sacado, Int32? prob);
}
}
