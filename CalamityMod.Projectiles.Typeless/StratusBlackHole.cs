using System;
using System.Linq;
using CalamityMod.CalPlayer;
using CalamityMod.Enums;
using CalamityMod.Systems.Graphic.PixelationSystem;
using CalamityMod.Systems.Mechanic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class StratusBlackHole : ModProjectile, ILocalizedModType, IModType
{
	private static Texture2D _TransparentBloomTex;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 94;
		base.Projectile.height = 94;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.DamageType = AverageDamageClass.Instance;
		base.Projectile.MaxUpdates = 1;
		base.Projectile.timeLeft = CalamityUtils.MinutesToFrames(10) * base.Projectile.MaxUpdates;
		base.Projectile.localNPCHitCooldown = 30 * base.Projectile.MaxUpdates;
		base.Projectile.aiStyle = 0;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.Opacity = 0f;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.975f;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 5)
		{
			base.Projectile.frame--;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame < 0)
		{
			base.Projectile.frame = Main.projFrames[base.Type] - 1;
		}
		if (base.Projectile.timeLeft < 30)
		{
			base.Projectile.Opacity = (float)base.Projectile.timeLeft / 30f;
		}
		else if (base.Projectile.Opacity < 1f)
		{
			base.Projectile.Opacity += 0.05f;
		}
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player player = enumerator.Current;
			if (!(player.Distance(base.Projectile.Center) <= 600f) || player.dead)
			{
				continue;
			}
			if (player.miscCounter % 30 == 15)
			{
				player.Calamity().StratusStarburst++;
				if (player.Calamity().StratusStarburst <= CalamityPlayer.MaxStratusStarburst)
				{
					player.Calamity().StarburstEntities.Add(new StarburstEntity(base.Projectile.Center));
				}
				player.Calamity().StratusStarburstResetTimer = (int)MathHelper.Max((float)player.Calamity().StratusStarburstResetTimer, 180f);
			}
			if (player.wingsLogic > 0 && player.wingTimeMax > 0)
			{
				if (player.wingTime <= 0f && player.Calamity().AvaliableStarburst > 0)
				{
					player.Calamity().StratusStarburst--;
					player.wingTime += 5f;
				}
				continue;
			}
			if (player.rocketBoots > 0)
			{
				if (player.rocketTime <= 0 && player.Calamity().AvaliableStarburst > 0)
				{
					player.Calamity().StratusStarburst--;
					player.rocketTime++;
				}
				continue;
			}
			int activeJumps = player.extraJumps.Where((ExtraJumpState x) => x.Enabled).Count();
			if (activeJumps > 0 && !player.AnyExtraJumpUsable() && player.Calamity().AvaliableStarburst >= 5 * activeJumps && !player.gravControl2)
			{
				player.Calamity().StratusStarburst -= 5 * activeJumps;
				player.RefreshDoubleJumps();
			}
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > (CalamityUtils.MinutesToFrames(10) - 45) * base.Projectile.MaxUpdates)
		{
			return false;
		}
		return targetHitbox.IntersectsConeFastInaccurate(base.Projectile.Center, 600f, 0f, (float)Math.PI * 2f);
	}

	public static Texture2D GetTransparentBloomTex()
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		if (_TransparentBloomTex == null)
		{
			_TransparentBloomTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
			Color[] BaseArray = (Color[])(object)new Color[_TransparentBloomTex.Width * _TransparentBloomTex.Height];
			Color[] ColorArray = (Color[])(object)new Color[_TransparentBloomTex.Width * _TransparentBloomTex.Height];
			_TransparentBloomTex.GetData<Color>(BaseArray);
			for (int i = 0; i < BaseArray.Length; i++)
			{
				ColorArray[i] = new Color((int)((Color)(ref BaseArray[i])).R, (int)((Color)(ref BaseArray[i])).R, (int)((Color)(ref BaseArray[i])).R, (int)((Color)(ref BaseArray[i])).R);
			}
			_TransparentBloomTex.SetData<Color>(ColorArray);
		}
		return _TransparentBloomTex;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		_ = base.Projectile.Center - Main.screenPosition;
		PixelationManager.AddPixelatedDrawer(delegate(Matrix matrix)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			DrawAura(this, matrix);
		}, GeneralDrawLayer.BeforeAllTiles);
		PixelationManager.AddPixelatedDrawer(delegate(Matrix matrix)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			DrawSingularity(this, matrix);
		}, GeneralDrawLayer.AfterProjectiles);
		PixelationManager.AddPixelatedDrawer(delegate(Matrix matrix)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			DrawAuraOutside(this, matrix);
		}, GeneralDrawLayer.AfterEverything);
		return false;
	}

	private static void DrawAuraOutside(StratusBlackHole mproj, Matrix matrix)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		Vector2 drawPosition = mproj.Projectile.Center - Main.screenPosition;
		Main.spriteBatch.EnterShaderRegion(null, null, matrix);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].UseOpacity(0.1f);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].UseSaturation(0.1f);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Neurons", (AssetRequestMode)2));
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].Apply();
		Texture2D telegraphBase = ModContent.Request<Texture2D>("CalamityMod/Particles/HigResThinCircle", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(telegraphBase, drawPosition, null, Color.SkyBlue * mproj.Projectile.Opacity * 0.75f, 0f, telegraphBase.Size() / 2f, 1200f * mproj.Projectile.Opacity / (float)telegraphBase.Width, (SpriteEffects)0);
		Main.spriteBatch.ExitShaderRegion(matrix);
	}

	private static void DrawAura(StratusBlackHole mproj, Matrix matrix)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		Vector2 drawPosition = mproj.Projectile.Center - Main.screenPosition;
		Texture2D telegraphBase = GetTransparentBloomTex();
		Main.EntitySpriteDraw(telegraphBase, drawPosition, null, Color.DarkSlateBlue * 0.75f * mproj.Projectile.Opacity, 0f, telegraphBase.Size() / 2f, 1500f * mproj.Projectile.Opacity / (float)telegraphBase.Width, (SpriteEffects)0);
		Main.spriteBatch.EnterShaderRegion(null, null, matrix);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].UseOpacity(1f);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].UseSaturation(0.2f);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/MeltyNoiseHighContrast", (AssetRequestMode)2));
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].Apply();
		telegraphBase = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(telegraphBase, drawPosition, null, Color.SkyBlue * 0.5f * mproj.Projectile.Opacity, 0f, telegraphBase.Size() / 2f, 1200f * mproj.Projectile.Opacity / (float)telegraphBase.Width, (SpriteEffects)0);
		Main.spriteBatch.EnterShaderRegion(null, null, matrix);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].UseOpacity(0.1f);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].UseSaturation(0.1f);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Neurons", (AssetRequestMode)2));
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].Apply();
		telegraphBase = ModContent.Request<Texture2D>("CalamityMod/Particles/HighResFoggyCircleHardEdge", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(telegraphBase, drawPosition, null, Color.SkyBlue * mproj.Projectile.Opacity * 0.75f, 0f, telegraphBase.Size() / 2f, 1200f * mproj.Projectile.Opacity / (float)telegraphBase.Width, (SpriteEffects)0);
		Main.spriteBatch.ExitShaderRegion(matrix);
	}

	private static void DrawSingularity(StratusBlackHole mproj, Matrix matrix)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		Vector2 drawPosition = mproj.Projectile.Center - Main.screenPosition;
		Texture2D telegraphBase = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/BasicCircle", (AssetRequestMode)2).Value;
		Main.spriteBatch.EnterShaderRegion(null, null, matrix);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].UseOpacity(0.5f);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].UseSaturation(0.2f);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/VoidGashes", (AssetRequestMode)2));
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].Apply();
		Main.EntitySpriteDraw(telegraphBase, drawPosition, null, Color.Lerp(Color.DarkSlateBlue, Color.SkyBlue, 0.75f), 0.5f, telegraphBase.Size() / 2f, 84f * mproj.Projectile.Opacity / (float)telegraphBase.Width, (SpriteEffects)0);
		Main.spriteBatch.EnterShaderRegion(null, null, matrix);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].UseOpacity(0.5f);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].UseSaturation(0.2f);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/VoidGashes", (AssetRequestMode)2));
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].Apply();
		telegraphBase = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/BasicCircle", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(telegraphBase, drawPosition, null, Color.Lerp(Color.DarkSlateBlue, Color.SkyBlue, 1f), 0f, telegraphBase.Size() / 2f, 84f * mproj.Projectile.Opacity / (float)telegraphBase.Width, (SpriteEffects)0);
		Main.spriteBatch.EnterShaderRegion(null, null, matrix);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].UseOpacity(0.25f);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].UseSaturation(0.1f);
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/VoidGashes", (AssetRequestMode)2));
		GameShaders.Misc["CalamityMod:OtherworldBarrierDistortion"].Apply();
		Main.EntitySpriteDraw(telegraphBase, drawPosition, null, Color.Black, 1f, telegraphBase.Size() / 2f, 72f * mproj.Projectile.Opacity / (float)telegraphBase.Width, (SpriteEffects)0);
		Main.spriteBatch.ExitShaderRegion(matrix);
		telegraphBase = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/BasicCircle", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(telegraphBase, drawPosition, null, Color.Black, 0f, telegraphBase.Size() / 2f, 64f * mproj.Projectile.Opacity / (float)telegraphBase.Width, (SpriteEffects)0);
	}
}
