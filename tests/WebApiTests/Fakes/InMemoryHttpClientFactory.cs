using HarmonyLib;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Reflection;
using WebApiTests.EndToEndTests;

namespace Rommelmarkten.EndToEndTests.WebApi.Fakes
{
    internal class InMemoryHttpClientFactory : IHttpClientFactory
    {
        private readonly RommelmarktenWebApi api;
        private readonly WebApplicationFactoryClientOptions options;
        private HttpClient client = null!;

        public InMemoryHttpClientFactory(RommelmarktenWebApi api, DelegatingHandler[] extraHandlers)
        {
            this.api = api;

            // add our custom http handlers to the HttpClient created by the WebApplicationFactory
            var harmony = new Harmony("be.sprucebit.addHandlersToCustomWebAppFactoryOptions");
            InMemoryHttpClientFactoryPatch.SetExtraHandlers(extraHandlers);

            if (Harmony.HasAnyPatches("be.sprucebit.addHandlersToCustomWebAppFactoryOptions"))
                harmony.UnpatchAll();

            harmony.PatchAll();

            options = new WebApplicationFactoryClientOptions();
        }

        public HttpClient CreateClient(string name)
        {
            if(client == null)
                client = api.CreateClient(options);
            return client;
        }
    }


    /// <summary>
    /// This patch allows extra handlers to be added to the HttpClient created by the WebApplicationFactory.
    /// </summary>
    [HarmonyPatch(typeof(WebApplicationFactoryClientOptions))]
    public class InMemoryHttpClientFactoryPatch
    {
        private static IEnumerable<DelegatingHandler> extraHandlers = [];

        public static void SetExtraHandlers(IEnumerable<DelegatingHandler> handlers)
        {
            extraHandlers = handlers;
        }

        static MethodBase TargetMethod()
        {
            return AccessTools.Method(typeof(WebApplicationFactoryClientOptions), "CreateHandlers");
        }

        static DelegatingHandler[] Postfix(DelegatingHandler[] handlers)
        {
            var list = handlers.ToList();
            list.InsertRange(0, extraHandlers);
            return list.ToArray();
        }
    }
}
