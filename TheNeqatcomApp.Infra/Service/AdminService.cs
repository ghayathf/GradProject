
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using TheNeqatcomApp.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;
using TheNeqatcomApp.Core.Repository;

namespace Neqatcom.Infra.Service
{
   public class AdminService:IAdminService
    {
        private readonly IAdminRepository adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }
        public void deleteComplaint(int cid)
        {
            adminRepository.deleteComplaint(cid);
        }
        public CategoriesStatistics categoriesStatistics()
        {
            return adminRepository.categoriesStatistics();
        }
        public ComplaintsStatistics complaintsStatistics()
        {
            return adminRepository.complaintsStatistics();
        }
        public AdminStatisticsLoanee AdminStatisticsLoanee()
        {
            return adminRepository.AdminStatisticsLoanee();
        }
        public LenderAdminStatistics lenderAdminStatistics()
        {
            return adminRepository.lenderAdminStatistics();
        }
        public List<ActorCounterDTO> ActorCounter()
        {
            return adminRepository.ActorCounter();
        }

        public List<Gpcommercialregister> GetGpcommercialregisters()
        {
            return adminRepository.GetGpcommercialregisters();
        }

        public List<LenderComplaints> GetLenderStoresComplaints()
        {
            return adminRepository.GetLenderStoresComplaints();
        }

        public void HandleRegistarction(int IDD)
        {
            adminRepository.HandleRegistarction(IDD);
        }

        public List<LoaneeCreditScores> loaneeCreditScores()
        {
            return adminRepository.loaneeCreditScores();
        }

        public void ManageLenderComplaints(int loaid, int CID)
        {
            adminRepository.ManageLenderComplaints(loaid, CID);
        }

        public List<CancleLoanAuto> CancleLoanAutomatically()
        {
            return adminRepository.CancleLoanAutomatically();
        }

        public List<CancleLoanMsgforLender> CancleLoanAutoMsgForLender()
        {
            return adminRepository.CancleLoanAutoMsgForLender();
        }
    }
}
