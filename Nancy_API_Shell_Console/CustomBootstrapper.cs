using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

/*  With a Custom BootStrapper, you can control the requests
 *  and responses globally using Before/After requests.
 * 
 *  Had to accomidate for "OPTIONS" calls which occur before the main
 *  method.
 *  
 *  This API is CORS enabled:
 *  https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS
 * 
 */

namespace Nancy_API_Shell_Console
{
    public class CustomNancyBootstrapper : DefaultNancyBootstrapper
    {
        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            //Before requests occurs before each method is called
            pipelines.BeforeRequest.AddItemToStartOfPipeline((ctx) =>
            {
                //Web browsers will send an "OPTIONS" call before each "POST/GET/PUT/etc..."
                //No header information will appear due to security during an OPTIONS call
                if (ctx.Request.Method == "OPTIONS")
                {
                    return null;
                }
                else
                {
                    //get token from header
                    var token = ctx.Request.Headers.Authorization;

                    //validate using Auth process, returns true or false
                    var Valid = true;

                    //set to null if true
                    if (Valid)
                    {
                        return null;
                    }
                    else
                    {
                        //return an Unauthorized status code if token failed to validate
                        //Optionaly: you can create a Response and return the Reponse if desired
                        return HttpStatusCode.Unauthorized;
                    }
                };

            });

            //After request occur after each method is called
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                if (ctx.Request.Method != "OPTIONS")
                {
                   //perform post request functions here
                }

                //This adds CORS enabling headers into the method response.
                //This conicides with the "OPTIONS" method call that occurs before
                //   each main method call.  Make sure to adjust Allow-Methods and Allow-Headers below.
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                                .WithHeader("Access-Control-Allow-Methods", "POST,GET,PUT,DELETE")
                                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type, Authorization");

            });
        }
    }
}
