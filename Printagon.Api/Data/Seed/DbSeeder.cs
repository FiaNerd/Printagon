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
                return;

            // ----------- ORDERS -----------
            var order1 = new Order
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                OrderNumber = 123,
                JobName = "Ica",
                PaperType = "Holmen View HS",
                PaperGramWeight = 53,
                PaperWidth = 1144,
                OrderStatus = OrderStatus.Active,
                CreatedAt = DateTime.Now
            };

            var order2 = new Order
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                OrderNumber = 234,
                JobName = "Hemmets Journal",
                PaperType = "Skogh papper",
                PaperGramWeight = 45,
                PaperWidth = 845,
                OrderStatus = OrderStatus.Paused,
                CreatedAt = DateTime.Now
            };

            // ----------- ROLLS -----------
            var roll1 = new Roll
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                RollNumber = 1,
                RollWeight = 998,
                RollWeightLeftOver = 0,
                PaperType = "Holmen View HS",
                CreatedAt = DateTime.Now
            };

            var roll2 = new Roll
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                RollNumber = 2,
                RollWeight = 1000,
                RollWeightLeftOver = 0,
                PaperType = "Holmen View HS",
                CreatedAt = DateTime.Now
            };

            var roll3 = new Roll
            {
                Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                RollNumber = 3,
                RollWeight = 1000,
                RollWeightLeftOver = 0,
                PaperType = "Skogh papper",
                CreatedAt = DateTime.Now
            };


            // ----------- ORDER ROLLS -----------
            var orderRoll1 = new OrderRoll
            {
                Order = order1,
                Roll = roll1,
                IntakeWeight = 998,
                OutputWeight = 564 
            };

            var orderRoll2 = new OrderRoll
            {
                Order = order2,
                Roll = roll2,
                IntakeWeight = 1000,
                OutputWeight = 800 
            };

            var orderRoll3 = new OrderRoll
            {
                Order = order2,
                Roll = roll3,
                IntakeWeight = 1000,
                OutputWeight = 900 
            };



            context.OrderRolls.AddRange(orderRoll1, orderRoll2, orderRoll3);
            context.Orders.AddRange(order1, order2);
            context.Rolls.AddRange(roll1, roll2, roll3);

            context.SaveChanges();
        }
    }
}