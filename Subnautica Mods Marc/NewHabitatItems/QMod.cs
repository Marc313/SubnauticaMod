// Default Libraries
using System.Reflection;
// Harmony and QModManager \\
using HarmonyLib;
using QModManager.API.ModLoading;
using Logger = QModManager.Utility.Logger;

namespace NewHabitatItems
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

            // Patch New Object
            BabyYoda grogu = new BabyYoda();
            grogu.Patch();

            var frog = new AlienFrog();
            frog.Patch();

            Logger.Log(Logger.Level.Info, "Patch successful!");
        }
    }
}
