using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using TheNeqatcomApp.Core.Data;
namespace TheNeqatcomApp.Core.Repository
{
    public interface IOfferRepository
    {
        List<Gpoffer> GetAllOffers();
        Gpoffer GetOfferById(int id);
        List<LoaneeMain> GetLoansRandomly();
        void CreateOffer(Gpoffer offer);
        void UpdateOffer(Gpoffer offer);
        void DeleteOffer(int id);
        List<LoaneeMain> GetLoaneeMain();

        List<Gpoffer> GetAllOferById(int id);
        List<OffersForLenderMain> GetOffersForLenderMain(int lendId);

    }
}
