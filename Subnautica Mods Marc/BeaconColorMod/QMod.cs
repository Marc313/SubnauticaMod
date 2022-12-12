// Default Libraries
using System.Reflection;
// Harmony and QModManager \\
using HarmonyLib;
using QModManager.API.ModLoading;
using Logger = QModManager.Utility.Logger;
// SMLHelper functionality \\
/*using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
using SMLHelper.V2.Handlers;*/

namespace BeaconColorMod
{
    [QModCore]
    public static class QMod
    {
        [QModPatch]
        public static void Patch()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var modName = ($"marc313_{assembly.GetName().Name}");
            Logger.Log(Logger.Level.Info, $"Patching {modName}");
            Harmony harmony = new Harmony(modName);
            harmony.PatchAll(assembly);
            Logger.Log(Logger.Level.Info, "Patch successful!");
        }
    }
}
