using CalamityMod.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.SunkenSeaCatches;

public class SparklingEmpress : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 44;
		base.Item.damage = 10;
		base.Item.noMelee = true;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.channel = true;
		base.Item.rare = 2;
		base.Item.mana = 5;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.UseSound = SoundID.Item13;
		base.Item.useStyle = 5;
		base.Item.shootSpeed = 14f;
		base.Item.shoot = ModContent.ProjectileType<SparklingLaser>();
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
	}
}
