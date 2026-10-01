using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class BrimstoneHellblast : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 255;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 10)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		int target = Player.FindClosest(base.Projectile.Center, 1, 1);
		float targetDist = ((target == -1 || Main.player[target].dead || !Main.player[target].active || Main.player[target] == null) ? 1000f : Vector2.Distance(Main.player[target].Center, base.Projectile.Center));
		Lighting.AddLight(base.Projectile.Center, 0.9f * base.Projectile.Opacity, 0f, 0f);
		if (targetDist < 1400f && base.Projectile.ai[1] == 2f)
		{
			float sine = (float)Math.Sin((float)base.Projectile.timeLeft * 0.575f / (float)Math.PI);
			Vector2 offset = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * 16f;
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + offset, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 8, 0.8f, Main.rand.NextBool() ? Color.Red : Color.Lerp(Color.Red, Color.Magenta, 0.5f)));
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center - offset, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 8, 0.8f, Main.rand.NextBool() ? Color.Red : Color.Lerp(Color.Red, Color.Magenta, 0.5f)));
		}
		if (base.Projectile.timeLeft < 51)
		{
			base.Projectile.Opacity -= 0.02f;
		}
		if (base.Projectile.ai[2] == 0f)
		{
			base.Projectile.ai[2] = 1f;
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 18f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.03f;
		}
		if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.spriteDirection = -1;
			base.Projectile.rotation = (float)Math.Atan2(0f - base.Projectile.velocity.Y, 0f - base.Projectile.velocity.X);
		}
		else
		{
			base.Projectile.spriteDirection = 1;
			base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X);
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

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.timeLeft >= 51;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && base.Projectile.timeLeft >= 51)
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

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
		for (int dust = 0; dust <= 5; dust++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 235);
		}
	}
}
