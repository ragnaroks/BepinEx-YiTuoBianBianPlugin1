using HarmonyLib;
using System;

namespace YiTuoBianBianPlugin1 {
    public static class Cheat5Harmony {
        /*
        [HarmonyPatch(typeof(Player),"Health",MethodType.Setter)]
        [HarmonyPrefix]
        [HarmonyWrapSafe]
        public static void HealthSetterPrefix (ref Int32 value) {
            value = 3;
            Plugin.Logger.LogWarning(String.Concat("Harmony => HealthSetterPrefix() => ",value));
        }
        */

        //int damage, Vector2 force, GameObject instigator, Vector3 hitPoint
        [HarmonyPatch(typeof(Player),"TakeDamage",typeof(Int32),typeof(UnityEngine.Vector2),typeof(UnityEngine.GameObject),typeof(UnityEngine.Vector3))]
        [HarmonyPrefix]
        [HarmonyWrapSafe]
        public static Boolean TakeDamageMethodPrefix () {
            return false;
        }
    }
}
