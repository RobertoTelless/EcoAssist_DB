using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;
using ApplicationServices.Interfaces;
using ModelServices.Interfaces.EntitiesServices;
using CrossCutting;
using System.Text.RegularExpressions;

namespace ApplicationServices.Services
{
    public class CPAppService : AppServiceBase<vwContasAPagar>, ICPAppService
    {
        private readonly ICPService _baseService;

        public CPAppService(ICPService baseService) : base(baseService)
        {
            _baseService = baseService;
        }

        public List<vwContasAPagar> GetAllItens()
        {
            return _baseService.GetAllItens();
        }

        public List<vwContasAPagar> GetByData(DateTime data)
        {
            return _baseService.GetByData(data);
        }

        public Int32 ExecuteFilter(DateTime? emissaoInicio, DateTime? emissaoFinal, DateTime? vencInicio, DateTime? vencFinal, DateTime? pagInicio, DateTime? pagFinal, String centroCusto, String beneficiario, String libPag, Int32? crit, out List<vwContasAPagar > objeto)
        {
            try
            {
                objeto = new List<vwContasAPagar>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _baseService.ExecuteFilter(emissaoInicio, emissaoFinal, vencInicio, vencFinal, pagInicio, pagFinal, centroCusto, beneficiario, libPag, crit);
                if (objeto.Count == 0)
                {
                    volta = 1;
                }
                return volta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
