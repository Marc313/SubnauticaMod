// Default Libraries
using System.Reflection;
// Harmony and QModManager \\
using HarmonyLib;
using QModManager.API.ModLoading;
using Logger = QModManager.Utility.Logger;
// SMLHelper functionality \\
using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
using SMLHelper.V2.Handlers;

// This class is made with the Subnautica modding guide:
// https://mroshaw.github.io/Subnautica/yourfirstmod_sn/codingthemod.html
namespace SubnauticaTutorialKnifeMod
{
    [QModCore]
    public static class QMod
    {
        // Make a Config object, allowing players to edit settings of the mod.
        internal static Config config { get; } = OptionsPanelHandler.Main.RegisterModOptions<Config>();

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

        // Config class for setting up the mod menu
        [Menu("Knife Damage")]
        public class Config : ConfigFile
        {
            [Slider("Knife Damage Modifier", Format = "{0:F2}", Min = 0.1f, Max = 5.0f, DefaultValue = 1.0f, Step = 0.1f)]
            public float KnifeDamageModifier = 1.0f;
        }
    }
}
