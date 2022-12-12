using HarmonyLib;
using Logger = QModManager.Utility.Logger;

namespace SubnauticaTutorialKnifeMod
{
    class KnifeModification
    {
        /*// Class that includes all of the patches I want to make to PlayerTool's OnDraw method
        [HarmonyPatch(typeof(PlayerTool))]
        [HarmonyPatch("OnDraw")]
        internal class PatchPlayerToolOnDraw
        {
            [HarmonyPostfix]
            public static void Postfix(PlayerTool __instance)
            {
                // Check if the PlayerTool is of type Knife
                if (__instance.GetType() == typeof(Knife))
                {
                    Knife knife = __instance as Knife;

                    float damageModifier = QMod.config.KnifeDamageModifier;
                    knife.damage *= damageModifier;
                    Logger.Log(Logger.Level.Debug, $"Modified knife damage with damagemodifier of {damageModifier}");
                }
            }
        }*/

        // Class that includes all of the patches I want to make to PlayerTool's OnToolActionStart method
        [HarmonyPatch(typeof(PlayerTool))]
        [HarmonyPatch("OnToolActionStart")]
        internal class PatchPlayerToolOnActionStart
        {
            [HarmonyPrefix]
            public static void Prefix(PlayerTool __instance)
            {
                // Check if the PlayerTool is of type Knife
                if (__instance.GetType() == typeof(Knife))
                {
                    Knife knife = __instance as Knife;

                    float damageModifier = QMod.config.KnifeDamageModifier;
                    float defaultKnifeDamage = 20f;  // Subnautica's default knife damage
                    knife.damage = defaultKnifeDamage * damageModifier;
                    Logger.Log(Logger.Level.Debug, $"Modified knife damage with damagemodifier of {damageModifier}. Current knife damage: {knife.damage}");
                }
            }

            [HarmonyPostfix]
            public static void RemoveOxygonOnKnifeSwing(PlayerTool __instance)
            {
                float damageModifier = QMod.config.KnifeDamageModifier;
                float oxygonLoss = 3 * damageModifier;
                Player.main.oxygenMgr.RemoveOxygen(oxygonLoss);
                Logger.Log(Logger.Level.Debug, $"Player lost {oxygonLoss} oxygon by swinging knife");
            }
        }
    }
}
