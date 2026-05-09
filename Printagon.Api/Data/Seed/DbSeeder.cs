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
                PaperGramWeight = 53,
                PaperWidth = 1144,
                CreatedAt = DateTime.Now
            };

            var roll2 = new Roll
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                RollNumber = 2,
                RollWeight = 1000,
                RollWeightLeftOver = 0,
                PaperType = "Holmen View HS",
                PaperGramWeight = 54,
                PaperWidth = 1144,
                CreatedAt = DateTime.Now
            };

            var roll3 = new Roll
            {
                Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                RollNumber = 3,
                RollWeight = 1000,
                RollWeightLeftOver = 0,
                PaperType = "Skogh papper",
                PaperGramWeight = 45,
                PaperWidth = 845,
                CreatedAt = DateTime.Now
            };

            var roll4 = new Roll
            {
                Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                RollNumber = 4,
                RollWeight = 978,
                RollWeightLeftOver = 0,
                PaperType = "Skogh papper",
                PaperGramWeight = 45,
                PaperWidth = 845,
                CreatedAt = DateTime.Now
            };



            // ----------- ORDER ROLLS -----------

            var orderRoll1 = new OrderRoll
            {
                Order = order1,
                Roll = roll1,

                IntakeWeight = 998,
                OutputWeight = 564,

                PaperType = roll1.PaperType,
                PaperGramWeight = roll1.PaperGramWeight,
                PaperWidth = roll1.PaperWidth,

                MatchesOrderPaper =
                  order1.PaperType == roll1.PaperType &&
                  order1.PaperGramWeight == roll1.PaperGramWeight &&
                  order1.PaperWidth == roll1.PaperWidth
            };

            var orderRoll2 = new OrderRoll
            {
                Order = order1,
                Roll = roll2,
                IntakeWeight = 1000,
                OutputWeight = 800
            };

            var orderRoll3 = new OrderRoll
            {
                Order = order1,
                Roll = roll3,
                IntakeWeight = 1000,
                OutputWeight = 800
            };
            var orderRoll4 = new OrderRoll
            {
                Order = order2,
                Roll = roll4,
                IntakeWeight = 1000,
                OutputWeight = 800 
            };

            var orderRoll5 = new OrderRoll
            {
                Order = order2,
                Roll = roll3,
                IntakeWeight = 1000,
                OutputWeight = 900 
            };

            var orderRoll6 = new OrderRoll
            {
                Order = order2,
                Roll = roll2,
                IntakeWeight = 980,
                OutputWeight = null
            };


          


            context.OrderRolls.AddRange(orderRoll1, orderRoll2, orderRoll3, orderRoll4, orderRoll5, orderRoll6);
            context.Orders.AddRange(order1, order2);
            context.Rolls.AddRange(roll1, roll2, roll3, roll4);

            context.SaveChanges();
        }
    }
}