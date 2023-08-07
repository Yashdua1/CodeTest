using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WireMock.Server;
using RestAssured.Request.Builders;
using System.Text.RegularExpressions;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace APIProject
{
    public class TestBase
    {
        public WireMockServer server;
        
        public RequestSpecification requestSpec;

        public bool IsValidURL(string str)
        {
            string strRegex = @"((http|https)://GetNextBirthday/)" + "\\d{4}\\-(0?[1-9]|1[012])\\-(0?[1-9]|[12][0-9]|3[01])";

            Regex re = new Regex(strRegex);
            if (re.IsMatch(str))
                return (true);
            else
                return (false);
        }

        [SetUp]
        public void StartServer()
        {
            if (server==null)
                server = WireMockServer.Start(9876);

            requestSpec = new RequestSpecBuilder()
                .WithHostName("localhost")
                .WithPort(9876)
                .Build();
        }

        [TearDown]
        public void StopServer()
        {
            try
            {
                server.Stop();
            }
            catch(Exception ex)
            { 
                Console.WriteLine(ex.StackTrace);
            }
        }

    }
}
