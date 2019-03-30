using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System;

/*  Modules can be added as simple as created a new Class (.cs) item
 *  to the project.  Make sure to set the class as PUBLIC and a NANCYMODULE.
 *  
 *  Added URL parameters example.  Remmed out due to objects needing to exist
 *  in URL.
 *  
 *  Request body is handled in JSON which is why I use the Newtonsoft.Json framework
 *  
 *  For more info, see Nancy documentation:
 *  https://github.com/NancyFx/Nancy/wiki/Documentation
 * 
 */

namespace Nancy_API_Shell_Console
{
    public class TestModule : NancyModule
    {
        public TestModule()
        {
            /*
             * Get example can also be a full function but this example
             * best suits the simplicity of doing a GET in Nancy
            */
            Get["/getTest"] = parameters => testObj;

            /*
             * Parameters can be passed in the URL link.  This is optional.
             * Ex: http://localhost:1234?myparam1={id1}&myparam2={id2} 
            */
            Post["/test"] = parameters =>
            {
                /*
                 * //example url parameters
                 * var urlParm1 = parameters.myparam1;
                 * var urlParm2 = parameters.myparam2;
                */

                //Bind your request to the class from the Request.Body
                var model = this.Bind<TestRequest>();

                //Create response object
                TestResponse testResponse = new TestResponse();

                //Http response code
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

        //This is used for the GET method example
        private static TestResponse testObj = new TestResponse()
        {
            success = true,
            err = null,
            output = "Successfully made a Nancy GET method call!"
        };

    }
    
}
