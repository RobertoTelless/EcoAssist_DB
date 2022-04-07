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
    public class CPService : ServiceBase<vwContasAPagar>, ICPService
    {
        private readonly ICPRepository _baseRepository;
        protected OnSuiteBIEntities Db = new OnSuiteBIEntities();

        public CPService(ICPRepository baseRepository) : base(baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public List<vwContasAPagar> GetAllItens()
        {
            return _baseRepository.GetAllItens();
        }

        public List<vwContasAPagar> GetByData(DateTime data)
        {
            return _baseRepository.GetByData(data);
        }

        public Int32 Create(vwContasAPagar item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _baseRepository.Add(item);
                    transaction.Commit();
                    return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }


        public Int32 Edit(vwContasAPagar item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    vwContasAPagar obj = _baseRepository.GetById(item.Ordem_de_Servico);
                    _baseRepository.Detach(obj);
                    _baseRepository.Update(item);
                    transaction.Commit();
                    return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public Int32 Delete(vwContasAPagar item)
        {
            using (DbContextTransaction transaction = Db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    _baseRepository.Remove(item);
                    transaction.Commit();
                    return 0;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public List<vwContasAPagar> ExecuteFilter(DateTime? emissaoInicio, DateTime? emissaoFinal, DateTime? vencInicio, DateTime? vencFinal, DateTime? pagInicio, DateTime? pagFinal, String centroCusto, String beneficiario, String libPag, Int32? crit)
        {
            return _baseRepository.ExecuteFilter(emissaoInicio, emissaoFinal, vencInicio, vencFinal, pagInicio, pagFinal, centroCusto, beneficiario, libPag, crit);
        }

    }
}
