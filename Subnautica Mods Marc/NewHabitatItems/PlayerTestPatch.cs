/*using HarmonyLib;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace NewHabitatItems
{
    *//*class PlayerTestPatch
    {
        [HarmonyPatch(typeof(Player))]
        [HarmonyPatch(nameof(Player.Awake))]
        internal class PlayerAwakePatch {

            [HarmonyPostfix]
            public static void GivePlayerBlokjeTest()
            {
                Player player = Player.main;
                Inventory inventory = player.GetComponent<Inventory>();

                GameObject pickupablePrefab = CraftData.GetPrefabForTechType(TechType.ArcadeGorgetoy);
                Pickupable pickupable = GameObject.Instantiate(pickupablePrefab).GetComponent<Pickupable>();

                //Logger.Log(Logger.Level.Debug, $"Arcade Gorge Toy is of type ")
                inventory.AddPending(pickupable);
                //inventory.OnAddItem();
            }
        }
    }*//*
}*/
