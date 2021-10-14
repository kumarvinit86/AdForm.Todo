
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IO;
using SeriLogger.DbLogger;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Adform.Todo.Service.Middleware
{       
    /// <summary>
    /// Custom Middleware to log the request and response of Api
    /// </summary>
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDbLogger _logger;
        IConfiguration _configuration;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        public RequestResponseLoggingMiddleware(RequestDelegate next,
                                                IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            _logger = SetupLog();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);
            await LogResponse(context);
        }
        /// <summary>
        /// method to log request comming to API
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            _logger.Debug($"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}");
            context.Request.Body.Position = 0;
        }

        /// <summary>
        /// reader to read the data from api pipe line
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }
        /// <summary>
        /// method to log response from API
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;
            await _next(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            _logger.Debug($"Http Response Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Response Body: {text}");
            await responseBody.CopyToAsync(originalBodyStream);
        }
        /// <summary>
        /// Settingup the Logger for the middleware
        /// </summary>
        /// <returns></returns>
        public DbLogger SetupLog()
        {
            DbLogger dbLogger = new DbLogger();
            return dbLogger.Initialize(
                _configuration.GetValue<string>("Connection:Data Source"),
                _configuration.GetValue<string>("Connection:Initial Catalog"),
                new Tuple<bool, bool, bool>
                (_configuration.GetValue<bool>("SeriLog:LogWarning"),
                _configuration.GetValue<bool>("SeriLog:LogError"),
                _configuration.GetValue<bool>("SeriLog:LogDebug")),
                new Tuple<string, string>(
                    _configuration.GetValue<string>("Connection:UserName"),
                    _configuration.GetValue<string>("Connection:Password")));
        }
    }
}
