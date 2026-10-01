using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class PrismRay : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 StartingPosition;

	public const int Lifetime = 30;

	public new string LocalizationCategory => "Projectiles.Melee";

	public Color RayColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return CalamityUtils.MulticolorLerp(RayHue, CalamityUtils.ExoPalette);
		}
	}

	public Color HueDownscaledRayColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return RayColor * 0.66f;
		}
	}

	public ref float RayHue => ref base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.localAI[1];

	public override string Texture => "CalamityMod/Projectiles/StarProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 14);
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 900;
		base.Projectile.friendly = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
	}

	public override void AI()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		Color rayColor = RayColor;
		DelegateMethods.v3_1 = ((Color)(ref rayColor)).ToVector3() * 0.5f;
		Utils.PlotTileLine(StartingPosition, base.Projectile.Center, 8f, DelegateMethods.CastLight);
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.direction = Main.rand.NextBool().ToDirectionInt();
			base.Projectile.localAI[0] = 1f;
		}
		base.Projectile.rotation = Time / 20f * ((float)Math.PI * 2f) * (float)base.Projectile.direction;
		base.Projectile.alpha = Utils.Clamp(base.Projectile.alpha - 18, 0, 255);
		if (base.Projectile.alpha == 0)
		{
			Vector2 center = base.Projectile.Center;
			rayColor = RayColor;
			Lighting.AddLight(center, ((Color)(ref rayColor)).ToVector3() * 0.5f);
		}
		for (int i = 0; i < 2; i++)
		{
			if (Main.rand.NextBool(10))
			{
				Dust dust = Dust.NewDustDirect(base.Projectile.Center, 0, 0, 267, 0f, 0f, 225, RayColor, 1.5f);
				dust.noGravity = true;
				dust.noLight = true;
				dust.scale = base.Projectile.Opacity;
				dust.position = base.Projectile.Center;
				dust.velocity = Vector2.UnitY.RotatedBy(base.Projectile.rotation + (float)Math.PI * 2f * (float)i / 2f) * 2.5f;
			}
		}
		if (Main.rand.NextBool(10))
		{
			Vector2 dustSpawnPosition = base.Projectile.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(20f, 120f);
			Point dustTileCoords = dustSpawnPosition.ToTileCoordinates();
			bool canSpawnDust = true;
			if (!WorldGen.InWorld(dustTileCoords.X, dustTileCoords.Y))
			{
				canSpawnDust = false;
			}
			if (canSpawnDust && WorldGen.SolidTile(dustTileCoords.X, dustTileCoords.Y))
			{
				canSpawnDust = false;
			}
			if (canSpawnDust)
			{
				Dust dust2 = Dust.NewDustDirect(dustSpawnPosition, 0, 0, 267, 0f, 0f, 127, RayColor);
				dust2.noGravity = true;
				dust2.position = dustSpawnPosition;
				dust2.velocity = -Vector2.UnitY * Main.rand.NextFloat(1.6f, 7.5f);
				dust2.fadeIn = Main.rand.NextFloat(1f, 2f);
				dust2.scale = Main.rand.NextFloat(0.6f, 1.2f);
				dust2.noLight = true;
				Dust dust3 = DustExtensions.BetterCloneDust(dust2);
				dust3.scale *= 0.65f;
				dust3.fadeIn *= 0.65f;
				dust3.color = new Color(255, 255, 255, 255);
			}
		}
		base.Projectile.scale = base.Projectile.Opacity * 0.5f;
		base.Projectile.velocity = Vector2.Zero;
		Time++;
		if (Time >= 30f)
		{
			base.Projectile.Kill();
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		float _ = 0f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), StartingPosition, base.Projectile.Center, base.Projectile.scale * 22f, ref _);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		Vector2 baseDrawPosition = base.Projectile.Center + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition;
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() / 2f;
		Color fadedRayColor = base.Projectile.GetAlpha(lightColor);
		Color fullbrightRayColor = HueDownscaledRayColor.MultiplyRGBA(new Color(255, 255, 255, 0));
		for (int i = 0; i < 6; i++)
		{
			Vector2 drawPosition = baseDrawPosition + ((float)Math.PI * 2f * (float)i / 6f + Main.GlobalTimeWrappedHourly * 3f).ToRotationVector2() * base.Projectile.scale * 4f;
			Main.EntitySpriteDraw(texture, drawPosition, frame, fullbrightRayColor, base.Projectile.rotation, origin, base.Projectile.scale * 2f, (SpriteEffects)0);
			Main.EntitySpriteDraw(texture, drawPosition, frame, fullbrightRayColor, 0f, origin, base.Projectile.scale * 2f, (SpriteEffects)0);
		}
		if (base.Projectile.Opacity > 0.3f)
		{
			Vector2 drawOffset = (StartingPosition - base.Projectile.Center) * 0.5f;
			Vector2 scale = default(Vector2);
			((Vector2)(ref scale))._002Ector(1f, ((Vector2)(ref drawOffset)).Length() * 2f / (float)texture.Height);
			float rotation = drawOffset.ToRotation() + (float)Math.PI / 2f;
			float drawFade = MathHelper.Clamp(MathHelper.Distance(15f, Time) / 20.01f, 0f, 1f);
			for (int j = 0; j < 3; j++)
			{
				Vector2 drawPosition2 = baseDrawPosition;
				drawPosition2 += ((float)Math.PI * 2f * (float)j / 3f + Main.GlobalTimeWrappedHourly * 3f).ToRotationVector2() * base.Projectile.scale * 2.5f;
				drawPosition2 += drawOffset;
				Main.EntitySpriteDraw(texture, drawPosition2, frame, fullbrightRayColor * drawFade * 0.8f, rotation, origin, scale, (SpriteEffects)0);
				Main.EntitySpriteDraw(texture, drawPosition2, frame, fadedRayColor * drawFade * 0.8f, rotation, origin, scale * 0.5f, (SpriteEffects)0);
			}
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		CreateKillExplosionBurstDust(Main.rand.Next(7, 13));
		if (Main.myPlayer == base.Projectile.owner)
		{
			Vector2 oldSize = base.Projectile.Size;
			base.Projectile.position = base.Projectile.Center;
			base.Projectile.width = (base.Projectile.height = 60);
			base.Projectile.Center = base.Projectile.position;
			base.Projectile.usesLocalNPCImmunity = true;
			base.Projectile.localNPCHitCooldown = 10;
			base.Projectile.Damage();
			base.Projectile.position = base.Projectile.Center;
			base.Projectile.Size = oldSize;
			base.Projectile.Center = base.Projectile.position;
		}
	}

	public void CreateKillExplosionBurstDust(int dustCount)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			Vector2 baseExplosionDirection = -Vector2.UnitY.RotatedByRandom(3.1415927410125732) * 3f;
			Vector2 outwardFireSpeedFactor = default(Vector2);
			((Vector2)(ref outwardFireSpeedFactor))._002Ector(2.1f, 2f);
			Color brightenedRayColor = RayColor;
			((Color)(ref brightenedRayColor)).A = byte.MaxValue;
			for (float i = 0f; i < (float)dustCount; i++)
			{
				Dust dust = Dust.NewDustDirect(base.Projectile.Center, 0, 0, 267, 0f, 0f, 0, brightenedRayColor);
				dust.position = base.Projectile.Center;
				dust.velocity = baseExplosionDirection.RotatedBy((float)Math.PI * 2f * i / (float)dustCount) * outwardFireSpeedFactor * Main.rand.NextFloat(0.8f, 1.2f);
				dust.noGravity = true;
				dust.scale = 1.1f;
				dust.fadeIn = Main.rand.NextFloat(1.4f, 2.4f);
				Dust dust2 = DustExtensions.BetterCloneDust(dust);
				dust2.scale /= 2f;
				dust2.fadeIn /= 2f;
				dust2.color = new Color(255, 255, 255, 255);
			}
			for (float i2 = 0f; i2 < (float)dustCount; i2++)
			{
				Dust dust3 = Dust.NewDustDirect(base.Projectile.Center, 0, 0, 267, 0f, 0f, 0, brightenedRayColor);
				dust3.position = base.Projectile.Center;
				dust3.velocity = baseExplosionDirection.RotatedBy((float)Math.PI * 2f * i2 / (float)dustCount) * outwardFireSpeedFactor * Main.rand.NextFloat(0.8f, 1.2f);
				dust3.velocity *= Main.rand.NextFloat() * 0.8f;
				dust3.noGravity = true;
				dust3.scale = Main.rand.NextFloat();
				dust3.fadeIn = Main.rand.NextFloat(1.4f, 2.4f);
				Dust dust4 = DustExtensions.BetterCloneDust(dust3);
				dust4.scale /= 2f;
				dust4.fadeIn /= 2f;
				dust4.color = new Color(255, 255, 255, 255);
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		return new Color(base.Projectile.Opacity, base.Projectile.Opacity, base.Projectile.Opacity, 0f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}
}
