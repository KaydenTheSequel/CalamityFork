using CalamityMod.Projectiles.Melee.Yoyos;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class SulphurousGrabber : ModItem, ILocalizedModType, IModType
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
		base.Item.height = 36;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.damage = 40;
		base.Item.knockBack = 3.5f;
		base.Item.useTime = 25;
		base.Item.useAnimation = 25;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item1;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.shoot = ModContent.ProjectileType<SulphurousGrabberYoyo>();
		base.Item.shootSpeed = 12f;
		base.Item.rare = 5;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
	}
}
