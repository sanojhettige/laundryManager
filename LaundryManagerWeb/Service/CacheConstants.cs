using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LaundryManagerWeb.Services
{
	public static class CacheConstants
	{
		public static string ProductCacheKey { get; set; } = "Products";
		
	}
    public static class SessionExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <example><![CDATA[
        /// var user = HttpContext
        ///   .Session
        ///   .GetOrStore<User>(
        ///      string.Format("User{0}", _userId), 
        ///      () => Repository.GetUser(_userId));
        ///
        /// ]]></example>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="generator"></param>
        /// <returns></returns>
        public static T GetOrStore<T>(this HttpSessionStateBase session, string name, Func<T> generator)
        {

            var result = session[name];
            if (result != null)
                return (T)result;

            result = generator != null ? generator() : default(T);
            session.Add(name, result);
            return (T)result;
        }

    }
}
