using _4thYearProject.Server.Services;
using _4thYearProject.Shared;
using _4thYearProject.Shared.Models.BusinessLogic;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Pages
{
    public partial class UserPanel
    {
        private const double FOTOSTOP_TAX = 0.20;
        private List<CategoryItem> PieChartItems;
        private List<OrderLineItemData> RevenueChartItems;
        private double RevenueTotal;

        [Inject] public IUserService UserService { get; set; }

        [Inject] public IUserDataService UserDataService { get; set; }

        [Inject] public IShoppingCartService ShoppingCartService { get; set; }

        public List<OrderLineItem> lineItems { get; set; }

        private ClaimsPrincipal Identity { get; set; }

        private string ClaimID { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Identity = await UserService.GetUserAsync();
            //First get user claims
            ClaimID = Identity.Claims.Where(c => c.Type.Equals("sub"))
                .Select(c => c.Value).SingleOrDefault().ToString();

            lineItems = (await ShoppingCartService.GetOrderLinesForArtist(ClaimID)).ToList();

            PieChartItems = calculatePieCharts(lineItems);
            RevenueTotal = totalRevenue(lineItems);
        }



        public double totalRevenue(List<OrderLineItem> items)
        {
            double Revenue = 0.0;
            foreach (var item in items)
            {
                Revenue += item.Price;
            }

            Revenue -= Math.Round(Revenue * FOTOSTOP_TAX, 2, MidpointRounding.ToEven);
            return Revenue;
        }


        public List<CategoryItem> calculatePieCharts(List<OrderLineItem> lineItems)
        {
            var categoryItems = new List<CategoryItem>();
            double PrintRevenue = 0;
            double ShirtRevenue = 0;
            double LicenseRevenue = 0;

            foreach (var orderLineItem in lineItems)
                switch (orderLineItem.Type)
                {
                    case "Print" when orderLineItem.OrderId != null:
                        PrintRevenue += orderLineItem.Price;
                        break;
                    case "Shirt" when orderLineItem.OrderId != null:
                        ShirtRevenue += orderLineItem.Price;
                        break;
                    case "License" when orderLineItem.OrderId != null:
                        LicenseRevenue += orderLineItem.Price;
                        break;
                }

            if (PrintRevenue != 0)
                PrintRevenue -= Math.Round(PrintRevenue * FOTOSTOP_TAX, 2,
                    MidpointRounding.AwayFromZero);

            if (ShirtRevenue != 0) ShirtRevenue -= Math.Round(ShirtRevenue * FOTOSTOP_TAX, 2, MidpointRounding.AwayFromZero);

            if (LicenseRevenue != 0)
                LicenseRevenue -= Math.Round(LicenseRevenue * FOTOSTOP_TAX, 2, MidpointRounding.AwayFromZero);


            categoryItems.Add(new CategoryItem { Type = "Print", Revenue = PrintRevenue });
            categoryItems.Add(new CategoryItem { Type = "Shirt", Revenue = ShirtRevenue });
            categoryItems.Add(new CategoryItem { Type = "License", Revenue = LicenseRevenue });

            return categoryItems;
        }



        public class CategoryItem
        {
            public string Type { get; set; }
            public double Revenue { get; set; }
        }

        public class OrderLineItemData
        {
            public DateTime Date { get; set; }
            public double Revenue { get; set; }
        }
    }
}
