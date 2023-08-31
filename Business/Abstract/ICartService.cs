using Entities.Concrete;
using Entities.DomainModals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICartService
    {
        void AddToCart(Cart cart, Product product);
        void RemoverFromCart(Cart cart, int productId);
        List<CartLine> List(Cart cart);
    }
}
