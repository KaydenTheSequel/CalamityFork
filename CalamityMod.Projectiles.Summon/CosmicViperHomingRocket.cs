using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class CosmicViperHomingRocket : ModProjectile, ILocalizedModType, IModType
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
		base.Projectile.light = 0.5f;
		base.Projectile.alpha = 255;
		base.Projectile.extraUpdates = 1;
		base.Projectile.scale = 1.18f;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.aiStyle = 1;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.tileCollide = false;
		base.AIType = 14;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
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
		float colorScale = (float)base.Projectile.alpha / 255f;
		Lighting.AddLight((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16, 1f * colorScale, 0.1f * colorScale, 1f * colorScale);
		Player player = Main.player[base.Projectile.owner];
		Vector2 center = base.Projectile.Center;
		float maxDistance = 800f;
		bool homeIn = false;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float extraDistance = (float)(npc.width / 2) + (float)(npc.height / 2);
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
				if (Vector2.Distance(npc2.Center, base.Projectile.Center) < maxDistance + extraDistance2)
				{
					center = npc2.Center;
					homeIn = true;
				}
			}
		}
		if (!homeIn)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if (n.CanBeChasedBy(base.Projectile))
				{
					float extraDistance3 = (float)(n.width / 2) + (float)(n.height / 2);
					if (Vector2.Distance(n.Center, base.Projectile.Center) < maxDistance + extraDistance3)
					{
						center = n.Center;
						homeIn = true;
						break;
					}
				}
			}
		}
		if (homeIn)
		{
			Vector2 moveDirection = base.Projectile.SafeDirectionTo(center, Vector2.UnitY);
			float homingInertia = 20f;
			float homingVelocity = 20f;
			base.Projectile.velocity = (base.Projectile.velocity * homingInertia + moveDirection * homingVelocity) / (homingInertia + 1f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 120);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
		base.Projectile.ExpandHitboxBy(50);
		for (int num621 = 0; num621 < 3; num621++)
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
}
