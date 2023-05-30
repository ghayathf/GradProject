using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using TheNeqatcomApp.Core.Data;

namespace TheNeqatcomApp.Core.Repository
{
   public interface ILoaneeComplaintsRepository
    {
        void ManageComplaints(int LID,int CID);
        List<LoaneeComplaintsDTO> GetAllCompliants();
        void CheckFiveDays();

    }
}
