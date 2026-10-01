using System;
using CalamityMod.Items.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class MarniteDeconstructorProj : ModProjectile
{
	public static Asset<Texture2D> GlowmaskTex;

	public static Asset<Texture2D> BloomTex;

	public static Asset<Texture2D> SelectionTex;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<MarniteDeconstructor>();

	public override string Texture => "CalamityMod/Items/Tools/MarniteDeconstructor";

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
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
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
			if (Main.tile[Player.tileTargetX, Player.tileTargetY].WallType != 0)
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

	public void DrawBeam(Texture2D beamTex, Vector2 direction, float beamProgress)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = Owner.MountedCenter + direction * 30f;
		Vector2 center = default(Vector2);
		Vector2 startPos = val + direction.RotatedBy(1.5707963705062866, center) * (float)Math.Cos((float)Math.PI * 2f * beamProgress + SpeenBeams * 0.06f) * 8f;
		float squareHeight = (beamProgress + SpeenBeams * 0.02f) % 1f;
		squareHeight = (((double)squareHeight < 0.25) ? 0f : (((double)squareHeight < 0.5) ? ((squareHeight - 0.25f) / 0.25f) : ((!((double)squareHeight < 0.75)) ? (1f - (squareHeight - 0.75f) / 0.25f) : 1f)));
		float squareWidth = (beamProgress + SpeenBeams * 0.02f) % 1f;
		squareWidth = (((double)squareWidth < 0.25) ? (squareWidth / 0.25f) : (((double)squareWidth < 0.5) ? 1f : ((!((double)squareWidth < 0.75)) ? 0f : (1f - (squareWidth - 0.5f) / 0.25f))));
		Vector2 endPos = base.Projectile.Center + new Vector2(squareWidth * 15.5f, squareHeight * 15.5f) - Vector2.One * 7.75f;
		float rotation = (endPos - startPos).ToRotation();
		Vector2 beamOrigin = new Vector2((float)beamTex.Width / 2f, (float)beamTex.Height);
		center = startPos - endPos;
		Vector2 beamScale = new Vector2(5.4f, ((Vector2)(ref center)).Length() / (float)beamTex.Height);
		center = default(Vector2);
		CalamityUtils.DrawChromaticAberration(direction.RotatedBy(1.5707963705062866, center), 1f, delegate(Vector2 offset, Color colorMod)
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
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Projectile.active)
		{
			return false;
		}
		Vector2 normalizedVelocity = base.Projectile.velocity.SafeNormalize(Vector2.Zero);
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
			GlowmaskTex = ModContent.Request<Texture2D>("CalamityMod/Items/Tools/MarniteDeconstructorBloom", (AssetRequestMode)2);
		}
		Texture2D value = GlowmaskTex.Value;
		float bloomOpacity = (float)Math.Pow(Math.Clamp(Timer / 100f, 0f, 1f), 2.0) * (0.85f + (0.5f + 0.5f * (float)Math.Sin(Main.GlobalTimeWrappedHourly))) * 0.8f;
		Color bloomColor = Color.Lerp(Color.DeepSkyBlue, Color.Chocolate, 0.5f + 0.5f * (float)Math.Sin(SpeenBeams * 0.2f + 1.2f));
		Main.EntitySpriteDraw(value, Owner.MountedCenter + normalizedVelocity * 10f - Main.screenPosition, null, bloomColor * bloomOpacity, base.Projectile.rotation, origin, base.Projectile.scale, effect);
		if (BloomTex == null)
		{
			BloomTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		}
		Texture2D bloomTex = BloomTex.Value;
		Main.EntitySpriteDraw(bloomTex, base.Projectile.Center - Main.screenPosition, null, Color.DeepSkyBlue * 0.3f, (float)Math.PI / 2f, bloomTex.Size() / 2f, 0.3f * base.Projectile.scale, (SpriteEffects)0);
		if (SelectionTex == null)
		{
			SelectionTex = ModContent.Request<Texture2D>("CalamityMod/Items/Tools/MarniteDeconstructorSelection", (AssetRequestMode)2);
		}
		Texture2D selectionTex = SelectionTex.Value;
		CalamityUtils.DrawChromaticAberration(Vector2.UnitX, 2f, delegate(Vector2 offset, Color colorMod)
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			Main.EntitySpriteDraw(selectionTex, base.Projectile.Center + offset - Main.screenPosition, null, bloomColor.MultiplyRGB(colorMod), 0f, selectionTex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		});
		Texture2D beamTex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/SimpleGradient", (AssetRequestMode)2).Value;
		for (int i = 0; i < 2; i++)
		{
			DrawBeam(beamTex, normalizedVelocity, (float)i / 2f);
		}
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}
}
