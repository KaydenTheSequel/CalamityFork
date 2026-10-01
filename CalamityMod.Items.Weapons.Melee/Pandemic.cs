using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee.Yoyos;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "ThePlaguebringer" })]
public class Pandemic : ModItem, ILocalizedModType, IModType
{
	public static float Reach = 480f;

	public static float Speed = 40f;

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
		base.Item.width = 30;
		base.Item.height = 32;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.damage = 100;
		base.Item.knockBack = 2.5f;
		base.Item.useTime = 22;
		base.Item.useAnimation = 22;
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item1;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.shoot = ModContent.ProjectileType<PandemicYoyo>();
		base.Item.shootSpeed = 14f;
		base.Item.rare = 8;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(5294).AddIngredient<InfectedArmorPlating>(6).AddIngredient<PlagueCellCanister>(10)
			.AddTile(134)
			.Register();
	}
}
