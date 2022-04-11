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
        protected DB_EcoBaseEntities Db = new DB_EcoBaseEntities();

        public AtendimentoService(IAtendimentoRepository baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public List<TICKET_ATENDIMENTO> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public List<TICKET_ATENDIMENTO> GetByData(DateTime data)
        {
            return _baseRepository.GetByData(data);
        }
        public List<TICKET_ATENDIMENTO> ExecuteFilter(Int32? categoria, Int32? cliente, Int32? ordem, Int32? status, String numero, String assunto, DateTime? inicio, DateTime? prevista)
        {
            return _baseRepository.ExecuteFilter(categoria, cliente, ordem, status, numero, assunto, inicio, prevista);
        }
    }
}
