using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using EntitiesServices.Model;
using EntitiesServices.Work_Classes;
using ModelServices.Interfaces.Repositories;
using ModelServices.Interfaces.EntitiesServices;
using CrossCutting;
using System.Data.Entity;
using System.Data;

namespace ModelServices.EntitiesServices
{
    public class OrdemServicoService : ServiceBase<ORDEM_SERVICO>, IOrdemServicoService
    {
        private readonly IOrdemServicoRepository _baseRepository;
        private readonly ITipoOrdemServicoRepository _tipoRepository;
        private readonly IParceiroRepository _parcRepository;
        private readonly IClienteRepository _clieRepository;
        private readonly IStatusOrdemServicoRepository _statRepository;
        private readonly IUsuarioRepository _usuaRepository;
        protected DB_EcoBaseEntities Db = new DB_EcoBaseEntities();

        public OrdemServicoService(IOrdemServicoRepository baseRepository, ITipoOrdemServicoRepository tipoRepository, IParceiroRepository parcRepository, IClienteRepository clieRepository, IStatusOrdemServicoRepository statRepository, IUsuarioRepository usuaRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _tipoRepository = tipoRepository;
            _parcRepository = parcRepository;
            _clieRepository = clieRepository;
            _statRepository = statRepository;
            _usuaRepository = usuaRepository;
        }

        public List<ORDEM_SERVICO> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public ORDEM_SERVICO GetItemById(Int32 id)
        {
            return _baseRepository.GetItemById(id);
        }

        public List<ORDEM_SERVICO> ExecuteFilter(Int32? tipo, Int32? cliente, Int32? status, String numero, DateTime? data, DateTime? agendamento, Int32? prestador)
        {
            return _baseRepository.ExecuteFilter(tipo, cliente, status, numero, data, agendamento, prestador);
        }

        public List<TIPO_ORDEM_SERVICO> GetAllTipos()
        {
            return _tipoRepository.GetAllItens();
        }

        public List<PARCEIRO> GetAllParceiros()
        {
            return _parcRepository.GetAllItens();
        }

        public List<CLIENTE> GetAllClientes()
        {
            return _clieRepository.GetAllItens();
        }

        public List<STATUS_ORDEM_SERVICO> GetAllStatus()
        {
            return _statRepository.GetAllItens();
        }

        public List<USUARIO_SUGESTAO> GetAllUsuarios()
        {
            return _usuaRepository.GetAllItens();
        }

    }
}
