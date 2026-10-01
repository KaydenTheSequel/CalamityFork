using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Meowthrower : ModItem, ILocalizedModType, IModType
{
	public int CatCounter;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 74;
		base.Item.height = 24;
		base.Item.damage = 29;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 8;
		base.Item.useAnimation = 32;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 0.4f;
		base.Item.UseSound = SoundID.Item34;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<MeowFire>();
		base.Item.shootSpeed = 8f;
		base.Item.useAmmo = AmmoID.Gel;
		base.Item.consumeAmmoOnFirstShotOnly = true;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		CatCounter++;
		if (CatCounter >= 4)
		{
			CatCounter = 0;
			Vector2 newPos = position + velocity.SafeNormalize(Vector2.UnitX) * 38f;
			Vector2 newVel = velocity.RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(6f, 15f) * (float)Main.rand.NextBool().ToDirectionInt())) * 1.5f;
			Projectile.NewProjectile(source, newPos, newVel, ModContent.ProjectileType<MeowCreature>(), damage, knockback, player.whoAmI);
		}
		for (int i = 0; i < 2; i++)
		{
			Vector2 newVel2 = velocity.RotatedByRandom(MathHelper.ToRadians(6f));
			Projectile.NewProjectile(source, position, newVel2, type, damage, knockback, player.whoAmI, i);
		}
		return false;
	}
}
