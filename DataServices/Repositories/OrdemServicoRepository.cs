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
    public class OrdemServicoRepository : RepositoryBase<ORDEM_SERVICO>, IOrdemServicoRepository
    {
        public List<ORDEM_SERVICO> GetAllItens()
        {
            IQueryable<ORDEM_SERVICO> query = Db.ORDEM_SERVICO;
            return query.ToList();
        }

        public ORDEM_SERVICO GetItemById(Int32 id)
        {
            IQueryable<ORDEM_SERVICO> query = Db.ORDEM_SERVICO;
            query = query.Where(p => p.ORSE_CD_ID == id);
            query = query.Include(p => p.ORDEM_SERVICO_AGENDAMENTO);
            query = query.Include(p => p.ORDEM_SERVICO_AGENDAMENTO_ANOTACAO);
            query = query.Include(p => p.ORDEM_SERVICO_ANEXO);
            query = query.Include(p => p.ORDEM_SERVICO_ANOTACAO);
            query = query.Include(p => p.ORDEM_SERVICO_HISTORICO);
            query = query.Include(p => p.ORDEM_SERVICO_PRESTADOR);
            query = query.Include(p => p.ORDEM_SERVICO_PRODUTO);
            query = query.Include(p => p.ORDEM_SERVICO_PROPOSTA);
            query = query.Include(p => p.PARCEIRO);
            query = query.Include(p => p.CLIENTE);
            query = query.Include(p => p.STATUS_ORDEM_SERVICO);
            query = query.Include(p => p.TICKET_ATENDIMENTO);
            return query.FirstOrDefault();
        }

        public List<ORDEM_SERVICO> ExecuteFilter(Int32? tipo, Int32? cliente, Int32? status, String numero, DateTime? data, DateTime? agendamento, Int32? prestador)
        {
            List<ORDEM_SERVICO> lista = new List<ORDEM_SERVICO>();
            IQueryable<ORDEM_SERVICO> query = Db.ORDEM_SERVICO;
            if (tipo != null & tipo > 0)
            {
                query = query.Where(p => p.TIOS_CD_ID == tipo);
            }
            if (cliente != null & cliente > 0)
            {
                query = query.Where(p => p.CLIE_CD_ID == cliente);
            }
            if (cliente != null & cliente > 0)
            {
                query = query.Where(p => p.CLIE_CD_ID == cliente);
            }
            if (status != null & status > 0)
            {
                query = query.Where(p => p.STOS_CD_ID == status);
            }
            if (!String.IsNullOrEmpty(numero))
            {
                query = query.Where(p => p.ORSE_NR_NUMERO.Contains(numero));
            }
            if (data != null)
            {
                query = query.Where(p => p.ORSE_DT_CADASTRO == data);
            }
            if (query != null)
            {
                query = query.Where(p => p.ORSE_IN_ATIVO == 1);
                query = query.OrderByDescending(a => a.ORSE_DT_CADASTRO);
                lista = query.ToList<ORDEM_SERVICO>();
            }
            return lista;
        }


    }
}
 