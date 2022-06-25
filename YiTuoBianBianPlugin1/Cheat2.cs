using System;
using System.Collections;

namespace YiTuoBianBianPlugin1 {
    public class Cheat2:IDisposable {
        private BepInEx.Logging.ManualLogSource Logger = null;
        private BepInEx.Configuration.ConfigEntry<Boolean> Enable = null;
        private UnityEngine.Coroutine Coroutine = null;
        
        public Cheat2 (BepInEx.Logging.ManualLogSource logger,BepInEx.Configuration.ConfigFile config) {
            this.Logger = logger;
            this.Enable = config.Bind<Boolean>("无限生命值方法二","enable",false,"是否激活此功能项");
            this.Enable.Value = false;
            this.Enable.SettingChanged += this.EnableSettingChanged;
        }

        #region IDisposable
        private Boolean disposedValue = false;
        protected virtual void Dispose (Boolean disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: 释放托管状态(托管对象)
                    this.Enable.SettingChanged -= this.EnableSettingChanged;
                }

                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                this.Enable = null;
                this.disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~Cheat2()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose () {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        private void EnableSettingChanged (Object sender, EventArgs eventArgs) {
            BepInEx.Configuration.SettingChangedEventArgs args = eventArgs as BepInEx.Configuration.SettingChangedEventArgs;
            if(args==null){return;}
            Boolean enabled = (Boolean)args.ChangedSetting.BoxedValue;
            this.Logger.LogWarning(String.Concat(args.ChangedSetting.Definition.Section," => ",enabled?"已激活":"已关闭"));
            //
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
