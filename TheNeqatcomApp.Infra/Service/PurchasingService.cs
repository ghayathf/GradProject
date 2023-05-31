
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.Repository;
using TheNeqatcomApp.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Neqatcom.Infra.Service
{
    public class PurchasingService : IPurchasingService
    {
        private readonly IPurchasingRepository purchasingRepository;

        public PurchasingService(IPurchasingRepository purchasingRepository)
        {
            this.purchasingRepository = purchasingRepository;
        }
        public void CreatePurchasing(Gppurchasing purchasing)
        {
            purchasingRepository.CreatePurchasing(purchasing);
        }

        public void DeletePurchasing(int id)
        {
            purchasingRepository.DeletePurchasing(id);
        }

        public void ForGiveMonthly(int id)
        {
            purchasingRepository.ForGiveMonthly(id);
        }

        public List<Gppurchasing> GetAllPurchasing()
        {
            return purchasingRepository.GetAllPurchasing();
        }

        public Gppurchasing GetPurchasingById(int id)
        {
            return purchasingRepository.GetPurchasingById(id);
        }
        public void PayCash(int id)
        {
            purchasingRepository.PayCash(id);
        }
        public void PayOnline(int id)
        {
            purchasingRepository.PayOnline(id);
        }
        public List<Gppurchasing> GettAllPayments(int id)
        {
            return purchasingRepository.GettAllPayments(id);
        }

        public void UpdatePurchasing(Gppurchasing purchasing)
        {
            purchasingRepository.UpdatePurchasing(purchasing);
        }
    }
}
