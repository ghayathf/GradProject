using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using System.Collections.Generic;
using TheNeqatcomApp.Core.Service;

namespace Neqatcom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService _offerService)
        {
            this._offerService = _offerService;
        }
        [HttpGet]
        [Route("GetAllOffers")]
        public List<Gpoffer> GetAllOffers()
        {
            return _offerService.GetAllOffers();
        }
        [HttpGet]
        [Route("GetAllOffersById/{id}")]
        public List<Gpoffer> GetAllOferById(int id)
        {
            return _offerService.GetAllOferById(id);
        }
        [HttpGet]
        [Route("GetOffersForLenderMain/{lendId}")]
        public List<OffersForLenderMain> GetOffersForLenderMain(int lendId)
        {
            return _offerService.GetOffersForLenderMain(lendId);
        }
        [HttpGet]
        [Route("GetLoaneeMain")]
        public List<LoaneeMain> GetLoaneeMain()
        {
            return _offerService.GetLoaneeMain();
        }



        [HttpGet]
        [Route("GetOfferById/{id}")]
        public Gpoffer GetOfferById(int id)
        {
            return _offerService.GetOfferById(id);
        }
        [HttpPost]
        [Route("CreateOffer")]
        public void CreateOffer(Gpoffer offer)
        {
            _offerService.CreateOffer(offer);
        }
        [HttpPut]
        [Route("UpdateOffer")]
        public void UpdateOffer(Gpoffer offer)
        {
            _offerService.UpdateOffer(offer);
        }
        [HttpDelete]
        [Route("DeleteOffer/{id}")]
        public void DeleteOffer(int id)
        {
            _offerService.DeleteOffer(id);
        }

        [HttpGet]
        [Route("GetLoansRandomly")]
        public List<LoaneeMain> GetLoansRandomly()
        {
            return _offerService.GetLoansRandomly();

        }

    }
}
