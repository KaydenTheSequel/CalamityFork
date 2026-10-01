using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class NuclearFuryProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 60;
		base.Projectile.height = 60;
		base.Projectile.alpha = 255;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 600;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 2;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 5;
	}

	public override void AI()
	{
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 1f)
		{
			base.Projectile.penetrate = -1;
		}
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] > 10f && Main.rand.NextBool(3))
		{
			int dustAmt = 6;
			for (int i = 0; i < dustAmt; i++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f).RotatedBy((double)(i - (dustAmt / 2 - 1)) * Math.PI / (double)dustAmt) + base.Projectile.Center;
				Vector2 randomRotation = (Main.rand.NextFloat() * (float)Math.PI - (float)Math.PI / 2f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
				int nuclearDust = Dust.NewDust(val + randomRotation, 0, 0, 217, randomRotation.X * 2f, randomRotation.Y * 2f, 100, default(Color), 1.4f);
				Dust obj = Main.dust[nuclearDust];
				obj.noGravity = true;
				obj.noLight = true;
				obj.velocity /= 4f;
				obj.velocity -= base.Projectile.velocity;
			}
			base.Projectile.alpha -= 5;
			if (base.Projectile.alpha < 50)
			{
				base.Projectile.alpha = 50;
			}
			Lighting.AddLight((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16, 0.1f, 0.4f, 0.6f);
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
		base.Projectile.rotation += base.Projectile.velocity.Y * 0.1f;
		int npcID = -1;
		Vector2 targetVec = base.Projectile.Center;
		float maxDistance = 500f;
		if (base.Projectile.localAI[0] > 0f)
		{
			base.Projectile.localAI[0]--;
		}
		if (base.Projectile.ai[0] == 0f && base.Projectile.localAI[0] == 0f)
		{
			for (int index = 0; index < Main.maxNPCs; index++)
			{
				NPC npc = Main.npc[index];
				if (npc.CanBeChasedBy(base.Projectile) && (base.Projectile.ai[0] == 0f || base.Projectile.ai[0] == (float)index + 1f))
				{
					float extraDistance = npc.width / 2 + npc.height / 2;
					bool canHit = true;
					if (extraDistance < maxDistance)
					{
						canHit = Collision.CanHit(base.Projectile.Center, 1, 1, npc.Center, 1, 1);
					}
					float npcDist = Vector2.Distance(npc.Center, targetVec);
					if ((npcDist < maxDistance + extraDistance) & canHit)
					{
						maxDistance = npcDist;
						targetVec = npc.Center;
						npcID = index;
					}
				}
			}
			if (npcID >= 0)
			{
				base.Projectile.ai[0] = (float)npcID + 1f;
				base.Projectile.netUpdate = true;
			}
		}
		if (base.Projectile.localAI[0] == 0f && base.Projectile.ai[0] == 0f)
		{
			base.Projectile.localAI[0] = 30f;
		}
		bool isHoming = false;
		if (base.Projectile.ai[0] != 0f)
		{
			int index2 = (int)(base.Projectile.ai[0] - 1f);
			if (Main.npc[index2].active && !Main.npc[index2].dontTakeDamage)
			{
				if (Math.Abs(base.Projectile.Center.X - Main.npc[index2].Center.X) + Math.Abs(base.Projectile.Center.Y - Main.npc[index2].Center.Y) < 1000f)
				{
					isHoming = true;
					targetVec = Main.npc[index2].Center;
				}
			}
			else
			{
				base.Projectile.ai[0] = 0f;
				isHoming = false;
				base.Projectile.netUpdate = true;
			}
		}
		if (isHoming)
		{
			double homeVelocity = (double)(targetVec - base.Projectile.Center).ToRotation() - (double)base.Projectile.velocity.ToRotation();
			if (homeVelocity > Math.PI)
			{
				homeVelocity -= Math.PI * 2.0;
			}
			if (homeVelocity < -Math.PI)
			{
				homeVelocity += Math.PI * 2.0;
			}
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(homeVelocity * 0.1);
		}
		float projSpeed = ((Vector2)(ref base.Projectile.velocity)).Length();
		((Vector2)(ref base.Projectile.velocity)).Normalize();
		base.Projectile.velocity = base.Projectile.velocity * (projSpeed + 0.0025f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
		for (int k = 0; k < 5; k++)
		{
			int dust = Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 34);
			Dust obj = Main.dust[dust];
			obj.velocity *= 0f;
			Main.dust[dust].noGravity = true;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, 200);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
