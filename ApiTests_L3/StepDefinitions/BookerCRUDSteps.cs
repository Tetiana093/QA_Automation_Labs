using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json.Linq;
using Reqnroll;
using System.Net;

namespace ApiTests_L3.StepDefinitions
{
    [Binding]
    public class BookerCRUDSteps
    {
        private RestClient _client = new RestClient("https://restful-booker.herokuapp.com");
        private RestResponse _response;
        private int _bookingId;
        private string _token;

        [Given("I authenticate as admin")]
        public void GivenIAuthenticateAsAdmin()
        {
            var request = new RestRequest("/auth", Method.Post);
            request.AddJsonBody(new { username = "admin", password = "password123" });
            _response = _client.Execute(request);
            _token = JObject.Parse(_response.Content)["token"].ToString();
        }

        [When("I create a new booking")]
        public void WhenICreateANewBooking()
        {
            var request = new RestRequest("/booking", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(new
            {
                firstname = "Tetiana",
                lastname = "Vatahovych",
                totalprice = 150,
                depositpaid = true,
                bookingdates = new { checkin = "2026-10-01", checkout = "2026-10-10" },
                additionalneeds = "Breakfast"
            });
            _response = _client.Execute(request);
            _bookingId = (int)JObject.Parse(_response.Content)["bookingid"];
        }

        [Then("the booking should be created successfully")]
        public void ThenTheBookingShouldBeCreatedSuccessfully()
        {
            
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(_bookingId, Is.GreaterThan(0));
        }

        [When("I get the booking details")]
        public void WhenIGetTheBookingDetails()
        {
            var request = new RestRequest($"/booking/{_bookingId}", Method.Get);
            request.AddHeader("Accept", "application/json");
            _response = _client.Execute(request);
        }

        [Then("the details should match the created booking")]
        public void ThenTheDetailsShouldMatchTheCreatedBooking()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var firstName = JObject.Parse(_response.Content)["firstname"].ToString();
            Assert.That(firstName, Is.EqualTo("Tetiana"));
        }

        [When("I update the booking")]
        public void WhenIUpdateTheBooking()
        {
            var request = new RestRequest($"/booking/{_bookingId}", Method.Put);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Cookie", $"token={_token}");
            request.AddJsonBody(new
            {
                firstname = "Tetiana",
                lastname = "Vatahovych",
                totalprice = 500,
                depositpaid = false,
                bookingdates = new { checkin = "2026-10-01", checkout = "2026-10-10" },
                additionalneeds = "Dinner"
            });
            _response = _client.Execute(request);
        }

        [Then("the booking should be updated successfully")]
        public void ThenTheBookingShouldBeUpdatedSuccessfully()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var price = (int)JObject.Parse(_response.Content)["totalprice"];
            Assert.That(price, Is.EqualTo(500));
        }

        [When("I delete the booking")]
        public void WhenIDeleteTheBooking()
        {
            var request = new RestRequest($"/booking/{_bookingId}", Method.Delete);
            request.AddHeader("Cookie", $"token={_token}");
            _response = _client.Execute(request);
        }

        [Then("the booking should no longer exist")]
        public void ThenTheBookingShouldNoLongerExist()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var getRequest = new RestRequest($"/booking/{_bookingId}", Method.Get);
            var getResponse = _client.Execute(getRequest);
            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}