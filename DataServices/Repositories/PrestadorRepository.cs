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
    public class PrestadorRepository : RepositoryBase<PRESTADOR>, IPrestadorRepository
    {
       
        public List<PRESTADOR> GetAllItens()
        {
            IQueryable<PRESTADOR> query = Db.PRESTADOR;
            return query.ToList();
        }

        public List<PRESTADOR> GetByNome(String nome)
        {
            IQueryable<PRESTADOR> query = Db.PRESTADOR;
            query = query.Where(p => p.PRES_NM_NOME.Contains(nome));
            return query.ToList();
        }

        public List<PRESTADOR> ExecuteFilter(String nome, String razao, String cnpj, String cidade, Int32? uf)
        {
            List<PRESTADOR> lista = new List<PRESTADOR>();
            IQueryable<PRESTADOR> query = Db.PRESTADOR;
            if (!String.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.PRES_NM_NOME.Contains(nome));
            }
            if (!String.IsNullOrEmpty(razao))
            {
                query = query.Where(p => p.PRES_NM_RAZAO_SOCIAL.Contains(razao));
            }
            if (!String.IsNullOrEmpty(cnpj))
            {
                query = query.Where(p => p.PRES_NR_CNPJ.Contains(cnpj));
            }
            if (query != null)
            {
                query = query.OrderByDescending(a => a.PRES_NM_NOME);
                lista = query.ToList<PRESTADOR>();
            }
            return lista;
        }
    }
}
 