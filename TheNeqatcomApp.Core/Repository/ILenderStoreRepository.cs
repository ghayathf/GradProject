using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using TheNeqatcomApp.Core.Data;

namespace TheNeqatcomApp.Core.Repository
{
    public interface ILenderStoreRepository
    {
        List<Gplenderstore> GetAllLenderStore();
        List<LenderUser> GetAllLenderUser();
        List<LoaneesForLendercs> GetAllLoaneesForLendercs(int id);
        Gplenderstore GetLenderStoreById(int id);
        void createLenderStore(Gplenderstore gplenderstore);
        void UpdateLenderStore(Gplenderstore gplenderstore);
        void DeleteLenderStore(int id);
        List<LoanOffer> GetAllLoanOffer(int lenderid,int loaneeid);
        LenderInfo GetLenderInfo(int id);
        void giveComplaintForLoanee(Gpcomplaint gpcomplaint);
        List<LenderPayment> GetLenderPayments(int lenderid);
        List<Lengths> GetLenderCounters(int IDD);


    }
}
