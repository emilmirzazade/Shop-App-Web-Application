using shopapp.entityy;
using System;
using System.Collections.Generic;
using System.Text;

namespace shopapp.data.Abstract
{
    public interface ICardRepository : IRepository<Card>
    {
        Card GetByUserId(string userId);
        void DeleteFromCard(int cardId, int productId);
    }
}
