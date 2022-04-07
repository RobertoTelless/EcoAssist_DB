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
    public class CPRepository : RepositoryBase<vwContasAPagar>, ICPRepository
    {
        public List<vwContasAPagar> GetAllItens()
        {
            IQueryable<vwContasAPagar> query = Db.vwContasAPagar;
            return query.ToList();
        }

        public List<vwContasAPagar> GetByData(DateTime data)
        {
            IQueryable<vwContasAPagar> query = Db.vwContasAPagar;
            query = query.Where(p => p.Data_de_Vencimento.Month == data.Month & p.Data_de_Vencimento.Year == data.Year);
            return query.ToList();
        }

        public List<vwContasAPagar> ExecuteFilter(DateTime? emissaoInicio, DateTime? emissaoFinal, DateTime? vencInicio, DateTime? vencFinal, DateTime? pagInicio, DateTime? pagFinal, String centroCusto, String beneficario, String libPag, Int32? crit)
        {
            List<vwContasAPagar> lista = new List<vwContasAPagar>();
            IQueryable<vwContasAPagar> query = Db.vwContasAPagar;
            if (!String.IsNullOrEmpty(libPag))
            {
                query = query.Where(p => p.Liberado_para_pagamento_ == libPag);
            }
            if (crit != null & crit > 0)
            {
                query = query.Where(p => p.Criticidade == crit);
            }
            if (!String.IsNullOrEmpty(centroCusto))
            {
                query = query.Where(p => p.Centro_de_Custos == centroCusto);
            }
            if (!String.IsNullOrEmpty(beneficario))
            {
                query = query.Where(p => p.Beneficario == beneficario);
            }
            if (emissaoInicio != null & emissaoFinal != null)
            {
                query = query.Where(p => p.Data_de_Emissao >= emissaoInicio & p.Data_de_Emissao <= emissaoFinal);
            }
            else if (emissaoInicio != null & emissaoFinal == null)
            {
                query = query.Where(p => p.Data_de_Emissao >= emissaoInicio);
            }
            else if (emissaoInicio == null & emissaoFinal != null)
            {
                query = query.Where(p => p.Data_de_Emissao <= emissaoFinal);
            }
            if (vencInicio != null & vencFinal != null)
            {
                query = query.Where(p => p.Data_de_Vencimento >= vencInicio & p.Data_de_Vencimento <= vencFinal);
            }
            else if (vencInicio != null & vencFinal == null)
            {
                query = query.Where(p => p.Data_de_Vencimento >= vencInicio);
            }
            else if (vencInicio == null & vencFinal != null)
            {
                query = query.Where(p => p.Data_de_Vencimento <= vencFinal);
            }
            if (pagInicio != null & pagFinal != null)
            {
                query = query.Where(p => p.Data_de_Pagamento >= pagInicio & p.Data_de_Pagamento <= pagFinal);
            }
            else if (pagInicio != null & pagFinal == null)
            {
                query = query.Where(p => p.Data_de_Pagamento >= pagInicio);
            }
            else if (pagInicio == null & pagFinal != null)
            {
                query = query.Where(p => p.Data_de_Pagamento <= pagFinal);
            }
            if (query != null)
            {
                query = query.OrderByDescending(a => a.Data_de_Vencimento);
                lista = query.ToList<vwContasAPagar>();
            }
            return lista;
        }

    }
}
 