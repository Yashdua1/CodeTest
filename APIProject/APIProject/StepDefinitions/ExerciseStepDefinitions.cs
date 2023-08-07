using System;
using System.Net;
using TechTalk.SpecFlow;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using NUnit.Framework;
using static RestAssured.Dsl;
using TechTalk.SpecFlow.Assist;

namespace APIProject.StepDefinitions
{
    [Binding]
    public class ExerciseStepDefinitions
    {
        private TestBase _test;
    
        public ExerciseStepDefinitions(TestBase test)
        {
            _test = test;
        }

        public void SetupPositiveStub(string strDOB, string strNextDOB)
        {
            _test.StartServer();

            _test.server.Given(
                    Request.Create().UsingPost().WithPath("/GetNextBirthday")
                         //.WithBody(new RegexMatcher("^\\d{4}\\-(0?[1-9]|1[012])\\-(0?[1-9]|[12][0-9]|3[01])$"))
                         .WithBody("DOB: " + strDOB)
                    )

                .RespondWith(Response.Create()
                                     .WithStatusCode(201)
                 );

            _test.server.Given(
                Request.Create().UsingGet().WithPath("/GetNextBirthday/"+strDOB)
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(200)
                .WithBody("Next Birthday DOB: " + strNextDOB)
            ); 

        }

        public void SetupNegativeStub(string strDOB, int statusCode)
        {
            _test.StartServer();

            _test.server.Given(
                    Request.Create().UsingPost().WithPath("/GetNextBirthday")
                         //.WithBody(new RegexMatcher("^\\d{4}\\-(0?[1-9]|1[012])\\-(0?[1-9]|[12][0-9]|3[01])$"))
                         .WithBody("DOB: " + strDOB)
                    )

                .RespondWith(Response.Create()
                                     .WithStatusCode(201)
                 );

            _test.server.Given(
                Request.Create().UsingGet().WithPath("/GetNextBirthday/" + strDOB)
            )
            .RespondWith(
                Response.Create()
                .WithStatusCode(statusCode)                
            );

        }

        public void SetupNegativeStub(int statusCode)
        {
            _test.StartServer();

            _test.server.Given(
                    Request.Create().UsingPost().WithPath("/GetNextBirthday")
                    )

                .RespondWith(Response.Create()
                                     .WithStatusCode(statusCode)
                 );
        }

        [Given(@"I want to ""([^""]*)"" resource /GetNextBirthday with DOB ""([^""]*)"", api execution returns NextBirthday ""([^""]*)""")]
        public void GivenIWantToResourceGetNextBirthdayWithDOB(string method, string strDOB, string strNextDOB)
        {
            SetupPositiveStub(strDOB, strNextDOB);

            Given()
                .Spec(_test.requestSpec)
                .Body("DOB: "+ strDOB)
                .When()
                .Post("/GetNextBirthday")
                .Then()
                .StatusCode(HttpStatusCode.Created);


            Given()
                .Spec(_test.requestSpec)
                .When()
                .Get("/GetNextBirthday/" + strDOB)
                .Then()
                .StatusCode(HttpStatusCode.OK)
                .Body("Next Birthday DOB: " + strNextDOB);

            _test.StopServer();
        }

        
        [Given(@"I want to ""([^""]*)"" resource /GetNextBirthday with (.*)(.*), api execution returns <NextBirthday>")]
        public void GivenIWantToResourceGetNextBirthdayWithApiExecutionReturnsNextBirthday(string strMethod, string strDOB, string strNextDob)
        {
            GivenIWantToResourceGetNextBirthdayWithDOB(strMethod, strDOB, strNextDob);
        }

        public void GivenIWantToResourceGetNextBirthdayWithDOB(string method, string strDOB, int code)
        {
            SetupNegativeStub(strDOB, code);

            Given()
                .Spec(_test.requestSpec)
                //.Body("DOB: 2001-12-31")
                .Body("DOB: " + strDOB)
                .When()
                .Post("/GetNextBirthday")
                .Then()
                .StatusCode(HttpStatusCode.Created);


            Given()
                .Spec(_test.requestSpec)
                .When()
                //.Get("/GetNextBirthday/2001-12-31")
                .Get("/GetNextBirthday/" + strDOB)
                .Then()
                .StatusCode(code);
                //.Body("Next Birthday DOB: 2023-12-31");

            _test.StopServer();
        }

        /*[Given(@"I want to ""([^""]*)"" resource /GetNextBirthday with (.*)(.*), api execution returns (.*)")]
        public void GivenIWantToResourceGetNextBirthdayWithApiExecutionReturns(string strMethod, string strDOB, int iStatusCode)
        {

            GivenIWantToResourceGetNextBirthdayWithDOB(strMethod, strDOB, iStatusCode);
        }*/

        [Given(@"I want to ""([^""]*)"" resource /GetNextBirthday with (.*)(.*), api execution returns status (.*)")]
        public void GivenIWantToResourceGetNextBirthdayWithApiExecutionReturns(string strMethod, string strDOB, string p2, string iStatusCode)
        {
            GivenIWantToResourceGetNextBirthdayWithDOB(strMethod, strDOB, Convert.ToInt32(iStatusCode));
        }

        [Given(@"I want to ""([^""]*)"" resource /GetNextBirthday with ""([^""]*)"", API execution returns status ""([^""]*)""")]
        public void GivenIWantToResourceGetNextBirthdayWithAPIExecutionReturnsStatus(string strMethod, string strDOB, string code)
        {
            int statusCode = Convert.ToInt32(code);
            SetupNegativeStub(statusCode);

            Given()
                .Spec(_test.requestSpec)
                .When()
                .Post("/GetNextBirthday")
                .Then()
                .StatusCode(statusCode);

            _test.StopServer();
        }


        [Then(@"The response schema matches ""([^""]*)""")]
        public void ThenTheResponseSchemaMatches(string p0)
        {
            throw new PendingStepException();
        }

        










    }
}
