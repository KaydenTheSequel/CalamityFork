using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee.Yoyos;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class TheGodsGambit : ModItem, ILocalizedModType, IModType
{
	public static float Reach = 400f;

	public static float Speed = 32f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Reach.ToTiles(), Speed);

	public override void SetStaticDefaults()
	{
		ItemID.Sets.Yoyo[base.Type] = true;
		ItemID.Sets.GamepadExtraRange[base.Type] = 15;
		ItemID.Sets.GamepadSmartQuickReach[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 38;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.damage = 28;
		base.Item.knockBack = 3.5f;
		base.Item.useTime = 21;
		base.Item.useAnimation = 21;
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item1;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.shoot = ModContent.ProjectileType<GodsGambitYoyo>();
		base.Item.shootSpeed = 10f;
		base.Item.rare = 4;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PurifiedGel>(18).AddIngredient<BlightedGel>(18).AddTile(220)
			.Register();
	}
}
