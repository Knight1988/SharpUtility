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
        public static T Clone<T>(this T source)
        {
            return JsonConvert.DeserializeObject<T>(source.ToJson(Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
        }

        /// <summary>
        /// Convert an object to json
        /// </summary>
        /// <param name="source">object to convert</param>
        /// <returns></returns>
        public static string ToJson(this object source)
        {
            return JsonConvert.SerializeObject(source);
        }

        /// <summary>
        /// Convert an object to json
        /// </summary>
        /// <param name="source">object to convert</param>
        /// <param name="formatting">formatting option</param>
        /// <returns></returns>
        public static string ToJson(this object source, Formatting formatting)
        {
            return JsonConvert.SerializeObject(source, formatting);
        }

        /// <summary>
        /// Convert an object to json
        /// </summary>
        /// <param name="source">object to convert</param>
        /// <param name="formatting">formatting option</param>
        /// <param name="settings">setting for serialized object</param>
        /// <returns></returns>
        public static string ToJson(this object source, Formatting formatting, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(source, formatting, settings);
        }
    }
}