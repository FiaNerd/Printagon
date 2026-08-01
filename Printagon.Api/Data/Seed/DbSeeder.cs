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
                Id = Guid.NewGuid(),
                OrderNumber = 1111,
                JobName = "Ica",
                PaperType = "Holmen View HS",
                PaperGramWeight = 53,
                PaperWidth = 1144,
                OrderStatus = OrderStatus.Active,
                CreatedAt = DateTime.Now
            };

            var order2 = new Order
            {
                Id = Guid.NewGuid(),
                OrderNumber = 2222,
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
                Id = Guid.NewGuid(),
                RollNumber = 1,
                RollWeight = 998,
                RollWeightLeftOver = 0,
                CreatedAt = DateTime.Now
            };

            var roll2 = new Roll
            {
                Id = Guid.NewGuid(),
                RollNumber = 2,
                RollWeight = 1000,
                RollWeightLeftOver = 0,
                CreatedAt = DateTime.Now
            };

            var roll3 = new Roll
            {
                Id = Guid.NewGuid(),
                RollNumber = 3,
                RollWeight = 1000,
                RollWeightLeftOver = 0,
                CreatedAt = DateTime.Now
            };

            var roll4 = new Roll
            {
                Id = Guid.NewGuid(),
                RollNumber = 4,
                RollWeight = 978,
                RollWeightLeftOver = 0,
                CreatedAt = DateTime.Now
            };

            // ----------- ORDER ROLLS -----------
            OrderRoll CreateOrderRoll(Order order, Roll roll, int intake, int output)
            {
                return new OrderRoll
                {
                    Id = Guid.NewGuid(),
                    OrderNumber = order.OrderNumber,
                    RollNumber = roll.RollNumber,

                    IntakeWeight = intake,
                    OutputWeight = output,

                    PaperType = order.PaperType,
                    PaperGramWeight = order.PaperGramWeight,
                    PaperWidth = order.PaperWidth,

                    MatchesOrderPaper = true,
                    CreatedAt = DateTime.Now
                };
            }

            var orderRoll1 = CreateOrderRoll(order1, roll1, 998, 564);
            var orderRoll2 = CreateOrderRoll(order1, roll2, 1000, 800);
            var orderRoll3 = CreateOrderRoll(order1, roll3, 1000, 800);

            var orderRoll4 = CreateOrderRoll(order2, roll4, 1000, 800);
            var orderRoll5 = CreateOrderRoll(order2, roll3, 1000, 900);
            var orderRoll6 = CreateOrderRoll(order2, roll2, 980, 0);

            context.Orders.AddRange(order1, order2);
            context.Rolls.AddRange(roll1, roll2, roll3, roll4);
            context.OrderRolls.AddRange(orderRoll1, orderRoll2, orderRoll3, orderRoll4, orderRoll5, orderRoll6);

            context.SaveChanges();
        }
    }
}
