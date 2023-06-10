using Dapper;
using TheNeqatcomApp.Core.Common;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TheNeqatcomApp.Core.Repository;

namespace Neqatcom.Infra.Repository
{
   public class MeetingRepository:IMeetingRepository
    {
        private readonly IDBContext dbContext;
        public MeetingRepository(IDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateMeeting(Gpmeeting meeting)
        {
            var p = new DynamicParameters();
            p.Add("MeetingDate", meeting.Startdate, DbType.DateTime, ParameterDirection.Input);
            p.Add("Meeting_Url", meeting.Meetingurl, DbType.String, ParameterDirection.Input);
            p.Add("feed_back", meeting.Feedbackk, DbType.Decimal, ParameterDirection.Input);
            p.Add("LoaneeIDD", meeting.Loaneeid, DbType.Int32, ParameterDirection.Input);
            p.Add("LenderIDD", meeting.Lenderid, DbType.Int32, ParameterDirection.Input);
            p.Add("Meeting_Time", meeting.Meetingtime, DbType.String, ParameterDirection.Input);
            p.Add("LoanIdd", meeting.Loanid, DbType.Int32, ParameterDirection.Input);

            dbContext.Connection.Execute("INSERT INTO GPMEETINGS (startdate, MeetingUrl, feedbackk, Loaneeid, Lenderid, MeetingTime, LoanId) VALUES (@MeetingDate, @Meeting_Url, @feed_back, @LoaneeIDD, @LenderIDD, @Meeting_Time, @LoanIdd)", p);
        }

        public void DeleteMeeting(int IDD)
        {
            var parameters = new { IDD };
            dbContext.Connection.Execute("DELETE FROM GPMEETINGS WHERE meetingID = @IDD", parameters);
        }

        public List<Gpmeeting> GetAllMeetings()
        {
            var sql = "SELECT * FROM GPMEETINGS";
            var meetings = dbContext.Connection.Query<Gpmeeting>(sql).ToList();
            return meetings;
        }

        public Gpmeeting GetMeetingByID(int IDD)
        {
            var sql = "SELECT * FROM GPMEETINGS WHERE meetingID = @IDD";
            var meeting = dbContext.Connection.QueryFirstOrDefault<Gpmeeting>(sql, new { IDD });

            return meeting;
        }

        public void UpdateMeeting(Gpmeeting meeting)
        {
            var parameters = new
            {
                IDD = meeting.Meetingid,
                MeetingDate = meeting.Startdate,
                Meeting_Url = meeting.Meetingurl,
                feed_back = meeting.Feedbackk,
                LoaneeIDD = meeting.Loaneeid,
                LenderIDD = meeting.Lenderid,
                Meeting_Time = meeting.Meetingtime
            };


            dbContext.Connection.Execute(@"UPDATE GPMEETINGS
                SET
                startdate = @MeetingDate,
                MeetingUrl = @Meeting_Url,
                feedbackk = @feed_back,
                Loaneeid = @LoaneeIDD,
                Lenderid = @LenderIDD,
                MeetingTime = @Meeting_Time
                WHERE meetingID = @IDD", parameters);
        }
    }
}
