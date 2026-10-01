using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ScourgeoftheDesertProj : ModProjectile, ILocalizedModType, IModType
{
	public int Time;

	public int TimeUnderground;

	public bool PostExitTiles;

	public bool InitialTileHit;

	public bool InsideTiles;

	public Vector2 SavedOldVelocity;

	public Vector2 NPCDestination;

	public bool SetPierce;

	public int postHitNoDig;

	public bool collideWithTiles = true;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/ScourgeoftheDesert";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 2;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 50;
	}

	public override void AI()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		if (!SetPierce)
		{
			base.Projectile.penetrate = (base.Projectile.Calamity().stealthStrike ? 4 : 2);
			SetPierce = true;
		}
		InsideTiles = Collision.SolidCollision(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX * 10f), 10, 10);
		Time++;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		float playerDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (Main.rand.NextBool(2))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(20f, 20f), Main.rand.NextBool() ? 288 : 207);
			dust.scale = Main.rand.NextFloat(0.3f, 0.55f);
			dust.noGravity = true;
			dust.velocity = -base.Projectile.velocity * 0.5f;
		}
		if (postHitNoDig > 0)
		{
			postHitNoDig--;
		}
		if (!InitialTileHit && Time > 45)
		{
			if (base.Projectile.velocity.Y < 0f)
			{
				base.Projectile.velocity.Y *= 0.95f;
			}
			base.Projectile.velocity.Y += 0.15f;
			base.Projectile.velocity.X *= 0.98f;
		}
		if (InitialTileHit && !InsideTiles && !PostExitTiles)
		{
			base.Projectile.extraUpdates = 4;
			if (!base.Projectile.Calamity().stealthStrike)
			{
				base.Projectile.timeLeft = 200;
			}
			SoundEngine.PlaySound(SoundID.NPCHit11 with
			{
				Volume = 1.3f,
				Pitch = 1.1f
			}, base.Projectile.Center);
			for (int i = 0; i <= 25; i++)
			{
				Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity * 6.5f, Main.rand.NextBool() ? 207 : 216, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(30f)) * Main.rand.NextFloat(0.3f, 0.9f), 0, default(Color), Main.rand.NextFloat(1.3f, 1.8f)).noGravity = true;
			}
			PostExitTiles = true;
		}
		if (PostExitTiles)
		{
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 10f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 1.02f;
			}
			if (base.Projectile.timeLeft % 2 == 0 && playerDist < 1400f)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center - base.Projectile.velocity * 8f, -base.Projectile.velocity * 0.1f, affectedByGravity: false, 9, 2.3f, Color.White * 0.1f));
			}
		}
		if (InsideTiles)
		{
			TimeUnderground++;
			Vector3 DustLight = default(Vector3);
			((Vector3)(ref DustLight))._002Ector(0.171f, 0.124f, 0.086f);
			Lighting.AddLight(base.Projectile.Center + base.Projectile.velocity, DustLight * 1.5f);
			if (Time % 15 == 0 && TimeUnderground < 120)
			{
				SoundStyle style = SoundID.WormDig with
				{
					Volume = 0.7f,
					Pitch = 0.2f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			NPC target = base.Projectile.Center.ClosestNPCAt(1800f);
			if (target == null)
			{
				if (base.Projectile.velocity.Y < 0f)
				{
					base.Projectile.velocity.Y *= 0.98f;
				}
				base.Projectile.velocity.Y += 0.08f;
				base.Projectile.velocity.X *= 0.99f;
				return;
			}
			NPCDestination = target.Center + target.velocity * 5f;
			float acceleration = 0.2f;
			float xDist = NPCDestination.X - base.Projectile.Center.X;
			float yDist = NPCDestination.Y - base.Projectile.Center.Y;
			float dist = (float)Math.Sqrt(xDist * xDist + yDist * yDist);
			dist = 10f / dist;
			xDist *= dist;
			yDist *= dist;
			if (Vector2.Distance(NPCDestination, base.Projectile.Center) < 1800f && TimeUnderground > 25)
			{
				if (base.Projectile.velocity.X < xDist)
				{
					base.Projectile.velocity.X = base.Projectile.velocity.X + acceleration;
					if (base.Projectile.velocity.X < 0f && xDist > 0f)
					{
						base.Projectile.velocity.X += acceleration;
					}
				}
				else if (base.Projectile.velocity.X > xDist)
				{
					base.Projectile.velocity.X = base.Projectile.velocity.X - acceleration;
					if (base.Projectile.velocity.X > 0f && xDist < 0f)
					{
						base.Projectile.velocity.X -= acceleration;
					}
				}
				if (base.Projectile.velocity.Y < yDist)
				{
					base.Projectile.velocity.Y = base.Projectile.velocity.Y + acceleration;
					if (base.Projectile.velocity.Y < 0f && yDist > 0f)
					{
						base.Projectile.velocity.Y += acceleration;
					}
				}
				else if (base.Projectile.velocity.Y > yDist)
				{
					base.Projectile.velocity.Y = base.Projectile.velocity.Y - acceleration;
					if (base.Projectile.velocity.Y > 0f && yDist < 0f)
					{
						base.Projectile.velocity.Y -= acceleration;
					}
				}
			}
		}
		CustomTileCollide();
	}

	public void CustomTileCollide()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		if (!collideWithTiles || postHitNoDig != 0 || !InsideTiles || Time <= 5)
		{
			return;
		}
		SavedOldVelocity = base.Projectile.velocity;
		collideWithTiles = false;
		if (!InitialTileHit)
		{
			for (int i = 0; i <= 25; i++)
			{
				Dust.NewDustPerfect(base.Projectile.Center + base.Projectile.velocity * 3f, Main.rand.NextBool() ? 207 : 216, -base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(30f)) * Main.rand.NextFloat(0.3f, 0.9f), 0, default(Color), Main.rand.NextFloat(1.3f, 1.8f)).noGravity = true;
			}
			base.Projectile.velocity = SavedOldVelocity * 0.7f;
			SoundStyle style = SoundID.WormDig with
			{
				Volume = 1.5f,
				Pitch = 1.1f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			InitialTileHit = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 5; i++)
		{
			Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 32 : 216, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.1f, 0.6f), 0, default(Color), Main.rand.NextFloat(0.4f, 0.8f)).noGravity = false;
		}
		if (base.Projectile.Calamity().stealthStrike)
		{
			if (PostExitTiles || InsideTiles)
			{
				base.Projectile.extraUpdates = 4;
			}
			base.Projectile.timeLeft = 600;
			Time = 10;
			TimeUnderground = 0;
			PostExitTiles = false;
			postHitNoDig = 50;
			InitialTileHit = false;
			collideWithTiles = true;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 25; i++)
		{
			Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity, Main.rand.NextBool(3) ? 216 : 207, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.05f, 2.2f), 0, default(Color), Main.rand.NextFloat(1.5f, 2.8f)).noGravity = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2, null, drawCentered: true, shrink: true);
		return false;
	}
}
