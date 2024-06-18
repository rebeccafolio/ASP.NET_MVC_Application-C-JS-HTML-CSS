using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITP245_2021_Fall.Models
{
    public class AboutModel
    {
        public string college { get; set; }
        public string degree { get; set; }
        public string years { get; set; }
        List<AboutModel> colleges = new List<AboutModel>();

    }

}