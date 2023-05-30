using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.Repository;
using TheNeqatcomApp.Core.Service;

namespace TheNeqatcomApp.Infra.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        public List<ActorCounterDTO> ActorCounter()
        {
            throw new NotImplementedException();
        }

        public AdminStatisticsLoanee AdminStatisticsLoanee()
        {
            throw new NotImplementedException();
        }

        public List<CancleLoanAuto> CancleLoanAutomatically()
        {
            throw new NotImplementedException();
        }

        public List<CancleLoanMsgforLender> CancleLoanAutoMsgForLender()
        {
            throw new NotImplementedException();
        }

        public CategoriesStatistics categoriesStatistics()
        {
            throw new NotImplementedException();
        }

        public ComplaintsStatistics complaintsStatistics()
        {
            throw new NotImplementedException();
        }

        public void deleteComplaint(int cid)
        {
            throw new NotImplementedException();
        }

        public List<Gpcommercialregister> GetGpcommercialregisters()
        {
            return adminRepository.GetGpcommercialregisters();
        }

        public List<LenderComplaints> GetLenderStoresComplaints()
        {
            throw new NotImplementedException();
        }

        public void HandleRegistarction(int IDD)
        {
            throw new NotImplementedException();
        }

        public LenderAdminStatistics lenderAdminStatistics()
        {
            throw new NotImplementedException();
        }

        public List<LoaneeCreditScores> loaneeCreditScores()
        {
            throw new NotImplementedException();
        }

        public void ManageLenderComplaints(int loaid, int CID)
        {
            throw new NotImplementedException();
        }
    }
}
