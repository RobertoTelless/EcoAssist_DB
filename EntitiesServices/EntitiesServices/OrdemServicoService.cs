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
        protected DB_EcoBaseEntities Db = new DB_EcoBaseEntities();

        public OrdemServicoService(IOrdemServicoRepository baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public List<ORDEM_SERVICO> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public List<ORDEM_SERVICO> ExecuteFilter(Int32? tipo, Int32? cliente, Int32? status, String numero, DateTime? data, DateTime agendamento, Int32? prestador)
        {
            return _baseRepository.ExecuteFilter(tipo, cliente, status, numero, data, agendamento, prestador);
        }


    }
}
