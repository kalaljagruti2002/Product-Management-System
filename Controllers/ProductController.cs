 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Image_Project.dbconnection;
using Image_Project.Models;

namespace Image_Project.Controllers
{
    public class ProductController : Controller
    {
        JagrutiDBEntities db=new JagrutiDBEntities();   
        public ActionResult Index()
        {
            return View();
        }
		public ActionResult SaveRecord(ProductModel model, HttpPostedFileBase ProdImage)
		{
			try
			{
				var fname = ProdImage.FileName;
				var extension = Path.GetExtension(ProdImage.FileName);
				if (extension.Equals(".jpg") || (extension.Equals(".png")))

				{
					string Filename = "IMG-" + DateTime.Now.ToString("yyyyyMMddhmmss") + fname;
					string savepath = Server.MapPath("~/Content/Images/");
					ProdImage.SaveAs(savepath + Filename);
					model.ProdImage = Filename;
					Product12 product = new Product12();
					product.ProdTotal = model.ProdTotal;
					product.ProdName = model.ProdName;
					product.ProdImage = model.ProdImage;
					product.ProdRate = model.ProdRate;
					product.ProdQty = model.ProdQty;
					product.Date = model.Date;
					db.Product12.Add(product);
					db.SaveChanges();
					return RedirectToAction("GetRecord");

				}
				else
				{
					return Content("Please upload only jpg and png image..!");
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public ActionResult GetRecord(int pagenumber = 1, string search = null, DateTime? startDate = null, DateTime? endDate = null)
		{
			 
            var data=db.Product12.ToList();
			ViewBag.TotalPageNo = Math.Ceiling(data.Count() / 3.0);
			ViewBag.pageNo = pagenumber;
			data =data.Skip((pagenumber - 1) * 3).Take(3).ToList();
			
			if (search == null && startDate == null)
			{
				return View(data);
			}
		
			else if (startDate !=null)
			{
				data = db.Product12.ToList();
				data = data.Where(record =>
				  record.Date >= startDate && record.Date <= endDate).ToList();
				return View(data);

			}
			else
			{
				return View(db.Product12.Where(x => x.ProdName.Contains(search) || search == null).ToList());
			}
	
	    }
		

		public ActionResult Edit(int id)
		{
			var data = db.Product12.Where(m=>m.ProdId == id).FirstOrDefault();
			return View(data);
		}
		public ActionResult EditSave(ProductModel model, HttpPostedFileBase ProdImage)
		{
			try
			{
				if (ProdImage == null)
				{
					var data = db.Product12.Where(m => m.ProdId == model.ProdId).FirstOrDefault();
					if (data != null)
					{
						data.ProdTotal = model.ProdTotal;
						data.ProdName = model.ProdName;
						data.ProdRate = model.ProdRate;
						data.ProdQty = model.ProdQty;
						data.Date = model.Date;
						db.SaveChanges();
					}
					else
					{
						return Content("Data Not Found...!");
					}

				}
				else
				{
					var frame = ProdImage.FileName;
					var extension = Path.GetExtension(ProdImage.FileName);
					if (extension.Equals(".jpg") || (extension.Equals(".png")))
					{
						string Filename = "IMG-" + DateTime.Now.ToString("yyyyyMMddhmmss") + frame;
						string savepath = Server.MapPath("~/Content/Images/");
						ProdImage.SaveAs(savepath + Filename);
						model.ProdImage = Filename;
						var data = db.Product12.Where(m => m.ProdId == model.ProdId).FirstOrDefault();
						if (data != null)
						{
							data.ProdTotal = model.ProdTotal;
							data.ProdName = model.ProdName;
							data.ProdRate = model.ProdRate;
							data.ProdImage = model.ProdImage;
							data.ProdQty = model.ProdQty;
							data.Date = model.Date;
							db.SaveChanges();
						}
						else
						{
							return Content("Data Not Found...!");
						}

					}

				}
				return RedirectToAction("GetRecord");

			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public ActionResult Details(int id)
		{
			var data = db.Product12.Where(m => m.ProdId == id).FirstOrDefault();
			return View(data);
		}
		public ActionResult Delete(int id)
		{

			var data = db.Product12.Where(m => m.ProdId == id).FirstOrDefault();
			db.Product12.Remove(data);
			db.SaveChanges();
			return RedirectToAction("GetRecord"); 
		}

	}
}