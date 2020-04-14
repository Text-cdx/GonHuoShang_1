using GonHuoSUI.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GonHuoSUI.Controllers
{
    /// <summary>
    /// 查询控制器基类
    /// </summary>
    public class QueryBaseControllers : System.Web.Mvc.Controller
    {
        /// <summary>
        /// 获取查询页面中的分页参数
        /// </summary>
        /// <param name="qord"></param>
        /// <returns></returns>
        protected QueryBaseRequestDto GetRequestPage(QueryBaseRequestDto qord)
        {
            if (Request.QueryString.AllKeys.Contains("next"))
            {
                qord.PageIndex = qord.PageIndex + 1;
            }
            else if (Request.QueryString.AllKeys.Contains("up"))
            {
                qord.PageIndex = qord.PageIndex - 1;
            }
            else if (Request.QueryString.AllKeys.Contains("first"))
            {
                qord.PageIndex = 1;
            }
            else if (Request.QueryString.AllKeys.Contains("last"))
            {
                qord.PageIndex = 999999999;
            }
            return qord;
        }
    }
}