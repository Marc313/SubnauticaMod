// System //
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using System.Collections;
// SMLHelper Functionality //
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Utility;
using Logger = QModManager.Utility.Logger;
using SMLHelper.V2.Handlers;

namespace NewHabitatItems
{
    class BabyYoda : Buildable
    {
        public string assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public override string AssetsFolder => Path.Combine(assemblyLocation, "Assets");

        public BabyYoda() : base("Grogu", "Baby Yoda", "A friendly creature that will keep you company!")
        {
            OnFinishedPatching += () => { techType = TechType; };
        }

        public static TechType techType;
        public override TechGroup GroupForPDA => TechGroup.InteriorModules;
        public override TechCategory CategoryForPDA => TechCategory.InteriorModule;

        protected override Sprite GetItemSprite()
        {
            string iconName = "LukeIcon2.png";
            string iconPath =  Path.Combine(AssetsFolder, iconName);

            if (File.Exists(iconPath))
            {
                Sprite icon = ImageUtils.LoadSpriteFromFile(iconPath);
                //SpriteHandler.RegisterSprite(techType, icon);
                return icon;
            }

            Logger.Log(Logger.Level.Error, "Luke Icon not found :(");
            return null;
        }

        protected override RecipeData GetBlueprintRecipe()
        {
            List<Ingredient> ingredients = new List<Ingredient>()
            {
                new Ingredient(TechType.Titanium, 1),
                new Ingredient(TechType.PrecursorIonCrystal, 3),
                new Ingredient(TechType.FiberMesh, 2),
                new Ingredient(TechType.CopperWire, 1),
                new Ingredient(TechType.Kyanite, 2)
            };
            RecipeData recipe =  new RecipeData
            {
                craftAmount = 1,
                Ingredients = ingredients
            };

            return recipe;
        }

        // Needs refactoring
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            var modelsAssetBundle = AssetBundle.LoadFromFile(Path.Combine(assemblyLocation, "newhabitatitems.models"));
            GameObject groguPrefab = new GameObject();
            GameObject grogu =  new GameObject();


            if (modelsAssetBundle == null)
            {
                Logger.Log(Logger.Level.Error, "Failed to load AssetBundle!");
            } else
            {
                Logger.Log(Logger.Level.Info, "AssetBundle Loaded Succesfully");
                groguPrefab = modelsAssetBundle.LoadAsset<GameObject>("NewLuke");
                grogu = GameObject.Instantiate(groguPrefab);

                modelsAssetBundle.Unload(false);
                Logger.Log(Logger.Level.Info, "Luke Loaded Succesfully");
            }

            grogu.SetActive(false);


            // Get ExecutiveToy
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.EmmanuelPendulum);
            yield return task;
            var toyPrefab = task.GetResult();
            GameObject toy = GameObject.Instantiate(toyPrefab);
            VFXOverlayMaterial toyVFX = toy.GetComponent<VFXOverlayMaterial>();

            // Set materials
            grogu.GetComponentInChildren<Renderer>().materials.ForEach(m => m.shader = Shader.Find("MarmosetUBER"));


            PrefabIdentifier prefabIdentifier = grogu.EnsureComponent<PrefabIdentifier>();
            Constructable groguConstructable = grogu.EnsureComponent<Constructable>();
            ConstructableBounds groguBounds = grogu.EnsureComponent<ConstructableBounds>();
            PlaceTool groguPlacer = grogu.EnsureComponent<PlaceTool>();
            SkyApplier skyApplier = grogu.transform.GetChild(0).gameObject.EnsureComponent<SkyApplier>();
            skyApplier.renderers = grogu.GetAllComponentsInChildren<Renderer>();


            VFXOverlayMaterial groguVFX = grogu.AddComponent<VFXOverlayMaterial>();
            BaseModuleLighting lighting = grogu.EnsureComponent<BaseModuleLighting>();
            //lighting = toyVFX.GetComponent<BaseModuleLighting>();
            //groguVFX.enabled = true;

            if (toyVFX != null)
            {
                groguVFX.material = toyVFX.material;
                groguVFX.mainCamera = toyVFX.mainCamera;
                Logger.Log(Logger.Level.Info, "Succesfully found VFXOverlayMaterial on toy");
            }
            else
            {
                Logger.Log(Logger.Level.Error, "Could not find VFXOverlayMaterial on toy");
            }

            //var groguContainer = grogu.AddComponent<StorageContainer>();

            // Placement rules.
            groguConstructable.allowedOnConstructables = true;
            groguConstructable.allowedOnCeiling = false;
            groguConstructable.allowedOnWall = true;
            groguConstructable.allowedOnGround = true;

            groguConstructable.allowedInSub = true;
            groguConstructable.allowedInBase = true;
            groguConstructable.allowedOutside = true;

            groguConstructable.rotationEnabled = true;
            groguConstructable.forceUpright = true;

            groguConstructable.techType = techType;
            groguConstructable.model = grogu.transform.GetChild(0).gameObject;


            groguBounds.bounds.size = grogu.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().bounds.size;

            groguPlacer.allowedOnCeiling = false;
            groguPlacer.allowedOnGround = true;
            groguPlacer.allowedInBase = true;
            groguPlacer.allowedOutside = true;
            groguPlacer.rotationEnabled = true;

            //groguContainer.width = 2;
            //groguContainer.height = 2;

            grogu.SetActive(true);

            gameObject.Set(grogu);
        }
    }
}
