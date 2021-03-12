using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CnWeb_FastFood.Models.Dao.Client;
using CnWeb_FastFood.Models.EF;
using CnWeb_FastFood.Models;
using CnWeb_FastFood.Areas.Admin.Models;
using System.Data.Entity;


namespace CnWeb_FastFood.Controllers
{
    public class CustomerLoginController : Controller
    {
        // GET: CustomerLogin
        SnackShopDBContext db = new SnackShopDBContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var dao = new CustomerDao();
                var result = dao.Login(collection["inputUserName"].ToString(), collection["UserPassword"].ToString());
                if (result == 1)
                {
                    var user = dao.GetById(collection["inputUserName"].ToString());
                    var userSession = new UserLogin();
                    userSession.Name = user.name;
                    userSession.UserID = user.id_customer;
                    userSession.Avatar = user.avatar;

                    Session.Add(CommonConstants.USER_SESSION, userSession.Name);
                    Session.Add(CommonConstants.ID_SESSION, userSession.UserID);
                    Session.Add(CommonConstants.AVATAR_SESSION, userSession.Avatar);
                    //thêm giỏ hàng
                    
                    Cart cart = db.Carts.Where(c =>c.id_customer == user.id_customer).FirstOrDefault();
                    // nếu tìm thấy giỏi hàng thì tiến hành thêm vào session
                    if(cart != null)
                    {
                        Session[CommonConstants.SubTotalCartSession] = (cart == null) ? 0 : cart.subtotal;
                        Session[CommonConstants.TotalCartSession] = (cart == null) ? 0 : cart.total;
                        var listCartDetail = db.CartDetails.Where(x => x.id_cart == cart.id_cart);
                        List<CartItem> list = new List<CartItem>();
                        foreach (var item in listCartDetail)
                        {
                            list.Add(new CartItem()
                            {
                                Products = item.Product,
                                Amount = item.amount.GetValueOrDefault(0),
                                Price = item.price.GetValueOrDefault(0),
                                IntoMoney = item.intoMoney.GetValueOrDefault(0),
                                discriptionProductDetail = item.discriptionProductDetail

                            });
                        }
                        Session.Add(CommonConstants.CartSession, list);
                        Session.Add(CommonConstants.Count, list.Count());

                        //tính tổng tiền giỏ hàng
                        decimal discount = 0;

                        if (!string.IsNullOrEmpty(cart.id_discountCode))
                        {
                            var dc = db.DiscountCodes.Find(cart.id_discountCode);
                            discount = dc.discount.GetValueOrDefault(0);

                        }
                        decimal subtotal = list.Sum(x => x.IntoMoney);
                        decimal total = subtotal - discount;
                        if (total < 0) total = 0;
                        Session[CommonConstants.SubTotalCartSession] = subtotal;
                        Session[CommonConstants.TotalCartSession] = total;
                    }
                   

                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Account isn't exist");
                }
                else
                {
                    ModelState.AddModelError("", "Login false");
                }
            }
            return RedirectToAction("Index");
        }
    }
}