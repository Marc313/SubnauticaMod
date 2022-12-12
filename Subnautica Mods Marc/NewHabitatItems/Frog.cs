using HarmonyLib;
using System.Collections.Generic;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using UnityEngine;

namespace NewHabitatItems
{
    class AlienFrog : Craftable
    {
        public AlienFrog() : base("AlienFrog", "Alien Frog", "Alien creature that shows resemblance to a frog.")
        {
        }

        public override TechGroup GroupForPDA => TechGroup.Resources;
        public override TechCategory CategoryForPDA => TechCategory.BasicMaterials;
        public override float CraftingTime => 1;
        public override CraftTree.Type FabricatorType => CraftTree.Type.Fabricator;
        public override string[] StepsToFabricatorTab => new string[] { "Resources", "Basic Materials"};
        public override bool HasSprite => false;

        protected override TechData GetBlueprintRecipe()
        {
            List<Ingredient> ingredients = new List<Ingredient>()
            {
                new Ingredient(TechType., 1),
                new Ingredient(TechType.Bleach, 1),
                new Ingredient(TechType.PrecursorIonCrystal, 1),
                new Ingredient(TechType.Titanium, 1)
            };
            TechData recipe = new TechData()
            {
                craftAmount = 1,
                Ingredients = ingredients
            };

            return recipe;
        }

        public override GameObject GetGameObject()
        {
            GameObject frog = Object.Instantiate(CraftData.GetPrefabForTechType(TechType.Titanium)); // Temp model
            Eatable frogEatable = frog.AddComponent<Eatable>();

            frogEatable.decomposes = false;
            frogEatable.foodValue = 40;
            frogEatable.waterValue = 20;
            frogEatable.allowOverfill = true;

            return frog;
        }
    }
}
