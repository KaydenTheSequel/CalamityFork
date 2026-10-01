using System;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class HolyBomb : ModProjectile, ILocalizedModType, IModType
{
	private float SquishAnimation = 1f;

	public int FireDamage;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 80;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 250;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.CooldownSlot = 1;
	}

	public override void OnSpawn(IEntitySource source)
	{
		FireDamage = Providence.FireDamage.CalculateProvidenceDamage();
		if (source is EntitySource_Parent { Entity: NPC parent } && parent.type == ModContent.NPCType<ProfanedGuardianDefender>())
		{
			FireDamage = ProfanedGuardianDefender.FireDamage;
		}
	}

	public override void AI()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		ProvUtils.ApplyGFBDamage(base.Projectile, 120, 20);
		Lighting.AddLight(base.Projectile.Center, 0.45f, 0.35f, 0f);
		SquishAnimation += 0.06f;
		SquishAnimation = MathHelper.Clamp(SquishAnimation, 0f, 1f);
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] % 120f == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
			for (int i = 0; i < 10; i++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center + new Vector2(0f, -15f), new Vector2(Main.rand.NextFloat(-10f, 10f), Main.rand.NextFloat(-4f, 2f)), affectedByGravity: false, 30, Main.rand.NextFloat(1f, 2.5f), ProvUtils.GetProjectileColor(255)));
			}
			float velocityY = -2f;
			SquishAnimation = 0f;
			if (base.Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, 0f, velocityY, ModContent.ProjectileType<HolyFlare>(), FireDamage, base.Projectile.knockBack, base.Projectile.owner);
			}
		}
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[1] = 1f;
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.975f;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return ProvUtils.GetProjectileColor(lightColor);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		float squish = CalamityUtils.SineBumpEasing(SquishAnimation, 1) * 0.25f;
		Texture2D texture = (ProvUtils.StandardAI() ? TextureAssets.Projectile[base.Type].Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/HolyBombNight", (AssetRequestMode)2).Value);
		int framing = texture.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		Projectile projectile = base.Projectile;
		Color projectileColor = ProvUtils.GetProjectileColor(lightColor, Outline: true);
		Vector2 scale = new Vector2(base.Projectile.scale + squish, base.Projectile.scale - squish);
		Vector2? offset = new Vector2(0f, -22f * (1f - squish));
		projectile.DrawBackglow(projectileColor, 4f, scale, texture, null, offset);
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)framing / 2f + 22f), new Vector2(base.Projectile.scale + squish, base.Projectile.scale - squish), (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		Color hiColor = ProvUtils.GetProjectileColor(255);
		ProvUtils.GetProjectileColor(0, Outline: true);
		for (int i = 0; i < 25; i++)
		{
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(10f), 0f), 6.2831854820251465), affectedByGravity: false, 10, Main.rand.NextFloat(0.8f, 1.2f), hiColor));
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, 0f, 0.5f, 0.1f, 4, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (float i2 = 0f; i2 < 1f; i2 += 0.25f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, hiColor, "CalamityMod/Particles/SoftRoundExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.02f * i2, 0.075f * i2, 24, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, hiColor, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.02f, 0.045f, 16, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		SoundEngine.PlaySound(SoundID.Item20.WithPitchOffset(-0.8f), base.Projectile.Center);
		SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballImpact, base.Projectile.Center);
		SoundEngine.PlaySound(SoundID.Item100.WithPitchOffset(0.4f), base.Projectile.Center);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && !target.creativeGodMode)
		{
			ProvUtils.ApplyDebuffs(target, 120);
		}
	}
}
