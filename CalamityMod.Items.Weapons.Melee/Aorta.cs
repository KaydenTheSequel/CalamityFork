using CalamityMod.Projectiles.Melee.Yoyos;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Aorta : ModItem, ILocalizedModType, IModType
{
	public static float Reach = 330f;

	public static float Speed = 25f;

	public static float Duration = 24f;

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
		base.Item.width = 30;
		base.Item.height = 26;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.damage = 31;
		base.Item.knockBack = 4.25f;
		base.Item.useTime = 22;
		base.Item.useAnimation = 22;
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item1;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.shoot = ModContent.ProjectileType<AortaYoyo>();
		base.Item.shootSpeed = 8f;
		base.Item.rare = 3;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
	}
}
