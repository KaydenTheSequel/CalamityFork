using System;
using CalamityMod.NPCs.Providence;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class HolyFlare : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 50;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 840;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		ProvUtils.ApplyGFBDamage(base.Projectile, 120, 10);
		Lighting.AddLight(base.Projectile.Center, 0.35f, 0.275f, 0f);
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 8)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		float velocityYCap = ((base.Projectile.position.Y + (float)base.Projectile.height < player.position.Y) ? 1f : 0.25f);
		base.Projectile.velocity.Y += 0.01f;
		if (base.Projectile.velocity.Y > velocityYCap)
		{
			base.Projectile.velocity.Y = velocityYCap;
		}
		float velocityX = ((!ProvUtils.StandardAI()) ? 0.025f : 0.02f);
		if (base.Projectile.position.X + (float)base.Projectile.width < player.position.X)
		{
			if (base.Projectile.velocity.X < 0f)
			{
				base.Projectile.velocity.X *= 0.99f;
			}
			base.Projectile.velocity.X += velocityX;
		}
		else if (base.Projectile.position.X > player.position.X + (float)player.width)
		{
			if (base.Projectile.velocity.X > 0f)
			{
				base.Projectile.velocity.X *= 0.99f;
			}
			base.Projectile.velocity.X -= velocityX;
		}
		float velocityXCap = ((!ProvUtils.StandardAI()) ? 10f : 8f);
		if (base.Projectile.velocity.X > velocityXCap || base.Projectile.velocity.X < 0f - velocityXCap)
		{
			base.Projectile.velocity.X *= 0.97f;
		}
		float vel = Math.Clamp((Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) / 2f, 0f, 1f);
		GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center + Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(15f), 0f), 6.2831854820251465), base.Projectile.velocity.RotatedBy(Math.PI) * 0.5f, affectedByGravity: false, 10, Main.rand.NextFloat(0.8f, 1.2f), ProvUtils.GetProjectileColor(255)));
		if (Main.rand.NextBool())
		{
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, Vector2.Zero, Color.LightSlateGray, Color.DarkSlateGray, Main.rand.NextFloat(vel), 150f, MathHelper.ToRadians(Main.rand.NextFloat(-1f, 1f))));
		}
		base.Projectile.rotation = MathHelper.Lerp(0f, MathHelper.WrapAngle(Vector2.Zero.AngleTo(base.Projectile.velocity) + MathHelper.ToRadians(-90f)), Math.Clamp(vel, 0f, 1f));
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return ProvUtils.GetProjectileColor(lightColor);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = (ProvUtils.StandardAI() ? TextureAssets.Projectile[base.Type].Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/HolyFlareNight", (AssetRequestMode)2).Value);
		int framing = texture.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		base.Projectile.DrawBackglow(ProvUtils.GetProjectileColor(lightColor, Outline: true), 4f, texture, null, (SpriteEffects)0);
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)framing / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
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
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
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
		SoundEngine.PlaySound(SoundID.DD2_BetsyFireballImpact.WithPitchOffset(0.6f), base.Projectile.Center);
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
