using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Critters;
using CalamityMod.Items.Fishing.AstralCatches;
using CalamityMod.Items.Fishing.BrimstoneCragCatches;
using CalamityMod.Items.Fishing.SunkenSeaCatches;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Items.Placeables.Astral;
using CalamityMod.Items.Placeables.Crags;
using CalamityMod.Items.Placeables.FurnitureAcidwood;
using CalamityMod.Items.Placeables.FurnitureDriftwood;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Items.Potions.Food;
using CalamityMod.Items.Tools;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class RecipeSystem : ModSystem
{
	public static int HardmodeAnvil;

	public static int HardmodeForge;

	public static int AnyFood;

	public static int AnyCopperOre;

	public static int AnySilverOre;

	public static int AnyGoldOre;

	public static int AnyEvilOre;

	public static int AnyCobaltOre;

	public static int AnyMythrilOre;

	public static int AnyAdamantiteOre;

	public static int AnyCopperBar;

	public static int AnySilverBar;

	public static int AnyGoldBar;

	public static int AnyEvilBar;

	public static int AnyCobaltBar;

	public static int AnyMythrilBar;

	public static int AnyAdamantiteBar;

	public static int Boss2Material;

	public static int CursedFlameIchor;

	public static int AnyEvilWater;

	public static int AnyStoneBlock;

	public static int AnySnowBlock;

	public static int AnyIceBlock;

	public static int AnySiltBlock;

	public static int AnyEvilBlock;

	public static int AnyGoodBlock;

	public static int AnyWoodenSword;

	public static int AnyHallowedHelmet;

	public static int AnyHallowedPlatemail;

	public static int AnyHallowedGreaves;

	public static int AnyGoldCrown;

	public static int LunarPickaxe;

	public static int LunarHamaxe;

	public static int AnyManaFlower;

	public static int AnyQuiver;

	public static int AnyTombstone;

	public static int AnyWings;

	public override void AddRecipes()
	{
		HandleRecipes();
	}

	public override void AddRecipeGroups()
	{
		HandleRecipeGroups();
	}

	public override void PostSetupContent()
	{
		AddShimmerRecipes();
	}

	private static void ModifyVanillaRecipeGroups()
	{
		RecipeGroup.recipeGroups[RecipeGroupID.Fireflies].ValidItems.Add(ModContent.ItemType<TwinklerItem>());
		RecipeGroup recipeGroup = RecipeGroup.recipeGroups[RecipeGroupID.Fruit];
		recipeGroup.ValidItems.Add(ModContent.ItemType<Barberry>());
		recipeGroup.ValidItems.Add(ModContent.ItemType<Cometfruit>());
		recipeGroup.ValidItems.Add(ModContent.ItemType<Jackfruit>());
		recipeGroup.ValidItems.Add(ModContent.ItemType<Lotus>());
		recipeGroup.ValidItems.Add(ModContent.ItemType<Mangosteen>());
		recipeGroup.ValidItems.Add(ModContent.ItemType<Salak>());
		RecipeGroup recipeGroup2 = RecipeGroup.recipeGroups[RecipeGroupID.Sand];
		recipeGroup2.ValidItems.Add(ModContent.ItemType<AstralSand>());
		recipeGroup2.ValidItems.Add(ModContent.ItemType<HardenedAstralSand>());
		recipeGroup2.ValidItems.Add(ModContent.ItemType<Dunesand>());
		recipeGroup2.ValidItems.Add(ModContent.ItemType<EutrophicSand>());
		recipeGroup2.ValidItems.Add(ModContent.ItemType<HardenedEutrophicSand>());
		recipeGroup2.ValidItems.Add(ModContent.ItemType<PolypSand>());
		recipeGroup2.ValidItems.Add(ModContent.ItemType<VolcanicSand>());
		recipeGroup2.ValidItems.Add(ModContent.ItemType<SulphurousSand>());
		RecipeGroup recipeGroup3 = RecipeGroup.recipeGroups[RecipeGroupID.Wood];
		recipeGroup3.ValidItems.Add(ModContent.ItemType<Acidwood>());
		recipeGroup3.ValidItems.Add(ModContent.ItemType<Driftwood>());
	}

	public static void HandleRecipeGroups()
	{
		ModifyVanillaRecipeGroups();
		AddOreAndBarRecipeGroups();
		AddEvilBiomeItemRecipeGroups();
		AddBiomeBlockRecipeGroups();
		AddEquipmentRecipeGroups();
		RecipeGroup group = new RecipeGroup(() => CalamityUtils.GetTextValue("Misc.RecipeGroup.HardmodeAnvil"), 525, 1220);
		HardmodeAnvil = RecipeGroup.RegisterGroup("HardmodeAnvil", group);
		group = new RecipeGroup(() => CalamityUtils.GetTextValue("Misc.RecipeGroup.HardmodeForge"), 524, 1221);
		HardmodeForge = RecipeGroup.RegisterGroup("HardmodeForge", group);
		AnyFood = RecipeGroup.RegisterGroup("AnyFood", GetFoodItems());
	}

	private static void AddOreAndBarRecipeGroups()
	{
		RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(12), 12, 699);
		AnyCopperOre = RecipeGroup.RegisterGroup("AnyCopperOre", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(20), 20, 703);
		AnyCopperBar = RecipeGroup.RegisterGroup("AnyCopperBar", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(14), 14, 701);
		AnySilverOre = RecipeGroup.RegisterGroup("AnySilverOre", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(21), 21, 705);
		AnySilverBar = RecipeGroup.RegisterGroup("AnySilverBar", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(13), 13, 702);
		AnyGoldOre = RecipeGroup.RegisterGroup("AnyGoldOre", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(19), 19, 706);
		AnyGoldBar = RecipeGroup.RegisterGroup("AnyGoldBar", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(56), 56, 880);
		AnyEvilOre = RecipeGroup.RegisterGroup("AnyEvilOre", group);
		group = new RecipeGroup(() => CalamityUtils.GetTextValue("Misc.RecipeGroup.AnyEvilBar"), 57, 1257);
		AnyEvilBar = RecipeGroup.RegisterGroup("AnyEvilBar", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(364), 364, 1104);
		AnyCobaltOre = RecipeGroup.RegisterGroup("AnyCobaltOre", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(381), 381, 1184);
		AnyCobaltBar = RecipeGroup.RegisterGroup("AnyCobaltBar", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(365), 365, 1105);
		AnyMythrilOre = RecipeGroup.RegisterGroup("AnyMythrilOre", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(382), 382, 1191);
		AnyMythrilBar = RecipeGroup.RegisterGroup("AnyMythrilBar", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(366), 366, 1106);
		AnyAdamantiteOre = RecipeGroup.RegisterGroup("AnyAdamantiteOre", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(391), 391, 1198);
		AnyAdamantiteBar = RecipeGroup.RegisterGroup("AnyAdamantiteBar", group);
	}

	private static void AddEvilBiomeItemRecipeGroups()
	{
		RecipeGroup group = new RecipeGroup(() => CalamityUtils.GetTextValue("Misc.RecipeGroup.Boss2Material"), 86, 1329);
		Boss2Material = RecipeGroup.RegisterGroup("Boss2Material", group);
		group = new RecipeGroup(() => CalamityUtils.GetTextValue("Misc.RecipeGroup.CursedFlameIchor"), 522, 1332);
		CursedFlameIchor = RecipeGroup.RegisterGroup("CursedFlameIchor", group);
		group = new RecipeGroup(() => CalamityUtils.GetTextValue("Misc.RecipeGroup.AnyEvilWater"), 423, 3477);
		AnyEvilWater = RecipeGroup.RegisterGroup("AnyEvilWater", group);
	}

	private static void AddBiomeBlockRecipeGroups()
	{
		Func<string> getName = () => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(3);
		int[] obj = new int[5] { 3, 61, 836, 409, 0 };
		obj[4] = ModContent.ItemType<AstralStone>();
		RecipeGroup group = new RecipeGroup(getName, obj);
		AnyStoneBlock = RecipeGroup.RegisterGroup("AnyStoneBlock", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(593), 593, ModContent.ItemType<AstralSnow>());
		AnySnowBlock = RecipeGroup.RegisterGroup("AnySnowBlock", group);
		Func<string> getName2 = () => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(664);
		int[] obj2 = new int[5] { 664, 833, 835, 834, 0 };
		obj2[4] = ModContent.ItemType<AstralIce>();
		group = new RecipeGroup(getName2, obj2);
		AnyIceBlock = RecipeGroup.RegisterGroup("AnyIceBlock", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(424), 424, 1103, ModContent.ItemType<NovaeSlag>());
		AnySiltBlock = RecipeGroup.RegisterGroup("AnySiltBlock", group);
		group = new RecipeGroup(() => CalamityUtils.GetTextValue("Misc.RecipeGroup.AnyEvilBlock"), 61, 836, 833, 835, 370, 1246, 3274, 3275, 3276, 3277);
		AnyEvilBlock = RecipeGroup.RegisterGroup("AnyEvilBlock", group);
		group = new RecipeGroup(() => CalamityUtils.GetTextValue("Misc.RecipeGroup.AnyGoodBlock"), 409, 834, 408, 3338, 3339);
		AnyGoodBlock = RecipeGroup.RegisterGroup("AnyGoodBlock", group);
	}

	private static void AddEquipmentRecipeGroups()
	{
		Func<string> getName = () => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(24);
		int[] obj = new int[10] { 24, 2745, 656, 2517, 653, 921, 659, 5284, 0, 0 };
		obj[8] = ModContent.ItemType<AcidwoodSword>();
		obj[9] = ModContent.ItemType<DriftwoodSword>();
		RecipeGroup group = new RecipeGroup(getName, obj);
		AnyWoodenSword = RecipeGroup.RegisterGroup("AnyWoodenSword", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(553), 553, 558, 559, 4873, 4897, 4898, 4896, 4899);
		AnyHallowedHelmet = RecipeGroup.RegisterGroup("AnyHallowedHelmet", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(551), 551, 4900);
		AnyHallowedPlatemail = RecipeGroup.RegisterGroup("AnyHallowedPlatemail", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(552), 552, 4901);
		AnyHallowedGreaves = RecipeGroup.RegisterGroup("AnyHallowedGreaves", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(264), 264, 715);
		AnyGoldCrown = RecipeGroup.RegisterGroup("AnyGoldCrown", group);
		Func<string> getName2 = () => CalamityUtils.GetTextValue("Misc.RecipeGroup.LunarPickaxe");
		int[] obj2 = new int[5] { 2786, 2776, 2781, 3466, 0 };
		obj2[4] = ModContent.ItemType<GenesisPickaxe>();
		group = new RecipeGroup(getName2, obj2);
		LunarPickaxe = RecipeGroup.RegisterGroup("LunarPickaxe", group);
		group = new RecipeGroup(() => CalamityUtils.GetTextValue("Misc.RecipeGroup.LunarHamaxe"), 3522, 3523, 3524, 3525);
		LunarHamaxe = RecipeGroup.RegisterGroup("LunarHamaxe", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(555), 555, 3991, 4000, 4001);
		AnyManaFlower = RecipeGroup.RegisterGroup("AnyManaFlower", group);
		group = new RecipeGroup(() => CalamityUtils.GetTextValue("Misc.RecipeGroup.AnyQuiver"), 1321, 4002, 4006);
		AnyQuiver = RecipeGroup.RegisterGroup("AnyQuiver", group);
		group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(321), 321, 1173, 1174, 1175, 1176, 1177, 3229, 3230, 3231, 3232, 3233);
		AnyTombstone = RecipeGroup.RegisterGroup("AnyTombstone", group);
	}

	private static RecipeGroup GetFoodItems()
	{
		List<int> foodIds = new List<int>();
		foreach (KeyValuePair<int, Item> item2 in ContentSamples.ItemsByType)
		{
			Item item = item2.Value;
			if (BuffID.Sets.IsWellFed[item.buffType])
			{
				foodIds.Add(item.type);
			}
		}
		return new RecipeGroup(() => CalamityUtils.GetTextValue("Misc.RecipeGroup.AnyFood"), foodIds.ToArray());
	}

	public static void HandleRecipes()
	{
		EditVanillaRecipes();
		Recipe.Create(259).AddIngredient(1330, 2).AddTile(18)
			.Register();
		Recipe.Create(75).AddIngredient<StarblightSoot>(5).AddTile(16)
			.Register()
			.DisableDecraft();
		Recipe.Create(1101).AddIngredient(ModContent.ItemType<Stohne>()).AddTile(303)
			.Register();
		Recipe.Create(771, 100).AddRecipeGroup("IronBar").AddIngredient(1432, 100)
			.AddIngredient(1347, 4)
			.AddTile(16)
			.Register();
		Recipe.Create(772, 100).AddRecipeGroup("IronBar").AddIngredient(1432, 100)
			.AddIngredient(1347, 5)
			.AddTile(16)
			.Register();
		Recipe.Create(29).AddIngredient(3, 5).AddIngredient(178, 2)
			.AddIngredient(188)
			.AddTile(16)
			.Register();
		Recipe.Create(1291).AddIngredient<PlantyMush>(10).AddIngredient<LivingShard>()
			.AddTile(134)
			.Register()
			.DisableDecraft();
		Recipe.Create(2274, 33).AddIngredient(8, 33).AddIngredient<SeaPrism>()
			.AddTile(16)
			.Register()
			.DisableDecraft();
		Recipe.Create(3213).AddIngredient(87).AddIngredient(320, 2)
			.AddIngredient<BloodOrb>()
			.AddIngredient(73, 15)
			.AddTile(16)
			.Register();
		Recipe.Create(4819).AddIngredient<ScorchedBone>(20).AddIngredient(4413)
			.AddTile(77)
			.Register();
		Recipe.Create(4263).AddIngredient(4090, 20).AddIngredient(4412)
			.AddTile(16)
			.Register();
		Recipe.Create(1256).AddIngredient(64).AddTile(16)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(64).AddIngredient(1256).AddTile(16)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(802).AddIngredient(162).AddTile(16)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(162).AddIngredient(802).AddTile(16)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(800).AddIngredient(96).AddTile(16)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(96).AddIngredient(800).AddTile(16)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(3062).AddIngredient(115).AddTile(114)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(115).AddIngredient(3062).AddTile(114)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(3223).AddIngredient(3224).AddTile(114)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(3224).AddIngredient(3223).AddTile(114)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(3020).AddIngredient(3023).AddTile(114)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(3023).AddIngredient(3020).AddTile(114)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(3007).AddIngredient(3008).AddTile(16)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(3008).AddIngredient(3007).AddTile(16)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(3012).AddIngredient(3013).AddTile(16)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(3013).AddIngredient(3012).AddTile(16)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(3014).AddIngredient(3006).AddTile(16)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(3006).AddIngredient(3014).AddTile(16)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(3015).AddIngredient(3016).AddTile(114)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		Recipe.Create(3016).AddIngredient(3015).AddTile(114)
			.AddCondition(Condition.InGraveyard)
			.Register()
			.DisableDecraft();
		AddAstralRecipeVariants();
		AddBloodOrbPotionRecipes();
		AddCookedFood();
		AddMiscItemRecipes();
		AddTombstoneRecipes();
		AddEarlyGameWeaponRecipes();
		AddEarlyGameAccessoryRecipes();
		AddHardmodeItemRecipes();
		AddArmorRecipes();
	}

	internal static void EditVanillaRecipes()
	{
		Dictionary<Func<Recipe, bool>, Action<Recipe>> dictionary = new Dictionary<Func<Recipe, bool>, Action<Recipe>>(128);
		dictionary.Add(Vanilla(4457), Disable);
		dictionary.Add(Vanilla(4458), Disable);
		dictionary.Add(Vanilla(259), ChangeIngredientStack(68, 2));
		dictionary.Add(Vanilla(51), JesterArrowRecipeEdit);
		dictionary.Add(Vanilla(2351), TeleportationPotionRecipeEdit);
		dictionary.Add(Vanilla(70), WormFoodRecipeEdit);
		dictionary.Add(Vanilla(1331), BloodySpineRecipeEdit);
		dictionary.Add(Vanilla(361), ChangeIngredientStack(362, 5));
		dictionary.Add(Vanilla(1130), BeenadeRecipeEdit);
		dictionary.Add(Vanilla(1006), ChangeIngredientStack(947, 4));
		dictionary.Add(Vanilla(1220), ChangeIngredientStack(1191, 10));
		dictionary.Add(Vanilla(1552), ChangeIngredientStack(183, 5));
		dictionary.Add(Vanilla(675), TrueNightsEdgeRecipeEdit);
		dictionary.Add(Vanilla(674), ChangeIngredientStack(1006, 12));
		dictionary.Add(Vanilla(5298), AddIngredient(ModContent.ItemType<PearlShard>(), 5));
		dictionary.Add(Vanilla(1164), AddIngredient(ModContent.ItemType<AerialiteBar>(), 3));
		dictionary.Add((Recipe recipe) => recipe.HasResult(5331) && !recipe.HasIngredient(1164), AddIngredient(ModContent.ItemType<AerialiteBar>(), 3));
		dictionary.Add(Vanilla(273), AddIngredient(ModContent.ItemType<PurifiedGel>(), 5));
		dictionary.Add(Vanilla(3993), AddIngredient(520, 5));
		dictionary.Add(Vanilla(425), RemoveIngredient(549));
		dictionary.Add(Vanilla(4874), AddIngredient(ModContent.ItemType<EssenceofHavoc>(), 4));
		dictionary.Add(Vanilla(3779), AddGroup(AnyAdamantiteBar, 2));
		dictionary.Add(Vanilla(757), AddIngredient(ModContent.ItemType<LivingShard>(), 12));
		dictionary.Add(Vanilla(1343), AddIngredient(ModContent.ItemType<ScoriaBar>(), 5));
		dictionary.Add(Vanilla(4956), ZenithRecipeEdit);
		dictionary.Add(VanillaEach(new int[16]
		{
			544, 556, 557, 5334, 389, 3283, 2750, 4911, 493, 492,
			761, 425, 545, 546, 1334, 1335
		}), ReplaceTile(134, 16));
		dictionary.Add(Vanilla(2535), RemoveIngredient(1225));
		dictionary.Add(VanillaEach(new int[3] { 119, 219, 4821 }), SwapIngredients(0, 1));
		dictionary.Add(VanillaEach(new int[4] { 2199, 2200, 2201, 2202 }), SwapIngredients(0, 1));
		dictionary.Add(Vanilla(1844), RemoveIngredient(1225));
		dictionary.Add(Vanilla(1958), RemoveIngredient(547));
		dictionary.Add(Vanilla(55), EnchantedBoomerangRecipeEdit);
		dictionary.Add(Vanilla(5438), FertilizerRecipeEdit);
		dictionary.Add(VanillaEach(new int[4] { 3468, 3469, 3470, 3471 }), LunarWingsRecipeEdits);
		dictionary.Add(Vanilla(3992), ReplaceIngredient(897, 536));
		Dictionary<Func<Recipe, bool>, Action<Recipe>> edits = dictionary;
		IEnumerator<Recipe> recipeEnumerator = Main.recipe.ToList().GetEnumerator();
		while (recipeEnumerator.MoveNext())
		{
			Recipe r = recipeEnumerator.Current;
			foreach (KeyValuePair<Func<Recipe, bool>, Action<Recipe>> kv in edits)
			{
				if (kv.Key(r))
				{
					kv.Value(r);
				}
			}
		}
		static Action<Recipe> AddGroup(int groupID, int stack = 1)
		{
			return delegate(Recipe recipe)
			{
				recipe.AddRecipeGroup(groupID, stack);
			};
		}
		static Action<Recipe> AddIngredient(int itemID, int stack = 1)
		{
			return delegate(Recipe recipe)
			{
				recipe.AddIngredient(itemID, stack);
			};
		}
		static Action<Recipe> ChangeIngredientStack(int itemID, int stack = 1)
		{
			return delegate(Recipe r2)
			{
				r2.ChangeIngredientStack(itemID, stack);
			};
		}
		static void Disable(Recipe recipe)
		{
			recipe.DisableRecipe();
		}
		static Action<Recipe> RemoveIngredient(int itemID)
		{
			return delegate(Recipe recipe)
			{
				recipe.RemoveIngredient(itemID);
			};
		}
		static Action<Recipe> ReplaceIngredient(int oldItemID, int newItemID)
		{
			return delegate(Recipe recipe)
			{
				int num = recipe.IngredientIndex(oldItemID);
				if (num != -1)
				{
					Item item = new Item();
					item.SetDefaults(newItemID);
					item.stack = recipe.requiredItem[num].stack;
					recipe.requiredItem[num] = item;
				}
			};
		}
		static Action<Recipe> ReplaceTile(int oldTileID, int newTileID)
		{
			return delegate(Recipe recipe)
			{
				int num = recipe.requiredTile.IndexOf(oldTileID);
				if (num != -1)
				{
					recipe.requiredTile[num] = newTileID;
				}
			};
		}
		static Action<Recipe> SwapIngredients(int i1, int i2)
		{
			return delegate(Recipe recipe)
			{
				if (recipe.requiredItem.Count >= i1 + 1 && recipe.requiredItem.Count >= i2 + 1)
				{
					Item value = recipe.requiredItem[i1];
					recipe.requiredItem[i1] = recipe.requiredItem[i2];
					recipe.requiredItem[i2] = value;
				}
			};
		}
		static Func<Recipe, bool> Vanilla(int itemID)
		{
			return (Recipe recipe) => recipe.Mod == null && recipe.HasResult(itemID);
		}
		static Func<Recipe, bool> VanillaEach(params int[] itemIDs)
		{
			return (Recipe recipe) => recipe.Mod == null && itemIDs.Any(recipe.HasResult);
		}
	}

	private static void JesterArrowRecipeEdit(Recipe r)
	{
		int intendedStack = 50;
		if (r.createItem.stack < intendedStack)
		{
			r.createItem.stack = intendedStack;
		}
		r.ChangeIngredientStack(40, intendedStack);
	}

	private static void TeleportationPotionRecipeEdit(Recipe r)
	{
		int intendedStack = 5;
		if (r.createItem.stack < intendedStack)
		{
			r.createItem.stack = intendedStack;
		}
		r.ChangeIngredientStack(126, intendedStack);
	}

	private static void BeenadeRecipeEdit(Recipe r)
	{
		int intendedStack = 4;
		if (r.createItem.stack < intendedStack)
		{
			r.createItem.stack = intendedStack;
		}
		r.ChangeIngredientStack(168, intendedStack);
	}

	private static void TrueNightsEdgeRecipeEdit(Recipe r)
	{
		int intendedStack = 3;
		r.ChangeIngredientStack(549, intendedStack);
		r.ChangeIngredientStack(548, intendedStack);
		r.ChangeIngredientStack(547, intendedStack);
	}

	private static void WormFoodRecipeEdit(Recipe r)
	{
		r.ChangeIngredientStack(67, 20);
		r.ChangeIngredientStack(68, 10);
	}

	private static void BloodySpineRecipeEdit(Recipe r)
	{
		r.ChangeIngredientStack(2886, 20);
		r.ChangeIngredientStack(1330, 10);
	}

	private static void ZenithRecipeEdit(Recipe r)
	{
		r.AddIngredient(ModContent.ItemType<AuricBar>(), 5);
		int idx = r.requiredTile.IndexOf(134);
		if (idx != -1)
		{
			r.requiredTile[idx] = ModContent.TileType<CosmicAnvil>();
		}
	}

	private static void LunarWingsRecipeEdits(Recipe r)
	{
		r.AddIngredient(575, 20);
		if (r.requiredItem.Count >= 3)
		{
			Item store = r.requiredItem[0];
			r.requiredItem[0] = r.requiredItem[2];
			r.requiredItem[2] = r.requiredItem[1];
			r.requiredItem[1] = store;
		}
	}

	private static void EnchantedBoomerangRecipeEdit(Recipe r)
	{
		r.AddRecipeGroup(AnyGoldBar, 8);
		r.AddTile(16);
		if (r.requiredItem.Count >= 3)
		{
			Item store = r.requiredItem[1];
			r.requiredItem[1] = r.requiredItem[2];
			r.requiredItem[2] = store;
			r.requiredItem[2].stack = 6;
		}
	}

	private static void FertilizerRecipeEdit(Recipe r)
	{
		r.AddCustomShimmerResult(5395, 3);
		r.AddCustomShimmerResult(ModContent.ItemType<AncientBoneDust>(), 3);
		r.AddCustomShimmerResult(172, 3);
		Recipe recipe = Recipe.Create(5438);
		recipe.AddIngredient(5395, 3);
		recipe.AddIngredient<ScorchedBone>(6);
		recipe.AddIngredient(172, 3);
		recipe.AddTile(13);
		recipe.Register();
		recipe.SortAfterFirstRecipesOf(5438);
		recipe.DisableDecraft();
	}

	private static void InsertShimmerResult(int result, int ingredient)
	{
		ItemID.Sets.ShimmerTransformToItem[result] = ItemID.Sets.ShimmerTransformToItem[ingredient];
		ItemID.Sets.ShimmerTransformToItem[ingredient] = result;
	}

	public static void AddShimmerRecipes()
	{
		int[] shimmerTransformToItem = ItemID.Sets.ShimmerTransformToItem;
		InsertShimmerResult(ModContent.ItemType<RogueEmblem>(), 2998);
		shimmerTransformToItem[3119] = 3118;
		shimmerTransformToItem[3118] = 3099;
		shimmerTransformToItem[3099] = 3119;
		shimmerTransformToItem[989] = 4144;
		shimmerTransformToItem[4144] = 989;
		shimmerTransformToItem[3290] = 3282;
		shimmerTransformToItem[4348] = 4347;
	}

	private static void AddAstralRecipeVariants()
	{
		Recipe recipe = Recipe.Create(5440, 10);
		recipe.AddIngredient(4389);
		recipe.AddIngredient<AstralClay>(10);
		recipe.AddTile(17);
		recipe.Register();
		recipe.SortAfterFirstRecipesOf(5440);
		recipe.DisableDecraft();
		Recipe recipe2 = Recipe.Create(356);
		recipe2.AddIngredient<AstralClay>(2);
		recipe2.AddTile(17);
		recipe2.Register();
		recipe2.SortAfterFirstRecipesOf(356);
		recipe2.DisableDecraft();
		Recipe recipe3 = Recipe.Create(222);
		recipe3.AddIngredient<AstralClay>(5);
		recipe3.AddTile(17);
		recipe3.Register();
		recipe3.SortAfterFirstRecipesOf(222);
		recipe3.DisableDecraft();
		Recipe recipe4 = Recipe.Create(5444, 10);
		recipe4.AddIngredient(5128);
		recipe4.AddIngredient<AstralClay>(10);
		recipe4.AddTile(17);
		recipe4.Register();
		recipe4.SortAfterFirstRecipesOf(5444);
		recipe4.DisableDecraft();
		Recipe recipe5 = Recipe.Create(5441, 10);
		recipe5.AddIngredient(4377);
		recipe5.AddIngredient<AstralClay>(10);
		recipe5.AddTile(17);
		recipe5.Register();
		recipe5.SortAfterFirstRecipesOf(5441);
		recipe5.DisableDecraft();
		Recipe recipe6 = Recipe.Create(5439, 10);
		recipe6.AddIngredient(4354);
		recipe6.AddIngredient<AstralClay>(10);
		recipe6.AddTile(17);
		recipe6.Register();
		recipe6.SortAfterFirstRecipesOf(5439);
		recipe6.DisableDecraft();
		Recipe recipe7 = Recipe.Create(5443, 10);
		recipe7.AddIngredient(5127);
		recipe7.AddIngredient<AstralClay>(10);
		recipe7.AddTile(17);
		recipe7.Register();
		recipe7.SortAfterFirstRecipesOf(5443);
		recipe7.DisableDecraft();
		Recipe recipe8 = Recipe.Create(350);
		recipe8.AddIngredient<AstralClay>(4);
		recipe8.AddTile(17);
		recipe8.Register();
		recipe8.SortAfterFirstRecipesOf(350);
		recipe8.DisableDecraft();
		Recipe recipe9 = Recipe.Create(4326);
		recipe9.AddIngredient<AstralClay>(2);
		recipe9.AddTile(17);
		recipe9.Register();
		recipe9.SortAfterFirstRecipesOf(4326);
		recipe9.DisableDecraft();
		Recipe recipe10 = Recipe.Create(5008);
		recipe10.AddIngredient<AstralClay>(12);
		recipe10.AddIngredient(154, 12);
		recipe10.AddTile(17);
		recipe10.Register();
		recipe10.SortAfterFirstRecipesOf(5008);
		recipe10.DisableDecraft();
		Recipe recipe11 = Recipe.Create(5048);
		recipe11.AddIngredient<AstralClay>(10);
		recipe11.AddIngredient(1992, 3);
		recipe11.AddTile(86);
		recipe11.Register();
		recipe11.SortAfterFirstRecipesOf(5048);
		recipe11.DisableDecraft();
		Recipe recipe12 = Recipe.Create(5442, 10);
		recipe12.AddIngredient(4378);
		recipe12.AddIngredient<AstralClay>(10);
		recipe12.AddTile(17);
		recipe12.Register();
		recipe12.SortAfterFirstRecipesOf(5442);
		recipe12.DisableDecraft();
		Recipe recipe13 = Recipe.Create(5054);
		recipe13.AddIngredient(170, 20);
		recipe13.AddIngredient<AstralDirt>(10);
		recipe13.AddIngredient(313);
		recipe13.AddTile(86);
		recipe13.Register();
		recipe13.SortAfterFirstRecipesOf(5054);
		recipe13.DisableDecraft();
		Recipe recipe14 = Recipe.Create(5055);
		recipe14.AddIngredient(225, 20);
		recipe14.AddIngredient<AstralDirt>(15);
		recipe14.AddTile(86);
		recipe14.Register();
		recipe14.SortAfterFirstRecipesOf(5055);
		recipe14.DisableDecraft();
		Recipe recipe15 = Recipe.Create(5056);
		recipe15.AddIngredient(225, 20);
		recipe15.AddIngredient<AstralDirt>(15);
		recipe15.AddTile(86);
		recipe15.Register();
		recipe15.SortAfterFirstRecipesOf(5056);
		recipe15.DisableDecraft();
		Recipe recipe16 = Recipe.Create(974, 3);
		recipe16.AddIngredient(8, 3);
		recipe16.AddIngredient<AstralIce>();
		recipe16.Register();
		recipe16.SortAfterFirstRecipesOf(974);
		recipe16.DisableDecraft();
		Recipe recipe17 = Recipe.Create(4617);
		recipe17.AddIngredient(4283);
		recipe17.AddIngredient(31);
		recipe17.AddIngredient<AstralSnow>();
		recipe17.AddTile(96);
		recipe17.Register();
		recipe17.SortAfterFirstRecipesOf(4617);
		recipe17.DisableDecraft();
		Recipe recipe18 = Recipe.Create(949, 15);
		recipe18.AddIngredient<AstralSnow>();
		recipe18.Register();
		recipe18.SortAfterFirstRecipesOf(949);
		recipe18.DisableDecraft();
		Recipe recipe19 = Recipe.Create(4383, 3);
		recipe19.AddIngredient(8, 3);
		recipe19.AddIngredient<HardenedAstralSand>();
		recipe19.Register();
		recipe19.SortAfterFirstRecipesOf(4383);
		recipe19.DisableDecraft();
	}

	private static void AddBloodOrbPotionRecipes()
	{
		short[] FiveOrbGroup = new short[27]
		{
			2997, 2351, 290, 295, 298, 297, 299, 304, 2329, 301,
			292, 289, 2326, 2344, 291, 302, 2327, 2325, 2322, 2354,
			2356, 2355, 2756, 2352, 2353, 2350, 4477
		};
		short[] TenOrbGroup = new short[12]
		{
			303, 305, 296, 300, 2324, 294, 293, 2359, 288, 4870,
			4478, 5211
		};
		short[] FifteenOrbGroup = new short[8] { 2349, 2347, 2346, 2345, 2323, 2328, 2348, 4479 };
		short[] array = FiveOrbGroup;
		foreach (short potion in array)
		{
			Recipe recipe = Recipe.Create(potion);
			recipe.AddIngredient(126);
			recipe.AddIngredient<BloodOrb>(5);
			recipe.AddTile(355);
			recipe.Register();
			recipe.SortAfterFirstRecipesOf(potion);
			recipe.DisableDecraft();
		}
		array = TenOrbGroup;
		foreach (short potion2 in array)
		{
			Recipe recipe2 = Recipe.Create(potion2);
			recipe2.AddIngredient(126);
			recipe2.AddIngredient<BloodOrb>(10);
			recipe2.AddTile(355);
			recipe2.Register();
			recipe2.SortAfterFirstRecipesOf(potion2);
			recipe2.DisableDecraft();
		}
		array = FifteenOrbGroup;
		foreach (short potion3 in array)
		{
			Recipe recipe3 = Recipe.Create(potion3);
			recipe3.AddIngredient(126);
			recipe3.AddIngredient<BloodOrb>(15);
			recipe3.AddTile(355);
			recipe3.Register();
			recipe3.SortAfterFirstRecipesOf(potion3);
			recipe3.DisableDecraft();
		}
	}

	private static void AddCookedFood()
	{
		Recipe recipe = Recipe.Create(2425);
		recipe.AddIngredient<TwinklingPollox>();
		recipe.AddTile(96);
		recipe.Register();
		recipe.SortAfterFirstRecipesOf(2425);
		recipe.DisableDecraft();
		Recipe recipe2 = Recipe.Create(2425);
		recipe2.AddIngredient<PrismaticGuppy>();
		recipe2.AddTile(96);
		recipe2.Register();
		recipe2.SortAfterFirstRecipesOf(2425);
		recipe2.DisableDecraft();
		Recipe recipe3 = Recipe.Create(2425);
		recipe3.AddIngredient<CoralskinFoolfish>();
		recipe3.AddTile(96);
		recipe3.Register();
		recipe3.SortAfterFirstRecipesOf(2425);
		recipe3.DisableDecraft();
		Recipe recipe4 = Recipe.Create(2425);
		recipe4.AddIngredient<GleamingCucumber>();
		recipe4.AddTile(96);
		recipe4.Register();
		recipe4.SortAfterFirstRecipesOf(2425);
		recipe4.DisableDecraft();
		Recipe recipe5 = Recipe.Create(2425);
		recipe5.AddIngredient<MoltenFishron>();
		recipe5.AddTile(96);
		recipe5.Register();
		recipe5.SortAfterFirstRecipesOf(2425);
		recipe5.DisableDecraft();
		Recipe recipe6 = Recipe.Create(2425);
		recipe6.AddIngredient<SpecularSturgeon>();
		recipe6.AddTile(96);
		recipe6.Register();
		recipe6.SortAfterFirstRecipesOf(2425);
		recipe6.DisableDecraft();
		Recipe recipe7 = Recipe.Create(2425);
		recipe7.AddIngredient<Squidoom>();
		recipe7.AddTile(96);
		recipe7.Register();
		recipe7.SortAfterFirstRecipesOf(2425);
		recipe7.DisableDecraft();
		Recipe recipe8 = Recipe.Create(4034);
		recipe8.AddIngredient<AldebaranAlewife>(2);
		recipe8.AddTile(96);
		recipe8.Register();
		recipe8.SortAfterFirstRecipesOf(4034);
		recipe8.DisableDecraft();
		Recipe recipe9 = Recipe.Create(4034);
		recipe9.AddIngredient<Bloodfin>(2);
		recipe9.AddTile(96);
		recipe9.Register();
		recipe9.SortAfterFirstRecipesOf(4034);
		recipe9.DisableDecraft();
		Recipe recipe10 = Recipe.Create(4034);
		recipe10.AddIngredient<CoastalDemonfish>(2);
		recipe10.AddTile(96);
		recipe10.Register();
		recipe10.SortAfterFirstRecipesOf(4034);
		recipe10.DisableDecraft();
		Recipe recipe11 = Recipe.Create(4034);
		recipe11.AddIngredient<Shadowfish>(2);
		recipe11.AddTile(96);
		recipe11.Register();
		recipe11.SortAfterFirstRecipesOf(4034);
		recipe11.DisableDecraft();
		Recipe recipe12 = Recipe.Create(4034);
		recipe12.AddIngredient<SunkenSailfish>(2);
		recipe12.AddTile(96);
		recipe12.Register();
		recipe12.SortAfterFirstRecipesOf(4034);
		recipe12.DisableDecraft();
		Recipe recipe13 = Recipe.Create(357);
		recipe13.AddIngredient(5);
		recipe13.AddIngredient<SeaMinnowItem>();
		recipe13.AddTile(96);
		recipe13.Register();
		recipe13.SortAfterFirstRecipesOf(357);
		recipe13.DisableDecraft();
		Recipe recipe14 = Recipe.Create(2427);
		recipe14.AddIngredient<CragBullhead>();
		recipe14.AddTile(18);
		recipe14.Register();
		recipe14.SortAfterFirstRecipesOf(2427);
		recipe14.DisableDecraft();
		Recipe recipe15 = Recipe.Create(2426);
		recipe15.AddIngredient<ProcyonidPrawn>();
		recipe15.AddTile(96);
		recipe15.Register();
		recipe15.SortAfterFirstRecipesOf(2426);
		recipe15.DisableDecraft();
		Recipe recipe16 = Recipe.Create(3532);
		recipe16.AddIngredient<PiggyItem>();
		recipe16.AddTile(96);
		recipe16.Register();
		recipe16.DisableDecraft();
	}

	private static void AddMiscItemRecipes()
	{
		Recipe recipe = Recipe.Create(4271);
		recipe.AddIngredient<BloodOrb>(10);
		recipe.AddRecipeGroup("AnyCopperBar", 3);
		recipe.AddTile(16);
		recipe.Register();
		recipe.DisableDecraft();
		Recipe recipe2 = Recipe.Create(602);
		recipe2.AddRecipeGroup(AnySnowBlock, 10);
		recipe2.AddIngredient(170, 5);
		recipe2.AddIngredient(520, 3);
		recipe2.AddIngredient(521, 3);
		recipe2.AddTile(16);
		recipe2.Register();
		recipe2.DisableDecraft();
		Recipe recipe3 = Recipe.Create(946);
		recipe3.AddIngredient(225, 5);
		recipe3.AddRecipeGroup("AnyCopperBar", 2);
		recipe3.AddTile(86);
		recipe3.Register();
		Recipe recipe4 = Recipe.Create(4276);
		recipe4.AddRecipeGroup("IronBar", 7);
		recipe4.AddRecipeGroup("AnyGoldBar", 3);
		recipe4.AddIngredient(178);
		recipe4.AddTile(16);
		recipe4.Register();
		Recipe recipe5 = Recipe.Create(4346);
		recipe5.AddIngredient(3, 100);
		recipe5.AddTile(16);
		recipe5.Register();
	}

	private static void AddEarlyGameWeaponRecipes()
	{
		Recipe recipe = Recipe.Create(284);
		recipe.AddIngredient(9, 7);
		recipe.AddTile(18);
		recipe.Register();
		Recipe recipe2 = Recipe.Create(3069);
		recipe2.AddIngredient(9, 5);
		recipe2.AddIngredient(8, 3);
		recipe2.AddIngredient(75);
		recipe2.AddCondition(Condition.NotRemixWorld);
		recipe2.AddTile(16);
		recipe2.Register();
		Recipe recipe3 = Recipe.Create(4281);
		recipe3.AddIngredient(2015);
		recipe3.AddRecipeGroup("Wood", 8);
		recipe3.AddTile(18);
		recipe3.Register();
		Recipe recipe4 = Recipe.Create(1309);
		recipe4.AddRecipeGroup("Wood", 6);
		recipe4.AddIngredient(23, 40);
		recipe4.AddTile(16);
		recipe4.Register();
		Recipe recipe5 = Recipe.Create(989);
		recipe5.AddIngredient<PearlShard>(10);
		recipe5.AddRecipeGroup(AnyGoldBar, 12);
		recipe5.AddIngredient(182);
		recipe5.AddIngredient(178);
		recipe5.AddTile(16);
		recipe5.Register();
		recipe5.DisableDecraft();
		Recipe recipe6 = Recipe.Create(65);
		recipe6.AddIngredient<AerialiteBar>(7);
		recipe6.AddIngredient(75, 10);
		recipe6.AddTile(16);
		recipe6.Register();
		recipe6.DisableDecraft();
		Recipe recipe7 = Recipe.Create(155);
		recipe7.AddIngredient<AerialiteBar>(7);
		recipe7.AddIngredient(154, 10);
		recipe7.AddTile(16);
		recipe7.Register();
		recipe7.DisableDecraft();
		Recipe recipe8 = Recipe.Create(165);
		recipe8.AddIngredient(531);
		recipe8.AddIngredient(317, 3);
		recipe8.AddIngredient(148);
		recipe8.AddTile(101);
		recipe8.Register();
		recipe8.DisableDecraft();
	}

	private static void AddEarlyGameAccessoryRecipes()
	{
		Recipe recipe = Recipe.Create(4341);
		recipe.AddRecipeGroup("Wood", 10);
		recipe.AddTile(106);
		recipe.Register();
		Recipe recipe2 = Recipe.Create(54);
		recipe2.AddIngredient(225, 10);
		recipe2.AddIngredient(290, 5);
		recipe2.AddTile(86);
		recipe2.Register();
		Recipe recipe3 = Recipe.Create(285);
		recipe3.AddRecipeGroup(AnyCopperBar, 5);
		recipe3.AddTile(16);
		recipe3.Register();
		Recipe recipe4 = Recipe.Create(212);
		recipe4.AddIngredient(331, 15);
		recipe4.AddIngredient(751, 5);
		recipe4.AddIngredient(3111, 5);
		recipe4.AddTile(16);
		recipe4.Register();
		Recipe recipe5 = Recipe.Create(950);
		recipe5.AddIngredient(5070, 3);
		recipe5.AddRecipeGroup("IronBar", 5);
		recipe5.AddTile(16);
		recipe5.Register();
		Recipe recipe6 = Recipe.Create(863);
		recipe6.AddIngredient(259, 5);
		recipe6.AddIngredient(302, 5);
		recipe6.AddTile(86);
		recipe6.Register();
		Recipe recipe7 = Recipe.Create(906);
		recipe7.AddIngredient(207, 3);
		recipe7.AddIngredient(173, 5);
		recipe7.AddRecipeGroup(AnyGoldBar, 5);
		recipe7.AddTile(16);
		recipe7.Register();
		recipe7.DisableDecraft();
		Recipe recipe8 = Recipe.Create(1323);
		recipe8.AddIngredient(208);
		recipe8.AddIngredient(173, 5);
		recipe8.AddIngredient(174, 5);
		recipe8.AddTile(16);
		recipe8.Register();
		Recipe recipe9 = Recipe.Create(987);
		recipe9.AddIngredient(31);
		recipe9.AddIngredient(751, 5);
		recipe9.AddRecipeGroup(AnySnowBlock, 5);
		recipe9.AddIngredient(320, 3);
		recipe9.AddTile(16);
		recipe9.Register();
		Recipe recipe10 = Recipe.Create(53);
		recipe10.AddIngredient(31);
		recipe10.AddIngredient(751, 5);
		recipe10.AddIngredient(320, 2);
		recipe10.AddTile(16);
		recipe10.Register();
		Recipe recipe11 = Recipe.Create(857);
		recipe11.AddIngredient(31);
		recipe11.AddIngredient(751, 5);
		recipe11.AddIngredient(169, 5);
		recipe11.AddIngredient<PearlShard>(3);
		recipe11.AddIngredient(320, 3);
		recipe11.AddTile(16);
		recipe11.Register();
		recipe11.DisableDecraft();
		Recipe recipe12 = Recipe.Create(4978);
		recipe12.AddIngredient(ModContent.ItemType<AncientBoneDust>(), 2);
		recipe12.AddIngredient(751, 5);
		recipe12.AddIngredient(320, 10);
		recipe12.AddTile(16);
		recipe12.Register();
		Recipe recipe13 = Recipe.Create(934);
		recipe13.AddIngredient(225, 10);
		recipe13.AddIngredient(323, 2);
		recipe13.AddIngredient<PearlShard>(5);
		recipe13.AddTile(16);
		recipe13.Register();
		recipe13.DisableDecraft();
		Recipe recipe14 = Recipe.Create(2423);
		recipe14.AddIngredient(2121, 6);
		recipe14.AddTile(16);
		recipe14.Register();
		recipe14.DisableDecraft();
		Recipe recipe15 = Recipe.Create(158);
		recipe15.AddRecipeGroup(AnyGoldBar, 8);
		recipe15.AddTile(16);
		recipe15.Register();
		Recipe recipe16 = Recipe.Create(159);
		recipe16.AddIngredient(3306);
		recipe16.AddIngredient(751, 10);
		recipe16.AddTile(220);
		recipe16.Register();
		Recipe recipe17 = Recipe.Create(156);
		recipe17.AddRecipeGroup(AnyCobaltBar, 5);
		recipe17.AddTile(16);
		recipe17.Register();
		recipe17.DisableDecraft();
		Recipe recipe18 = Recipe.Create(4822);
		recipe18.AddIngredient(225, 8);
		recipe18.AddIngredient(175, 5);
		recipe18.AddIngredient(173, 4);
		recipe18.AddTile(16);
		recipe18.Register();
		Recipe recipe19 = Recipe.Create(3017);
		recipe19.AddIngredient(225, 7);
		recipe19.AddIngredient(208);
		recipe19.AddIngredient(195, 5);
		recipe19.AddTile(86);
		recipe19.Register();
		Recipe recipe20 = Recipe.Create(1921);
		recipe20.AddIngredient(225, 10);
		recipe20.AddTile(86);
		recipe20.Register();
		Recipe recipe21 = Recipe.Create(3084);
		recipe21.AddRecipeGroup("IronBar", 5);
		recipe21.AddTile(16);
		recipe21.Register();
		Recipe recipe22 = Recipe.Create(5139);
		recipe22.AddIngredient<Driftwood>(10);
		recipe22.AddIngredient(4413);
		recipe22.AddRecipeGroup("IronBar");
		recipe22.AddTile(16);
		recipe22.Register();
	}

	private static void AddArmorRecipes()
	{
		Recipe recipe = Recipe.Create(803);
		recipe.AddIngredient(225, 4);
		recipe.AddIngredient(5070);
		recipe.AddTile(86);
		recipe.Register();
		Recipe recipe2 = Recipe.Create(804);
		recipe2.AddIngredient(225, 8);
		recipe2.AddIngredient(5070, 2);
		recipe2.AddTile(86);
		recipe2.Register();
		Recipe recipe3 = Recipe.Create(805);
		recipe3.AddIngredient(225, 6);
		recipe3.AddIngredient(5070);
		recipe3.AddTile(86);
		recipe3.Register();
	}

	private static void AddHardmodeItemRecipes()
	{
		Recipe recipe = Recipe.Create(2223);
		recipe.AddIngredient(1552, 16);
		recipe.AddTile(134);
		recipe.Register();
		recipe.DisableDecraft();
		Recipe recipe2 = Recipe.Create(4760);
		recipe2.AddRecipeGroup(AnyCobaltBar, 12);
		recipe2.AddIngredient(520, 4);
		recipe2.AddTile(16);
		recipe2.Register();
		recipe2.DisableDecraft();
		Recipe recipe3 = Recipe.Create(4457, 333);
		recipe3.AddIngredient(773, 333);
		recipe3.AddIngredient(3467);
		recipe3.AddTile(412);
		recipe3.Register();
		Recipe recipe4 = Recipe.Create(4458, 333);
		recipe4.AddIngredient(774, 333);
		recipe4.AddIngredient(3467);
		recipe4.AddTile(412);
		recipe4.Register();
	}

	private static void AddTombstoneRecipes()
	{
		short[] woodenTombstones = new short[2] { 1174, 1173 };
		short[] stoneTombstones = new short[4] { 1176, 1175, 1177, 321 };
		short[] goldenTombstones = new short[5] { 3229, 3230, 3231, 3232, 3233 };
		short[] array = woodenTombstones;
		for (int i = 0; i < array.Length; i++)
		{
			Recipe recipe = Recipe.Create(array[i]);
			recipe.AddRecipeGroup(RecipeGroupID.Wood, 15);
			recipe.AddTile(106);
			recipe.Register();
			recipe.DisableDecraft();
		}
		array = stoneTombstones;
		for (int i = 0; i < array.Length; i++)
		{
			Recipe recipe2 = Recipe.Create(array[i]);
			recipe2.AddRecipeGroup(AnyStoneBlock, 15);
			recipe2.AddTile(283);
			recipe2.Register();
			recipe2.DisableDecraft();
		}
		array = goldenTombstones;
		for (int i = 0; i < array.Length; i++)
		{
			Recipe recipe3 = Recipe.Create(array[i]);
			recipe3.AddRecipeGroup(AnyStoneBlock, 15);
			recipe3.AddRecipeGroup(AnyGoldBar);
			recipe3.AddTile(283);
			recipe3.Register();
			recipe3.DisableDecraft();
		}
	}
}
