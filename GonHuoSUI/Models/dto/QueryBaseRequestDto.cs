using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GonHuoSUI.Models.Dto
{
    /// <summary>
    ///查询分页的基类
    /// </summary>
    public class QueryBaseRequestDto
    {
        public int PageIndex { get; set; }
    }
}