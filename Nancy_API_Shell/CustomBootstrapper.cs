using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Nancy_API_Shell
{
    public class CustomNancyBootstrapper : DefaultNancyBootstrapper
    {
        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            //validate
            pipelines.BeforeRequest.AddItemToStartOfPipeline((ctx) =>
            {
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

                    //set to null if 
                    if (Valid)
                    {
                        return null;
                    }
                    else
                    {
                        return HttpStatusCode.Unauthorized;
                    }
                };

            });

            //CORS Enable
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                if (ctx.Request.Method != "OPTIONS")
                {
                   //perform post request functions here
                }

                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                                .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type, Authorization");

            });
        }
    }
}
