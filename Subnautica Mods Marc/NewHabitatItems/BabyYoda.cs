// System //
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
// SMLHelper Functionality //
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Utility;
using Sprite = Atlas.Sprite;
using Logger = QModManager.Utility.Logger;

namespace NewHabitatItems
{
    class BabyYoda : Buildable
    {
        public override string AssetsFolder => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets");
        //public override string IconFileName => "LukeIcon.png";

        public BabyYoda() : base("Grogu", "Baby Yoda", "A friendly creature that will keep you company!")
        {
            //OnFinishedPatching += () => { techType = TechType; };
        }

        public static TechType techType;
        public override TechGroup GroupForPDA => TechGroup.InteriorModules;
        public override TechCategory CategoryForPDA => TechCategory.InteriorModule;
        public override bool HasSprite => false;

        /*protected override Sprite GetItemSprite()
        {
            string iconName = "LukeIcon.png";
            Sprite sprite = ImageUtils.LoadSpriteFromFile(Path.Combine(AssetsFolder, iconName));

            if (sprite != default)
            {
                Logger.Log(Logger.Level.Debug, "Baby Yoda sprite loaded succesfully :))");
            }
            else
            {
                //Logger.Log(Logger.Level.Error, $"Could not find {iconName} in {AssetsFolder}");
            }
            return sprite;
        }*/

        protected override TechData GetBlueprintRecipe()
        {
            List<Ingredient> ingredients = new List<Ingredient>()
            {   
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.PrecursorIonCrystal, 3),
                new Ingredient(TechType.FiberMesh, 2),
                new Ingredient(TechType.CopperWire, 1),
                new Ingredient(TechType.SpadefishEgg, 1),
                new Ingredient(TechType.Kyanite, 2)
            };
            TechData recipe = new TechData()
            {
                craftAmount = 1,
                Ingredients = ingredients
            };

            return recipe;
        }

        // Copied from armourstand.cs
        public override GameObject GetGameObject()
        {
            GameObject grogu = Object.Instantiate(CraftData.GetPrefabForTechType(TechType.ArcadeGorgetoy)); // Temp model
            Constructable groguConstructable = grogu.AddComponent<Constructable>();
            PlaceTool groguPlacer = grogu.AddComponent<PlaceTool>();

            // Placement rules.
            groguConstructable.allowedOnConstructables = true;
            groguConstructable.allowedOnCeiling = false;
            groguConstructable.allowedOnWall = false;
            groguConstructable.allowedOnGround = true;

            groguConstructable.allowedInSub = true;
            groguConstructable.allowedInBase = true;
            groguConstructable.allowedOutside = true;

            groguConstructable.rotationEnabled = true;

            groguConstructable.techType = TechType;
            groguConstructable.model = grogu.transform.GetChild(0).gameObject;

            groguPlacer.allowedOnCeiling = false;
            groguPlacer.allowedOnGround = true;
            groguPlacer.allowedInBase = true;
            groguPlacer.allowedOutside = true;
            groguPlacer.rotationEnabled = true;

            return grogu;
        }
    }
}
