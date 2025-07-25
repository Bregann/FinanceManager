﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.DTOs.Banking
{
    public class GoCardlessAccessTokenResponse
    {
        [JsonProperty("access")]
        public string AccessToken { get; set; } = "";

        [JsonProperty("access_expires")]
        public int AccessExpires { get; set; }

        [JsonProperty("refresh")]
        public string RefreshToken { get; set; } = "";

        [JsonProperty("refresh_expires")]
        public int RefreshExpires { get; set; }
    }
}
