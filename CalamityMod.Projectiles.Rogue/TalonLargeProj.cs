using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class TalonLargeProj : ModProjectile, ILocalizedModType, IModType
{
	private NPC hitTarget;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 2;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 85, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
		if (Main.rand.NextBool(10))
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 111, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.alpha += 12;
			if (base.Projectile.alpha >= 255)
			{
				if (base.Projectile.ai[1] >= 4f)
				{
					base.Projectile.Kill();
					return;
				}
				int index = hitTarget.whoAmI;
				if (!hitTarget.CanBeChasedBy(base.Projectile))
				{
					float checkDist = 1000f;
					index = -1;
					ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
					while (enumerator.MoveNext())
					{
						NPC n = enumerator.Current;
						if (n.CanBeChasedBy(base.Projectile))
						{
							float currentNPCDist = Vector2.Distance(n.Center, base.Projectile.Center);
							if (currentNPCDist < checkDist)
							{
								checkDist = currentNPCDist;
								index = n.whoAmI;
							}
						}
					}
				}
				if (index != -1)
				{
					Vector2 spawnOffset = Main.npc[index].Center + Vector2.UnitX.RotatedByRandom(0.39269909262657166).RotatedBy(Main.rand.NextBool() ? ((float)Math.PI) : 0f) * Main.rand.NextFloat(140f, 180f);
					Vector2 spawnVel = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(spawnOffset, Main.npc[index], 12f, 3);
					if (Main.myPlayer == base.Projectile.owner)
					{
						Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), spawnOffset, spawnVel, ModContent.ProjectileType<TalonLargeProj>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, base.Projectile.ai[1] + 1f);
						projectile.Calamity().stealthStrike = true;
						projectile.tileCollide = false;
						(projectile.ModProjectile as TalonLargeProj).hitTarget = hitTarget;
					}
					GeneralParticleHandler.SpawnParticle(new PointParticle(spawnOffset, Vector2.Normalize(spawnVel), affectedByGravity: false, 6, 2.25f, Color.Cyan));
				}
				base.Projectile.Kill();
			}
		}
		base.Projectile.ai[0]++;
		base.Projectile.tileCollide = base.Projectile.ai[1] == 0f && base.Projectile.ai[0] > 2f;
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
	}

	public override bool? CanDamage()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.numHits > 0)
		{
			return false;
		}
		if (hitTarget != null)
		{
			Rectangle rect = base.Projectile.getRect();
			if (!((Rectangle)(ref rect)).Intersects(hitTarget.getRect()))
			{
				return false;
			}
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		hitTarget = target;
		base.Projectile.tileCollide = false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], Color.White);
		return false;
	}
}
