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
    public class OrdemServicoAppService : AppServiceBase<ORDEM_SERVICO>, IOrdemServicoAppService
    {
        private readonly IOrdemServicoService _baseService;

        public OrdemServicoAppService(IOrdemServicoService baseService) : base(baseService)
        {
            _baseService = baseService;
        }

        public List<ORDEM_SERVICO> GetAllItens()
        {
            return _baseService.GetAllItens();
        }

        public Int32 ExecuteFilter(Int32? tipo, Int32? cliente, Int32? status, String numero, DateTime? data, DateTime agendamento, Int32? prestador, out List<ORDEM_SERVICO> objeto)
        {
            try
            {
                objeto = new List<ORDEM_SERVICO>();
                Int32 volta = 0;

                // Processa filtro
                objeto = _baseService.ExecuteFilter(tipo, cliente, status, numero, data, agendamento, prestador);
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
