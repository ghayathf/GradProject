using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using TheNeqatcomApp.Core.Service;

namespace TheNeqatcomApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LenderStoreController : ControllerBase
    {
        private readonly ILenderStoreService _lenderStoreService;
        public LenderStoreController(ILenderStoreService lenderStoreService)
        {
            _lenderStoreService = lenderStoreService;
        }
        [HttpGet]
        [Route("GetLenderInfo/{id}")]
        public LenderInfo GetLenderInfo(int id)
        {
            return _lenderStoreService.GetLenderInfo(id);
        }
        [HttpGet]
        [Route("GetAllLenderStore")]
        public List<Gplenderstore> GetAllLenderStore()
        {
            return _lenderStoreService.GetAllLenderStore();
        }
        [HttpGet]
        [Route("GetAllLenderUser")]
        public List<LenderUser> GetAllLenderUser()
        {
            return _lenderStoreService.GetAllLenderUser();
        }
        [HttpGet]
        [Route("GetAllLoanOffer/{lenderid}/{loaneeid}")]
        public List<LoanOffer> GetAllLoanOffer(int lenderid, int loaneeid)
        {
            return _lenderStoreService.GetAllLoanOffer(lenderid, loaneeid);
        }
        [HttpGet]
        [Route("GetLenderStoreById/{id}")]
        public Gplenderstore GetLenderStoreById(int id)
        {
            return _lenderStoreService.GetLenderStoreById(id);
        }
        [HttpGet]
        [Route("LoaneesForLenders/{id}")]
        public List<LoaneesForLendercs> GetAllLoaneesForLendercs(int id)
        {
            return _lenderStoreService.GetAllLoaneesForLendercs(id);
        }
        [HttpPost]
        [Route("giveComplaintForLoanee")]
        public void giveComplaintForLoanee(Gpcomplaint gpcomplaint)
        {
            _lenderStoreService.giveComplaintForLoanee(gpcomplaint);
        }
        [HttpPost]
        [Route("createLenderStore")]
        public void createLenderStore(Gplenderstore gplenderstore)
        {
            _lenderStoreService.createLenderStore(gplenderstore);
        }
        [HttpPut]
        [Route("UpdateLenderStore")]
        public void UpdateLenderStore(Gplenderstore gplenderstore)
        {
            _lenderStoreService.UpdateLenderStore(gplenderstore);
        }
        [HttpDelete]
        [Route("DeleteLenderStore/{id}")]
        public void DeleteLenderStore(int id)
        {
            _lenderStoreService.DeleteLenderStore(id);
        }
        [HttpGet]
        [Route("GetLenderPayments/{id}")]
        public List<LenderPayment> GetLenderPayments(int id)
        {
            return _lenderStoreService.GetLenderPayments(id);
        }
        [HttpGet]
        [Route("GetLenderCounters/{lenderid}")]
        public List<Lengths> GetLenderCounters(int lenderid)
        {
            return _lenderStoreService.GetLenderCounters(lenderid);
        }

    }
}
