using System;
using System.IO;
using System.Linq;

namespace OS.DAL.PgSql.IntegrationTests
{
    public static class Extensions
    {
        public static T[] Get<T>(this string fileName, Func<T, T> mapper)
            where T : class
        {
            using (var stream = new FileStream(fileName, FileMode.Open))
            using (var reader = new StreamReader(stream))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T[]>(reader.ReadToEnd()).Select(mapper).ToArray();
            }
        }
    }
}