using CnWeb_FastFood.Models;
using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models.EF;
using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CnWeb_FastFood.Controllers
{
    public class ShopCheckoutController : BaseController
    {
        // GET: ShopCheckout
        public static Checkout bill;
        SnackShopDBContext db = new SnackShopDBContext();
        public ActionResult Index()
        {
            if (Session["ID_SESSION"] != null)
            {
                ViewBag.customer = new CustomerDao().getByID(Convert.ToInt32(Session["ID_SESSION"]));
            }
            decimal discount = bill.Total - bill.Subtotal;
            ViewBag.discount = discount;

            ViewBag.CreditList = new List<SelectListItem>()
            {
                new SelectListItem() { Value="ACB", Text= "ACB Ngân hàng Á Châu" },
                new SelectListItem() { Value="Agribank", Text= "Agribank Ngân hàng Nông nghiệp và Phát triển Nông thôn VN" },
                new SelectListItem() { Value="BIDV", Text= "BIDV Ngân hàng Đầu tư và Phát triển Việt Nam" },
                new SelectListItem() { Value="HSBC", Text= "HSBC Ngân hàng TNHH một thành viên HSBC (Việt Nam)" },
                new SelectListItem() { Value="MSB", Text= "MSB Ngân hàng Hàng Hải Việt Nam" },
                new SelectListItem() { Value="Oceanbank", Text= "Oceanbank Ngân hàng Đại Dương" },
                new SelectListItem() { Value="STB", Text= "Vietcombank Ngân hàng Ngoại thương Việt Nam" },
                new SelectListItem() { Value="TPBank", Text= "TPBank Ngân hàng Tiên Phong" },
                new SelectListItem() { Value="VCB", Text= "Sacombank Ngân hàng Sài Gòn Thương Tín" },                
                new SelectListItem() { Value="VPBank", Text= "VPBank Ngân hàng Việt Nam Thịnh Vượng" },   
            };
            return View(bill);
        }

        [HttpGet]
        public ActionResult ConfirmOrder(string address, string phone)
        {                        

            Bill newBill = new Bill();

            newBill.subtotal = bill.Subtotal;
            newBill.total = bill.Total;
            newBill.creatDate = DateTime.Now;
            newBill.id_customer = bill.Id_customer;
            newBill.discountCode = bill.DiscountCode;
            newBill.discount = newBill.subtotal - newBill.total;
            newBill.address = address;
            newBill.phone = phone;
            newBill.id_status = 1;
            
            db.Bills.Add(newBill);
            db.SaveChanges();

            var id_bill = db.Bills.Where(b => b.id_customer == bill.Id_customer).OrderByDescending(b => b.creatDate).First().id_bill;

            foreach(CartItem line in bill.Item)
            {
                BillDetail newBillDetail = new BillDetail();
                newBillDetail.id_bill = id_bill;
                newBillDetail.id_product = line.Products.id_product;
                newBillDetail.price = line.Price;
                newBillDetail.amount = line.Amount;
                newBillDetail.intoMoney = line.IntoMoney;
                string textDetailProduct = "";
                var listDetailProduct = db.ProductDetails.Where(p => p.id_product == line.Products.id_product).ToList();
                if (listDetailProduct.Count != 0)
                {
                    foreach (ProductDetail lineDetail in listDetailProduct)
                    {
                        textDetailProduct = textDetailProduct + lineDetail.amount + " " + lineDetail.name + "\r\n";
                    }
                    textDetailProduct.Remove(textDetailProduct.Length - 1);
                    newBillDetail.discriptionProductDetail = textDetailProduct;
                }
                db.BillDetails.Add(newBillDetail);
                db.SaveChanges();

                //sent mail
                string nameCus = db.Customers.Find(newBill.id_customer).name.ToString();
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/template/neworder.html"));
                content = content.Replace("{{CustomerName}}", newBill.Customer.name);
                content = content.Replace("{{Phone}}", newBill.phone);
                content = content.Replace("{{Email}}", "không có");
                content = content.Replace("{{Address}}", newBill.address);
                content = content.Replace("{{Total}}", newBill.total.ToString());

                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                new MailHelper().SendMail(toEmail, "đơn hàng mới từ FastFood", content);

            }
            return RedirectToAction("Index", "CustomerBill");            
        }

        public ActionResult AddBill(string billModel)
        {
            var jsonBill = new JavaScriptSerializer().Deserialize<Checkout>(billModel);
            List<CartItem> listBill = jsonBill.Item;
            ProductDao pdao = new ProductDao();
            foreach (var line in listBill)
            {
                line.Products = pdao.getByID(line.Products.id_product);
                line.Price = line.Products.salePrice.GetValueOrDefault(0);
                line.IntoMoney = line.Price * line.Amount;
            }
            decimal subtotal = listBill.Sum(x => x.IntoMoney);
            decimal total = subtotal;
            if (!string.IsNullOrEmpty(jsonBill.DiscountCode))
            {
                var discountCode = new SnackShopDBContext().DiscountCodes.Find(jsonBill.DiscountCode);
                if(discountCode== null)
                {
                    jsonBill.DiscountCode = "";
                }
                else
                {
                    total = subtotal - Convert.ToDecimal(discountCode.discount);
                }
                
            }
            jsonBill.Subtotal = subtotal;
            jsonBill.Total = total;
            jsonBill.Item = listBill;
            bill = jsonBill;

            return Json(new { newUrl = Url.Action("Index", "ShopCheckout") });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}