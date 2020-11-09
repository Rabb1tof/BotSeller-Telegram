using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace BotSeller_Telegram.Extentions
{
    public static class StartupExtentions
    {
        public static IServiceCollection AddBag<T>(this IServiceCollection collection)
            => collection.AddSingleton<ConcurrentBag<T>>();
    }
}
