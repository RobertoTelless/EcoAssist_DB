using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;
using ModelServices.Interfaces.Repositories;
using EntitiesServices.Work_Classes;
using System.Data.Entity;
using EntitiesServices.DTO;

namespace DataServices.Repositories
{
    public class OrdemServicoRepository : RepositoryBase<OrdemServico>, IOrdemServicoRepository
    {
        public List<OrdemServico> GetAllItens()
        {
            IQueryable<OrdemServico> query = Db.OrdemServico;
            query = query.Where(p => p.DataExecucaoFim == null & p.DataExecucaoInicio != null);
            return query.ToList();
        }

        public List<OrdemServico> GetOSAtraso(DateTime hoje)
        {
            IQueryable<OrdemServico> query = Db.OrdemServico;
            query = query.Where(p => p.DataExecucaoFimPrevisao < hoje & p.DataExecucaoFim == null & p.DataExecucaoInicio != null);
            return query.ToList();
        }

        public List<OrdemServico> GetAllItensIniciadas()
        {
            IQueryable<OrdemServico> query = Db.OrdemServico;
            query = query.Where(p => p.DataExecucaoInicio != null);
            return query.ToList();
        }

        public List<OrdemServico> GetOSPendencias()
        {
            IQueryable<OrdemServico> query = Db.OrdemServico;
            query = query.Where(p => p.DataExecucaoInicio != null & p.ComPendencia);
            return query.ToList();
        }

        public List<OrdemServico> GetOSPesquisa()
        {
            IQueryable<OrdemServico> query = Db.OrdemServico;
            query = query.Where(p => p.DataExecucaoInicio != null & p.OrdemServicoPesquisa.Count > 0);
            return query.ToList();
        }
        
        public List<OrdemServico> GetOSAvaliacao()
        {
            IQueryable<OrdemServico> query = Db.OrdemServico;
            query = query.Where(p => p.DataExecucaoInicio != null & p.OrdemServicoAvaliacao.Count > 0);
            
            
            
            
            
            return query.ToList();
        }


    }
}
 