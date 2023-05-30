using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheNeqatcomApp.Core.Service
{
    public interface IHomeService
    {
        List<Gphomepage> GetAllHomeInformation();
        Gphomepage GetHomeInformationById(int id);
        void CreateHomeInformation(Gphomepage finalHomepage);
        void UpdateHomeInformation(Gphomepage finalHomepage);

        void DeleteHomeInformation(int id);
        List<Lengths> getTableLength();
        List<LoaneeReminder> GetLoaneestoRemind();
        void UpdateBeforeReminder();
        List<LoaneeReminder> GetLoaneesInPayDaytoRemind();
        void UpdateInPayDateReminder();
        List<LoaneeReminder> GetLoaneeslatePayDaytoRemind();
        void UpdateLatePayDateReminder();
        void CalculateCreditScores();
        List<Boolean> CreditScoreStatus(int loaneeid);

    }
}
