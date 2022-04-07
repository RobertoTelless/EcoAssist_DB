using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using EntitiesServices.Model;
using EntitiesServices.DTO;
using EntitiesServices.Work_Classes;
using ModelServices.Interfaces.Repositories;
using ModelServices.Interfaces.EntitiesServices;
using CrossCutting;
using System.Data.Entity;
using System.Data;

namespace ModelServices.EntitiesServices
{
    public class OrdemServicoService : ServiceBase<OrdemServico>, IOrdemServicoService
    {
        private readonly IOrdemServicoRepository _baseRepository;
        protected OnSuiteBIEntities Db = new OnSuiteBIEntities();

        public OrdemServicoService(IOrdemServicoRepository baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public List<OrdemServico> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public List<OrdemServico> GetOSAtraso(DateTime hoje)
        {
            return _baseRepository.GetOSAtraso(hoje);
        }

        public List<OrdemServico> GetAllItensIniciadas()
        {
            return _baseRepository.GetAllItensIniciadas();
        }

        public List<OrdemServico> GetOSPendencias()
        {
            return _baseRepository.GetOSPendencias();
        }

        public List<OrdemServico> GetOSPesquisa()
        {
            return _baseRepository.GetOSPesquisa();
        }

        public List<OrdemServico> GetOSAvaliacao()
        {
            return _baseRepository.GetOSAvaliacao();
        }
    }
}
