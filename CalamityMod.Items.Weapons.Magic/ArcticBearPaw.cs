using CalamityMod.Projectiles.Magic;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class ArcticBearPaw : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 24;
		base.Item.damage = 80;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 18;
		base.Item.useTime = 28;
		base.Item.useAnimation = 28;
		base.Item.useStyle = 5;
		base.Item.useTurn = false;
		base.Item.noMelee = true;
		base.Item.knockBack = 10f;
		base.Item.value = CalamityGlobalItem.RarityLightPurpleBuyPrice;
		base.Item.rare = 6;
		base.Item.UseSound = SoundID.Item8;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<ArcticBearPawProj>();
		base.Item.shootSpeed = 27f;
	}
}
