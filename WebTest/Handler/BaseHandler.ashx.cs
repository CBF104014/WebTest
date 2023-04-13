using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Web;
using WebTest.Helper;

namespace WebTest.Handler
{
    /// <summary>
    /// BaseHandler 的摘要描述
    /// </summary>
    public class BaseHandler : IHttpHandler
    {
		public HttpContext _context { get { return HttpContext.Current; } }
		public bool IsReusable { get { return false; } }
		public void ProcessRequest(HttpContext context)
		{
            try
			{
				var UrlArr = HttpContext.Current.Request.RawUrl.Split('/');
				var HandlerName = UrlArr.First(x => x.Contains(".ashx")).Replace(".ashx", "");
				var FunctionName = UrlArr[UrlArr.Length - 1];
				var ClassType = Type.GetType($"WebTest.Handler.{HandlerName}");
				var HandlerConstructor = ClassType.GetConstructor(new Type[] { });
				ClassType.GetMethod(FunctionName).Invoke(HandlerConstructor.Invoke(new object[] { }), null);
			}
            catch (Exception ex)
            {
				ex.ToLog();
                throw;
            }
		}
		private void Dump(object data, HttpStatusCode statusCode)
		{
			var EncodStr = _context.Request.Headers["Accept-Encoding"].ToString().ToLower();
			if (EncodStr.Contains("gzip"))
			{
				_context.Response.AppendHeader("Content-encoding", "gzip");
				_context.Response.Filter = new GZipStream(_context.Response.Filter, CompressionMode.Compress);
			}
			else
			{
				_context.Response.AppendHeader("Content-encoding", "deflate");
				_context.Response.Filter = new DeflateStream(_context.Response.Filter, CompressionMode.Compress);
			}
			_context.Response.StatusCode = (int)statusCode;
			_context.Response.ContentType = "application/json; charset=utf-8";
			JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
			{
				ContractResolver = new DefaultContractResolver
				{
					NamingStrategy = new DefaultNamingStrategy()
				}
			};
			_context.Response.Write(JsonConvert.SerializeObject(data, Formatting.Indented, jsonSerializerSettings));
		}
		protected void DumpData(object data)
		{
			Dump(data, HttpStatusCode.OK);
		}
	}
}