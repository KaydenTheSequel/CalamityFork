using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class JellyghoulBolt : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public float Progress => 1f - (float)base.Projectile.timeLeft / 300f;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		Main.projFrames[base.Type] = 20;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.penetrate = -1;
		base.Projectile.hostile = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 1f * base.Projectile.Opacity, 0.1f * base.Projectile.Opacity, 0.1f);
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 4 % Main.projFrames[base.Type];
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] % 5f == 0f)
		{
			Color pulseColor = ((!Main.rand.NextBool()) ? (Main.rand.NextBool() ? Color.OrangeRed : Color.IndianRed) : (Main.rand.NextBool() ? Color.Red : Color.DarkRed));
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, pulseColor, new Vector2(0.5f, 1f), base.Projectile.velocity.ToRotation(), 0.1f, 0.2f + 0.2f * (1f - Progress), 10));
		}
		Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 296, 0f, 0f, 0, default(Color), 1.5f);
		dust.noGravity = true;
		dust.velocity = Vector2.Zero;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 60);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 3; i++)
		{
			int idx = Dust.NewDust(base.Projectile.position, 8, 8, 235, 0f, 0f, 0, default(Color), 0.75f);
			Main.dust[idx].noGravity = true;
			Dust obj = Main.dust[idx];
			obj.velocity *= 3f;
			idx = Dust.NewDust(base.Projectile.position, 8, 8, 235, 0f, 0f, 0, default(Color), 0.75f);
			Main.dust[idx].noGravity = true;
			Dust obj2 = Main.dust[idx];
			obj2.velocity *= 3f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:ExobladePierce"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/BlobbyNoise", (AssetRequestMode)2));
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseImage2("Images/Extra_189");
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseColor(Color.Red);
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseSecondaryColor(Color.OrangeRed);
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(SlashWidthFunction, SlashColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ExobladePierce"]), 30);
		_ = TextureAssets.Projectile[base.Type].Value;
		_ = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		_ = base.Projectile.frame;
		return false;
	}

	public float SlashWidthFunction(float _, Vector2 vertexPos)
	{
		return 16f * Utils.GetLerpValue(0f, 0.1f, _, clamped: true);
	}

	public Color SlashColorFunction(float _, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return Color.IndianRed * base.Projectile.Opacity;
	}
}
