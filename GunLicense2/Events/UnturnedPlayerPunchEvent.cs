using Microsoft.Extensions.Logging;
using OpenMod.API.Eventing;
using OpenMod.Unturned.Players.Equipment.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using SDG.Unturned;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using OpenMod.Extensions.Games.Abstractions.Transforms;
using Microsoft.Extensions.Configuration;

namespace GunLicense.Events
{
    public class UnturnedPlayerPunchEventListener : IEventListener<UnturnedPlayerPunchEvent>
    {
        private readonly ILogger<UnturnedPlayerPunchEventListener> m_Logger;
        private readonly IConfiguration m_Configuration;
        public UnturnedPlayerPunchEventListener(ILogger<UnturnedPlayerPunchEventListener> logger, IConfiguration configuration)
        {
            m_Logger = logger;
            m_Configuration = configuration;
        }

        public async Task HandleEventAsync(object sender, UnturnedPlayerPunchEvent @event)
        {
            Player player = @event.Player.Player;
            Physics.Raycast(player.look.aim.position, player.look.aim.forward, out var hitInfo, 10f, RayMasks.BARRICADE);
            if (hitInfo.transform == null)
            {
                m_Logger.LogInformation("Nothing hit");
            }

            BarricadeDrop drop = BarricadeManager.FindBarricadeByRootTransform(hitInfo.transform);
            if (drop == null)
            {
                m_Logger.LogInformation("hitInfo.transform was null");
                return;
            }
            if (drop.instanceID == 181)
            {
                Barricade barricade = new Barricade(1408);
                UnityEngine.Vector3 point = new UnityEngine.Vector3(-100, 50, -100);
                BarricadeManager.dropBarricade(barricade, player.transform, point, 0, 0, 0, 0, 0);
            }

            await @event.Player.PrintMessageAsync($"Has golpeado: {drop.instanceID}");
        }
    }
}