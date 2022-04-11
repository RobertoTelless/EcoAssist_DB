using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using ModelServices.Interfaces.Repositories;
using EntitiesServices.Work_Classes;
using System.Data.Entity;

namespace DataServices.Repositories
{
    public class AtendimentoRepository : RepositoryBase<TICKET_ATENDIMENTO>, IAtendimentoRepository
    {
        private ClienteEnderecoRepository endRep;
        
        public List<TICKET_ATENDIMENTO> GetAllItens()
        {
            IQueryable<TICKET_ATENDIMENTO> query = Db.TICKET_ATENDIMENTO;
            return query.ToList();
        }

        public List<TICKET_ATENDIMENTO> GetByData(DateTime data)
        {
            IQueryable<TICKET_ATENDIMENTO> query = Db.TICKET_ATENDIMENTO;
            query = query.Where(p => p.ATEN_DT_INICIO == data);
            return query.ToList();
        }

        public List<TICKET_ATENDIMENTO> ExecuteFilter(Int32? categoria, Int32? cliente, Int32? ordem, Int32? status, String numero, String assunto, DateTime? inicio, DateTime? prevista)
        {
            List<TICKET_ATENDIMENTO> lista = new List<TICKET_ATENDIMENTO>();
            IQueryable<TICKET_ATENDIMENTO> query = Db.TICKET_ATENDIMENTO;
            if (categoria != null & categoria > 0)
            {
                query = query.Where(p => p.CAAT_CD_ID == categoria);
            }
            if (cliente != null & cliente > 0)
            {
                query = query.Where(p => p.CLIE_CD_ID == cliente);
            }
            if (ordem != null & ordem > 0)
            {
                query = query.Where(p => p.ORSE_CD_ID == ordem);
            }
            if (ordem != null & ordem > 0)
            {
                query = query.Where(p => p.ORSE_CD_ID == ordem);
            }
            if (status != null & status > 0)
            {
                query = query.Where(p => p.ATST_CD_ID == status);
            }
            if (!String.IsNullOrEmpty(numero))
            {
                query = query.Where(p => p.ATEN_NR_NUMERO.Contains(numero));
            }
            if (!String.IsNullOrEmpty(assunto))
            {
                query = query.Where(p => p.ATEN_NM_ASSUNTO.Contains(assunto));
            }
            if (inicio != null)
            {
                query = query.Where(p => p.ATEN_DT_INICIO == inicio);
            }
            if (prevista != null)
            {
                query = query.Where(p => p.ATEN_DT_PREVISTA == prevista);
            }
            if (query != null)
            {
                query = query.OrderByDescending(a => a.ATEN_DT_INICIO);
                lista = query.ToList<TICKET_ATENDIMENTO>();
            }
            return lista;
        }
    }
}
 