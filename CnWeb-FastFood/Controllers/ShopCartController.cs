using CnWeb_FastFood.Models;
using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CnWeb_FastFood.Controllers
{
    public class ShopCartController : Controller
    {
        SnackShopDBContext db = new SnackShopDBContext();

        // GET: ShopCart
        public ActionResult Index(string discountCode)
        {

            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();           
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            decimal discount = 0;
            try
            {
                if (!string.IsNullOrEmpty(discountCode))
                {
                    var dc = new DiscountCodeDao().getByID(discountCode);
                    discount = dc.discount.GetValueOrDefault(0);

                }
            }
            catch
            {
                ;
            }
            
            decimal subtotal = list.Sum(x => x.IntoMoney);
            decimal total = subtotal - discount;
            if (total < 0) total = 0;
            Session[CommonConstants.SubTotalCartSession] =subtotal;
            Session[CommonConstants.TotalCartSession] = total;
            ViewBag.discountCode = discountCode;

            return View(list);
        }


        public ActionResult AddItem(int productId, int quantity)
        {
            var product = new ProductDao().getByID(productId);
            var cart = Session[CommonConstants.CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Products.id_product == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Products.id_product == productId)
                        {
                            item.Amount += quantity;
                            item.Price = item.Products.salePrice.GetValueOrDefault(0);
                            item.IntoMoney = item.Price * item.Amount;
                        }
                    }
                }
                //trong giỏ hàng chưa có
                // tạo mới 1 đối tượng cart item
                else
                {
                    var item = new CartItem();
                    item.Products = product;
                    item.Amount = quantity;
                    item.Price = item.Products.salePrice.GetValueOrDefault(0);
                    item.IntoMoney = item.Price * item.Amount;
                    list.Add(item);

                }
                Session[CommonConstants.CartSession] = list;
                Session[CommonConstants.Count] = list.Count();

            }
            else
            {
                var item = new CartItem();
                item.Products = product;
                item.Amount = quantity;
                item.Price = item.Products.salePrice.GetValueOrDefault(0);
                item.IntoMoney = item.Price * item.Amount;
                var list = new List<CartItem>();
                list.Add(item);

                Session[CommonConstants.CartSession] = list;
                Session[CommonConstants.Count] = list.Count();
            }
            UpdateCart();
            return RedirectToAction("Index");
        }

        public JsonResult AddToDetail(string product)
        {

            var jsonCart = new JavaScriptSerializer().Deserialize<CartItem>(product);
            var item = jsonCart;
            AddItem(item.Products.id_product, item.Amount);
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return Json(new
            {
                status = true,
                count = list.Count,
                total = list.Sum(x => x.IntoMoney)
            });
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Products.id_product == item.Products.id_product);
                if (jsonItem != null)
                {
                    item.Amount = jsonItem.Amount;
                    item.Price = item.Products.salePrice.GetValueOrDefault(0);
                    item.IntoMoney = item.Price * item.Amount;
                }

            }
            Session[CommonConstants.CartSession] = sessionCart;
            Session[CommonConstants.Count] = sessionCart.Count();
            UpdateCart();
            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteAll()
        {
            Session[CommonConstants.CartSession] = null;
            Session[CommonConstants.Count] = 0;
            UpdateCart();
            return Json(new
            {
                status = true
            });
        }

        public ActionResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }

        public ActionResult Delete(int? id)
        {

            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];
            var item = sessionCart.SingleOrDefault(x => x.Products.id_product == id);
            sessionCart.Remove(item);
            Session[CommonConstants.Count] = (int)Session[CommonConstants.Count]-1 ;
            UpdateCart();
            return RedirectToAction("Index");
        }

        private void UpdateCart()
        {
             int id_customer = Int32.Parse(Session[CommonConstants.ID_SESSION].ToString());
            Cart cart = db.Carts.Where(c => c.id_customer == id_customer).FirstOrDefault();
            List<CartDetail> listCart= db.CartDetails.Where(cd => cd.id_cart == cart.id_cart).ToList();
            db.CartDetails.RemoveRange(listCart);
            db.SaveChanges();
            List<CartDetail> newListCart = new List<CartDetail>();
            foreach(var item in (List<CartItem>)Session[CommonConstants.CartSession])
            {
                newListCart.Add(new CartDetail()
                {
                    amount = item.Amount,
                    discriptionProductDetail = item.discriptionProductDetail,
                    id_cart = cart.id_cart,
                    id_product = item.Products.id_product,
                    intoMoney = item.IntoMoney,
                    price = item.Price 
                });
            }
            db.CartDetails.AddRange(newListCart);
            db.SaveChanges();
        }
        

    }
}