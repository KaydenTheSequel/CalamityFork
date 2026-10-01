using CalamityMod.Projectiles.Magic;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class SnowstormStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 66;
		base.Item.height = 66;
		base.Item.damage = 39;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.channel = true;
		base.Item.mana = 18;
		base.Item.useTime = 70;
		base.Item.useAnimation = 70;
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item46;
		base.Item.shoot = ModContent.ProjectileType<Snowflake>();
		base.Item.shootSpeed = 7f;
		base.Item.autoReuse = true;
	}
}
