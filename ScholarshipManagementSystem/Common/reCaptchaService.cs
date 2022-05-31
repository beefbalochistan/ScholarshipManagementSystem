﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScholarshipManagementSystem.Common
{
    public class reCaptchaService
    {
        public virtual async Task<reCaptchaRespo> tokenVerify(string token)
        {
            reCaptchaData data = new reCaptchaData
            {
                response = token,
                secret = "6LcaHCwgAAAAADlXl0BhmgoMENUNRBMm9wQ-QPu5"
            };

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={data.secret}&response={data.response}");
            var reCaptcharesponse = JsonConvert.DeserializeObject<reCaptchaRespo>(response);
            return reCaptcharesponse;
        }
    }
    public class reCaptchaData
    {
        public string response { get; set; }
        public string secret { get; set; }
    }
    public class reCaptchaRespo
    {
        public bool success { get; set; }
        public DateTime challenge_ts { get; set; }
        public string hostname { get; set; }
        public long score { get; set; }
    }
}
