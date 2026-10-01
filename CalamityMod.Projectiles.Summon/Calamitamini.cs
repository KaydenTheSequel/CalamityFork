using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class Calamitamini : ModProjectile, ILocalizedModType, IModType
{
	public const float Range = 1300f;

	public const float SeparationAnxietyMin = 1500f;

	public const float SeparationAnxietyMax = 3200f;

	public const float SafeDist = 200f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 36);
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
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f1: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (base.Projectile.localAI[0] == 0f)
		{
			int dustAmt = 36;
			for (int d = 0; d < dustAmt; d++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(d - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.Projectile.Center;
				Vector2 dustVel = val - base.Projectile.Center;
				int brim = Dust.NewDust(val + dustVel, 0, 0, 235, dustVel.X * 1.75f, dustVel.Y * 1.75f, 100, default(Color), 1.1f);
				Main.dust[brim].noGravity = true;
				Main.dust[brim].velocity = dustVel;
			}
			base.Projectile.localAI[0]++;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<Calamitamini>();
		player.AddBuff(ModContent.BuffType<EntropysVigilBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.cEyes = false;
			}
			if (modPlayer.cEyes)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		base.Projectile.MinionAntiClump();
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 3)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 2)
		{
			base.Projectile.frame = 0;
		}
		Vector2 targetVec = base.Projectile.position;
		float maxDistance = 1300f;
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
					targetVec = npc.Center;
					foundTarget = true;
				}
			}
		}
		if (!foundTarget)
		{
			for (int index = 0; index < Main.maxNPCs; index++)
			{
				NPC npc2 = Main.npc[index];
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
						targetVec = npc2.Center;
						foundTarget = true;
					}
				}
			}
		}
		float sepAnxietyDist = 1500f;
		if (foundTarget)
		{
			sepAnxietyDist = 3200f;
		}
		if (Vector2.Distance(player.Center, base.Projectile.Center) > sepAnxietyDist)
		{
			base.Projectile.ai[0] = 1f;
			base.Projectile.tileCollide = false;
			base.Projectile.netUpdate = true;
		}
		if (foundTarget && base.Projectile.ai[0] == 0f)
		{
			Vector2 vecToTarget = targetVec - base.Projectile.Center;
			float num2 = ((Vector2)(ref vecToTarget)).Length();
			((Vector2)(ref vecToTarget)).Normalize();
			if (num2 > 200f)
			{
				float speedMult = 8f;
				vecToTarget *= speedMult;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + vecToTarget) / 41f;
			}
			else
			{
				float speedMult2 = -4f;
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
			float returnSpeed = 6f;
			if (returningToPlayer)
			{
				returnSpeed = 18f;
			}
			Vector2 returnSpot = player.Center - base.Projectile.Center + new Vector2(0f, -60f);
			float num3 = ((Vector2)(ref returnSpot)).Length();
			if (num3 > 200f && returnSpeed < 10f)
			{
				returnSpeed = 10f;
			}
			if (((num3 < 200f) & returningToPlayer) && !Collision.SolidCollision(base.Projectile.Center, base.Projectile.width, base.Projectile.height))
			{
				base.Projectile.ai[0] = 0f;
				base.Projectile.netUpdate = true;
			}
			if (num3 > 2000f)
			{
				base.Projectile.position.X = player.Center.X - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = player.Center.Y - (float)(base.Projectile.height / 2);
				base.Projectile.netUpdate = true;
			}
			if (num3 > 70f)
			{
				((Vector2)(ref returnSpot)).Normalize();
				returnSpot *= returnSpeed;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + returnSpot) / 41f;
			}
			else if (base.Projectile.velocity.X == 0f && base.Projectile.velocity.Y == 0f)
			{
				base.Projectile.velocity.X = -0.15f;
				base.Projectile.velocity.Y = -0.05f;
			}
		}
		if (foundTarget)
		{
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(base.Projectile.AngleTo(targetVec) + (float)Math.PI, 0.1f);
		}
		else
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI;
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1] += Main.rand.Next(2, 4);
		}
		if (base.Projectile.ai[1] > 100f)
		{
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.ai[0] != 0f)
		{
			return;
		}
		float projSpeed = 10f;
		int projType = ModContent.ProjectileType<BrimstoneLaserSummon>();
		if (foundTarget && base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[1]++;
			if (Main.myPlayer == base.Projectile.owner && Collision.CanHitLine(base.Projectile.Center, base.Projectile.width, base.Projectile.height, targetVec, 0, 0))
			{
				Vector2 velocity = base.Projectile.Center.DirectionTo(targetVec) * projSpeed;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, projType, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				base.Projectile.netUpdate = true;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		_ = base.Projectile.spriteDirection;
		_ = -1;
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int frameHeight = texture.Height / Main.projFrames[base.Type];
		int yStart = frameHeight * base.Projectile.frame;
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, yStart, texture.Width, frameHeight), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)frameHeight / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}
}
