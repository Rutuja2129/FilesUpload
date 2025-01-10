using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using CodeTentaclesTaks.Models;
using PagedList;
using System.IO;

namespace CodeTentaclesTaks.Controllers
{
    public class ProductController : Controller
    {
        CodeTentaclesTaskEntities context = new CodeTentaclesTaskEntities();
        [HttpGet]
        public ActionResult AddProduct()
        {
            if (Session["Uname"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }
        [HttpPost]
        public ActionResult AddProduct(tblProduct p, HttpPostedFileBase Img)
        {
            if (Session["Uname"] != null)
            {
                try
                {
                    if (Img != null)
                    {
                        string filename = Img.FileName;
                        string path = Path.Combine(Server.MapPath("~/Content/Images"), filename);
                        Img.SaveAs(path);
                        p.Image = filename;
                    }
                    context.tblProducts.Add(p);
                    context.SaveChanges();
                    return RedirectToAction("ProductList");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("AddProduct");
                }
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }


        }
        [HttpGet]
        public ActionResult ProductList(string sortOrder, string CurrentSort, int? page, string Filter_Value, string Search_Data)
        {
            if (Session["Uname"] != null)
            {
                int pageSize = 10;
                int pageIndex = 1;
                pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                ViewBag.CurrentSort = sortOrder;
                sortOrder = String.IsNullOrEmpty(sortOrder) ? "Name" : sortOrder;

                if (!string.IsNullOrEmpty(Search_Data))
                {
                    page = 1;
                }
                else
                {
                    Search_Data = Filter_Value;
                }

                ViewBag.FilterValue = Search_Data;

                var prolist = context.tblProducts.AsQueryable();

                if (!string.IsNullOrEmpty(Search_Data))
                {
                    prolist = prolist.Where(p => p.Name.ToUpper().Contains(Search_Data.ToUpper()));
                }

                switch (sortOrder)
                {
                    case "Name":
                        prolist = CurrentSort == "Name" ?
                                  prolist.OrderByDescending(m => m.Name) :
                                  prolist.OrderBy(m => m.Name);
                        break;
                    case "Amount":
                        prolist = CurrentSort == "Amount" ?
                                  prolist.OrderByDescending(m => m.Amount) :
                                  prolist.OrderBy(m => m.Amount);
                        break;
                    default:
                        prolist = prolist.OrderBy(m => m.Name);
                        break;
                }

                var pagedProlist = prolist.ToPagedList(pageIndex, pageSize);
                return View(pagedProlist);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }


        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            if (Session["Uname"] != null)
            {
                var product = context.tblProducts.Find(id);
                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }


        }
        [HttpPost]
        public ActionResult EditProduct(tblProduct p, HttpPostedFileBase Img)
        {
            if (Session["Uname"] != null)
            {
                try
                {
                    var product = context.tblProducts.Find(p.ProductId);
                    if (Img != null)
                    {
                        string filename = Img.FileName;
                        string path = Path.Combine(Server.MapPath("~/Content/Images"), filename);
                        Img.SaveAs(path);
                        product.Image = filename;
                    }
                    product.Name = p.Name;
                    product.Description = p.Description;
                    product.Amount = p.Amount;
                    context.SaveChanges();
                    return RedirectToAction("ProductList");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("EditProduct");
                }
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }


        }
    }
}