using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebTest.Helper;

namespace WebTest.Handler
{
    /// <summary>
    /// HomeHandler 的摘要描述
    /// </summary>
    public class HomeHandler : BaseHandler
    {
        public void DumpData()
        {
            try
            {
                var InputText = (_context.Request["InputText"]).ToString();
                DumpData($"你輸入:{InputText}");
            }
            catch (Exception ex)
            {
                ex.ToLog();
                throw;
            }
        }
    }
}