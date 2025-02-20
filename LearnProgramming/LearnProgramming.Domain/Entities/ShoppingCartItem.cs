﻿namespace LearnProgramming.Domain.Entities
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; } = 1;
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
