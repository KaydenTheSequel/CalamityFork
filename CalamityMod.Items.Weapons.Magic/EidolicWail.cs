using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class EidolicWail : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 60;
		base.Item.height = 60;
		base.Item.damage = 943;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 30;
		base.Item.useTime = 10;
		base.Item.useAnimation = 30;
		base.Item.reuseDelay = 40;
		base.Item.useLimitPerAnimation = 3;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 1f;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.UseSound = CommonCalamitySounds.WyrmScreamSound;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 14f;
		base.Item.shoot = ModContent.ProjectileType<EidolicWailSoundwave>();
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-5f, 0f);
	}
}
