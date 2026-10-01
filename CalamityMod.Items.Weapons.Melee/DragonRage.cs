using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class DragonRage : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 128;
		base.Item.height = 140;
		base.Item.damage = 1075;
		base.Item.knockBack = 7.5f;
		base.Item.useAnimation = (base.Item.useTime = 25);
		base.Item.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 14f;
		base.Item.shoot = ModContent.ProjectileType<DragonRageStaff>();
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.DD2_SkyDragonsFurySwing;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}
}
