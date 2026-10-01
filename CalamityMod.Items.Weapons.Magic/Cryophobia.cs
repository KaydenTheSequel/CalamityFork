using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Cryophobia : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 34;
		base.Item.damage = 96;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 18;
		base.Item.useTime = 40;
		base.Item.useAnimation = 40;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 1.5f;
		base.Item.value = CalamityGlobalItem.RarityLightPurpleBuyPrice;
		base.Item.rare = 6;
		base.Item.UseSound = SoundID.Item117;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 6f;
		base.Item.shoot = ModContent.ProjectileType<CryoBlast>();
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}
}
