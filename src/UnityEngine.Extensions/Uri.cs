using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static partial class Extensions
{

    public static string UrlAppendParameters(this string url, params string[] parameters)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(url);
        if (url.IndexOf('?') == -1)
        {
            sb.Append("?");
        }
        for (int i = 0; i < parameters.Length - 1; i += 2)
        {
            string name = parameters[i];
            string value = parameters[i + 1];
            if (string.IsNullOrEmpty(name))
                continue;
            name = Uri.EscapeDataString(name);
            if (value != null)
                value = Uri.EscapeDataString(value);
            if (sb[sb.Length - 1] != '&')
                sb.Append("&");
            sb.Append(name)
                .Append('=')
                .Append(value);

        }
        return sb.ToString();
    }
}
