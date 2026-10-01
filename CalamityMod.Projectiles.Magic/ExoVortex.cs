using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class ExoVortex : ModProjectile, ILocalizedModType, IModType
{
	public const float HueShiftAcrossAfterimages = 0.2f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public float Hue => base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 35;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 60;
		base.Projectile.height = 60;
		base.Projectile.alpha = 255;
		base.Projectile.scale = 0.01f;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.MaxUpdates = 4;
		base.Projectile.timeLeft = base.Projectile.MaxUpdates * 120;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 18;
		base.Projectile.ignoreWater = true;
		base.Projectile.hide = true;
	}

	public override void AI()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(1300f);
		if (potentialTarget != null)
		{
			float flySpeed = 40f / (float)base.Projectile.MaxUpdates;
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.SafeDirectionTo(potentialTarget.Center) * flySpeed, 0.02f);
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.04f;
		Vector2 center = base.Projectile.Center;
		Color white = Color.White;
		Lighting.AddLight(center, ((Color)(ref white)).ToVector3() * 0.9f);
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 20f, Time, clamped: true);
		base.Projectile.scale = Utils.Remap(Time, 0f, (float)base.Projectile.MaxUpdates * 15f, 0.01f, 1.5f) * Utils.GetLerpValue(0f, (float)base.Projectile.MaxUpdates * 16f, base.Projectile.timeLeft, clamped: true);
		base.Projectile.ExpandHitboxBy((int)(base.Projectile.scale * 62f));
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindProjectiles.Add(index);
	}

	public float PrimitiveWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return (float)base.Projectile.width * 0.6f * MathHelper.SmoothStep(0.6f, 1f, Utils.GetLerpValue(0f, 0.3f, completionRatio, clamped: true));
	}

	public Color PrimitiveTrailColor(float completionRatio, Vector2 vertexPos)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		float hue = Hue % 1f + 0.2f;
		if (hue >= 0.99f)
		{
			hue = 0.99f;
		}
		float velocityOpacityFadeout = Utils.GetLerpValue(2f, 5f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
		return CalamityUtils.MulticolorLerp(hue, CalamityUtils.ExoPalette) * base.Projectile.Opacity * (1f - completionRatio) * Utils.GetLerpValue(0.04f, 0.2f, completionRatio, clamped: true) * velocityOpacityFadeout;
	}

	public Vector2 PrimitiveOffsetFunction(float completionRatio, Vector2 _)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		return base.Projectile.Size * 0.5f + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * base.Projectile.scale * 2f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
		Texture2D worleyNoise = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/BlobbyNoise", (AssetRequestMode)2).Value;
		float spinRotation = Main.GlobalTimeWrappedHourly * 5.2f;
		Main.spriteBatch.EnterShaderRegion();
		GameShaders.Misc["CalamityMod:SideStreakTrail"].UseImage1("Images/Misc/Perlin");
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(PrimitiveWidthFunction, PrimitiveTrailColor, PrimitiveOffsetFunction, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:SideStreakTrail"]), 51);
		Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
		GameShaders.Misc["CalamityMod:ExoVortex"].UseOpacity(1f);
		GameShaders.Misc["CalamityMod:ExoVortex"].Apply();
		for (int i = 0; i < 5; i++)
		{
			float increment = Hue % 1f + (float)i / 4f * 0.2f;
			Vector2 scale = MathHelper.Lerp(1f, 0.6f, (float)i / 4f) * base.Projectile.Size / worleyNoise.Size() * 2f;
			Vector2 drawOffset = Vector2.UnitY * base.Projectile.scale * 6f;
			Color c = CalamityUtils.MulticolorLerp(increment, CalamityUtils.ExoPalette) * base.Projectile.Opacity;
			Vector2 drawPosition = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f - Main.screenPosition;
			Main.spriteBatch.Draw(worleyNoise, drawPosition - drawOffset, (Rectangle?)null, c, 0f - spinRotation, worleyNoise.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(worleyNoise, drawPosition + drawOffset, (Rectangle?)null, c, spinRotation, worleyNoise.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		}
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}
}
