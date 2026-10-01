using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class MourningSkull : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 26;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.extraUpdates = 2;
	}

	public override void AI()
	{
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] < 0f)
		{
			base.Projectile.alpha = 0;
		}
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 50;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.spriteDirection = -1;
			base.Projectile.rotation = (float)Math.Atan2(0.0 - (double)base.Projectile.velocity.Y, 0.0 - (double)base.Projectile.velocity.X);
		}
		else
		{
			base.Projectile.spriteDirection = 1;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		}
		if (base.Projectile.ai[0] >= 0f && base.Projectile.ai[0] < 200f)
		{
			int npcTracker = (int)base.Projectile.ai[0];
			if (Main.npc[npcTracker].active)
			{
				Vector2 projPos = base.Projectile.Center;
				float npcXDist = Main.npc[npcTracker].position.X - projPos.X;
				float npcYDist = Main.npc[npcTracker].position.Y - projPos.Y;
				float npcDistance = (float)Math.Sqrt(npcXDist * npcXDist + npcYDist * npcYDist);
				npcDistance = 8f / npcDistance;
				npcXDist *= npcDistance;
				npcYDist *= npcDistance;
				base.Projectile.velocity.X = (base.Projectile.velocity.X * 14f + npcXDist) / 15f;
				base.Projectile.velocity.Y = (base.Projectile.velocity.Y * 14f + npcYDist) / 15f;
			}
			else
			{
				float homingRange = 1000f;
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC n = enumerator.Current;
					if (n.CanBeChasedBy(base.Projectile))
					{
						float targetX = n.position.X + (float)(n.width / 2);
						float targetY = n.position.Y + (float)(n.height / 2);
						float targetDist = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - targetX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - targetY);
						if (targetDist < homingRange && Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, n.position, n.width, n.height))
						{
							homingRange = targetDist;
							base.Projectile.ai[0] = n.whoAmI;
						}
					}
				}
			}
			if (base.Projectile.velocity.X < 0f)
			{
				base.Projectile.spriteDirection = -1;
				base.Projectile.rotation = (float)Math.Atan2(0.0 - (double)base.Projectile.velocity.Y, 0.0 - (double)base.Projectile.velocity.X);
			}
			else
			{
				base.Projectile.spriteDirection = 1;
				base.Projectile.rotation = base.Projectile.velocity.ToRotation();
			}
			int eightConst = 8;
			int mourningDust = Dust.NewDust(new Vector2(base.Projectile.position.X + (float)eightConst, base.Projectile.position.Y + (float)eightConst), base.Projectile.width - eightConst * 2, base.Projectile.height - eightConst * 2, Main.rand.NextBool() ? 5 : 6);
			Dust obj = Main.dust[mourningDust];
			obj.velocity *= 0.5f;
			Dust obj2 = Main.dust[mourningDust];
			obj2.velocity += base.Projectile.velocity * 0.5f;
			Main.dust[mourningDust].noGravity = true;
			Main.dust[mourningDust].noLight = true;
			Main.dust[mourningDust].scale = 1.4f;
		}
		else
		{
			base.Projectile.Kill();
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, Main.rand.Next(0, 128));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.position);
		for (int i = 0; i < 5; i++)
		{
			int bloody = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 5, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[bloody];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[bloody].scale = 0.5f;
				Main.dust[bloody].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 10; j++)
		{
			int fiery = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
			Main.dust[fiery].noGravity = true;
			Dust obj2 = Main.dust[fiery];
			obj2.velocity *= 5f;
			fiery = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[fiery];
			obj3.velocity *= 2f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(189, 300);
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int k = 0; k < 2; k++)
			{
				Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 174, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.position.X, base.Projectile.position.Y, (float)Main.rand.Next(-35, 36) * 0.2f, (float)Main.rand.Next(-35, 36) * 0.2f, ModContent.ProjectileType<TinyFlare>(), (int)((double)base.Projectile.damage * 0.35), base.Projectile.knockBack * 0.35f, Main.myPlayer);
			}
		}
	}
}
