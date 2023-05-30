using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using TheNeqatcomApp.Core.Data;

namespace TheNeqatcomApp.Core.Service
{
   public interface ILoanService
    {
        void CreateLoan(Gploan loan);
        void UpdateLoan(Gploan loan);
        void DeleteLoan(int IDD);
        Gploan GetLoanByID(int IDD);
        List<Gploan> GetAllLoans();
        List<RequestedLoan> GetAllRequestedLoan(int LSID, int statuss);
        void UpdateLoanStatus(int LoanID, int status);
        void ConfirmNewLoanInfo(Gploan loan);
        int ExistingLoanCounter(int LoaneeID);

        List<RequestedLoan> GetAllRequestedPostPone(int LSID, int statuss);
        void UpdatePostponeStatus(int LoanID, int status,int loaneeidd);
        void RequestNewLoan(int loaneeid, int offerid, int totalmonths);
    }
}
