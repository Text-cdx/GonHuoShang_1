using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GonHuoSModel;
using GonHuoSBll;
using System.Linq.Expressions;
using GonHuoSUI.Models.dto;

namespace GonHuoSUI.Controllers
{
    public class GonHuoController : QueryBaseControllers
    {
        // GET: GonHuo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Query(QueryTJDto dto)
        {
            Expression<Func<GonhuoShang, bool>> where = i => i.TTime >= dto.State && i.TTime <= dto.End;
            if (dto.gType != 9999)
            {
                where = where.And(i => i.GType == dto.gType);
            }
            if (!string.IsNullOrEmpty(dto.gname))
            {
                where = where.And(i => i.GName.IndexOf(dto.gname) != -1);
            }
            GetRequestPage(dto);
            var pageIndex = dto.PageIndex;
            var count = 0;
            var pageCount = 0;
            var aniBll = new GonhuoService();
            var lis = aniBll.GetByWhereDesc(where, i => i.XTime, ref pageIndex, ref count, ref pageCount, 4);

            var aniType = new TypeService();
            var type = aniType.GetAll();
            type.Insert(0, new GonHuoSModel.Type() { Tid = 9999, TName = "全部" });
            ViewBag.GType = new SelectList(type, "Tid", "TName");

            ViewBag.PageIndex = pageIndex;
            ViewBag.Count = count;
            ViewBag.PageCount = pageCount;
            ViewBag.Gonhuo = lis;
            ViewBag.dto1 = dto;
            return View();
        }

        public ActionResult AddResult()
        {
            var aniType = new TypeService();
            var type = aniType.GetAll();
            ViewBag.GType = new SelectList(type, "Tid", "TName");
            return View();
        }

        public ActionResult Add(GonhuoShang d)
        {
            d.TTime = DateTime.Now;
            d.XTime = DateTime.Now;
            d.ZhuangTai = 1;
            var a = new GonhuoService();
            var s = a.Add(d);
            return RedirectToAction("Query");
        }

        public ActionResult UpdateResult(int id)
        {
            var a = new GonhuoService();
            var s = a.GetByWhere(i => i.Gid == id).SingleOrDefault();

            var aniType = new TypeService();
            var type = aniType.GetAll();
            ViewBag.GType = new SelectList(type, "Tid", "TName",s.GType);
            return View(s);
        }

        public ActionResult Updt(GonhuoShang d, int id)
        {
            var a = new GonhuoService();
            var s = a.GetByWhere(i => i.Gid == id).SingleOrDefault();
            d.TTime = s.TTime;
            d.XTime = DateTime.Now;
            d.Gid = id;
            d.ZhuangTai = s.ZhuangTai;
            var aa = new GonhuoService();
            var ss = aa.Updt(d);
            return RedirectToAction("Query");
        }

        public ActionResult DeleteResult(int id)
        {
            var a = new GonhuoService();
            var s = a.GetByWhere(i => i.Gid == id).SingleOrDefault();

            var aniType = new TypeService();
            var type = aniType.GetAll();
            ViewBag.GType = new SelectList(type, "Tid", "TName", s.GType);
            return View(s);
        }

        public ActionResult Dele(GonhuoShang d, int id)
        {
            d.Gid = id;
            var a = new GonhuoService();
            var s = a.Dele(d);
            return RedirectToAction("Query");
        }

        public ActionResult QueryAddress(int id)
        {
            Expression<Func<AddressBiao, bool>> where = i => i.Gid == id;
            var aniBll = new AddressService();
            var lis = aniBll.GetByWhere(where);
            ViewBag.id = id;

            ViewBag.Addres = lis;
            return View();
        }

        public ActionResult AddAddress(int id)
        {
            ViewBag.id = id;
            return View();
        }

        public ActionResult AddAddre(AddressBiao d)
        {
            d.TTime = DateTime.Now;
            d.XTime = DateTime.Now;
            d.ZhuangTai = 1;
            var a = new AddressService();
            var s = a.Add(d);
            return RedirectToAction("Query");
        }

        public ActionResult DeleteAddressResult(int id)
        {
            ViewBag.id = id;
            var a = new AddressService();
            var s = a.GetByWhere(i => i.Aid == id).SingleOrDefault();
            return View(s);
        }

