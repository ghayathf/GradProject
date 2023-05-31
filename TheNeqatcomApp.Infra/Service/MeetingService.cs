using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.Repository;
using TheNeqatcomApp.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Neqatcom.Infra.Service
{
   public class MeetingService:IMeetingService
    {
        private readonly IMeetingRepository meetingRepository;
        public MeetingService(IMeetingRepository meetingRepository)
        {
            this.meetingRepository = meetingRepository;
        }

        public void CreateMeeting(Gpmeeting meeting)
        {
            meetingRepository.CreateMeeting(meeting);
        }

        public void DeleteMeeting(int IDD)
        {
            meetingRepository.DeleteMeeting(IDD);
        }

        public List<Gpmeeting> GetAllMeetings()
        {
            return meetingRepository.GetAllMeetings();
        }

        public Gpmeeting GetMeetingByID(int IDD)
        {
            return meetingRepository.GetMeetingByID(IDD);
        }

        public void UpdateMeeting(Gpmeeting meeting)
        {
            meetingRepository.UpdateMeeting(meeting);
        }
    }
}
