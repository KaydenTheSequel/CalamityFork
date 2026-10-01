using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.Melee.Yoyos;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "Whirlpool" })]
public class Riptide : ModItem, ILocalizedModType, IModType
{
	public static float Reach = 288f;

	public static float Speed = 25f;

	public static float Duration = 18f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Reach.ToTiles(), Speed, Duration);

	public override void SetStaticDefaults()
	{
		ItemID.Sets.Yoyo[base.Type] = true;
		ItemID.Sets.GamepadExtraRange[base.Type] = 15;
		ItemID.Sets.GamepadSmartQuickReach[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 48;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.damage = 12;
		base.Item.knockBack = 1f;
		base.Item.useTime = 25;
		base.Item.useAnimation = 25;
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item1;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.shoot = ModContent.ProjectileType<RiptideYoyo>();
		base.Item.shootSpeed = 18f;
		base.Item.rare = 2;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PearlShard>(3).AddIngredient<SeaPrism>(7).AddIngredient<Navystone>(10)
			.AddTile(16)
			.Register();
	}
}
