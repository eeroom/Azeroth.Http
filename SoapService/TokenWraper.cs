﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoapService
{
    public class TokenWraper:System.Web.Services.Protocols.SoapHeader
    {
        public string Jwt { get; set; }
    }
}