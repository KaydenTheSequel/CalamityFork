using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class PrismMine : ModProjectile, ILocalizedModType, IModType
{
	public List<Vector2> MinesToConnectTo = new List<Vector2>();

	public const float DamageFactorLowerBound = 0.425f;

	public const float MineConnectDistanceMax = 1200f;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.extraUpdates = 1;
		base.Projectile.localNPCHitCooldown = 7;
		base.Projectile.timeLeft = 280;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.96f;
		Vector2 center = base.Projectile.Center;
		Color cyan = Color.Cyan;
		Lighting.AddLight(center, ((Color)(ref cyan)).ToVector3());
		float idealScale = MathHelper.Lerp(0.93f, 1.07f, (float)Math.Sin((float)Math.PI * 2f * (float)base.Projectile.timeLeft / 14f) * 0.5f + 0.5f);
		base.Projectile.scale = MathHelper.Lerp(0.15f, idealScale, Utils.GetLerpValue(0f, 15f, Time, clamped: true) * Utils.GetLerpValue(0f, 15f, base.Projectile.timeLeft, clamped: true));
		Time++;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		foreach (Projectile mine in LocateOtherMines())
		{
			if (base.Projectile.timeLeft <= 12)
			{
				break;
			}
			float _ = 0f;
			if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, mine.Center, 20f, ref _))
			{
				return true;
			}
		}
		return base.Colliding(projHitbox, targetHitbox);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float damageFactor = 1f - 3f * (float)Math.Pow((float)base.Projectile.numHits / 14f, 2.0) + 2f * (float)Math.Pow((float)base.Projectile.numHits / 14f, 3.0);
		if (base.Projectile.numHits > 12 || damageFactor < 0.425f)
		{
			damageFactor = 0.425f;
		}
		modifiers.SourceDamage *= damageFactor;
	}

	public List<Projectile> LocateOtherMines()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		List<Projectile> mines = new List<Projectile>();
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile proj = enumerator.Current;
			if (proj.type == base.Projectile.type && proj.timeLeft > 12 && proj.whoAmI != base.Projectile.whoAmI && base.Projectile.WithinRange(proj.Center, 1200f))
			{
				mines.Add(proj);
			}
		}
		return mines;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		Texture2D baseTexture = TextureAssets.Projectile[base.Type].Value;
		Texture2D glowTexture = ModContent.Request<Texture2D>(Texture + "Glowmask", (AssetRequestMode)2).Value;
		Texture2D laserTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/PrismMineArc", (AssetRequestMode)2).Value;
		Vector2 origin = baseTexture.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		foreach (Projectile mine in LocateOtherMines())
		{
			float fade = Utils.GetLerpValue(8f, 24f, base.Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(8f, 24f, mine.timeLeft, clamped: true);
			drawLineTo(mine.Center, Color.White * fade);
		}
		Main.EntitySpriteDraw(baseTexture, drawPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		Rectangle glowFrame = glowTexture.Frame(1, 6, 0, (int)Time / 4 % 6);
		Color glowColor = Color.White * 0.5f;
		((Color)(ref glowColor)).A = 0;
		Main.EntitySpriteDraw(glowTexture, drawPosition, glowFrame, glowColor, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
		void drawLineTo(Vector2 destination, Color laserColor)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0083: Unknown result type (might be due to invalid IL or missing references)
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_009d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			float rotation = base.Projectile.AngleTo(destination);
			float remainingDistance = base.Projectile.Distance(destination) - 30f;
			float laserScale = 0.5f;
			Rectangle frame = laserTexture.Frame(1, 7, 0, (int)(Main.GlobalTimeWrappedHourly * 10f + (float)base.Projectile.identity * 3f) % 7);
			Vector2 laserOrigin = frame.Size() * 0.5f;
			while (remainingDistance > (float)frame.Height * laserScale)
			{
				Vector2 laserDrawPosition = base.Projectile.Center + base.Projectile.SafeDirectionTo(destination, -Vector2.UnitY) * remainingDistance - Main.screenPosition;
				Main.EntitySpriteDraw(laserTexture, laserDrawPosition, frame, laserColor, rotation, laserOrigin, laserScale, (SpriteEffects)0);
				remainingDistance -= (float)frame.Height * laserScale;
			}
		}
	}
}
