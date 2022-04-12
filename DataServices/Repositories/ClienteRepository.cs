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
    public class ClienteRepository : RepositoryBase<CLIENTE>, IClienteRepository
    {
        private ClienteEnderecoRepository endRep;
        
        public List<CLIENTE> GetAllItens()
        {
            IQueryable<CLIENTE> query = Db.CLIENTE;
            //query = query.Where(p => p.CLIE_IN_ATIVO == 1);
            return query.ToList();
        }

        public List<CLIENTE> GetByNome(String nome)
        {
            IQueryable<CLIENTE> query = Db.CLIENTE;
            query = query.Where(p => p.CLIE_NM_NOME.Contains(nome));
            return query.ToList();
        }

        public List<CLIENTE> ExecuteFilter(Int32? tipo, Int32? origem, String nome, String razao, Int32? pessoa, String cpf, String cnpj, String cidade, Int32? uf)
        {
            List<CLIENTE> lista = new List<CLIENTE>();
            IQueryable<CLIENTE> query = Db.CLIENTE;
            if (tipo != null & tipo > 0)
            {
                query = query.Where(p => p.TICL_CD_ID == tipo);
            }
            if (origem != null & origem > 0)
            {
                query = query.Where(p => p.ORCL_CD_ID == origem);
            }
            if (!String.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.CLIE_NM_NOME.Contains(nome));
            }
            if (!String.IsNullOrEmpty(razao))
            {
                query = query.Where(p => p.CLIE_NM_RAZAO_SOCIAL.Contains(razao));
            }
            if (pessoa != null & pessoa > 0)
            {
                query = query.Where(p => p.CLIE_IN_TIPO_PESSOA == pessoa);
            }
            if (!String.IsNullOrEmpty(cpf))
            {
                query = query.Where(p => p.CLIE_NR_CPF.Contains(cpf));
            }
            if (!String.IsNullOrEmpty(cnpj))
            {
                query = query.Where(p => p.CLIE_NR_CNPJ.Contains(cnpj));
            }
            if (query != null)
            {
                query = query.Where(p => p.CLIE_IN_ATIVO == 1);
                query = query.OrderByDescending(a => a.CLIE_NM_NOME);
                lista = query.ToList<CLIENTE>();
            }
            return lista;
        }
    }
}
 