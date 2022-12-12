using HarmonyLib;
using System.Collections.Generic;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using UnityEngine;
using System.Collections;

namespace NewHabitatItems
{
    class AlienFrog : Craftable
    {
        public AlienFrog() : base("AlienFrog", "Alien Frog", "Alien creature that shows resemblance to a frog.")
        {
        }

        public override TechGroup GroupForPDA => TechGroup.Resources;
        public override TechCategory CategoryForPDA => TechCategory.BasicMaterials;
        public override float CraftingTime => 3;
        public override CraftTree.Type FabricatorType => CraftTree.Type.Fabricator;
        public override string[] StepsToFabricatorTab => new string[] { "Sustenance"};

        protected override RecipeData GetBlueprintRecipe()
        {
            List<Ingredient> ingredients = new List<Ingredient>()
            {
                new Ingredient(TechType.Nickel, 1),
                new Ingredient(TechType.Lithium, 1),
                new Ingredient(TechType.PrecursorIonCrystal, 1),
                new Ingredient(TechType.Titanium, 2)
            };
            RecipeData recipe = new RecipeData()
            {
                craftAmount = 1,
                Ingredients = ingredients
            };

            return recipe;
        }

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.Titanium);
            yield return task;
            GameObject frogPrefab = task.GetResult();

            GameObject frog = GameObject.Instantiate(frogPrefab);
            Eatable frogEatable = frog.AddComponent<Eatable>();

            frogEatable.decomposes = false;
            frogEatable.foodValue = 40;
            frogEatable.waterValue = 20;

            gameObject.Set(frog);
        }
    }
}
