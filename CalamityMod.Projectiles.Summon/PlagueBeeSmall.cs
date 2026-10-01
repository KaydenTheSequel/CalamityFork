using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class PlagueBeeSmall : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/Rogue/PlaguenadeBee";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 420;
		base.Projectile.ignoreWater = true;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner != Main.myPlayer)
		{
			base.Projectile.Kill();
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
		Vector2 center = base.Projectile.Center;
		float maxDistance = 800f;
		bool homeIn = false;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 30f)
		{
			Player player = Main.player[base.Projectile.owner];
			if (player.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[player.MinionAttackTargetNPC];
				if (npc.CanBeChasedBy(base.Projectile) && !npc.wet)
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
					}
				}
			}
			else
			{
				for (int npcIndex = 0; npcIndex < Main.maxNPCs; npcIndex++)
				{
					NPC npc2 = Main.npc[npcIndex];
					if (npc2.CanBeChasedBy(base.Projectile) && !npc2.wet)
					{
						float extraDistance2 = npc2.width / 2 + npc2.height / 2;
						bool canHit2 = true;
						if (extraDistance2 < maxDistance)
						{
							canHit2 = Collision.CanHit(base.Projectile.Center, 1, 1, npc2.Center, 1, 1);
						}
						if ((Vector2.Distance(npc2.Center, base.Projectile.Center) < maxDistance + extraDistance2) & canHit2)
						{
							center = npc2.Center;
							homeIn = true;
							break;
						}
					}
				}
			}
		}
		if (!homeIn)
		{
			center.X = base.Projectile.Center.X + base.Projectile.velocity.X * 100f;
			center.Y = base.Projectile.Center.Y + base.Projectile.velocity.Y * 100f;
		}
		float velocityTweak = 0.14f;
		Vector2 projPos = base.Projectile.Center;
		Vector2 velocity = center - projPos;
		float targetDist = ((Vector2)(ref velocity)).Length();
		targetDist = 15f / targetDist;
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
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int frameHeight = texture.Height / Main.projFrames[base.Type];
		int drawStart = frameHeight * base.Projectile.frame;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, drawStart, texture.Width, frameHeight), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)frameHeight / 2f), base.Projectile.scale, spriteEffects, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 2; i++)
		{
			int plague = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 89, base.Projectile.velocity.X, base.Projectile.velocity.Y, 50);
			Main.dust[plague].noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 60);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 60);
	}
}
