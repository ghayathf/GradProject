using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheNeqatcomApp.Core.Data;
using TheNeqatcomApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheNeqatcomApp.Core.Service;

namespace Neqatcom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService meetingService;
        public MeetingController(IMeetingService meetingService)
        {
            this.meetingService = meetingService;
        }
        [HttpPost]
        [Route("CreateMeeting")]
        public void CreateMeeting(Gpmeeting meeting)
        {
            meetingService.CreateMeeting(meeting);
        }
        [HttpDelete]
        [Route("DeleteMeeting/{id}")]
        public void DeleteMeeting(int id)
        {
            meetingService.DeleteMeeting(id);
        }
        [HttpGet]
        [Route("GetMeetingById/{id}")]
        public Gpmeeting GetMeetingById(int id)
        {
            return meetingService.GetMeetingByID(id);
        }
        [HttpGet]
        [Route("GetAllMeetings")]
        public List<Gpmeeting> GetAllMeetings()
        {
            return meetingService.GetAllMeetings();
        }
        [HttpPut]
        [Route("UpdateMeeting")]
        public void UpdateMeeting(Gpmeeting meeting)
        {
            meetingService.UpdateMeeting(meeting);
        }
    }
}
