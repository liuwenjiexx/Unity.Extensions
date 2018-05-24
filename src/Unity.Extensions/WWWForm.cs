using System.Text;
using UnityEngine;

namespace LWJ.Unity
{
    public static partial class Extensions
    {

        public static void AddField(this WWWForm source, string fieldName, string[] values)
        {
            if (values == null)
                return;
            foreach (var item in values)
                source.AddField(fieldName, item);
        }
        public static void AddField(this WWWForm source, string fieldName, string[] values, System.Text.Encoding e)
        {
            if (values == null)
                return;
            foreach (var item in values)
                source.AddField(fieldName, item, e);
        }
        public static void AddField(this WWWForm source, string fieldName, int[] values)
        {
            if (values == null)
                return;
            foreach (var item in values)
                source.AddField(fieldName, item);
        }

        public static string ToGetUrl(this WWWForm formData, string baseUrl)
        {
            if (formData == null)
                return baseUrl;

            int index = baseUrl.IndexOf('?');

            if (index < 0)
                baseUrl += "?";
            else if (!baseUrl.EndsWith("&"))
                baseUrl += "&";

            baseUrl += Encoding.UTF8.GetString(formData.data);

            return baseUrl;
        }



    }
}
