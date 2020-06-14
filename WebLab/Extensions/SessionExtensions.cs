﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace WebLab.Extensions
{
	public static class SessionExtensions {
        public static void Set<T>(this ISession session, string key, T item) {
            var serializedItem = JsonConvert.SerializeObject(item);
            session.SetString(key, serializedItem);
        }

        public static T Get<T>(this ISession session, string key) {
            var item = session.GetString(key);
            return item == null
                ? Activator.CreateInstance<T>() // default(T)
                : JsonConvert.DeserializeObject<T>(item);
        }
    }
}
