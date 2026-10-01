using System;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class AMRShot : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.light = 0.5f;
		base.Projectile.alpha = 255;
		base.Projectile.extraUpdates = 10;
		base.Projectile.scale = 1.18f;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
	}

	public override void AI()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= (int)(((Vector2)(ref base.Projectile.velocity)).Length() * 0.9f);
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha < 140)
		{
			return new Color(255, 255, 255, 100);
		}
		return Color.Transparent;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.Center);
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffects(target.Center, hit.Crit);
		target.AddBuff(ModContent.BuffType<MarkedforDeath>(), 300);
		target.Calamity().miscDefenseLoss = 25;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffects(target.Center, crit: true);
		target.AddBuff(ModContent.BuffType<MarkedforDeath>(), 300);
	}

	private void OnHitEffects(Vector2 targetPos, bool crit)
	{
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		int extraProjectileAmt;
		if (crit)
		{
			IEntitySource source = base.Projectile.GetSource_FromThis();
			extraProjectileAmt = 4;
			for (int x = 0; x < extraProjectileAmt; x++)
			{
				if (base.Projectile.owner == Main.myPlayer)
				{
					bool fromRight = x >= 2;
					CalamityUtils.ProjectileBarrage(source, base.Projectile.Center, targetPos, fromRight, 500f, 500f, 0f, 500f, 10f, ModContent.ProjectileType<AMR2>(), (int)((double)base.Projectile.damage * 0.15), base.Projectile.knockBack * 0.1f, base.Projectile.owner);
				}
			}
			return;
		}
		IEntitySource source2 = base.Projectile.GetSource_FromThis();
		extraProjectileAmt = 2;
		for (int i = 0; i < extraProjectileAmt; i++)
		{
			if (base.Projectile.owner == Main.myPlayer)
			{
				bool fromRight = i > 0;
				CalamityUtils.ProjectileBarrage(source2, base.Projectile.Center, targetPos, fromRight, 500f, 500f, 0f, 500f, 10f, ModContent.ProjectileType<AMR2>(), (int)((double)base.Projectile.damage * 0.15), base.Projectile.knockBack * 0.1f, base.Projectile.owner);
			}
		}
	}
}
