using CalamityMod.Projectiles.Melee.Yoyos;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class YinYo : ModItem, ILocalizedModType, IModType
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
		base.Item.width = 50;
		base.Item.height = 44;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.damage = 44;
		base.Item.knockBack = 3.5f;
		base.Item.useTime = 25;
		base.Item.useAnimation = 25;
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item1;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.shoot = ModContent.ProjectileType<YinYoyo>();
		base.Item.shootSpeed = 12f;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3317).AddIngredient(527).AddIngredient(528)
			.AddIngredient(520, 7)
			.AddIngredient(521, 7)
			.AddTile(16)
			.Register();
	}
}
