using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheNeqatcomApp.Core.Service
{
    public  interface IOfferService
    {
        List<Gpoffer> GetAllOffers();
        Gpoffer GetOfferById(int id);
        void CreateOffer(Gpoffer offer);
        List<LoaneeMain> GetLoansRandomly();
        void UpdateOffer(Gpoffer offer);
        void DeleteOffer(int id);
        List<LoaneeMain> GetLoaneeMain();
        List<Gpoffer> GetAllOferById(int id);
        List<OffersForLenderMain> GetOffersForLenderMain(int lendId);

    }
}
