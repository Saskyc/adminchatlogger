using Exiled.API.Features;
using HarmonyLib;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Utf8Json;

namespace harmonypatch
{
    public sealed class Plugin : Plugin<Config>
    {

        public override string Author => "Enes";

        public override string Name => "logadminchat";

        public override string Prefix => Name.ToUpper();

        public static Plugin Instance;

        private Harmony _harmony;
        
        public static IEnumerator<float> webhooksend(string reason)
        {
            UnityWebRequest webRequest = new UnityWebRequest(Plugin.Instance.Config.webhook, "POST");
            var message = new{ content = reason };
            UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(JsonSerializer.Serialize(message));
            uploadHandlerRaw.contentType = "application/json";
            UploadHandlerRaw uploadHandler = uploadHandlerRaw;
            webRequest.uploadHandler = uploadHandler;
            yield return Timing.WaitUntilDone(webRequest.SendWebRequest());
        }
        
        public override void OnEnabled()
        {
            base.OnEnabled();
            Instance = this;
            _harmony = new Harmony($"adminchat");
            Log.Debug($"yes");
            _harmony.PatchAll();
            Log.Debug($"PATCHED!");
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            _harmony.UnpatchAll("adminchat");
            Log.Debug($"unpatchedig!");
            _harmony = null;
            Instance = null;
        }
    }
}
