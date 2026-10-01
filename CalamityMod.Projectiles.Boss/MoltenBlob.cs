using System;
using CalamityMod.NPCs.Providence;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class MoltenBlob : ModProjectile, ILocalizedModType, IModType
{
	private float Landing = 1f;

	private bool colliding;

	private bool inLava;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 900;
		base.Projectile.scale = 1.5f;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		ProvUtils.ApplyGFBDamage(base.Projectile, 120, 10);
		Lighting.AddLight(base.Projectile.Center, 0.3f, 0.225f, 0f);
		Landing += 0.1f;
		Landing = MathHelper.Clamp(Landing, 0f, 1f);
		colliding = base.Projectile.velocity.Y == 0f;
		if (colliding)
		{
			Landing = 0f;
			base.Projectile.localAI[2] = 5f;
			base.Projectile.velocity.X *= 0.8f;
		}
		base.Projectile.velocity.X *= 0.95f;
		if (base.Projectile.wet || base.Projectile.lavaWet)
		{
			if (!inLava)
			{
				base.Projectile.position.Y -= base.Projectile.velocity.Y;
				base.Projectile.velocity.Y = 0f;
				inLava = true;
			}
		}
		else if (!inLava)
		{
			if (base.Projectile.velocity.Y < 0f)
			{
				base.Projectile.velocity.Y = 0f;
			}
			base.Projectile.velocity.Y += 0.15f;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 1)
		{
			base.Projectile.frame = 0;
		}
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return ProvUtils.GetProjectileColor(0);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = (ProvUtils.StandardAI() ? TextureAssets.Projectile[base.Type].Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/MoltenBlobNight", (AssetRequestMode)2).Value);
		int framing = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y6 = framing * (1 + base.Projectile.frame);
		float squish = CalamityUtils.SineBumpEasing(Landing, 1) * 0.5f;
		base.Projectile.rotation = 0f;
		if (!colliding)
		{
			y6 = 0;
			squish = 0f - MathHelper.Clamp(((Vector2)(ref base.Projectile.velocity)).Length() / 15f, 0f, 0.5f);
			base.Projectile.rotation = Vector2.Zero.AngleTo(base.Projectile.velocity) + (float)Math.PI / 2f;
		}
		Vector2 vec = default(Vector2);
		((Vector2)(ref vec))._002Ector(0f, (float)framing * squish);
		Projectile projectile = base.Projectile;
		Color projectileColor = ProvUtils.GetProjectileColor(base.Projectile.alpha, Outline: true);
		Vector2 scale = new Vector2(1f + squish, 1f - squish);
		Vector2? offset = vec;
		projectile.DrawBackglow(projectileColor, 4f, scale, texture, null, offset);
		Main.spriteBatch.Draw(texture, base.Projectile.Center + vec - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)framing / 2f), new Vector2(1f + squish, 1f - squish), (SpriteEffects)0, 0f);
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
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		Color hiColor = ProvUtils.GetProjectileColor(255);
		ProvUtils.GetProjectileColor(0, Outline: true);
		for (int i = 0; i < 25; i++)
		{
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(3f), 0f), 6.2831854820251465), affectedByGravity: false, 10, Main.rand.NextFloat(0.1f, 0.4f), hiColor));
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, 0f, 0.5f, 0.1f, 4, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (float i2 = 0f; i2 < 1f; i2 += 0.25f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, hiColor, "CalamityMod/Particles/SoftRoundExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.002f * i2, 0.0125f * i2, 8, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, hiColor, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.002f, 0.015f, 5, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && !target.creativeGodMode)
		{
			ProvUtils.ApplyDebuffs(target, 120);
		}
	}
}
