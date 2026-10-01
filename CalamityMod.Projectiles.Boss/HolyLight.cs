using System;
using CalamityMod.Dusts;
using CalamityMod.Enums;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class HolyLight : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/StarProj";

	public override void SetDefaults()
	{
		base.Projectile.localAI[1] = Main.rand.NextFloat(30f);
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 200;
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[2] == 0f)
		{
			SoundStyle soundStyle = SoundID.DD2_WitherBeastCrystalImpact with
			{
				MaxInstances = 10
			};
			SoundEngine.PlaySound(in soundStyle, base.Projectile.Center);
			Color col = default(Color);
			((Color)(ref col))._002Ector(54, 209, 54);
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, col, "CalamityMod/Particles/BlastCone", new Vector2(Main.rand.NextFloat(4f, 7f), 1.5f), Vector2.Zero.AngleTo(base.Projectile.velocity), 1f, 0f, 30, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0f, 0.6f, 0f);
		if (base.Projectile.ai[0] < 240f)
		{
			base.Projectile.ai[0]++;
			if (base.Projectile.timeLeft < 160)
			{
				base.Projectile.timeLeft = 160;
			}
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 16f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.01f;
		}
		int index = Player.FindClosest(base.Projectile.position, base.Projectile.width, base.Projectile.height);
		Player player = Main.player[index];
		if (player != null)
		{
			float playerDist = Vector2.Distance(player.Center, base.Projectile.Center);
			if (!player.immune && playerDist < 50f && !player.dead && base.Projectile.position.X < player.position.X + (float)player.width && base.Projectile.position.X + (float)base.Projectile.width > player.position.X && base.Projectile.position.Y < player.position.Y + (float)player.height && base.Projectile.position.Y + (float)base.Projectile.height > player.position.Y)
			{
				int healAmt = (int)base.Projectile.ai[1];
				player.HealPlayer(healAmt, HealTextType.Local);
				NetMessage.SendData(66, -1, -1, null, index, healAmt);
				base.Projectile.Kill();
			}
			Color col = default(Color);
			((Color)(ref col))._002Ector(54, 209, 54);
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, -base.Projectile.velocity * 0.8f, affectedByGravity: false, 5, 0.06f, col * 0.85f, new Vector2(1f, 0.3f), quickShrink: true, glow: false, 1.5f));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		float vel = ((Vector2)(ref base.Projectile.velocity)).Length() / 8f;
		base.Projectile.localAI[1] += vel;
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Color brightGreen = default(Color);
		((Color)(ref brightGreen))._002Ector(54, 209, 54, 0);
		Vector2 projDirection = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		Vector2 halfTextureSize = value.Size() / 2f;
		Color halfBrightGreen = brightGreen * 0.5f;
		float timeLeftColorScale = MathHelper.Lerp(0.5f, 1.5f, Math.Abs(MathF.Sin(base.Projectile.localAI[1] / 10f)));
		base.Projectile.rotation += MathHelper.ToRadians(timeLeftColorScale * 2f);
		Vector2 timeLeftDrawEffect = new Vector2(0.5f, 1f) * timeLeftColorScale;
		Vector2 timeLeftDrawEffect2 = new Vector2(0.5f, 1f) * timeLeftColorScale;
		brightGreen *= timeLeftColorScale;
		halfBrightGreen *= timeLeftColorScale;
		Vector2 position3 = projDirection + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * Utils.GetLerpValue(0.5f, 1f, base.Projectile.localAI[0] / 60f, clamped: true) * 0f;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(value, position3, null, brightGreen, (float)Math.PI / 2f - base.Projectile.rotation, halfTextureSize, timeLeftDrawEffect, spriteEffects);
		Main.EntitySpriteDraw(value, position3, null, brightGreen, 0f - base.Projectile.rotation, halfTextureSize, timeLeftDrawEffect2, spriteEffects);
		Main.EntitySpriteDraw(value, position3, null, halfBrightGreen, (float)Math.PI / 2f - base.Projectile.rotation, halfTextureSize, timeLeftDrawEffect * 0.6f, spriteEffects);
		Main.EntitySpriteDraw(value, position3, null, halfBrightGreen, 0f - base.Projectile.rotation, halfTextureSize, timeLeftDrawEffect2 * 0.6f, spriteEffects);
		Main.EntitySpriteDraw(value, position3, null, brightGreen, (float)Math.PI / 4f + base.Projectile.rotation, halfTextureSize, timeLeftDrawEffect * 0.6f, spriteEffects);
		Main.EntitySpriteDraw(value, position3, null, brightGreen, (float)Math.PI * 3f / 4f + base.Projectile.rotation, halfTextureSize, timeLeftDrawEffect2 * 0.6f, spriteEffects);
		Main.EntitySpriteDraw(value, position3, null, halfBrightGreen, (float)Math.PI / 4f + base.Projectile.rotation, halfTextureSize, timeLeftDrawEffect * 0.36f, spriteEffects);
		Main.EntitySpriteDraw(value, position3, null, halfBrightGreen, (float)Math.PI * 3f / 4f + base.Projectile.rotation, halfTextureSize, timeLeftDrawEffect2 * 0.36f, spriteEffects);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.Item14 with
		{
			Pitch = -0.3f,
			Volume = 0.7f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		style = new SoundStyle("CalamityMod/Sounds/Custom/PlantyMushMine", 3);
		style.Volume = 0.5f;
		style.Pitch = 0.3f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		Color particleColor = default(Color);
		((Color)(ref particleColor))._002Ector(54, 209, 54);
		Color smokeColor = Color.Lerp(particleColor, Color.DarkSlateGray, 0.5f);
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, smokeColor, "CalamityMod/Particles/SoftRoundExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 0.06f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (int i = 0; i < 7; i++)
		{
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, Vector2.UnitX.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(7f), smokeColor, 30, Main.rand.NextFloat(0.6f, 1f), 0.5f, Main.rand.NextFloat(-0.03f, 0.03f), glowing: true));
		}
		for (int j = 0; j < 8; j++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), Vector2.UnitX.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(1.8f, 10f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(1f, 1.8f);
			dust.color = particleColor;
			dust.noLightEmittence = true;
		}
	}
}
