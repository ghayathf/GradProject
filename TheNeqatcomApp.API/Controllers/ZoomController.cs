﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using TheNeqatcomApp.Core.DTO;

namespace TheNeqatcomApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoomController : ControllerBase
    {
        private readonly HttpClient _client;

        public ZoomController(HttpClient client)
        {
            _client = client;
        }
        [HttpGet]
        [Route("GetAllMeetings")]
        public async Task<IActionResult> GetAllMeetings()
        {
            var accessToken = "eyJzdiI6IjAwMDAwMSIsImFsZyI6IkhTNTEyIiwidiI6IjIuMCIsImtpZCI6IjRjMDU0Yzc5LTkzYjgtNGEyMC04MjhkLWM3MTQ1OTkyODY1ZSJ9.eyJ2ZXIiOjksImF1aWQiOiJiOWI0NmFkNzZhYTY2ZmYxYWM0ZGE2NTJlZmMwYTczMCIsImNvZGUiOiJaUnJtWWFoMml0UHd0RjNDNjRnUlZDNmMwWDB0bTBiX1EiLCJpc3MiOiJ6bTpjaWQ6VDRLNW9qS29RNG9KU0dKYnN2enBRIiwiZ25vIjowLCJ0eXBlIjowLCJ0aWQiOjAsImF1ZCI6Imh0dHBzOi8vb2F1dGguem9vbS51cyIsInVpZCI6IndNVlVOWlRqVDEtV0tEcktqVGQxUkEiLCJuYmYiOjE2ODk4MTIyOTcsImV4cCI6MTY4OTgxNTg5NywiaWF0IjoxNjg5ODEyMjk3LCJhaWQiOiJtclRBRWhVN1FSQ3lZeExiRVRRY2VBIn0.NblhhbFRI8F_QgKZV9-rLAmWrk8j49um4m-mrz3eMcAVvBTCXnXrGVlUfbZfVhd076R6CvYsI4va9bdDPKgelg";
            var requestUrl = "https://api.zoom.us/v2/users/me/meetings";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var meetings = JsonConvert.DeserializeObject<ZoomMeetings>(responseContent);

                // Extract the meeting links from the meetings result
                var meetingLinks = new List<string>();
                foreach (var meeting in meetings.Meetings)
                {
                    meetingLinks.Add(meeting.join_url);
                }

                return Ok(meetingLinks);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [HttpPost]
        [Route("CreateMeeting")]
        public async Task<IActionResult> CreateMeeting([FromBody] ZoomMeeting meeting)
        {
            var accessToken = "eyJzdiI6IjAwMDAwMSIsImFsZyI6IkhTNTEyIiwidiI6IjIuMCIsImtpZCI6IjRjMDU0Yzc5LTkzYjgtNGEyMC04MjhkLWM3MTQ1OTkyODY1ZSJ9.eyJ2ZXIiOjksImF1aWQiOiJiOWI0NmFkNzZhYTY2ZmYxYWM0ZGE2NTJlZmMwYTczMCIsImNvZGUiOiJaUnJtWWFoMml0UHd0RjNDNjRnUlZDNmMwWDB0bTBiX1EiLCJpc3MiOiJ6bTpjaWQ6VDRLNW9qS29RNG9KU0dKYnN2enBRIiwiZ25vIjowLCJ0eXBlIjowLCJ0aWQiOjAsImF1ZCI6Imh0dHBzOi8vb2F1dGguem9vbS51cyIsInVpZCI6IndNVlVOWlRqVDEtV0tEcktqVGQxUkEiLCJuYmYiOjE2ODk4MTIyOTcsImV4cCI6MTY4OTgxNTg5NywiaWF0IjoxNjg5ODEyMjk3LCJhaWQiOiJtclRBRWhVN1FSQ3lZeExiRVRRY2VBIn0.NblhhbFRI8F_QgKZV9-rLAmWrk8j49um4m-mrz3eMcAVvBTCXnXrGVlUfbZfVhd076R6CvYsI4va9bdDPKgelg"; // your access token here
            var requestUrl = "https://api.zoom.us/v2/users/me/meetings";

            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var content = new StringContent(JsonConvert.SerializeObject(meeting), Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                // wait for 1 second before fetching the meetings to give time for the newly created meeting to propagate
                await Task.Delay(TimeSpan.FromSeconds(1));

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ZoomMeetingResponse>(json);

                return Ok(result);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }

        
    }
}
