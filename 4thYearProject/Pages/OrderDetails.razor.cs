using System.Data;
using System.IO;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace _4thYearProject.Server.Pages
{
    using _4thYearProject.Server.Services;
    using _4thYearProject.Shared;
    using _4thYearProject.Shared.Models.BusinessLogic;
    using Microsoft.AspNetCore.Components;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public partial class OrderDetails : ComponentBase
    {

        [Inject]
        public IUserDataService UserDataService { get; set; }

        [Inject]
        public IUserService _userService { get; set; }

        [Inject]
        public IShoppingCartService shoppingCartDataService { get; set; }

        [Inject]
        public NavigationManager UriHelper { get; set; }
        
        [Inject]
        public IHttpClientFactory _clientFactory { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }

        [Parameter]
        public string OrderId { get; set; }

        private Order Order;
        private double X { get; set; }
        private double Y { get; set; }


        public double TotalPrice { get; set; }
        internal ClaimsPrincipal identity;

        protected async override Task OnInitializedAsync()
        {

            //TODO validate user is owner
            identity = await _userService.GetUserAsync();


            string LoggedInID = identity.Claims.Where(c => c.Type.Equals("sub"))
                      .Select(c => c.Value).SingleOrDefault().ToString();


            Order = (await shoppingCartDataService.GetOrderById(Int32.Parse(OrderId)));

                if (Order.OrderAddress.UserPostcode != null)
                {
                    var (item1, item2) = await GetCoordsAsync(Order.OrderAddress.UserAddress + " " + Order.OrderAddress.UserAddress2 + " " + Order.OrderAddress.UserCountry + " " + Order.OrderAddress.UserCity + " " + Order.OrderAddress.UserPostcode);
                    X = item1;
                    Y = item2;
                }

        }


        void Navigate()
        {
            UriHelper.NavigateTo("/orders/");
        }


        public async Task<Tuple<double, double>> GetCoordsAsync(string postcode)
        {
            var url = string.Format("https://maps.google.com/maps/api/geocode/xml?key={1}&address={0}&sensor=false", Uri.EscapeDataString(postcode), Configuration["GoogleMapAPI"]);

            Console.WriteLine(url);

            var request = new HttpRequestMessage(HttpMethod.Get,
                url);

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);


            var recieveStream = await response.Content.ReadAsStreamAsync();
            var encode = Encoding.GetEncoding("utf-8");

            var readstream = new StreamReader(recieveStream, encode);

            var dsResult = new DataSet();

            dsResult.ReadXml(readstream);

            readstream.Close();

            var output = new Tuple<double, double>(0, 0);

            var dt = new DataTable();

            foreach (DataRow row in dsResult.Tables["result"].Rows)
            {
                var geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"])[0]["geometry_id"].ToString();

                var location = dsResult.Tables["location"].Select("geometry_id=" + geometry_id)[0];

                output = Tuple.Create(Convert.ToDouble(location["lat"]), Convert.ToDouble(location["lng"]));
            }
            ;

            return output;
        }

    }
}

