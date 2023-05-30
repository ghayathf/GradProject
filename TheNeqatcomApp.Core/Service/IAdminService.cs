﻿using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using TheNeqatcomApp.Core.Data;

namespace TheNeqatcomApp.Core.Service
{
    public interface IAdminService
    {
        List<Gpcommercialregister> GetGpcommercialregisters();
        void HandleRegistarction(int IDD);
        List<LoaneeCreditScores> loaneeCreditScores();
        List<ActorCounterDTO> ActorCounter();
        void deleteComplaint(int cid);
        List<LenderComplaints> GetLenderStoresComplaints();
        void ManageLenderComplaints(int loaid, int CID);
        AdminStatisticsLoanee AdminStatisticsLoanee();
        LenderAdminStatistics lenderAdminStatistics();
        ComplaintsStatistics complaintsStatistics();
        CategoriesStatistics categoriesStatistics();
        List<CancleLoanAuto> CancleLoanAutomatically();
        List<CancleLoanMsgforLender> CancleLoanAutoMsgForLender();
    }
}