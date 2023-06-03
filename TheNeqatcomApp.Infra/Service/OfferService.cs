using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using TheNeqatcomApp.Core.Repository;
using TheNeqatcomApp.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheNeqatcomApp.Infra.Service
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository offerRepository;

        public OfferService(IOfferRepository offerRepository)
        {
            this.offerRepository = offerRepository;
        }
        public void CreateOffer(Gpoffer offer)
        {
            offerRepository.CreateOffer(offer);
        }
        public List<LoaneeMain> GetLoaneeMain()
        {
            return offerRepository.GetLoaneeMain();
        }
        public List<OffersForLenderMain> GetOffersForLenderMain(int lendId)
        {
            return offerRepository.GetOffersForLenderMain(lendId);
        }
        public void DeleteOffer(int id)
        {
            offerRepository.DeleteOffer(id);
        }

        public List<Gpoffer> GetAllOferById(int id)
        {
           return  offerRepository.GetAllOferById(id);
        }

        public List<Gpoffer> GetAllOffers()
        {
            return offerRepository.GetAllOffers();
        }

        public Gpoffer GetOfferById(int id)
        {
            return offerRepository.GetOfferById(id);
        }

        public void UpdateOffer(Gpoffer offer)
        {
            offerRepository.UpdateOffer(offer);
        }

        public List<LoaneeMain> GetLoansRandomly()
        {
            return offerRepository.GetLoansRandomly();
        }
    }
}
