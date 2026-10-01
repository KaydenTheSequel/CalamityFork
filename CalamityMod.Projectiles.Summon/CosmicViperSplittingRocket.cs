using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class CosmicViperSplittingRocket : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 600;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_068a: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0695: Unknown result type (might be due to invalid IL or missing references)
		//IL_069a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		float colorScale = (float)base.Projectile.alpha / 255f;
		Lighting.AddLight((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16, 1f * colorScale, 0.1f * colorScale, 1f * colorScale);
		Vector2 center = base.Projectile.Center;
		float maxDistance = 800f;
		float explode = 16f;
		bool homeIn = false;
		Player player = Main.player[base.Projectile.owner];
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float extraDistance = (float)(npc.width / 2) + (float)(npc.height / 2);
				if (Vector2.Distance(npc.Center, base.Projectile.Center) < explode + extraDistance)
				{
					int numProj = 4;
					if (base.Projectile.owner == Main.myPlayer)
					{
						Vector2 speed = default(Vector2);
						for (int i = 0; i < numProj; i++)
						{
							((Vector2)(ref speed))._002Ector((float)Main.rand.Next(-50, 51), (float)Main.rand.Next(-50, 51));
							while (speed.X == 0f && speed.Y == 0f)
							{
								((Vector2)(ref speed))._002Ector((float)Main.rand.Next(-50, 51), (float)Main.rand.Next(-50, 51));
							}
							((Vector2)(ref speed)).Normalize();
							speed *= (float)Main.rand.Next(30, 61) * 0.1f * 2f;
							int splitRocket = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, speed, ModContent.ProjectileType<CosmicViperSplitRocket1>(), (int)((double)base.Projectile.damage * 0.25), base.Projectile.knockBack, base.Projectile.owner);
							if (Main.projectile.IndexInRange(splitRocket))
							{
								Main.projectile[splitRocket].originalDamage = (int)((float)base.Projectile.originalDamage * 0.25f);
							}
						}
					}
					SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
					base.Projectile.Kill();
					return;
				}
				if (Vector2.Distance(npc.Center, base.Projectile.Center) < maxDistance + extraDistance)
				{
					center = npc.Center;
					homeIn = true;
				}
			}
		}
		else if (Main.npc[(int)base.Projectile.ai[0]].active && base.Projectile.ai[0] != -1f)
		{
			NPC npc2 = Main.npc[(int)base.Projectile.ai[0]];
			if (npc2.CanBeChasedBy(base.Projectile))
			{
				float extraDistance2 = (float)(npc2.width / 2) + (float)(npc2.height / 2);
				if (Vector2.Distance(npc2.Center, base.Projectile.Center) < explode + extraDistance2)
				{
					int numProj2 = 4;
					if (base.Projectile.owner == Main.myPlayer)
					{
						Vector2 speed2 = default(Vector2);
						for (int j = 0; j < numProj2; j++)
						{
							((Vector2)(ref speed2))._002Ector((float)Main.rand.Next(-50, 51), (float)Main.rand.Next(-50, 51));
							while (speed2.X == 0f && speed2.Y == 0f)
							{
								((Vector2)(ref speed2))._002Ector((float)Main.rand.Next(-50, 51), (float)Main.rand.Next(-50, 51));
							}
							((Vector2)(ref speed2)).Normalize();
							speed2 *= (float)Main.rand.Next(30, 61) * 0.1f * 2f;
							int rocket = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, speed2, ModContent.ProjectileType<CosmicViperSplitRocket1>(), (int)((float)base.Projectile.damage * 0.25f), base.Projectile.knockBack, base.Projectile.owner);
							if (Main.projectile.IndexInRange(rocket))
							{
								Main.projectile[rocket].originalDamage = (int)((float)base.Projectile.originalDamage * 0.25f);
							}
						}
					}
					SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
					base.Projectile.Kill();
					return;
				}
				if (Vector2.Distance(npc2.Center, base.Projectile.Center) < maxDistance + extraDistance2)
				{
					center = npc2.Center;
					homeIn = true;
				}
			}
		}
		if (!homeIn)
		{
			for (int npcIndex = 0; npcIndex < Main.maxNPCs; npcIndex++)
			{
				NPC npc3 = Main.npc[npcIndex];
				if (!npc3.CanBeChasedBy(base.Projectile))
				{
					continue;
				}
				float extraDistance3 = (float)(npc3.width / 2) + (float)(npc3.height / 2);
				if (Vector2.Distance(npc3.Center, base.Projectile.Center) < explode + extraDistance3)
				{
					int numProj3 = 4;
					if (base.Projectile.owner == Main.myPlayer)
					{
						for (int k = 0; k < numProj3; k++)
						{
							Vector2 speed3 = CalamityUtils.RandomVelocity(50f, 30f, 60f, 0.2f);
							int rocket2 = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, speed3, ModContent.ProjectileType<CosmicViperSplitRocket1>(), base.Projectile.damage / numProj3, base.Projectile.knockBack, base.Projectile.owner, Main.rand.Next(2));
							if (Main.projectile.IndexInRange(rocket2))
							{
								Main.projectile[rocket2].originalDamage = (int)((float)base.Projectile.originalDamage * 0.25f);
							}
						}
					}
					SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
					base.Projectile.Kill();
					return;
				}
				if (Vector2.Distance(npc3.Center, base.Projectile.Center) < maxDistance + extraDistance3)
				{
					center = npc3.Center;
					homeIn = true;
				}
			}
		}
		if (homeIn)
		{
			Vector2 moveDirection = base.Projectile.SafeDirectionTo(center, Vector2.UnitY);
			float homingInertia = 15f;
			float homingVelocity = 30f;
			base.Projectile.velocity = (base.Projectile.velocity * homingInertia + moveDirection * homingVelocity) / (homingInertia + 1f);
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 3)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(50);
		for (int i = 0; i < 3; i++)
		{
			int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, Main.rand.NextBool(3) ? 56 : 242, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[idx];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[idx].scale = 0.5f;
			}
			if (Main.rand.NextBool())
			{
				Main.dust[idx].scale *= 0.5f;
				Main.dust[idx].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 120);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
