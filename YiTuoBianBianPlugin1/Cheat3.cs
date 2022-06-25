using System;
using System.Collections;

namespace YiTuoBianBianPlugin1 {
    public class Cheat3:UnityEngine.MonoBehaviour {
        private BepInEx.Logging.ManualLogSource Logger = null;
        private BepInEx.Configuration.ConfigEntry<Boolean> Enable = null;
        private UnityEngine.Coroutine Coroutine = null;
        
        private void Awake () {
            this.Logger = Plugin.Logger;
            this.Enable = Plugin.Config.Bind<Boolean>("无限生命值方法三","enable",false,"是否激活此功能项");
            this.Enable.Value = false;
            this.Enable.SettingChanged += this.EnableSettingChanged;
        }

        private void OnDestroy () {
            this.Enable.SettingChanged -= this.EnableSettingChanged;
        }

        private void EnableSettingChanged (Object sender, EventArgs eventArgs) {
            BepInEx.Configuration.SettingChangedEventArgs args = eventArgs as BepInEx.Configuration.SettingChangedEventArgs;
            if(args==null){return;}
            Boolean enabled = (Boolean)args.ChangedSetting.BoxedValue;
            this.Logger.LogWarning(String.Concat(args.ChangedSetting.Definition.Section," => ",enabled?"已激活":"已关闭"));
            if (enabled) {
                this.Coroutine = BepInEx.ThreadingHelper.Instance.StartCoroutine(this.AddHealthCoroutine());
            } else {
                BepInEx.ThreadingHelper.Instance.StopCoroutine(this.Coroutine);
            }
        }

        private IEnumerator AddHealthCoroutine () {
            while (true) {
                this.AddHealth();
                yield return new UnityEngine.WaitForSeconds(1);
            }
        }

        private void AddHealth () {
            if(!this.Enable.Value){return;}
            if(GameManager.Instance == null || GameManager.Instance.Player == null){return;}
            GameManager.Instance.Player.GiveHealth(3,null);
        }

        public void Toggle () {
            this.Enable.Value = !this.Enable.Value;
        }
    }
}
