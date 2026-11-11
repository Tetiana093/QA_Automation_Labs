using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json.Linq;
using Reqnroll;
using System.Net;

namespace ApiTests_L3.StepDefinitions
{
    [Binding]
    public class PublicApiSteps
    {
        private RestClient _client = new RestClient("https://catfact.ninja");
        private RestResponse _response;

        [When("I request a random cat fact")]
        public void WhenIRequestARandomCatFact()
        {
            var request = new RestRequest("/fact", Method.Get);
            _response = _client.Execute(request);
        }

        [Then("the status code should be 200 OK")]
        public void ThenTheStatusCodeShouldBe200OK()
        {
            // Використовуємо новий синтаксис NUnit 4
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then("the response should contain a text fact")]
        public void ThenTheResponseShouldContainATextFact()
        {
            var json = JObject.Parse(_response.Content);
            var fact = json["fact"].ToString();

            // Замінюємо IsNotNull та IsTrue
            Assert.That(fact, Is.Not.Null);
            Assert.That(fact.Length, Is.GreaterThan(0), "Факту немає у відповіді!");
        }
    }
}