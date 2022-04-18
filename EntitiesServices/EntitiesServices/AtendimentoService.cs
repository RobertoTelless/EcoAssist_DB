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
    public class AtendimentoService : ServiceBase<TICKET_ATENDIMENTO>, IAtendimentoService
    {
        private readonly IAtendimentoRepository _baseRepository;
        private readonly ICategoriaAtendimentoRepository _cateRepository;
        private readonly IDepartamentoRepository _deptRepository;
        private readonly IClienteRepository _clieRepository;
        private readonly IAtendimentoStatusRepository _statRepository;
        private readonly IUsuarioRepository _usuaRepository;
        private readonly IOrdemServicoRepository _orseRepository;
        protected DB_EcoBaseEntities Db = new DB_EcoBaseEntities();

        public AtendimentoService(IAtendimentoRepository baseRepository, ICategoriaAtendimentoRepository cateRepository, IDepartamentoRepository deptRepository, IClienteRepository clieRepository, IAtendimentoStatusRepository statRepository, IOrdemServicoRepository orseRepository, IUsuarioRepository usuaRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
            _cateRepository = cateRepository;
            _deptRepository = deptRepository;
            _clieRepository = clieRepository;   
            _statRepository = statRepository;
            _usuaRepository = usuaRepository;
            _orseRepository = orseRepository;
        }

        public List<TICKET_ATENDIMENTO> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public List<TICKET_ATENDIMENTO> GetByData(DateTime data)
        {
            return _baseRepository.GetByData(data);
        }

        public TICKET_ATENDIMENTO GetItemById(Int32 id)
        {
            return _baseRepository.GetItemById(id);
        }

        public List<TICKET_ATENDIMENTO> ExecuteFilter(Int32? categoria, Int32? cliente, Int32? ordem, Int32? status, String numero, String assunto, DateTime? inicio, DateTime? prevista)
        {
            return _baseRepository.ExecuteFilter(categoria, cliente, ordem, status, numero, assunto, inicio, prevista);
        }

        public List<CATEGORIA_ATENDIMENTO> GetAllCats()
        {
            return _cateRepository.GetAllItens();
        }

        public List<DEPARTAMENTO> GetAllDeptos()
        {
            return _deptRepository.GetAllItens();
        }

        public List<CLIENTE> GetAllClientes()
        {
            return _clieRepository.GetAllItens();
        }

        public List<ATENDIMENTO_STATUS> GetAllStatus()
        {
            return _statRepository.GetAllItens();
        }

        public List<USUARIO_SUGESTAO> GetAllUsuarios()
        {
            return _usuaRepository.GetAllItens();
        }

        public List<ORDEM_SERVICO> GetAllOrdensServico()
        {
            return _orseRepository.GetAllItens();
        }
    }
}
