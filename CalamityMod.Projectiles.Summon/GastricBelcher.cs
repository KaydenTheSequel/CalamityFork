using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class GastricBelcher : ModProjectile, ILocalizedModType, IModType
{
	private bool initialized;

	private int bubbleCounter;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0934: Unknown result type (might be due to invalid IL or missing references)
		//IL_0939: Unknown result type (might be due to invalid IL or missing references)
		//IL_086a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0875: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a14: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		base.Projectile.Calamity();
		if (!initialized)
		{
			int dustAmt = 36;
			for (int dustIndex = 0; dustIndex < dustAmt; dustIndex++)
			{
				int randomDust = Utils.SelectRandom<int>(Main.rand, 33, 89);
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy(((float)dustIndex - ((float)dustAmt / 2f - 1f)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.Projectile.Center;
				Vector2 dustVel = val - base.Projectile.Center;
				int water = Dust.NewDust(val + dustVel, 0, 0, randomDust, dustVel.X * 1.75f, dustVel.Y * 1.75f, 100, default(Color), 1.1f);
				Main.dust[water].noGravity = true;
				Main.dust[water].velocity = dustVel;
			}
			initialized = true;
		}
		if (base.Projectile.frameCounter++ % 6 == 0)
		{
			base.Projectile.frame++;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<GastricBelcher>();
		player.AddBuff(ModContent.BuffType<GastricAberrationBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.gastricBelcher = false;
			}
			if (modPlayer.gastricBelcher)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		base.Projectile.MinionAntiClump();
		float maxDistance = 700f;
		Vector2 targetVec = base.Projectile.position;
		bool foundTarget = false;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float extraDist = npc.width / 2 + npc.height / 2;
				float targetDist = Vector2.Distance(npc.Center, base.Projectile.Center);
				bool canHit = true;
				if (extraDist < maxDistance)
				{
					canHit = Collision.CanHit(base.Projectile.Center, 1, 1, npc.Center, 1, 1);
				}
				if ((!foundTarget && targetDist < maxDistance + extraDist) & canHit)
				{
					maxDistance = targetDist;
					targetVec = npc.Center;
					foundTarget = true;
				}
			}
		}
		if (!foundTarget)
		{
			for (int npcIndex = 0; npcIndex < Main.maxNPCs; npcIndex++)
			{
				NPC npc2 = Main.npc[npcIndex];
				if (npc2.CanBeChasedBy(base.Projectile))
				{
					float extraDist2 = npc2.width / 2 + npc2.height / 2;
					float targetDist2 = Vector2.Distance(npc2.Center, base.Projectile.Center);
					bool canHit2 = true;
					if (extraDist2 < maxDistance)
					{
						canHit2 = Collision.CanHit(base.Projectile.Center, 1, 1, npc2.Center, 1, 1);
					}
					if ((!foundTarget && targetDist2 < maxDistance + extraDist2) & canHit2)
					{
						maxDistance = targetDist2;
						targetVec = npc2.Center;
						foundTarget = true;
					}
				}
			}
		}
		float returnDist = 1000f;
		if (foundTarget)
		{
			returnDist = 2200f;
		}
		if (Vector2.Distance(player.Center, base.Projectile.Center) > returnDist)
		{
			base.Projectile.ai[0] = 1f;
			base.Projectile.netUpdate = true;
		}
		if (foundTarget && base.Projectile.ai[0] == 0f)
		{
			Vector2 vecToTarget = targetVec - base.Projectile.Center;
			float targetDist3 = ((Vector2)(ref vecToTarget)).Length();
			((Vector2)(ref vecToTarget)).Normalize();
			if (targetDist3 > 200f)
			{
				float speedMult = ((targetDist3 > 400f) ? 16f : ((targetDist3 > 250f) ? 9f : 5f));
				vecToTarget *= speedMult;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + vecToTarget) / 41f;
			}
			else
			{
				float speedMult2 = -3f;
				vecToTarget *= speedMult2;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + vecToTarget) / 41f;
			}
		}
		else
		{
			bool returningToPlayer = false;
			if (!returningToPlayer)
			{
				returningToPlayer = base.Projectile.ai[0] == 1f;
			}
			float speedMult3 = 10f;
			if (returningToPlayer)
			{
				speedMult3 = 21f;
			}
			Vector2 vecToPlayer = player.Center - base.Projectile.Center + new Vector2(0f, -60f);
			float num2 = ((Vector2)(ref vecToPlayer)).Length();
			if (num2 < 200f && speedMult3 > 8f)
			{
				speedMult3 = 1f;
			}
			if (((num2 < 150f) & returningToPlayer) && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
			{
				base.Projectile.ai[0] = 0f;
				base.Projectile.netUpdate = true;
			}
			if (num2 > 2000f)
			{
				base.Projectile.position.X = player.Center.X - (float)base.Projectile.width / 2f;
				base.Projectile.position.Y = player.Center.Y - (float)base.Projectile.height / 2f;
				base.Projectile.netUpdate = true;
			}
			if (num2 > 70f)
			{
				((Vector2)(ref vecToPlayer)).Normalize();
				vecToPlayer *= speedMult3;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + vecToPlayer) / 41f;
			}
			else if (base.Projectile.velocity.X == 0f && base.Projectile.velocity.Y == 0f)
			{
				base.Projectile.velocity.X = -0.15f;
				base.Projectile.velocity.Y = -0.05f;
			}
		}
		if (foundTarget)
		{
			base.Projectile.spriteDirection = (base.Projectile.direction = (targetVec.X - base.Projectile.Center.X > 0f).ToDirectionInt());
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(base.Projectile.AngleTo(targetVec) + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI)), 0.1f);
		}
		else
		{
			base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1] += Main.rand.Next(1, 4);
		}
		if (base.Projectile.ai[1] > 100f)
		{
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.ai[0] != 0f || !foundTarget || base.Projectile.ai[1] != 0f || Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		if (modPlayer.soundCooldown <= 0)
		{
			SoundStyle style = SoundID.NPCDeath13 with
			{
				Volume = SoundID.NPCDeath13.Volume * 0.5f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			modPlayer.soundCooldown = Main.rand.Next(120, 180);
		}
		base.Projectile.ai[1]++;
		Vector2 velocity = targetVec - base.Projectile.Center;
		((Vector2)(ref velocity)).Normalize();
		float vomitSpeedMult = 20f;
		Vector2 vomitVel = velocity * vomitSpeedMult;
		vomitVel.Y += Main.rand.NextFloat(-30f, 30f) * 0.05f;
		vomitVel.X += Main.rand.NextFloat(-30f, 30f) * 0.05f;
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, vomitVel, ModContent.ProjectileType<GastricBelcherVomit>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, Main.rand.Next(3));
		if (bubbleCounter++ % 3 == 2)
		{
			for (int projCount = 0; projCount < 3; projCount++)
			{
				float bubbleSpeedMult = 14f;
				Vector2 bubbleVel = velocity * bubbleSpeedMult;
				bubbleVel.Y += Main.rand.NextFloat(-50f, 50f) * 0.05f;
				bubbleVel.X += Main.rand.NextFloat(-50f, 50f) * 0.05f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, bubbleVel, ModContent.ProjectileType<GastricBelcherBubble>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
		base.Projectile.netUpdate = true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int frameHeight = texture.Height / Main.projFrames[base.Type];
		int y6 = frameHeight * base.Projectile.frame;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, y6, texture.Width, frameHeight), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)frameHeight / 2f), base.Projectile.scale, spriteEffects, 0f);
		return false;
	}
}
