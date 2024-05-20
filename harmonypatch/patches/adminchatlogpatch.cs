using Exiled.API.Features;
using HarmonyLib;
using MEC;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Scp079.Rewards;
using PlayerRoles.PlayableScps.Scp939;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Extensions;
using UnityEngine.Networking;
using Utf8Json;

namespace harmonypatch.patches
{

    [HarmonyPatch(typeof(RemoteAdmin.CommandProcessor), nameof(RemoteAdmin.CommandProcessor.ProcessAdminChat))]
    public class cmdpatch
    {
        [HarmonyPrefix]
        private static bool Prefix(ref string q, ref CommandSender sender)
        {
            string text = string.Empty;
            Exiled.API.Features.Player.TryGet(sender, out Player player);

            if (player is null)
            {
                text = null;
                text = $"SERVER CONSOLE: {q.ToString()}";

                if (!Plugin.Instance.Config.webhook.IsEmpty())
                {
                    Timing.RunCoroutine(Plugin.webhooksend(text));
                    Log.Info(text);
                    return false;
                    
                }

                Log.Info(text);
                return false;
            }
            
          
            if (!player.RemoteAdminPermissions.HasFlag(PlayerPermissions.AdminChat))
            {
                text = string.Empty;
                return false;
            }

            if (player.DoNotTrack)
            {
                if (Plugin.Instance.Config.ignorednt)
                {
                    text = $"NICKNAME: {player.Nickname} ({player.UserId}) MESSAGE: {q} \n TIME:{DateTime.Now}";
                    if (!Plugin.Instance.Config.webhook.IsEmpty())
                    {
                        Timing.RunCoroutine(Plugin.webhooksend(text));
                        return true;
                    }
                }
                return false;
            }
            
           text = $"NICKNAME: {player.Nickname} ({player.UserId}) MESSAGE: {q} \n TIME:{DateTime.Now}";

            if (!Plugin.Instance.Config.webhook.IsEmpty())
            {
                Timing.RunCoroutine(Plugin.webhooksend(text));
                return true;
            }
            
            Log.Info(text);
            return true;
        }
    }
}