        public ActionResult DeleAddres(AddressBiao d, int id)
        {
            d.Aid = id;
            var a = new AddressService();
            var s = a.Dele(d);
            return RedirectToAction("Query");
        }


        public ActionResult ShowInfo(int id)
        {
            var a = new AddressService();
            var s = a.GetByWhere(i => i.Aid == id).SingleOrDefault();
            ViewBag.id = id;
            return PartialView(s);
        }

        public ActionResult QueryDeletePage(AddressBiao d, int id)
        {
            d.Aid = id;
            var a = new AddressService();
            var s = a.Dele(d);
            return RedirectToAction("Query");
        }

        /// <summary>
        /// ajax查询所有
        /// </summary>
        /// <returns></returns>
        public ActionResult Queryssss()
        {
            var aniType = new TypeService();
            var type = aniType.GetAll();
            type.Insert(0, new GonHuoSModel.Type() { Tid = 9999, TName = "全部" });
            ViewBag.GType = new SelectList(type, "Tid", "TName");
            return View();
        }

        public ActionResult Qusss(int GType, string GName,int pageIndex)
        {
            var a = new GonhuoService();
            //组合条件
            Expression<Func<GonhuoShang, bool>> where = item => true;
            if (GType!=9999)
            {
                //当类型不是全部选中项，则按照类型组合条件
                where = where.And(item => item.GType.Equals(GType));
            }
            if (!string.IsNullOrEmpty(GName))
            {
                where = where.And(item => item.GName.IndexOf(GName) != -1);
            }
            var pageCount = 0;
            var count = 0;
            var s = a.GetByWhereDesc(where,item=>item.XTime,ref pageIndex, ref count, ref pageCount,2);
            //格式转换
            var newFormatList = s.Select(item => new { Gid = item.Gid, GName = item.GName, TypeName = item.Type.TName, Email = item.Email, Youbian = item.Youbian, Phone = item.Phone, TTime = item.TTime.ToString("yyyy-MM-dd HH:mm:ss"), XTime = item.XTime.ToString("yyyy-MM-dd HH:mm:ss") });
            var result = new
            {
                PageIndex = pageIndex,
                PageCount=pageCount,
                Count=count,
                gonhuosInfor = newFormatList
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QueryAddAjax()
        {
            var aniType = new TypeService();
            var type = aniType.GetAll();
            ViewBag.GType = new SelectList(type, "Tid", "TName");
            return View();
        }

        public ActionResult AddAjax(GonhuoShang d)
        {
            d.TTime = DateTime.Now;
            d.XTime = DateTime.Now;
            d.ZhuangTai = 1;
            var a = new GonhuoService();
            var s = a.Add(d);
            var result = new {
                ActionResult=s,
                Msg=s ? "添加成功" : "添加失败"
            };
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult QueryUpdateAjax(int id)
        {
            var a = new GonhuoService();
            var s = a.GetByWhere(i => i.Gid == id).SingleOrDefault();

            var aniType = new TypeService();
            var type = aniType.GetAll();
            ViewBag.GType = new SelectList(type, "Tid", "TName", s.GType);
            return View(s);
        }

        public ActionResult UpdateAjax(GonhuoShang d)
        {
            var a = new GonhuoService();
            var s = a.GetByWhere(i => i.Gid == d.Gid).SingleOrDefault();
            d.TTime = s.TTime;
            d.XTime = DateTime.Now;
            d.ZhuangTai = s.ZhuangTai;
            var aa = new GonhuoService();
            var ss = aa.Updt(d);
            var result = new
            {
                ActionResult = ss,
                Msg = ss ? "修改成功" : "修改失败"
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QueryDeleteAjax(int id)
        {
            var a = new GonhuoService();
            var s = a.GetByWhere(i => i.Gid == id).SingleOrDefault();

            var aniType = new TypeService();
            var type = aniType.GetAll();
            ViewBag.GType = new SelectList(type, "Tid", "TName", s.GType);
            return View(s);
        }

        public ActionResult DeleAjax(GonhuoShang d)
        {
            var a = new GonhuoService();
            var s = a.Dele(d);
            var result = new
            {
                ActionResult = s,
                Msg = s ? "删除成功" : "删除失败"
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}