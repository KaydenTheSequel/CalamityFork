using System;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class MarniteObliteratorProj : ModProjectile
{
	public static Asset<Texture2D> GlowmaskTex;

	public static Asset<Texture2D> BloomTex;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<MarniteObliterator>();

	public override string Texture => "CalamityMod/Items/Tools/MarniteObliterator";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float MoveInIntervals => ref base.Projectile.localAI[0];

	public ref float SpeenBeams => ref base.Projectile.localAI[1];

	public ref float Timer => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.hide = true;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void AI()
	{
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		Timer++;
		SpeenBeams += ((Timer > 140f) ? 1f : (1f + 2f * (float)Math.Pow(1f - Timer / 140f, 2.0)));
		if (base.Projectile.soundDelay <= 0)
		{
			SoundEngine.PlaySound(in MarniteObliterator.UseSound, base.Projectile.Center);
			base.Projectile.soundDelay = 23;
		}
		Vector2 val = Owner.Center - base.Projectile.Center;
		if (((Vector2)(ref val)).Length() >= 5f)
		{
			val = Owner.MountedCenter - base.Projectile.Center;
			Color blue;
			if (((Vector2)(ref val)).Length() >= 30f)
			{
				blue = Color.Blue;
				DelegateMethods.v3_1 = ((Color)(ref blue)).ToVector3() * 0.5f;
				Utils.PlotTileLine(Owner.MountedCenter + Owner.MountedCenter.DirectionTo(base.Projectile.Center) * 30f, base.Projectile.Center, 8f, DelegateMethods.CastLightOpen);
			}
			Vector2 center = base.Projectile.Center;
			blue = Color.Blue;
			Lighting.AddLight(center, ((Color)(ref blue)).ToVector3() * 0.7f);
		}
		if (MoveInIntervals > 0f)
		{
			MoveInIntervals--;
		}
		if (Owner.CantUseHoldout())
		{
			base.Projectile.Kill();
		}
		else if (MoveInIntervals <= 0f && Main.myPlayer == base.Projectile.owner)
		{
			Vector2 newVelocity = Owner.Calamity().mouseWorld - Owner.MountedCenter;
			if (Main.tile[Player.tileTargetX, Player.tileTargetY].HasTile)
			{
				newVelocity = new Vector2((float)Player.tileTargetX, (float)Player.tileTargetY) * 16f + Vector2.One * 8f - Owner.MountedCenter;
				MoveInIntervals = 2f;
			}
			newVelocity = Vector2.Lerp(newVelocity, base.Projectile.velocity, 0.7f);
			if (float.IsNaN(newVelocity.X) || float.IsNaN(newVelocity.Y))
			{
				newVelocity = -Vector2.UnitY;
			}
			if (((Vector2)(ref newVelocity)).Length() < 50f)
			{
				newVelocity = newVelocity.SafeNormalize(-Vector2.UnitY) * 50f;
			}
			int tileBoost = Owner.inventory[Owner.selectedItem].tileBoost;
			int fullRangeX = (Player.tileRangeX + tileBoost - 1) * 16 + 11;
			int fullRangeY = (Player.tileRangeY + tileBoost - 1) * 16 + 11;
			newVelocity.X = Math.Clamp(newVelocity.X, -fullRangeX, fullRangeX);
			newVelocity.Y = Math.Clamp(newVelocity.Y, -fullRangeY, fullRangeY);
			if (newVelocity != base.Projectile.velocity)
			{
				base.Projectile.netUpdate = true;
			}
			base.Projectile.velocity = newVelocity;
		}
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.ChangeDir(Math.Sign(base.Projectile.velocity.X));
		Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, base.Projectile.velocity.ToRotation() * Owner.gravDir - (float)Math.PI / 2f);
		Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, base.Projectile.velocity.ToRotation() * Owner.gravDir - (float)Math.PI / 2f - (float)Math.PI / 8f * (float)Owner.direction);
		Owner.SetDummyItemTime(2);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.Center = Owner.MountedCenter + base.Projectile.velocity;
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		float fadeOpacity = (float)Math.Sqrt(1f - completionRatio);
		return Color.DeepSkyBlue * fadeOpacity;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return 29.4f * completionRatio;
	}

	public void DrawBeam(Texture2D beamTex, Vector2 direction, int beamIndex)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = Owner.MountedCenter + direction * 17f;
		Vector2 center = default(Vector2);
		Vector2 startPos = val + direction.RotatedBy(1.5707963705062866, center) * (float)Math.Cos((float)Math.PI * 2f * (float)beamIndex / 3f + SpeenBeams * 0.06f) * 13f;
		float rotation = (base.Projectile.Center - startPos).ToRotation();
		Vector2 beamOrigin = new Vector2((float)beamTex.Width / 2f, (float)beamTex.Height);
		center = startPos - base.Projectile.Center;
		Vector2 beamScale = new Vector2(5.4f, ((Vector2)(ref center)).Length() / (float)beamTex.Height);
		center = default(Vector2);
		CalamityUtils.DrawChromaticAberration(direction.RotatedBy(1.5707963705062866, center), 4f, delegate(Vector2 offset, Color colorMod)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00de: Unknown result type (might be due to invalid IL or missing references)
			//IL_00df: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_010a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0118: Unknown result type (might be due to invalid IL or missing references)
			//IL_011e: Unknown result type (might be due to invalid IL or missing references)
			Color val2 = Color.Lerp(Color.Blue, Color.Goldenrod, 0.5f + 0.5f * (float)Math.Sin(SpeenBeams * 0.2f));
			val2 *= 0.54f;
			val2 = val2.MultiplyRGB(colorMod);
			Main.EntitySpriteDraw(beamTex, startPos + offset - Main.screenPosition, null, val2, rotation + (float)Math.PI / 2f, beamOrigin, beamScale, (SpriteEffects)0);
			beamScale.X = 2.4f;
			val2 = Color.Lerp(Color.DeepSkyBlue, Color.Chocolate, 0.5f + 0.5f * (float)Math.Sin(SpeenBeams * 0.2f + 1.2f));
			val2 = val2.MultiplyRGB(colorMod);
			Main.EntitySpriteDraw(beamTex, startPos + offset - Main.screenPosition, null, val2, rotation + (float)Math.PI / 2f, beamOrigin, beamScale, (SpriteEffects)0);
		});
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Projectile.active)
		{
			return false;
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		Vector2 normalizedVelocity = base.Projectile.velocity.SafeNormalize(Vector2.Zero);
		Texture2D beamTex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/SimpleGradient", (AssetRequestMode)2).Value;
		for (int i = 0; i < 3; i++)
		{
			if ((float)Math.Sin((float)Math.PI * 2f * (float)i / 3f + SpeenBeams * 0.06f) < 0f)
			{
				DrawBeam(beamTex, normalizedVelocity, i);
			}
		}
		if (BloomTex == null)
		{
			BloomTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		}
		Texture2D bloomTex = BloomTex.Value;
		Main.EntitySpriteDraw(bloomTex, base.Projectile.Center - Main.screenPosition, null, Color.DeepSkyBlue * 0.3f, (float)Math.PI / 2f, bloomTex.Size() / 2f, 0.3f * base.Projectile.scale, (SpriteEffects)0);
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/DoubleTrail", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail((Vector2[])(object)new Vector2[2]
		{
			base.Projectile.Center,
			Owner.MountedCenter - normalizedVelocity * 13f
		}, new PrimitiveSettings(WidthFunction, ColorFunction, null, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 30);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(9f, (float)tex.Height / 2f);
		SpriteEffects effect = (SpriteEffects)0;
		if ((float)Owner.direction * Owner.gravDir < 0f)
		{
			effect = (SpriteEffects)2;
		}
		Main.EntitySpriteDraw(tex, Owner.MountedCenter + normalizedVelocity * 10f - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, effect);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		if (GlowmaskTex == null)
		{
			GlowmaskTex = ModContent.Request<Texture2D>("CalamityMod/Items/Tools/MarniteObliteratorBloom", (AssetRequestMode)2);
		}
		Texture2D value = GlowmaskTex.Value;
		float bloomOpacity = (float)Math.Pow(Math.Clamp(Timer / 100f, 0f, 1f), 2.0) * (0.85f + (0.5f + 0.5f * (float)Math.Sin(Main.GlobalTimeWrappedHourly))) * 0.8f;
		Main.EntitySpriteDraw(color: Color.Lerp(Color.DeepSkyBlue, Color.Chocolate, 0.5f + 0.5f * (float)Math.Sin(SpeenBeams * 0.2f + 1.2f)) * bloomOpacity, texture: value, position: Owner.MountedCenter + normalizedVelocity * 10f - Main.screenPosition, sourceRectangle: null, rotation: base.Projectile.rotation, origin: origin, scale: base.Projectile.scale, effects: effect);
		for (int j = 0; j < 3; j++)
		{
			if ((float)Math.Sin((float)Math.PI * 2f * (float)j / 3f + SpeenBeams * 0.06f) >= 0f)
			{
				DrawBeam(beamTex, normalizedVelocity, j);
			}
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}
}
