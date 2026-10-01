using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class SlitheringEels : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 40;
		base.Item.damage = 50;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 11;
		base.Item.useAnimation = (base.Item.useTime = 40);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.NPCHit13;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<SlitheringEelProjectile>();
		base.Item.shootSpeed = 14f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity.RotatedByRandom(0.32499998807907104), type, damage, knockback, player.whoAmI);
		return false;
	}
}
