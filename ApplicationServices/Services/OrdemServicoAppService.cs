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
using EntitiesServices.DTO;

namespace ApplicationServices.Services
{
    public class OrdemServicoAppService : AppServiceBase<OrdemServico>, IOrdemServicoAppService
    {
        private readonly IOrdemServicoService _baseService;

        public OrdemServicoAppService(IOrdemServicoService baseService) : base(baseService)
        {
            _baseService = baseService;
        }

        public List<OrdemServico> GetAllItens()
        {
            return _baseService.GetAllItens();
        }

        public List<OrdemServico> GetOSAtraso(DateTime hoje)
        {
            return _baseService.GetOSAtraso(hoje);
        }

        public List<OrdemServico> GetAllItensIniciadas()
        {
            return _baseService.GetAllItensIniciadas();
        }

        public List<OrdemServico> GetOSPendencias()
        {
            return _baseService.GetOSPendencias();
        }

        public List<OrdemServico> GetOSPesquisa()
        {
            return _baseService.GetOSPesquisa();
        }

        public List<OrdemServico> GetOSAvaliacao()
        {
            return _baseService.GetOSAvaliacao();
        }
    }
}
