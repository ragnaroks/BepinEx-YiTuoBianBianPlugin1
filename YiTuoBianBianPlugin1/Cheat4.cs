using System;
using System.Threading;
using System.Threading.Tasks;

namespace YiTuoBianBianPlugin1 {
    public class Cheat4:UnityEngine.MonoBehaviour {
        private BepInEx.Logging.ManualLogSource Logger = null;
        private BepInEx.Configuration.ConfigEntry<Boolean> Enable = null;
        private Task Task1 = null;
        private CancellationTokenSource CancellationTokenSource = null;
        
        private void Awake () {
            this.Logger = Plugin.Logger;
            this.Enable = Plugin.Config.Bind<Boolean>("无限生命值方法四","enable",false,"是否激活此功能项");
            this.Enable.Value = false;
            this.Enable.SettingChanged += this.EnableSettingChanged;
            this.CancellationTokenSource = new CancellationTokenSource();
        }

        private void Start () {
            this.Task1 = Task.Factory.StartNew(this.AddHealthAsync,this.CancellationTokenSource.Token);
        }

        private void OnDestroy () {
            this.Enable.SettingChanged -= this.EnableSettingChanged;
            this.CancellationTokenSource.Cancel();
            this.CancellationTokenSource.Dispose();
            this.Task1.Dispose();
        }

        private void EnableSettingChanged (Object sender,EventArgs eventArgs) {
            BepInEx.Configuration.SettingChangedEventArgs args = eventArgs as BepInEx.Configuration.SettingChangedEventArgs;
            if(args==null){return;}
            Boolean enabled = (Boolean)args.ChangedSetting.BoxedValue;
            this.Logger.LogWarning(String.Concat(args.ChangedSetting.Definition.Section," => ",enabled?"已激活":"已关闭"));
        }

        private async Task AddHealthAsync () {
            while (!this.CancellationTokenSource.IsCancellationRequested) {
                this.AddHealth();
                await Task.Delay(1000);
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
