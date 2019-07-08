using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jobs.Common
{
    [Serializable]
    public class UserLogin
    {
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string Ten { set; get; }
        public int LoaiTK { set; get; }
    }
}