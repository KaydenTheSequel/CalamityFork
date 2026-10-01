using CalamityMod.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class AlulaAustralis : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 52;
		base.Item.damage = 65;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 8;
		base.Item.useTime = 17;
		base.Item.useAnimation = 17;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = SoundID.Item9;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AuroraAustralis>();
		base.Item.shootSpeed = 13f;
	}
}
