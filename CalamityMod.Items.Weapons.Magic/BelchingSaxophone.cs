using System;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class BelchingSaxophone : ModItem, ILocalizedModType, IModType
{
	public const int BaseDamage = 32;

	private int counter;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 22;
		base.Item.damage = 32;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 10;
		base.Item.useTime = 12;
		base.Item.useAnimation = 24;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AcidicReed>();
		base.Item.shootSpeed = 20f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		counter++;
		if (Main.rand.NextBool())
		{
			Vector2 speed = velocity.RotatedBy(MathHelper.ToRadians((float)Main.rand.Next(-15, 16)));
			((Vector2)(ref speed)).Normalize();
			speed *= 15f;
			speed.Y -= Math.Abs(speed.X) * 0.2f;
			Projectile.NewProjectile(source, position, speed, ModContent.ProjectileType<AcidicSaxBubble>(), damage, knockback, player.whoAmI);
		}
		velocity.X += (float)Main.rand.Next(-40, 41) * 0.05f;
		velocity.Y += (float)Main.rand.Next(-40, 41) * 0.05f;
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, (counter % 2 == 0) ? 1f : 0f);
		if (Main.rand.NextBool())
		{
			int noteProj = Utils.SelectRandom<int>(Main.rand, 76, 77, 78);
			int note = Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 0.75f, velocity.Y * 0.75f, noteProj, (int)((double)damage * 0.75), knockback, player.whoAmI);
			if (note.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[note].DamageType = DamageClass.Magic;
				Main.projectile[note].usesLocalNPCImmunity = true;
				Main.projectile[note].localNPCHitCooldown = 10;
			}
		}
		return false;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(0f, 18f);
	}
}
