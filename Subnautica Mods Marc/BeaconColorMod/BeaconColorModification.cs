using HarmonyLib;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace BeaconColorMod
{
    class BeaconColorModification
    {
        [HarmonyPatch(typeof(uGUI_IconGrid))]
        [HarmonyPatch("OnPointerEnter")]
        internal class PatchBeaconColor {

            [HarmonyPostfix]
            public static void Postfix(uGUI_IconGrid __instance)
            {
                __instance.colorBackgroundNormal = Color.magenta;
                __instance.colorBackgroundHover = Color.magenta;
                __instance.colorBackgroundPress = Color.magenta;
                __instance.UpdateNow();
                Logger.Log(Logger.Level.Debug, "Changed iconColors to magenta", null, true);
            }
        }
    }
}
