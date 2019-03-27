using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System;

namespace Nancy_API_Shell
{
    public class TestModule : NancyModule
    {
        public TestModule()
        {
            //parameters can be passed in the URL link.  This is optional.
            Post["/test"] = parameters =>
            {
                //bind your request to the class
                var model = this.Bind<TestRequest>();

                //create response object
                TestResponse testResponse = new TestResponse();

                //http response code
                HttpStatusCode httpStatusCode = new HttpStatusCode();

                try
                {
                    //do you process in here, return successful response with output message and default 200 status code
                    testResponse.success = true;
                    testResponse.output = "Output Message here";
                    httpStatusCode = HttpStatusCode.OK;
                }
                catch(Exception ex)
                {
                    //process failed, return message generic message and default http 500 response code
                    testResponse.success = false;
                    testResponse.err = ex.Message;
                    httpStatusCode = HttpStatusCode.InternalServerError;
                }


                //http response, use the Newsoft json serialize instead of Response.AsJson
                Response response = JsonConvert.SerializeObject(testResponse); //fill in a response based off of a public class/structure
                response.ContentType = "application/json";
                response.Headers.Add("Vary", "Accept");
                response.StatusCode = httpStatusCode;

                return response;
            };
        }

        //modify test request based on incoming json format
        public class TestRequest
        {
            public string input1 { get; set; }
            public string input2 { get; set; }
        }

        //create ouput response object that will be serialized to json format
        public class TestResponse
        {
            public bool success { get; set; }
            public object err { get; set; }
            public string output { get; set; }
        }

    }
    
}
