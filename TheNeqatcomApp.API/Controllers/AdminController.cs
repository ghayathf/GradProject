using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using TheNeqatcomApp.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neqatcom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminService adminService;
        private readonly ILoaneeComplaintsService lcService;

        public AdminController(IAdminService adminService,ILoaneeComplaintsService lcService)
        {
            this.adminService = adminService;
            this.lcService = lcService;
        }
        [HttpPut]
        [Route("deleteComplaint/{cid}")]
        public void deleteComplaint(int cid)
        {
            adminService.deleteComplaint(cid);
        }
        [HttpGet]
        [Route("categoriesStatistics")]
        public CategoriesStatistics categoriesStatistics()
        {
            return adminService.categoriesStatistics();
        }
        [HttpGet]
        [Route("complaintsStatistics")]
        public ComplaintsStatistics complaintsStatistics()
        {
            return adminService.complaintsStatistics();
        }
        [HttpGet]
        [Route("lenderAdminStatistics")]
        public LenderAdminStatistics lenderAdminStatistics()
        {
            return adminService.lenderAdminStatistics();
        }
        [HttpGet]
        [Route("AdminStatisticsLoanee")]
        public AdminStatisticsLoanee AdminStatisticsLoanee()
        {
            return adminService.AdminStatisticsLoanee();
        }
        [HttpGet]
        [Route("GetAllCommercialRegistres")]
        public List<Gpcommercialregister> GetAllCommecialregister()
        {
            return adminService.GetGpcommercialregisters();
        }
        [HttpPut]
        [Route("AcceptLenderRegistration/{id}")]
        public void AcceptLenderRegistration(int id)
        {
            adminService.HandleRegistarction(id);
        }
        [HttpPost]
        [Route("ManageLoaneeComplaints/{lid}/{CID}")]
        public void ManageLoaneeComplaints(int lid, int CID)
        {
            lcService.ManageComplaints(lid, CID);
        }
        [HttpGet]
        [Route("GetAllComplaints")]
        public List<LoaneeComplaintsDTO> GetAllComplaints()
        {
            return lcService.GetAllCompliants();
        }
        [HttpPost]
        [Route("CheckFiveDays")]
        public void CheckFiveDays()
        {
            lcService.CheckFiveDays();
        }
        [HttpGet]
        [Route("LoaneeCreditScores")]
        public List<LoaneeCreditScores> LoaneeCreditScores()
        {
            return adminService.loaneeCreditScores();
        }
        [HttpGet]
        [Route("ActorCounter")]
        public List<ActorCounterDTO> ActorCounter()
        {
            return adminService.ActorCounter();
        }
        [HttpGet]
        [Route("GetLenderStoresComplaints")]
        public List<LenderComplaints> GetLenderStoresComplaints()
        {
            return adminService.GetLenderStoresComplaints();
        }
        [HttpPost]
        [Route("ManageLenderComplaints/{loaid}/{CID}")]
        public void ManageLenderComplaints(int loaid, int CID)
        {
            adminService.ManageLenderComplaints(loaid, CID);
        }
        [HttpGet]
        [Route("CancleLoanAutomatically")]
        public List<CancleLoanAuto> CancleLoanAutomatically()
        {
            return adminService.CancleLoanAutomatically();
        }
        [HttpGet]
        [Route("CancleLoanAutoMsgForLender")]
        public List<CancleLoanMsgforLender> CancleLoanAutoMsgForLender()
        {
            return adminService.CancleLoanAutoMsgForLender();
        }

    }
}
