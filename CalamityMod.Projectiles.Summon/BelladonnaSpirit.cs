using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class BelladonnaSpirit : BaseMinionProjectile
{
	public override int AssociatedProjectileTypeID => ModContent.ProjectileType<BelladonnaSpirit>();

	public override int AssociatedBuffTypeID => ModContent.BuffType<BelladonnaSpiritBuff>();

	public override ref bool AssociatedMinionBool => ref base.ModdedOwner.belladonaSpirit;

	public override bool PreHardmodeMinionTileVision => true;

	public override int AnimationFrames => 5;

	public ref float ShootingTimer => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.width = 28;
		base.Projectile.height = 48;
	}

	public override void MinionAI()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		FollowPlayer();
		if (base.Target != null)
		{
			AttackTarget();
		}
		base.Projectile.MinionAntiClump();
		base.Projectile.spriteDirection = ((base.Target == null) ? MathF.Sign(base.Projectile.velocity.X) : MathF.Sign(base.Target.Center.X - base.Projectile.Center.X));
		base.Projectile.ForceNetUpdate(ignoreCurrentNetSpam: false);
	}

	public void FollowPlayer()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.WithinRange(base.Owner.Center, EnemyDistanceDetection) && !base.Projectile.WithinRange(base.Owner.Center, 300f))
		{
			base.Projectile.velocity = (base.Owner.Center - base.Projectile.Center) / 30f;
		}
		else if (!base.Projectile.WithinRange(base.Owner.Center, 160f))
		{
			base.Projectile.velocity = (base.Projectile.velocity * 37f + base.Projectile.SafeDirectionTo(base.Owner.Center) * 17f) / 40f;
		}
		if (!base.Projectile.WithinRange(base.Owner.Center, EnemyDistanceDetection))
		{
			base.Projectile.position = base.Owner.Center;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.3f;
		}
	}

	public void AttackTarget()
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity.Y -= MathHelper.Lerp(0f, 0.005f, ShootingTimer % 75f);
		ShootingTimer++;
		if (ShootingTimer == 75f && Main.myPlayer == base.Projectile.owner)
		{
			Vector2 petalShootVelocity = -Vector2.UnitY * Main.rand.NextFloat(7f, 8.5f) + Vector2.UnitX * base.Projectile.velocity.X + Vector2.UnitY * base.Projectile.velocity.Y * 0.35f;
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, petalShootVelocity, ModContent.ProjectileType<BelladonnaPetal>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner).rotation = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
			ShootingTimer = 0f;
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			int dustAmount = 45;
			for (int dustIndex = 0; dustIndex < dustAmount; dustIndex++)
			{
				Vector2 velocity = ((float)Math.PI * 2f / 45f * (float)dustIndex).ToRotationVector2() * Main.rand.NextFloat(3f, 4.5f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 39, velocity);
				dust.noGravity = true;
				dust.scale = ((Vector2)(ref velocity)).Length() * 0.1f;
				dust.velocity *= 0.3f;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		Rectangle sourceRectangle = value.Frame(1, AnimationFrames, 0, base.Projectile.frame);
		Main.EntitySpriteDraw(origin: sourceRectangle.Size() * 0.5f, effects: (SpriteEffects)(base.Projectile.spriteDirection == -1), texture: value, position: position, sourceRectangle: sourceRectangle, color: base.Projectile.GetAlpha(lightColor), rotation: base.Projectile.rotation, scale: base.Projectile.scale);
		return false;
	}
}
