using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class EmesisGore : ModProjectile, ILocalizedModType, IModType
{
	public bool setStats = true;

	public int rotDirection = 1;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 4;
		base.Projectile.timeLeft = 800;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		float num = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (setStats)
		{
			rotDirection = ((!Main.rand.NextBool()) ? 1 : (-1));
			base.Projectile.rotation = Main.rand.NextFloat(-20f, 20f);
			setStats = false;
		}
		base.Projectile.rotation += 0.01f * (float)rotDirection * Utils.GetLerpValue(0f, 800f, base.Projectile.timeLeft);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.9975f;
		if (base.Projectile.ai[0] < 20f && base.Projectile.timeLeft > 695 && base.Projectile.timeLeft < 785)
		{
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(0.0017f * base.Projectile.ai[0] * base.Projectile.ai[2]);
		}
		base.Projectile.alpha = (int)Utils.Remap(base.Projectile.timeLeft, 70f, 0f, 0f, 255f);
		if (num < 1400f && base.Projectile.timeLeft > 70 && base.Projectile.timeLeft < 790)
		{
			if (base.Projectile.timeLeft % 5 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + base.Projectile.velocity * Main.rand.NextFloat(-2f, -1f), -base.Projectile.velocity * 0.3f, affectedByGravity: false, 6, 0.07f, Color.Lerp(Color.Green, Color.Chartreuse, 0.8f) * 0.5f, new Vector2(1f, 0.3f), quickShrink: true, glow: false));
			}
			if (Main.rand.NextBool(5))
			{
				Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(20f, 20f), Main.rand.NextBool(4) ? 215 : 75, -base.Projectile.velocity.RotatedByRandom(0.1) * Main.rand.NextFloat(0.1f, 0.3f), 0, default(Color), Main.rand.NextFloat(0.5f, 1.2f)).noGravity = true;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 420);
		for (int k = 0; k < 7; k++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(5) ? 28 : 215, Utils.RotatedByRandom(new Vector2(11f, 11f), 100.0) * Main.rand.NextFloat(0.05f, 0.8f));
			dust.scale = Main.rand.NextFloat(0.75f, 1.25f);
			dust.noGravity = false;
			GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(11f, 11f), 100.0) * Main.rand.NextFloat(0.5f, 1f), affectedByGravity: true, Main.rand.Next(23, 36), Main.rand.NextFloat(0.8f, 1.2f), Color.Chartreuse * Main.rand.NextFloat(0.15f, 0.5f)));
		}
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCKilled/PerfLargeDeath");
		style.Volume = 0.6f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 420);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/OldDukeGore", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation;
		Vector2 rotationPoint = value.Size() * 0.5f;
		Main.EntitySpriteDraw(value, drawPosition, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 20f, targetHitbox);
	}
}
