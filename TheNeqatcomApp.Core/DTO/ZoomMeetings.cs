using System;
using System.Collections.Generic;
using System.Text;

namespace TheNeqatcomApp.Core.DTO
{
    public class ZoomMeeting
    {
        public string Id { get; set; }
        public string Topic { get; set; }
        public string join_url { get; set; }
        public string StartUrl { get; set; }
    }

    public class ZoomMeetings
    {
        public List<ZoomMeeting> Meetings { get; set; }
    }
    public class ZoomMeetingResponse
    {
        public string join_url { get; set; }
    }
}
