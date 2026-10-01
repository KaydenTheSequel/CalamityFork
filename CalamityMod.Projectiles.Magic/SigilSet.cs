using System;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class SigilSet : ModProjectile, ILocalizedModType, IModType
{
	private const float RuneLerpTime = 22f;

	private const float RuneDelayTime = 3f;

	private const float StartRadius = 240f;

	private const float BaseRadius = 150f;

	private const float GhostFlashDuration = 22f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float FadeoutFlag => ref base.Projectile.ai[2];

	public ref float RuneTimer => ref base.Projectile.localAI[2];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 550;
		base.Projectile.height = 550;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		Player p = Main.player[base.Projectile.owner];
		if (p == null || !p.active || p.dead || p.HeldItem.type != ModContent.ItemType<UnstableCastersGauntlet>())
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.Center = p.MountedCenter + Vector2.UnitY * p.gfxOffY;
		base.Projectile.rotation += 0.01f;
		Lighting.AddLight(base.Projectile.Center, 1f, 1f, 1f);
		if (base.Projectile.frameCounter++ > 3)
		{
			base.Projectile.frameCounter = 0;
			if (base.Projectile.frame++ > 2)
			{
				base.Projectile.frame = 0;
			}
		}
		if (FadeoutFlag == 1f)
		{
			base.Projectile.alpha += 13;
			if (base.Projectile.alpha >= 255)
			{
				base.Projectile.Kill();
			}
		}
		else
		{
			base.Projectile.alpha = Utils.Clamp(base.Projectile.alpha - 25, 0, 255);
		}
		RuneTimer++;
		if (base.Projectile.ai[1] == 0f)
		{
			int[] sigilTypes = new int[6]
			{
				ModContent.ProjectileType<IgnisSigil>(),
				ModContent.ProjectileType<AquaSigil>(),
				ModContent.ProjectileType<TerraSigil>(),
				ModContent.ProjectileType<AerSigil>(),
				ModContent.ProjectileType<OrdoSigil>(),
				ModContent.ProjectileType<PerditoSigil>()
			};
			for (int i = 0; i < 6; i++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, sigilTypes[i], base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, base.Projectile.identity, i);
			}
			base.Projectile.ai[1] = 1f;
		}
		else
		{
			int activeSigilCount = 0;
			for (int j = 0; j < Main.maxProjectiles; j++)
			{
				Projectile proj = Main.projectile[j];
				if (proj.active && proj.ai[0] == (float)base.Projectile.identity && proj.owner == base.Projectile.owner)
				{
					int projType = proj.type;
					if ((projType == ModContent.ProjectileType<IgnisSigil>() || projType == ModContent.ProjectileType<AquaSigil>() || projType == ModContent.ProjectileType<TerraSigil>() || projType == ModContent.ProjectileType<AerSigil>() || projType == ModContent.ProjectileType<OrdoSigil>() || projType == ModContent.ProjectileType<PerditoSigil>() || projType == ModContent.ProjectileType<WarpSigil>()) && proj.ai[2] <= 0f)
					{
						activeSigilCount++;
					}
				}
			}
			if (activeSigilCount == 0)
			{
				base.Projectile.localAI[0]++;
				if (base.Projectile.localAI[0] >= 50f)
				{
					FadeoutFlag = 1f;
				}
			}
			else
			{
				base.Projectile.localAI[0] = 0f;
			}
		}
		base.Projectile.timeLeft = 4;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		Texture2D main = TextureAssets.Projectile[base.Type].Value;
		Texture2D smol = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/ThaumRingSmall", (AssetRequestMode)2).Value;
		Texture2D rune = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/ThaumRune", (AssetRequestMode)2).Value;
		Asset<Texture2D> ghostTextureAsset = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/ThaumRuneGhost", (AssetRequestMode)2);
		float drawOpacity = 1f - (float)base.Projectile.alpha / 255f;
		Main.EntitySpriteDraw(smol, base.Projectile.Center - Main.screenPosition, null, Color.White * 0.5f * drawOpacity, base.Projectile.rotation, smol.Size() / 2f, base.Projectile.scale + MathF.Cos(2f * Main.GlobalTimeWrappedHourly + 5f) * 0.0027f, (SpriteEffects)0);
		Main.EntitySpriteDraw(main, base.Projectile.Center - Main.screenPosition, main.Frame(1, 4, 0, base.Projectile.frame), Color.White * 0.5f * drawOpacity, base.Projectile.rotation, new Vector2((float)(main.Width / 2), (float)(main.Height / 8)), base.Projectile.scale, (SpriteEffects)0);
		for (int i = 0; i < 14; i++)
		{
			float runeStartTick = (float)i * 3f;
			float t = MathHelper.Clamp((RuneTimer - runeStartTick) / 22f, 0f, 1f);
			float ease = MathHelper.SmoothStep(0f, 1f, t);
			float baseDist = 150f + MathF.Sin(Main.GlobalTimeWrappedHourly * 2f) * 10f;
			float lerpedRadius = MathHelper.Lerp(240f, baseDist, ease);
			float angle = (float)Math.PI * 2f * (float)i / 14f;
			Vector2 circleOffset = Vector2.UnitX.RotatedBy(angle) * lerpedRadius;
			circleOffset = circleOffset.RotatedBy((0f - base.Projectile.rotation) * 0.6f);
			Vector2 animatedPos = base.Projectile.Center + circleOffset;
			float runeAlpha = ease;
			Main.EntitySpriteDraw(rune, animatedPos - Main.screenPosition, rune.Frame(1, 7, 0, i % 6), Color.White * 0.5f * drawOpacity * runeAlpha, (animatedPos - base.Projectile.Center).ToRotation() + (float)Math.PI / 2f, new Vector2((float)(rune.Width / 2), (float)(rune.Height / 14)), base.Projectile.scale, (SpriteEffects)0);
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect)null, Main.GameViewMatrix.ZoomMatrix);
		Texture2D ghostTexture = ghostTextureAsset.Value;
		Vector2 runeOrigin = default(Vector2);
		for (int j = 0; j < 14; j++)
		{
			float runeStartTick2 = (float)j * 3f;
			float t2 = MathHelper.Clamp((RuneTimer - runeStartTick2) / 22f, 0f, 1f);
			float ease2 = MathHelper.SmoothStep(0f, 1f, t2);
			float peakTime = 0.25f;
			float rampUp = Utils.GetLerpValue(0f, peakTime, t2, clamped: true);
			float rampDown = Utils.GetLerpValue(1f, peakTime, t2, clamped: true);
			float ghostOpacity = Math.Min(rampUp, rampDown);
			if (ghostOpacity > 0f)
			{
				float baseDist2 = 150f + MathF.Sin(Main.GlobalTimeWrappedHourly * 2f) * 10f;
				float lerpedRadius2 = MathHelper.Lerp(240f, baseDist2, ease2);
				float angle2 = (float)Math.PI * 2f * (float)j / 14f;
				Vector2 circleOffset2 = Vector2.UnitX.RotatedBy(angle2) * lerpedRadius2;
				circleOffset2 = circleOffset2.RotatedBy((0f - base.Projectile.rotation) * 0.6f);
				Vector2 animatedPos2 = base.Projectile.Center + circleOffset2;
				Rectangle runeFrame = ghostTexture.Frame(1, 7, 0, j % 6);
				((Vector2)(ref runeOrigin))._002Ector((float)(runeFrame.Width / 2), (float)(runeFrame.Height / 2));
				Main.EntitySpriteDraw(ghostTexture, animatedPos2 - Main.screenPosition, runeFrame, Color.White * drawOpacity * ghostOpacity * 0.6f, (animatedPos2 - base.Projectile.Center).ToRotation() + (float)Math.PI / 2f, runeOrigin, base.Projectile.scale * (1f + ghostOpacity * 0.3f), (SpriteEffects)0);
			}
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect)null, Main.GameViewMatrix.ZoomMatrix);
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
