// using System;

// // using Coravel;
// // using Coravel.Scheduling.Schedule.Interfaces;
// using Microsoft.AspNetCore.Http;
// using System.Threading.Tasks;
// using System.IO;
// using Microsoft.AspNetCore.Http.Internal;
// using System.Text;
// using BlueLight.Main;
// using Microsoft.Extensions.DependencyInjection;

// namespace ExchangeLemonCore
// {

//     public class RequestResponseLoggingMiddleware
//     {
//         private readonly RequestDelegate _next;

//         public RequestResponseLoggingMiddleware(RequestDelegate next)
//         {
//             _next = next;

//         }




//         public async Task Invoke(HttpContext context)
//         {

//             var loggingContext = LoggingContext.Generate();

//             //First, get the incoming request
//             var request = await FormatRequest(context.Request);

//             var userName = context.User.Identity.Name;
//             var input = new LogHttpInput()
//             {
//                 data = request.data,
//                 method = request.method,
//                 path = request.path,
//                 userName = userName
//             };

//             Guid sessionId = await LogRequest(loggingContext, input);

//             //Copy a pointer to the original response body stream
//             var originalBodyStream = context.Response.Body;

//             //Create a new memory stream...
//             using (var responseBody = new MemoryStream())
//             {
//                 //...and use that for the temporary response body
//                 context.Response.Body = responseBody;

//                 //Continue down the Middleware pipeline, eventually returning to this class
//                 await _next(context);

//                 //Format the response from the server
//                 var response = await FormatResponse(context.Response);




//                 //TODO: [sample_app] Save log to chosen datastore
//                 await LogResponse(loggingContext, sessionId, response.data, response.statusCode);

//                 //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
//                 await responseBody.CopyToAsync(originalBodyStream);
//             }
//         }

//         private async Task LogResponse(LoggingContext loggingContext, Guid sessionId, string response, int statusCode)
//         {
//             if (!FeatureRepo.UseRequestWebLog)
//             {
//                 return;
//             }

//             var httpRaw = new LogHttpRaw()
//             {
//                 IsRequest = false,
//                 Content = response,
//                 // SessionId = sessionId,
//                 StatusCode = statusCode

//             };
//             loggingContext.LogHttpRaws.Add(httpRaw);
//             await loggingContext.SaveChangesAsync();
//         }

//         class LogHttpInput : LogHttpBase
//         {
//             public string userName;
//         }


//         class LogHttpBase
//         {
//             public string data;
//             public string path;
//             public string method;

//         }
//         private async Task<Guid> LogRequest(LoggingContext loggingContext, LogHttpInput input)
//         {

//             var sessionId = Guid.NewGuid();

//             if (!FeatureRepo.UseRequestWebLog)
//             {
//                 return sessionId;
//             }

//             var httpRaw = new LogHttpRaw()
//             {
//                 IsRequest = true,
//                 Content = input.data,
//                 // SessionId = sessionId,
//                 Path = input.path,
//                 Method = input.method,
//                 UserName = input.userName
//             };
//             loggingContext.LogHttpRaws.Add(httpRaw);
//             await loggingContext.SaveChangesAsync();
//             return sessionId;
//         }

//         private async Task<LogHttpBase> FormatRequest(HttpRequest request)
//         {
//             var body = request.Body;

//             //This line allows us to set the reader for the request back at the beginning of its stream.
//             //request.EnableRewind();

//             ////We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
//             //var buffer = new byte[Convert.ToInt32(request.ContentLength)];

//             ////...Then we copy the entire request stream into the new buffer.
//             //await request.Body.ReadAsync(buffer, 0, buffer.Length);
//             //request.Body.Position = 0;


//             var ms = new MemoryStream();
//             await request.Body.CopyToAsync(ms);
//             //We convert the byte[] into a string using UTF8 encoding...
//             //var bodyAsText = Encoding.UTF8.GetString(buffer);

//             var reader = new StreamReader(ms);
//             string bodyAsText = reader.ReadToEnd();


//             //..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
//             request.Body = body;

//             var method = request.Method;


//             var output = $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
//             var path = request.Path;


//             var result = new LogHttpInput()
//             {
//                 method = method,
//                 path = path,
//                 data = output
//             };
//             return result;
//             //return output;
//         }

//         private async Task<(string data, int statusCode)> FormatResponse(HttpResponse response)
//         {
//             //We need to read the response stream from the beginning...
//             response.Body.Seek(0, SeekOrigin.Begin);

//             //...and copy it into a string
//             string text = await new StreamReader(response.Body).ReadToEndAsync();

//             //We need to reset the reader for the response so that the client can read it.
//             response.Body.Seek(0, SeekOrigin.Begin);

//             //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
//             //var output =  $"{response.StatusCode}: {text}";
//             (string data, int statusCode) result = (data: text, statusCode: response.StatusCode);
//             return result;
//             //return output;
//         }
//     }



// }
