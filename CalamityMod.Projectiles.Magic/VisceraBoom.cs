using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VisceraBoom : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 250);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 40;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 0f)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCKilled/PerfLargeDeath");
			style.Volume = 0.5f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int i = 0; i <= 14; i++)
			{
				GeneralParticleHandler.SpawnParticle(new BloodParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(15f, 15f), 100.0) * Main.rand.NextFloat(0.1f, 0.9f) + new Vector2(0f, -7f), 60, Main.rand.NextFloat(0.4f, 0.65f), (!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.Red));
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, (!ChildSafety.Disabled) ? Color.CornflowerBlue : Color.DarkRed, "CalamityMod/Particles/DetailedExplosion", Vector2.One, Main.rand.NextFloat(-15f, 15f), 0.16f, 0.87f, 15, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, (Color)((!ChildSafety.Disabled) ? Color.CornflowerBlue : new Color(255, 32, 32)), "CalamityMod/Particles/DustyCircleHardEdge", Vector2.One, Main.rand.NextFloat(-15f, 15f), 0.03f, 0.155f, 40, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			base.Projectile.ai[0] = 1f;
		}
		for (int j = 0; j <= 2; j++)
		{
			float speed = ((base.Projectile.ai[1] > 0f) ? 25 : 15);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, (!ChildSafety.Disabled) ? 16 : (Main.rand.NextBool() ? 60 : 5));
			dust.scale = Main.rand.NextFloat(1f, 2f) * Utils.GetLerpValue(0f, 40f, base.Projectile.timeLeft, clamped: true);
			dust.velocity = Utils.RotatedByRandom(new Vector2(speed, speed), 100.0) * Main.rand.NextFloat(0.1f, 0.9f);
			dust.noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BurningBlood>(), 150);
		target.AddBuff(ModContent.BuffType<Laceration>(), 150);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<BurningBlood>(), 150);
		target.AddBuff(ModContent.BuffType<Laceration>(), 150);
	}
}
