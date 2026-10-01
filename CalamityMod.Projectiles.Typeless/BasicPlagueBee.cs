using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class BasicPlagueBee : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public float projRotValue;

	public NPC npc;

	public int tileCollisions;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/Rogue/PlaguenadeBee";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 2;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 12;
	}

	public override void AI()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		Color trailColor = Color.Lime;
		if (time == 0)
		{
			trailColor = (Main.rand.NextBool() ? Color.LawnGreen : Color.Lime);
		}
		time++;
		if (base.Projectile.ai[1] <= 0f)
		{
			base.Projectile.ai[1] = 90f;
		}
		if (base.Projectile.ai[2] <= 0f)
		{
			base.Projectile.ai[2] = 2f;
		}
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
		base.Projectile.rotation += (float)base.Projectile.spriteDirection * MathHelper.ToRadians(45f);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 3)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		if (tileCollisions < 7)
		{
			if ((float)time % base.Projectile.ai[2] == 0f)
			{
				GeneralParticleHandler.SpawnParticle(new PointParticle(base.Projectile.Center, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 7, 0.35f, trailColor * 0.45f));
				if (Main.rand.NextBool())
				{
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 89, -base.Projectile.velocity.RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(0.2f, 0.6f), 0, default(Color), Main.rand.NextFloat(0.5f, 0.8f));
					dust.noGravity = true;
					dust.alpha = Main.rand.Next(90, 221);
				}
			}
		}
		else
		{
			base.Projectile.Kill();
		}
		Vector2 center = base.Projectile.Center;
		float maxDistance = 800f;
		bool homeIn = false;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 30f)
		{
			for (int npcIndex = 0; npcIndex < Main.maxNPCs; npcIndex++)
			{
				if (time % 10 == 0 && npc == null)
				{
					npc = base.Projectile.Center.ClosestNPCAt(1000f, ignoreTiles: false);
				}
				if (npc == null || !npc.CanBeChasedBy(base.Projectile))
				{
					return;
				}
				if (npc.CanBeChasedBy(base.Projectile))
				{
					float extraDistance = npc.width / 2 + npc.height / 2;
					bool canHit = true;
					if (extraDistance < maxDistance)
					{
						canHit = Collision.CanHit(base.Projectile.Center, 1, 1, npc.Center, 1, 1);
					}
					if ((Vector2.Distance(npc.Center, base.Projectile.Center) < maxDistance + extraDistance) & canHit)
					{
						center = npc.Center;
						homeIn = true;
						break;
					}
				}
			}
		}
		if (!homeIn)
		{
			center.X = base.Projectile.Center.X + base.Projectile.velocity.X * 100f;
			center.Y = base.Projectile.Center.Y + base.Projectile.velocity.Y * 100f;
		}
		float velocityTweak = 0.3f;
		Vector2 projPos = base.Projectile.Center;
		Vector2 velocity = center - projPos;
		float targetDist = ((Vector2)(ref velocity)).Length();
		targetDist = 8f / targetDist;
		velocity.X *= targetDist;
		velocity.Y *= targetDist;
		if (base.Projectile.velocity.X < velocity.X)
		{
			base.Projectile.velocity.X += velocityTweak;
			if (base.Projectile.velocity.X < 0f && velocity.X > 0f)
			{
				base.Projectile.velocity.X += velocityTweak * 2f;
			}
		}
		else if (base.Projectile.velocity.X > velocity.X)
		{
			base.Projectile.velocity.X -= velocityTweak;
			if (base.Projectile.velocity.X > 0f && velocity.X < 0f)
			{
				base.Projectile.velocity.X -= velocityTweak * 2f;
			}
		}
		if (base.Projectile.velocity.Y < velocity.Y)
		{
			base.Projectile.velocity.Y += velocityTweak;
			if (base.Projectile.velocity.Y < 0f && velocity.Y > 0f)
			{
				base.Projectile.velocity.Y += velocityTweak * 2f;
			}
		}
		else if (base.Projectile.velocity.Y > velocity.Y)
		{
			base.Projectile.velocity.Y -= velocityTweak;
			if (base.Projectile.velocity.Y > 0f && velocity.Y < 0f)
			{
				base.Projectile.velocity.Y -= velocityTweak * 2f;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		tileCollisions++;
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int frameHeight = texture.Height / Main.projFrames[base.Type];
		int drawStart = frameHeight * base.Projectile.frame;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, drawStart, texture.Width, frameHeight), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)frameHeight / 2f), base.Projectile.scale, spriteEffects, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 5; i++)
		{
			Dust.NewDustPerfect(base.Projectile.Center, 89, base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.1f, 0.8f), 0, default(Color), Main.rand.NextFloat(0.7f, 0.85f)).noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), (int)base.Projectile.ai[1]);
		base.Projectile.ai[0] = 15f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), (int)base.Projectile.ai[1]);
	}

	public override bool? CanDamage()
	{
		if (!(base.Projectile.ai[0] <= 30f))
		{
			return null;
		}
		return false;
	}
}
