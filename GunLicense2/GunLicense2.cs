using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Cysharp.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OpenMod.API.Persistence;
using OpenMod.API.Plugins;
using OpenMod.Unturned.Plugins;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more, visit https://openmod.github.io/openmod-docs/devdoc/guides/getting-started.html

[assembly: PluginMetadata("GunLicense", DisplayName = "GunLicense")]

namespace GunLicense
{
    public class MyOpenModPlugin : OpenModUnturnedPlugin
    {
        public const string OwnersKey = "LicenseData";
        private readonly IDataStore m_DataStore;
        private readonly IConfiguration m_Configuration;
        private readonly IStringLocalizer m_StringLocalizer;
        private readonly ILogger<MyOpenModPlugin> m_Logger;

        public MyOpenModPlugin(
            IDataStore dataStore,
            IConfiguration configuration,
            IStringLocalizer stringLocalizer,
            ILogger<MyOpenModPlugin> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            m_DataStore = dataStore;
            m_Configuration = configuration;
            m_StringLocalizer = stringLocalizer;
            m_Logger = logger;
        }

        protected override async UniTask OnLoadAsync()
        {
            m_Logger.LogInformation("Hello World!");

            //var config = new LicenseData();
            //m_Configuration.Bind(config);

            if (!await m_DataStore.ExistsAsync(OwnersKey))
            {
                await SeedData();
            }

            var data = await m_DataStore.LoadAsync<LicenseData>(OwnersKey);
            foreach (var target in data.TargetPositions)
            {
                m_Logger.LogInformation($"{target.x} {target.y} {target.z}");
            }
            await m_DataStore.SaveAsync<LicenseData>(OwnersKey, data);
        }

        protected override async UniTask OnUnloadAsync()
        {
            m_Logger.LogInformation(m_StringLocalizer["plugin_events:plugin_stop"]);
            string var = (m_Configuration["plugin_events:one"]);
        }

        private async Task SeedData()
        {
            // Create default data
            List<UnityEngine.Vector3> targetPositions = new List<UnityEngine.Vector3>
            {
                new UnityEngine.Vector3(-102, 50, -100),
                new UnityEngine.Vector3(-104, 50, -100),
                new UnityEngine.Vector3(-105, 50, -100)
            };

            LicenseData defaultData = new LicenseData
            {
                NotePosition = new UnityEngine.Vector3(-100f, 50f, -100f),
                TargetPositions = targetPositions
            };

            await m_DataStore.SaveAsync(OwnersKey, defaultData);
        }
    }
}