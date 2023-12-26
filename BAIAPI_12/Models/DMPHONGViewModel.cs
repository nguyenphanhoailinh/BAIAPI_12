using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAIAPI_12.Models
{
    public class DMPHONGViewModel
    {
        public string MaPhong { get; set; }
        public string TenPhong { get; set; }
        public ICollection<DSNVViewModel> DSNVs { get; set; }
    }
}