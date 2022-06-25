using System;

namespace YiTuoBianBianPlugin1 {
    public class Cheat5:UnityEngine.MonoBehaviour {
        private BepInEx.Logging.ManualLogSource Logger = null;
        private BepInEx.Configuration.ConfigEntry<Boolean> Enable = null;
        private HarmonyLib.Harmony HarmonyInstance = null;
        
        private void Awake () {
            this.Logger = Plugin.Logger;
            this.Enable = Plugin.Config.Bind<Boolean>("无限生命值方法五","enable",false,"是否激活此功能项");
            this.Enable.Value = false;
            this.Enable.SettingChanged += this.EnableSettingChanged;
        }

        private void OnDestroy () {
            this.Enable.SettingChanged -= this.EnableSettingChanged;
        }

        private void EnableSettingChanged (Object sender,EventArgs eventArgs) {
            BepInEx.Configuration.SettingChangedEventArgs args = eventArgs as BepInEx.Configuration.SettingChangedEventArgs;
            if(args==null){return;}
            Boolean enabled = (Boolean)args.ChangedSetting.BoxedValue;
            this.Logger.LogWarning(String.Concat(args.ChangedSetting.Definition.Section," => ",enabled?"已激活":"已关闭"));
            if (enabled) {
                this.HarmonyInstance = HarmonyLib.Harmony.CreateAndPatchAll(typeof(Cheat5Harmony));
            } else {
                this.HarmonyInstance.UnpatchSelf();
            }
        }

        public void Toggle () {
            this.Enable.Value = !this.Enable.Value;
        }
    }
}
