﻿using shopapp.business.Abstract;
using shopapp.data.Abstract;
using shopapp.entityy;
using System;
using System.Collections.Generic;
using System.Text;

namespace shopapp.business.Concrete
{
    public class CardManager : ICardService
    {
        private ICardRepository _cardRepository;
        public CardManager(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public void AddToCard(string userId, int productId, int quantity)
        {
            var card = GetCardByUserId(userId);
            if (card!=null)
            {
                //Product sebetde varmi
                //Product sebetde var ve yeni product elave edilir
                var index = card.CardItems.FindIndex(i => i.ProductId == productId);
                if (index<0)
                {
                    card.CardItems.Add(new CardItem()
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CardId = card.Id
                    });
                }
                else
                {
                    card.CardItems[index].Quantity += quantity;
                }
                _cardRepository.Update(card);
            }
        }

        public void DeleteFromCard(string userId, int productId)
        {
            var card = GetCardByUserId(userId);
            if (card != null)
                _cardRepository.DeleteFromCard(card.Id, productId);
        }

        public Card GetCardByUserId(string userId)
        {
            return _cardRepository.GetByUserId(userId);
        }

        public void InitializeCard(string userId)
        {
            _cardRepository.Create(new Card() {UserId=userId });
        }
    }
}
