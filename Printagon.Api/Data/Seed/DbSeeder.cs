using Printagon.Api.Enums;
using Printagon.Api.Models;


namespace Printagon.Api.Data.Seed
{
    public class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Orders.Any())
            {
                return;
            }

            // ----------- ORDERS -----------
            var order1 = new Order
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                OrderNumber = 123,
                JobName = "Ica",
                YearlyNumber = 2,
                PaperType = "Holmen View HS",
                GramWeight = 53,
                RollWidth = 1144,
                OrderStatus =  OrderStatus.Active,
                CreatedAt = DateTime.Now,
                Rolls = new List<Roll>()
            };

            // ----------- ORDERS -----------
            var order2 = new Order
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                OrderNumber = 234,
                JobName = "Hemmets Journal",
                YearlyNumber = 1,
                PaperType = "Skogh papper",
                GramWeight = 45,
                RollWidth = 845,
                OrderStatus = OrderStatus.Paused,
                CreatedAt = DateTime.Now,
                Rolls = new List<Roll>()
            };

            var order3 = new Order
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                OrderNumber = 234,
                JobName = "Hemmets Journal",
                YearlyNumber = 1,
                PaperType = "Skogh papper",
                GramWeight = 45,
                RollWidth = 845,
                OrderStatus = OrderStatus.Completed,
                CreatedAt = DateTime.Now,
                Rolls = new List<Roll>()
            };

            // ----------- ROLLS -----------
            var roll1 = new Roll
            {
                Id = Guid.NewGuid(),
                PaperType = "Holmen View HS",
                GramWeight = 53,
                RollWidth = 1144,
                RollNumber = 1,
                RollWeight = 1000,
                Comment = "First roll",
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Orders = new List<Order> { order1 }
            };

            var roll2 = new Roll
            {
                Id = Guid.NewGuid(),
                PaperType = "Holmen View HS",
                GramWeight = 53,
                RollWidth = 1144,
                RollNumber = 2,
                RollWeight = 1000,
                Comment = "Second roll",
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Orders = new List<Order> { order1 }
            };

            var roll3= new Roll
            {
                Id = Guid.NewGuid(),
                PaperType = "Holmen View HS",
                GramWeight = 52,
                RollWidth = 845,
                RollNumber = 2,
                RollWeight = 1000,
                RollWeightLeftOver = 500,
                Comment = "Second roll",
                CreatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Orders = new List<Order> { order2 }
            };

            // Koppla ihop båda sidor
            order1.Rolls.Add(roll1);
            context.Rolls.AddRange(roll1, roll2);
            order1.Rolls.Add(roll2);
            order2.Rolls.Add(roll3);

            // Lägg till i DbContext
            context.Orders.Add(order1);
            context.Orders.Add(order2);


            context.Rolls.AddRange(roll3);

            context.SaveChanges();
        }
    }
}