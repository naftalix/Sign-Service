﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace SignStorageApi.Infrastructure
{
    public static class Extentions
    {

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotNull(this object obj)
        {
            return !obj.IsNull();
        }

        public static bool NotNullOrEmpty(this string str)
        {
            return str != null && str != string.Empty;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return !str.NotEmpty();
        }

        public static bool NotEmpty<T>(this IEnumerable<T> col)
        {
            if (col.IsNotNull() && col.Any())
            {
                return true;
            }

            return false;
        }

        public static void ForEach<T>(this IEnumerable<T> col, Action<T> action)
        {
            ArgumentNotNull(col, "col");
            ArgumentNotNull(action, "action");

            foreach (var item in col)
            {
                action(item);
            }


        }

        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void ArgumentNotEmptyString(string argumentValue, string argumentName)
        {
            if (argumentValue.Trim().Length == 0)
                throw new ArgumentException("string cannot be empty ", argumentName);
        }

        public static byte[] ToByteArray<T>(this T obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T FromByteArray<T>(this byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
    }
}