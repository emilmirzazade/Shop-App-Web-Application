using Microsoft.EntityFrameworkCore;
using shopapp.data.Abstract;
using shopapp.entityy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shopapp.data.Concretee.EfCore
{
    public class EFCoreCardRepository : EfCoreGenericRepository<Card, ShopContext>, ICardRepository
    {
        public void DeleteFromCard(int cardId, int productId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"delete from CardItems where CardId=@p0 and ProductId=@p1";
                context.Database.ExecuteSqlRaw(cmd, cardId, productId);
            }
        }

        public Card GetByUserId(string userId)
        {
            using (var context = new ShopContext())
            {
                return context.Cards
                              .Include(i => i.CardItems)
                              .ThenInclude(i => i.Product)
                              .FirstOrDefault(i=>i.UserId==userId);
                              
            }
        }
        public override void Update(Card entity)
        {
            using (var context = new ShopContext())
            {
                context.Cards.Update(entity);
                context.SaveChanges();
            }
        }
    }
}
