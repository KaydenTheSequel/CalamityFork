using CalamityMod.Projectiles.Melee.Yoyos;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class SmokingComet : ModItem, ILocalizedModType, IModType
{
	public static float Reach = 320f;

	public static float Speed = 20f;

	public static float Duration = 21f;

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
		base.Item.width = 36;
		base.Item.height = 40;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.damage = 17;
		base.Item.knockBack = 1.5f;
		base.Item.useTime = 25;
		base.Item.useAnimation = 25;
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item1;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.shoot = ModContent.ProjectileType<SmokingCometYoyo>();
		base.Item.shootSpeed = 14f;
		base.Item.rare = 2;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.Calamity().donorItem = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(182, 5).AddIngredient(181, 5).AddIngredient(3111, 10)
			.AddIngredient(75, 15)
			.AddIngredient(117, 10)
			.AddTile(16)
			.Register();
	}
}
