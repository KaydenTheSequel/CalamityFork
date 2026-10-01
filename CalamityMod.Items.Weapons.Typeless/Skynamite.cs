using CalamityMod.Projectiles.Typeless;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Typeless;

[LegacyName(new string[] { "AeroDynamite" })]
public class Skynamite : ModItem, ILocalizedModType, IModType
{
	public const int Damage = 250;

	public const float Knockback = 10f;

	public new string LocalizationCategory => "Items.Weapons.Typeless";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 99;
		ItemID.Sets.ItemsThatCountAsBombsForDemolitionistToSpawn[base.Type] = true;
		ItemID.Sets.CanBePlacedOnWeaponRacks[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 8;
		base.Item.height = 28;
		base.Item.useAnimation = (base.Item.useTime = 40);
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.shootSpeed = 5f;
		base.Item.shoot = ModContent.ProjectileType<AeroExplosive>();
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = Item.sellPrice(0, 0, 4);
		base.Item.rare = 1;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.Bombs;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(167).AddTile(305).Register();
	}
}
