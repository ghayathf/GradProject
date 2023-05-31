using System;
using System.Collections.Generic;
using System.Text;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using TheNeqatcomApp.Core.Repository;
using TheNeqatcomApp.Core.Service;

namespace TheNeqatcomApp.Infra.Service
{
  public  class LoanService:ILoanService
    {
        private readonly ILoanRepository loanRepository;
        public LoanService(ILoanRepository loanRepository)
        {
            this.loanRepository = loanRepository;
        }

        public void ConfirmNewLoanInfo(Gploan loan)
        {
            loanRepository.ConfirmNewLoanInfo(loan);
        }

        public void CreateLoan(Gploan loan)
        {
            loanRepository.CreateLoan(loan);
        }

        public void DeleteLoan(int IDD)
        {
            loanRepository.DeleteLoan(IDD);
        }

        public int ExistingLoanCounter(int LoaneeID)
        {
            return loanRepository.ExistingLoanCounter(LoaneeID);
        }

        public List<Gploan> GetAllLoans()
        {
            return loanRepository.GetAllLoans();
        }

        public List<RequestedLoan> GetAllRequestedLoan(int LSID, int statuss)
        {
            return loanRepository.GetAllRequestedLoan(LSID, statuss);
        }

        public List<RequestedLoan> GetAllRequestedPostPone(int LSID, int statuss)
        {
            return loanRepository.GetAllRequestedPostPone(LSID, statuss);
        }

        public Gploan GetLoanByID(int IDD)
        {
            return loanRepository.GetLoanByID(IDD);
        }

        public void RequestNewLoan(int loaneeid, int offerid, int totalmonths)
        {
            loanRepository.RequestNewLoan(loaneeid, offerid, totalmonths);
        }

        public void UpdateLoan(Gploan loan)
        {
            loanRepository.UpdateLoan(loan);
        }

        public void UpdateLoanStatus(int LoanID, int status)
        {
            loanRepository.UpdateLoanStatus(LoanID, status);
        }

        public void UpdatePostponeStatus(int LoanID, int status, int loaneeidd)
        {
            loanRepository.UpdatePostponeStatus(LoanID, status, loaneeidd);
        }
    }
}
