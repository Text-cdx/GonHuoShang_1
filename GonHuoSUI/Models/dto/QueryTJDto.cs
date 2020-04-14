using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GonHuoSUI.Models.dto
{
    [Serializable]
    public class QueryTJDto: Dto.QueryBaseRequestDto
    {
        public string gname { get; set; }

        int gtype = 9999;
        public int gType
        {
            get
            {
                return gtype;
            }

            set
            {
                gtype = value;
            }
        }

        DateTime? state = DateTime.Now.AddDays(-30);
        public DateTime? State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }
        DateTime? end = DateTime.Now;
        public DateTime? End
        {
            get
            {
                return end;
            }

            set
            {
                end = value;
            }
        }
    }
}