using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using TheNeqatcomApp.Core.Data;

namespace TheNeqatcomApp.Core.Service
{
   public interface ILoaneeService
    {  
        void CreateLoanee(Gploanee loanee);
        void UpdateLoanee(Gploanee loanee);
        void DeleteLoanee(int IDD);
        Gploanee GetLoaneeByID(int IDD);
        List<Gploanee> GetAllLoanees();
        List<LoaneeUser> GetAllLoaneeUser();     
        List<CurrentAndFinishedLoans> GetCurrentAndFinishedLoans(int LID);
        void giveComplaintForLender(Gpcomplaint gpcomplaint);
        List<ConfirmLoans> GetLoansToConfirm(int loaneeidd);
        List<Gpnationalnumber> GetAllGpnationalnumber();
        void GiveRateForLender(int IDD, int feedbak);

    }
}
