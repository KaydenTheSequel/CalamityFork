using System;
using System.Collections.Generic;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss.BrainOfCthulhu;

public class CrimsonEye : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	private ref float Time => ref base.Projectile.ai[0];

	private ref float DistanceRatio => ref base.Projectile.ai[1];

	private ref float ExplosionTime => ref base.Projectile.ai[2];

	private static float TimeToExplode => 30f;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DontAttachHideToAlpha[base.Type] = true;
		Main.projFrames[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 100;
		base.Projectile.height = 36;
		base.Projectile.penetrate = -1;
		base.Projectile.Opacity = 1f;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 300;
		base.Projectile.scale = 1f;
		base.Projectile.hostile = false;
		base.Projectile.hide = true;
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 32; i++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 117);
		}
	}

	public override void AI()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		if (Time > 45f)
		{
			Player closest = Main.player[Player.FindClosest(base.Projectile.position, base.Projectile.width, base.Projectile.height)];
			DistanceRatio = 1f - MathHelper.Clamp((base.Projectile.Center.Distance(closest.Center) - 128f) / 120f, 0f, 1f);
			if (DistanceRatio >= 1f && base.Projectile.frame == 4)
			{
				if (base.Projectile.timeLeft < 120)
				{
					base.Projectile.timeLeft = 120;
				}
				if (ExplosionTime == TimeToExplode)
				{
					base.Projectile.hostile = true;
					GeneralParticleHandler.SpawnParticle(new DetailedExplosion(base.Projectile.Center, Vector2.Zero, Color.Orange, Vector2.One, Main.rand.NextFloatDirection(), 0.3f, 0.9f, 24));
					base.Projectile.Resize(256, 256);
				}
				else if (base.Projectile.width == 256)
				{
					base.Projectile.active = false;
				}
				ExplosionTime++;
			}
			else
			{
				if (ExplosionTime > 0f)
				{
					ExplosionTime--;
				}
				if (base.Projectile.width == 256)
				{
					base.Projectile.active = false;
				}
			}
			if (Time > 65f)
			{
				if (base.Projectile.timeLeft <= 20)
				{
					base.Projectile.frame = (base.Projectile.timeLeft - 5) / 5;
				}
				else
				{
					base.Projectile.frame = 4;
				}
			}
			else if (Time % 5f == 0f)
			{
				base.Projectile.frame++;
			}
		}
		Time++;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindNPCsAndTiles.Add(index);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		if (ExplosionTime >= TimeToExplode)
		{
			return false;
		}
		if (ExplosionTime > 0f)
		{
			float ratio = ExplosionTime / TimeToExplode;
			Vector3 lightLevels = ((Color)(ref lightColor)).ToVector3();
			float lightLevel = (lightLevels.X + lightLevels.Y + lightLevels.Z) / 3f;
			lightColor = Color.Lerp(lightColor, Color.Lerp(Color.Red, Color.Gold, (float)Math.Sin(ExplosionTime / 4f) / 2f + 0.5f) * lightLevel, ratio);
			((Color)(ref lightColor)).A = byte.MaxValue;
		}
		return true;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		if (ExplosionTime >= TimeToExplode)
		{
			return;
		}
		float ratio = CalamityUtils.SineOutEasing(ExplosionTime / TimeToExplode, 1);
		if (base.Projectile.frame == 4)
		{
			Texture2D tex = ModContent.Request<Texture2D>(Texture + "Pupil", (AssetRequestMode)2).Value;
			Vector2 rangeScale = default(Vector2);
			((Vector2)(ref rangeScale))._002Ector(2.25f, 0.75f);
			Vector2 dir = base.Projectile.DirectionTo(Main.LocalPlayer.Center);
			if (ExplosionTime > 0f)
			{
				dir = ((!(ratio < 0.25f)) ? Main.rand.NextVector2CircularEdge(1f, 1f) : Vector2.Lerp(dir, Vector2.Zero, ratio * 4f));
			}
			if (base.Projectile.timeLeft <= 50)
			{
				dir *= (float)(base.Projectile.timeLeft - 20) / 30f;
			}
			float range = 6f;
			if (ratio > 0.25f)
			{
				range = MathHelper.Lerp(0f, 6f, (ratio - 0.25f) * 0.75f);
			}
			else if (Time < 85f)
			{
				float lerp = CalamityUtils.SineOutEasing((Time - 65f) / 20f, 1);
				range = MathHelper.Lerp(0f, range, lerp);
			}
			Main.EntitySpriteDraw(tex, base.Projectile.Center + dir * rangeScale * range - Main.screenPosition, null, lightColor, 0f, tex.Size() * 0.5f, 1f, (SpriteEffects)0);
		}
		if (DistanceRatio > 0f)
		{
			float opacity = DistanceRatio;
			if (Time <= 65f)
			{
				opacity *= (Time - 45f) / 20f;
			}
			if (base.Projectile.timeLeft <= 20)
			{
				opacity *= (float)base.Projectile.timeLeft / 20f;
			}
			Main.spriteBatch.EnterShaderRegion();
			Texture2D telegraphBase = ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)2).Value;
			GameShaders.Misc["CalamityMod:CircularAoETelegraph"].UseOpacity(opacity);
			GameShaders.Misc["CalamityMod:CircularAoETelegraph"].UseColor(Color.Lerp(Color.Red, Color.OrangeRed, 0.7f * (float)Math.Pow(0.5 + 0.5 * Math.Sin(Main.GlobalTimeWrappedHourly), 3.0)));
			GameShaders.Misc["CalamityMod:CircularAoETelegraph"].UseSecondaryColor(Color.Lerp(Color.Yellow, Color.White, 0.5f));
			GameShaders.Misc["CalamityMod:CircularAoETelegraph"].UseSaturation(ratio * 0.5f + 0.5f);
			GameShaders.Misc["CalamityMod:CircularAoETelegraph"].Apply();
			Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
			Main.EntitySpriteDraw(telegraphBase, drawPosition, null, lightColor, 0f, telegraphBase.Size() / 2f, 248f, (SpriteEffects)0);
			Main.spriteBatch.ExitShaderRegion();
		}
	}
}
