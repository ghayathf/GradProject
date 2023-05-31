using System;
using System.Collections.Generic;
using System.Text;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using TheNeqatcomApp.Core.Repository;
using TheNeqatcomApp.Core.Service;

namespace TheNeqatcomApp.Infra.Service
{
    public class LenderStoreService : ILenderStoreService
    {
        private readonly ILenderStoreRepository _lenderStoreRepository;

        public LenderStoreService(ILenderStoreRepository lenderStoreRepository)
        {
            this._lenderStoreRepository = lenderStoreRepository;
        }
        public void createLenderStore(Gplenderstore gplenderstore)
        {
            _lenderStoreRepository.createLenderStore(gplenderstore);
        }

        public void DeleteLenderStore(int id)
        {
            _lenderStoreRepository.DeleteLenderStore(id);
        }

        public List<Gplenderstore> GetAllLenderStore()
        {
          return   _lenderStoreRepository.GetAllLenderStore();
        }

        public List<LenderUser> GetAllLenderUser()
        {
            return _lenderStoreRepository.GetAllLenderUser();
        }

        public List<LoaneesForLendercs> GetAllLoaneesForLendercs(int id)
        {
            return _lenderStoreRepository.GetAllLoaneesForLendercs(id);
        }

        public List<LoanOffer> GetAllLoanOffer(int lenderid, int loaneeid)
        {
            return _lenderStoreRepository.GetAllLoanOffer(lenderid, loaneeid);
        }

        public List<Lengths> GetLenderCounters(int IDD)
        {
            return _lenderStoreRepository.GetLenderCounters(IDD);
        }

        public LenderInfo GetLenderInfo(int id)
        {
            return _lenderStoreRepository.GetLenderInfo(id);
        }

        public List<LenderPayment> GetLenderPayments(int lenderid)
        {
            return _lenderStoreRepository.GetLenderPayments(lenderid);
        }

        public Gplenderstore GetLenderStoreById(int id)
        {
            return _lenderStoreRepository.GetLenderStoreById(id);
        }

        public void giveComplaintForLoanee(Gpcomplaint gpcomplaint)
        {
          _lenderStoreRepository.giveComplaintForLoanee(gpcomplaint);
        }

        public void UpdateLenderStore(Gplenderstore gplenderstore)
        {
            _lenderStoreRepository.UpdateLenderStore(gplenderstore);
        }
    }
}
