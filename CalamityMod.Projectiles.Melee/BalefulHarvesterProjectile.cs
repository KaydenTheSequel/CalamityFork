using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class BalefulHarvesterProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 24);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
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
		if (base.Projectile.ai[0] >= 0f && base.Projectile.ai[0] < 200f)
		{
			int npcTracker = (int)base.Projectile.ai[0];
			if (Main.npc[npcTracker].active)
			{
				Vector2 projDirection = base.Projectile.Center;
				float projXVel = Main.npc[npcTracker].position.X - projDirection.X;
				float projYVel = Main.npc[npcTracker].position.Y - projDirection.Y;
				float projVelocity = (float)Math.Sqrt(projXVel * projXVel + projYVel * projYVel);
				projVelocity = 8f / projVelocity;
				projXVel *= projVelocity;
				projYVel *= projVelocity;
				base.Projectile.velocity.X = (base.Projectile.velocity.X * 14f + projXVel) / 15f;
				base.Projectile.velocity.Y = (base.Projectile.velocity.Y * 14f + projYVel) / 15f;
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
						float npcX = n.position.X + (float)(n.width / 2);
						float npcY = n.position.Y + (float)(n.height / 2);
						float npcDist = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - npcX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - npcY);
						if (npcDist < homingRange && Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, n.position, n.width, n.height))
						{
							homingRange = npcDist;
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
			for (int j = 0; j < 2; j++)
			{
				Dust dust = Dust.NewDustDirect(new Vector2(base.Projectile.position.X + 4f, base.Projectile.position.Y + 4f), base.Projectile.width - 8, base.Projectile.height - 8, Main.rand.NextBool() ? 5 : 6, base.Projectile.velocity.X * 0.2f, base.Projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
				dust.position -= base.Projectile.velocity * 2f;
				dust.noGravity = true;
				dust.velocity *= 0.3f;
			}
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
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.position);
		for (int j = 0; j < 3; j++)
		{
			int deathDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 5, 0f, 0f, 100, default(Color), 1.25f);
			Dust obj = Main.dust[deathDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[deathDust].scale = 0.5f;
				Main.dust[deathDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int k = 0; k < 5; k++)
		{
			int deathDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 2.5f);
			Main.dust[deathDust2].noGravity = true;
			Dust obj2 = Main.dust[deathDust2];
			obj2.velocity *= 5f;
			deathDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj3 = Main.dust[deathDust2];
			obj3.velocity *= 2f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(323, 240);
		if (base.Projectile.owner == Main.myPlayer)
		{
			int maxProjectiles = 2;
			for (int k = 0; k < maxProjectiles; k++)
			{
				Vector2 flareVelocity = Main.rand.NextVector2CircularEdge(3.5f, 3.5f) + base.Projectile.oldVelocity;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, flareVelocity, ModContent.ProjectileType<TinyFlare>(), (int)((double)base.Projectile.damage * 0.35), base.Projectile.knockBack * 0.35f, Main.myPlayer);
			}
		}
	}
}
