using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using WMAgility2;
using Xunit;

namespace WMAgility.Tests
{
    [Binding]
    public class Steps
        {
            private WebApplicationFactory<Startup> factory;
            private HttpClient client;
            private IWebHost host;
            private IServiceScope scope;
            private HttpResponseMessage response;

            public Steps()
            {
                Init();
            }

            private void Init()
            {
                System.Console.WriteLine("init");
                factory = new WebApplicationFactory<Startup>();
                client = factory.CreateClient();
                host = factory.Server?.Host;
                scope = host.Services.CreateScope();
            }

            ~Steps()
            {
                System.Console.WriteLine("disposing");
                if (scope != null)
                    scope.Dispose();
                if (client != null)
                    client.Dispose();
                if (factory != null)
                    factory.Dispose();
            }

            [When(@"I go to page '([^\']*)'")]
            public async Task WhenIGoToPage(string uri)
            {
                response = await client.GetAsync(uri);
            }

            [Then(@"the http result should be OK")]
            public void TheHTTPResultShouldBeOK()
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
            }

            [Then(@"the body contains '([^\']*)'")]
            public async void TheBodyContains(string str)
            {
                var parser = new HtmlParser();
                var content = await response.Content.ReadAsStringAsync();
                var document = parser.ParseDocument(content);
                Assert.NotNull(document);
                Assert.Contains(str, document.Body.TextContent);
            }
        }
    }
