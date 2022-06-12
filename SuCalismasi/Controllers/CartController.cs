using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Water.Controllers
{
    public class CartController : Controller
    {
        public struct CartProduct
        {
            public Models.Product Product;
            public short Count;
            public float Total;
        }
        public string Add(short id)
        {
            string? cart = Request.Cookies["cart"];
            string[] cartItems;
            string[] itemDetails;
            short itemCount ;
            string newCart = "";    
            string cartItem;
            short totalCount=0;
            bool itemExists = false;
            CookieOptions cookieOptions = new CookieOptions();
            if (cart== null)
            {
                newCart =id.ToString()+ ":1";
                totalCount = 1;
            }
            else
            {
                cartItems = cart.Split(',');
                for(short i = 0; i < cartItems.Length; i++)
                {
                    cartItem = cartItems[i];
                    itemDetails=cartItem.Split(':');
                    itemCount = Convert.ToInt16(itemDetails[1]);
                    if (itemDetails[0] == id.ToString())
                    {
                        itemCount++;
                        itemExists=true; 
                    }
                    totalCount += itemCount;
                    newCart = newCart + itemDetails[0] + ":" + itemCount.ToString();
                    if (i < cartItems.Length - 1)
                    {
                        newCart = newCart + ",";
                    }
                }
                if (itemExists == false)
                {
                    newCart = newCart + "," + id.ToString() + ":1";
                    totalCount++;
                }              
            }
            cookieOptions.Path = "/";
            cookieOptions.Expires = DateTime.MaxValue;
            Response.Cookies.Append("cart", newCart,cookieOptions);
            return totalCount.ToString();
        }
        //public string ProductSubtotal(short id, short count)
        //{
        //    short subTotal = 0;

        //    return subTotal.ToString();
        //}
        //public string CartTotal(float[] subTotals)
        //{
        //    float cartTotal = 0;
        //    foreach(float subTotalsItem in subTotals)
        //    {
        //        cartTotal += subTotalsItem;
        //    }
        //    return cartTotal.ToString();
        //}
        public IActionResult Index()
        {
            DbContextOptions<Models.WaterContext> options = new DbContextOptions<Models.WaterContext>();
            Models.WaterContext waterContext= new Models.WaterContext(options);
            Areas.Admin.Controllers.ProductsController productsController =new Areas.Admin.Controllers.ProductsController(waterContext);
            Models.Product product;
            short productId ;           
            byte i = 0;
            string? cart = Request.Cookies["cart"];
            string[] cartItems, itemDetails;
            short itemCount;
            string cartItem;
            List<CartProduct> cartProducts = new List<CartProduct>();
            if (cart != null)
            {
                cartItems = cart.Split(',');
                for ( i = 0; i < cartItems.Length; i++)
                {
                    cartItem = cartItems[i];
                    itemDetails = cartItem.Split(':');
                    CartProduct cartProduct = new CartProduct();
                    productId = Convert.ToInt16(itemDetails[0]);
                    product = productsController.Product(productId);
                    cartProduct.Product = product;
                    cartProduct.Count= Convert.ToInt16(itemDetails[1]);
                    cartProduct.Total = cartProduct.Count * product.Price;
                    //cartProduct.products = 
                    //cartProduct.count = 

                    //itemCount = Convert.ToInt16(itemDetails[1]);

                    cartProducts.Add(cartProduct);
                    ViewData["product"] = cartProducts;
                }
               
            }  
            return View();
        }  
    }
}
