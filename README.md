![Image description](http://blog.jonathanchannon.com/images/blogpostimages/nancy-horizontal-framed-bf-wb-620x240.png)

# Nancy Rest API Shell

 Default Nancy Rest API Console App/Windows Service

## Uses

Easy to build and copy Nancy API shell for C# .NET windows services and console apps.  
Make Rest API development easier by using this dynamic shell to create your next Rest API.


## Notes

1. This is a CORS enabled API.  You can alter this by going into the CustomBootstrapper.cs and altering
   the below code:
   
    ```
    ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "POST,GET,PUT,DELETE")
                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type, Authorization");
                                
    ```
    
2. I have separated the console app and the windows service into their own projects.  My preference is windows
   service due to its ability to self host and can manage from the Windows Services Console.  This also gives
   you the ability to create separate services with separate APIs.
   
   
   
 # Happy Coding!
