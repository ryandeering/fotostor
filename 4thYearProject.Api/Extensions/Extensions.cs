﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace _4thYearProject.Api.Extensions
{
    public static partial class Extensions
{
    /// <summary>
    ///     Encodes a URL string.
    /// </summary>
    /// <param name="str">The text to encode.</param>
    /// <returns>An encoded string.</returns>
    public static String UrlEncode(this String str)
    {
        return HttpUtility.UrlEncode(str);
    }

    /// <summary>
    ///     Encodes a URL string using the specified encoding object.
    /// </summary>
    /// <param name="str">The text to encode.</param>
    /// <param name="e">The  object that specifies the encoding scheme.</param>
    /// <returns>An encoded string.</returns>
    public static String UrlEncode(this String str, Encoding e)
    {
        return HttpUtility.UrlEncode(str, e);
    }
    }
}