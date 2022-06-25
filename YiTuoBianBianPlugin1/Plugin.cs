using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using System;

namespace YiTuoBianBianPlugin1 {
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME,PLUGIN_VERSION)]
    [BepInProcess("YTBB.exe")]
    public class Plugin : BaseUnityPlugin {
        public const String PLUGIN_GUID = "net.skydust.BepinEx.YiTuoBianBianPlugin1";
        public const String PLUGIN_NAME = "一坨便便的插件";
        public const String PLUGIN_VERSION = "0.1.1";
        public static new ManualLogSource Logger = null;
        public static new ConfigFile Config = null;
        
        private Cheat1 Cheat1 = null;
        private readonly KeyboardShortcut Cheat1Hotkey = new KeyboardShortcut(UnityEngine.KeyCode.Alpha1,UnityEngine.KeyCode.LeftControl);
        private Cheat2 Cheat2 = null;
        private readonly KeyboardShortcut Cheat2Hotkey = new KeyboardShortcut(UnityEngine.KeyCode.Alpha2,UnityEngine.KeyCode.LeftControl);
        private Cheat3 Cheat3= null;
        private readonly KeyboardShortcut Cheat3Hotkey = new KeyboardShortcut(UnityEngine.KeyCode.Alpha3,UnityEngine.KeyCode.LeftControl);
        private Cheat4 Cheat4= null;
        private readonly KeyboardShortcut Cheat4Hotkey = new KeyboardShortcut(UnityEngine.KeyCode.Alpha4,UnityEngine.KeyCode.LeftControl);
        private Cheat5 Cheat5= null;
        private readonly KeyboardShortcut Cheat5Hotkey = new KeyboardShortcut(UnityEngine.KeyCode.Alpha5,UnityEngine.KeyCode.LeftControl);

        private void Awake () {
            Plugin.Logger = base.Logger;
            Plugin.Config = base.Config;
            Plugin.Config.SaveOnConfigSet = true;
            this.Cheat1 = new Cheat1(base.Logger,base.Config);
            this.Cheat2 = new Cheat2(base.Logger,base.Config);
            this.Cheat3 = this.gameObject.AddComponent<Cheat3>();
            this.Cheat4 = this.gameObject.AddComponent<Cheat4>();
            this.Cheat5 = this.gameObject.AddComponent<Cheat5>();
            Plugin.Logger.LogInfo("插件已加载");
        }

        private void Start () {
            Plugin.Logger.LogInfo("插件已启动");
        }

        private void OnDestroy () {
            this.Cheat1.Dispose();
            Plugin.Logger.LogInfo("插件已卸载");
        }

        private void Update () {
            if(this.Cheat1Hotkey.IsUp() && this.Cheat1!=null){ this.Cheat1.Toggle(); }
            if(this.Cheat2Hotkey.IsUp() && this.Cheat2!=null){ this.Cheat2.Toggle(); }
            if(this.Cheat3Hotkey.IsUp() && this.Cheat3!=null){ this.Cheat3.Toggle(); }
            if(this.Cheat4Hotkey.IsUp() && this.Cheat4!=null){ this.Cheat4.Toggle(); }
            if(this.Cheat5Hotkey.IsUp() && this.Cheat5!=null){ this.Cheat5.Toggle(); }
            //Plugin.Logger.LogInfo("插件循环");
        }
    }
}
