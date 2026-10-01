using System;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using CalamityMod.Projectiles.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class IgnisSigilFireball : ModProjectile, ILocalizedModType, IModType
{
	public ref float Time => ref base.Projectile.ai[0];

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 9;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 92;
		base.Projectile.height = 92;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
		base.Projectile.alpha = 255;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 10f, Time, clamped: true);
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter % 4 == 3)
			{
				base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
			}
		}
		else
		{
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.scale += 0.2f;
			if (base.Projectile.scale >= 1.75f)
			{
				base.Projectile.Kill();
			}
			if (base.Projectile.timeLeft > 5)
			{
				base.Projectile.timeLeft = 5;
			}
		}
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0.22f / 255f, (float)(255 - base.Projectile.alpha) * 0.05f / 255f, (float)(255 - base.Projectile.alpha) * 0.05f / 255f);
		base.Projectile.scale -= 0.01f;
		if (base.Projectile.scale <= 0f)
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.ai[0] <= 3f)
		{
			base.Projectile.ai[0]++;
			return;
		}
		GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center, base.Projectile.velocity, base.Projectile.scale * 1.5f, Color.OrangeRed, 3, 1f, 0f, 1f));
		base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.125f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(189, 180);
		base.Projectile.ai[1] = 1f;
		base.Projectile.penetrate = -1;
	}

	private float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.Lerp(0f, MathHelper.Lerp(base.Projectile.scale * 132f, 0f, completionRatio), MathF.Pow(completionRatio, 0.4f));
	}

	private Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.GlobalTimeWrappedHourly;
		float fadeOpacity = Utils.GetLerpValue(0.5f, 0f, completionRatio, clamped: true) * base.Projectile.Opacity;
		return Color.Lerp(Color.IndianRed, Color.Orange, completionRatio) * fadeOpacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 340);
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int frameHeight = texture.Height / Main.projFrames[base.Type];
		Rectangle sourceRect = default(Rectangle);
		((Rectangle)(ref sourceRect))._002Ector(0, frameHeight * base.Projectile.frame, texture.Width, frameHeight);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, sourceRect, Color.White, base.Projectile.rotation, sourceRect.Size() * 0.5f, 1f, (SpriteEffects)0);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/UnstableCastersGauntlet/IgnisSigilHit");
		style.Volume = 0.7f;
		style.PitchVariance = 0.1f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		for (int i = 0; i < 40; i++)
		{
			Vector2 vel = Utils.RotatedByRandom(new Vector2(10f, 10f), 100.0) * Main.rand.NextFloat(0.85f, 1.2f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Utils.RotatedBy(new Vector2(Main.rand.NextFloat(-22f, -30f), Main.rand.NextFloat(-4f, 4f)), (double)(base.Projectile.rotation - (float)Math.PI / 2f), default(Vector2)), Main.rand.NextBool(5) ? 174 : 35, vel * Main.rand.NextFloat(0.1f, 0.9f) + new Vector2(0f, -2f));
			dust.noGravity = false;
			dust.scale = Main.rand.NextFloat(1.1f, 1.7f);
		}
		for (int j = 0; j < 8; j++)
		{
			Vector2 vel2 = Utils.RotatedByRandom(new Vector2(10f, 10f), 100.0) * Main.rand.NextFloat(0.85f, 1.2f);
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center + Utils.RotatedBy(new Vector2(Main.rand.NextFloat(-22f, -30f), Main.rand.NextFloat(-4f, 4f)), (double)(base.Projectile.rotation - (float)Math.PI / 2f), default(Vector2)), vel2 * 0.8f, ModContent.ProjectileType<IgnisSigilEmbers>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center + Utils.RotatedBy(new Vector2(-26f, 0f), (double)(base.Projectile.rotation - (float)Math.PI / 2f), default(Vector2)), Vector2.Zero, Color.OrangeRed, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.265f, 21, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center + Utils.RotatedBy(new Vector2(-26f, 0f), (double)(base.Projectile.rotation - (float)Math.PI / 2f), default(Vector2)), Vector2.Zero, ModContent.ProjectileType<IgnisSigilFireballExplosion>(), (int)((float)base.Projectile.damage * 3.35f), base.Projectile.knockBack, base.Projectile.owner);
			projectile.ai[1] = 145f;
			projectile.localAI[1] = Main.rand.NextFloat(0.1f, 0.2f);
			projectile.netUpdate = true;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		Color val = Color.Lerp(Color.LightGoldenrodYellow, Color.Orange, 0.5f);
		((Color)(ref val)).A = 0;
		return val * base.Projectile.Opacity;
	}
}
