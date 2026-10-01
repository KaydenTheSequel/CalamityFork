using System;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SilvaCrystalExplosion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/StarProj";

	public ref float OwnerCheck => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 14);
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 900;
	}

	public override void AI()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		Color colorToUse = Main.hslToRgb(base.Projectile.ai[0], 1f, 0.5f);
		if (OwnerCheck < 0f || OwnerCheck >= 1000f || (!Main.projectile[(int)OwnerCheck].active && Main.projectile[(int)OwnerCheck].type != ModContent.ProjectileType<SilvaCrystal>()))
		{
			base.Projectile.ai[1] = -1f;
		}
		else
		{
			DelegateMethods.v3_1 = ((Color)(ref colorToUse)).ToVector3() * 0.5f;
			Utils.PlotTileLine(base.Projectile.Center, Main.projectile[(int)OwnerCheck].Center, 8f, DelegateMethods.CastLight);
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = Main.rand.NextFloat(0.8f) + 0.8f;
			base.Projectile.direction = (Main.rand.NextBool() ? 1 : (-1));
		}
		base.Projectile.rotation = base.Projectile.localAI[1] / 20f * ((float)Math.PI * 2f) * (float)base.Projectile.direction;
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 16;
			if (base.Projectile.alpha < 0)
			{
				base.Projectile.alpha = 0;
			}
		}
		if (base.Projectile.alpha == 0)
		{
			Lighting.AddLight(base.Projectile.Center, ((Color)(ref colorToUse)).ToVector3() * 0.5f);
		}
		for (int i = 0; i < 2; i++)
		{
			if (Main.rand.NextBool(10))
			{
				Vector2 silvaDustVel = Vector2.UnitY.RotatedBy((float)i * (float)Math.PI + base.Projectile.rotation) * 2.5f;
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267, silvaDustVel, 225, colorToUse, base.Projectile.Opacity * base.Projectile.localAI[0]);
				dust.noGravity = true;
				dust.noLight = true;
			}
			if (Main.rand.NextBool(10))
			{
				Vector2 silvaDustVel2 = Vector2.UnitY.RotatedBy((float)i * (float)Math.PI) * 2.5f;
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 267, silvaDustVel2, 225, colorToUse, base.Projectile.Opacity * base.Projectile.localAI[0]);
				dust2.noGravity = true;
				dust2.noLight = true;
			}
		}
		if (Main.rand.NextBool(10))
		{
			Main.rand.NextFloat(1f, 3f);
			float fadeIn = Main.rand.NextFloat(1f, 2f);
			float dustScale = Main.rand.NextFloat(1f, 2f);
			Vector2 randVector = Main.rand.NextVector2Circular(1f, 1f).SafeNormalize(Vector2.Zero) * Main.rand.NextFloat(20f, 120f);
			Vector2 dustPos = base.Projectile.Center + randVector;
			Point dustCoords = dustPos.ToTileCoordinates();
			if (WorldGen.InWorld(dustCoords.X, dustCoords.Y) && !WorldGen.SolidTile(dustCoords.X, dustCoords.Y))
			{
				Dust dust3 = Dust.NewDustPerfect(dustPos, 267, -Vector2.UnitY * Main.rand.NextFloat(1.6f, 7.5f), 127, colorToUse, dustScale);
				dust3.noGravity = true;
				dust3.fadeIn = fadeIn;
				dust3.noLight = true;
				Dust dust4 = DustExtensions.BetterCloneDust(dust3);
				dust4.scale *= 0.65f;
				dust4.fadeIn *= 0.65f;
				dust4.color = new Color(255, 255, 255, 255);
			}
		}
		base.Projectile.scale = base.Projectile.Opacity / 2f * base.Projectile.localAI[0];
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] >= 15f)
		{
			base.Projectile.Kill();
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		Vector2 spinningpoint = -Vector2.UnitY.RotatedByRandom(3.1415927410125732) * 3f;
		int rando = Main.rand.Next(7, 13);
		Vector2 dustVel = new Vector2(2.1f, 2f) * Main.rand.NextFloat(0.8f, 1.2f);
		Color newColor = Main.hslToRgb(base.Projectile.ai[0], 1f, 0.5f);
		((Color)(ref newColor)).A = byte.MaxValue;
		for (int i = 0; i < rando; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267, spinningpoint.RotatedBy((float)Math.PI * 2f * (float)i / (float)rando) * dustVel, 0, newColor, 2f);
			dust.noGravity = true;
			dust.fadeIn = Main.rand.NextFloat() * 2f;
			Dust dust2 = DustExtensions.BetterCloneDust(dust);
			dust2.scale /= 2f;
			dust2.fadeIn /= 2f;
			dust2.color = Color.White;
		}
		for (int j = 0; j < rando; j++)
		{
			Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center, 267, spinningpoint.RotatedBy((float)Math.PI * 2f * (float)j / (float)rando) * dustVel * Main.rand.NextFloat(0.8f), 0, newColor, Main.rand.NextFloat());
			dust3.noGravity = true;
			dust3.fadeIn = Main.rand.NextFloat() * 2f;
			Dust dust4 = DustExtensions.BetterCloneDust(dust3);
			dust4.scale /= 2f;
			dust4.fadeIn /= 2f;
			dust4.color = Color.White;
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			base.Projectile.friendly = true;
			base.Projectile.usesIDStaticNPCImmunity = true;
			base.Projectile.idStaticNPCHitCooldown = 10;
			base.Projectile.ExpandHitboxBy(60);
			base.Projectile.Damage();
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 0);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Vector2 projPos = base.Projectile.Center + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition;
		Color projColor = Main.hslToRgb(base.Projectile.ai[0], 1f, 0.5f).MultiplyRGBA(new Color(255, 255, 255, 0));
		Vector2 origin = tex.Size() / 2f;
		SpriteEffects sp = (SpriteEffects)0;
		Main.EntitySpriteDraw(tex, projPos, null, projColor, base.Projectile.rotation, origin, base.Projectile.scale * 2f, sp);
		Main.EntitySpriteDraw(tex, projPos, null, projColor, 0f, origin, base.Projectile.scale * 2f, sp);
		if (OwnerCheck != -1f && base.Projectile.Opacity > 0.3f)
		{
			Color colorArea = Lighting.GetColor((int)((double)base.Projectile.position.X + (double)base.Projectile.width * 0.5) / 16, (int)(((double)base.Projectile.position.Y + (double)base.Projectile.height * 0.5) / 16.0));
			Color colorAlpha = base.Projectile.GetAlpha(colorArea);
			Vector2 projDirection = Main.projectile[(int)OwnerCheck].Center - base.Projectile.Center;
			Vector2 projDistance = default(Vector2);
			((Vector2)(ref projDistance))._002Ector(1f, ((Vector2)(ref projDirection)).Length() / (float)tex.Height);
			float drawRotation = projDirection.ToRotation() + (float)Math.PI / 2f;
			float colorClamp = MathHelper.Clamp(MathHelper.Distance(15f, base.Projectile.localAI[1]) / 20f, 0f, 1f);
			if (colorClamp > 0f)
			{
				Main.EntitySpriteDraw(tex, projPos + projDirection / 2f, null, projColor * colorClamp, drawRotation, origin, projDistance, sp);
				Main.EntitySpriteDraw(tex, projPos + projDirection / 2f, null, colorAlpha * colorClamp, drawRotation, origin, projDistance / 2f, sp);
			}
		}
		return false;
	}
}
