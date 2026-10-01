using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class BrimstoneBarrage : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 44;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 690;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		int target = Player.FindClosest(base.Projectile.Center, 1, 1);
		float targetDist = ((target == -1 || Main.player[target].dead || !Main.player[target].active || Main.player[target] == null) ? 1000f : Vector2.Distance(Main.player[target].Center, base.Projectile.Center));
		if (((Vector2)(ref base.Projectile.velocity)).Length() < base.Projectile.ai[2])
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.01f;
			if (((Vector2)(ref base.Projectile.velocity)).Length() > base.Projectile.ai[2])
			{
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= base.Projectile.ai[2];
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.timeLeft < 60)
		{
			base.Projectile.Opacity = MathHelper.Clamp((float)base.Projectile.timeLeft / 60f, 0f, 1f);
		}
		if (base.Projectile.ai[0] == 2f && base.Projectile.timeLeft > 570)
		{
			int player = Player.FindClosest(base.Projectile.Center, 1, 1);
			Vector2 vector = Main.player[player].Center - base.Projectile.Center;
			float scaleFactor = ((Vector2)(ref base.Projectile.velocity)).Length();
			((Vector2)(ref vector)).Normalize();
			vector *= scaleFactor;
			base.Projectile.velocity = (base.Projectile.velocity * 15f + vector) / 16f;
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= scaleFactor;
		}
		if ((base.Projectile.ai[1] == 2f || (base.Projectile.ai[1] == 4f && time > 10)) && targetDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center - base.Projectile.velocity * 0.5f, -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 0.6f), affectedByGravity: false, (int)MathHelper.Clamp(9f * Utils.GetLerpValue(630f, 690f, base.Projectile.timeLeft), 2f, 9f), 1.1f, (Main.rand.NextBool() ? Color.Red : Color.Lerp(Color.Red, Color.Magenta, 0.5f)) * base.Projectile.Opacity * 0.85f));
		}
		if (base.Projectile.ai[1] == 3f)
		{
			if (base.Projectile.timeLeft > 600)
			{
				Projectile projectile4 = base.Projectile;
				projectile4.velocity *= 1.015f;
			}
			base.Projectile.scale = 0.85f;
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(4f, 4f), 182);
			dust.noGravity = true;
			dust.velocity = -base.Projectile.velocity * 0.5f * Main.rand.NextFloat(0.1f, 0.9f);
			dust.scale = Main.rand.NextFloat(0.2f, 0.6f);
		}
		Lighting.AddLight(base.Projectile.Center, 0.75f * base.Projectile.Opacity, 0f, 0f);
		time++;
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.Opacity == 1f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && base.Projectile.Opacity == 1f)
		{
			if (base.Projectile.ai[0] == 0f || Main.zenithWorld)
			{
				target.AddBuff(ModContent.BuffType<VulnerabilityHex>(), 180);
			}
			else
			{
				target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 120);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		((Color)(ref lightColor)).R = (byte)(255f * base.Projectile.Opacity);
		if (CalamityGlobalNPC.SCal != -1 && NPC.AnyNPCs(ModContent.NPCType<SupremeCalamitas>()) && Main.npc[CalamityGlobalNPC.SCal].active && Main.npc[CalamityGlobalNPC.SCal].ModNPC<SupremeCalamitas>().permafrost)
		{
			((Color)(ref lightColor)).G = (byte)(255f * base.Projectile.Opacity);
			((Color)(ref lightColor)).B = (byte)(255f * base.Projectile.Opacity);
			((Color)(ref lightColor)).R = 0;
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 10f * base.Projectile.scale, targetHitbox);
	}
}
