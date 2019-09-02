using System.Web;
using System.Web.Mvc;
using MvcThrottle;

namespace WebApplication1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            var throttleFilter = new ThrottlingFilter
            {
                Policy = new ThrottlePolicy(perSecond: 1, perMinute: 10, perHour: 60 * 10, perDay: 600 * 10)
                {
                    IpThrottling = true
                },
                Repository = new CacheRepository()
            };

            filters.Add(new HandleErrorAttribute());
        }
    }
}
