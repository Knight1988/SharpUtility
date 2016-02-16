using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace SharpUtility.Core
{
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Clone an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Clone<T>(T source)
        {
            return JsonConvert.DeserializeObject<T>(source.ToJson());
        }

        /// <summary>
        /// Convert an object to json
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToJson(this object source)
        {
            return JsonConvert.SerializeObject(source);
        }
    }
}