using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WPF.Extensions
{
    public static class JsonSerializerExtension
    {
        public static async Task<string> ToJsonAsync(this object obj)
        {
            using MemoryStream stream = new();
            await JsonSerializer.SerializeAsync(stream, obj);
            stream.Seek(0, SeekOrigin.Begin);
            using StreamReader reader = new(stream);   
            return await reader.ReadToEndAsync();
        }
    }
}
