using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Graphics.Primitives;
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

public class ArtAttackStar : ModProjectile, ILocalizedModType, IModType
{
	public const int StarShapeCreationDelay = 12;

	public new string LocalizationCategory => "Projectiles.Magic";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 180;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 9000;
		base.Projectile.alpha = 255;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.ownedProjectileCounts[ModContent.ProjectileType<ArtAttackHoldout>()] <= 0 && Time >= 2f)
		{
			base.Projectile.Kill();
			return;
		}
		bool shapeIsComplete = false;
		int shapeEndPoint = -1;
		Vector2 val = base.Projectile.position - base.Projectile.oldPos[1];
		float distanceTraveled = ((Vector2)(ref val)).Length();
		List<Vector2> cleanOldPositions = base.Projectile.oldPos.Where(delegate(Vector2 p)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return p != Vector2.Zero;
		}).ToList();
		if (Time > 12f)
		{
			int start = 12;
			int end = cleanOldPositions.Count;
			float averageDistanceFromStar = 0f;
			for (int i = end - 1; i >= start; i--)
			{
				float distanceFromStar = Vector2.Distance(base.Projectile.position, base.Projectile.oldPos[i]);
				if (distanceFromStar < distanceTraveled * 0.7f + 30f)
				{
					shapeIsComplete = true;
					if (shapeEndPoint == -1)
					{
						shapeEndPoint = i;
					}
				}
				averageDistanceFromStar += distanceFromStar;
			}
			averageDistanceFromStar /= (float)(end - start);
			if (averageDistanceFromStar < distanceTraveled + 70f)
			{
				shapeIsComplete = false;
			}
		}
		EmitIdleDust();
		if (!Owner.channel | shapeIsComplete)
		{
			if (shapeIsComplete)
			{
				SoundEngine.PlaySound(in SoundID.DD2_DarkMageHealImpact, Owner.Center);
				DoShapeHitAreaChecks(cleanOldPositions, shapeEndPoint);
			}
			base.Projectile.Kill();
		}
		else
		{
			base.Projectile.Opacity = MathHelper.Clamp(base.Projectile.Opacity + 0.1f, 0f, 1f);
			if (Main.myPlayer == base.Projectile.owner)
			{
				DoMouseMovement();
			}
			Time++;
		}
	}

	public void DoMouseMovement()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = Owner.ClampedMouseWorld();
		base.Projectile.ForceNetUpdate();
	}

	public void EmitIdleDust()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 3; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(8f, 8f), 261);
			dust.velocity = Main.rand.NextVector2Circular(6f, 6f) - ((base.Projectile.position - base.Projectile.oldPos[1]) / 3f).RotatedByRandom(0.5099999904632568);
			dust.color = Main.hslToRgb(Main.rand.NextFloat(), 1f, Main.rand.NextFloat(0.5f, 0.9f));
			((Color)(ref dust.color)).A = 128;
			dust.scale = Main.rand.NextFloat(1.3f, 1.6f);
			dust.fadeIn = 0.4f;
			dust.noGravity = true;
		}
	}

	public void DoShapeHitAreaChecks(List<Vector2> cleanOldPositions, int shapeEndPoint)
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		float damageFactor = MathHelper.Lerp(1f, 18f, Utils.GetLerpValue(0f, 180f, Time, clamped: true));
		int damage = (int)((float)base.Projectile.damage * damageFactor);
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		Vector2 topLeft = default(Vector2);
		Vector2 bottomRight = default(Vector2);
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (!n.CanBeChasedBy())
			{
				continue;
			}
			bool enemyIsInShape = false;
			((Vector2)(ref topLeft))._002Ector(1000000f, 1000000f);
			((Vector2)(ref bottomRight))._002Ector(-1000000f, -1000000f);
			for (int j = 0; j < shapeEndPoint; j++)
			{
				topLeft.X = MathHelper.Min(topLeft.X, cleanOldPositions[j].X);
				topLeft.Y = MathHelper.Min(topLeft.Y, cleanOldPositions[j].Y);
				bottomRight.X = MathHelper.Max(bottomRight.X, cleanOldPositions[j].X);
				bottomRight.Y = MathHelper.Max(bottomRight.Y, cleanOldPositions[j].Y);
			}
			Vector2 center = (topLeft + bottomRight) * 0.5f;
			Vector2 area = new Vector2(Math.Abs(bottomRight.X - topLeft.X), Math.Abs(bottomRight.Y - topLeft.Y)) * 0.8f;
			Rectangle shapeRectangle = Utils.CenteredRectangle(center, area);
			for (int i = 0; i < cleanOldPositions.Count; i++)
			{
				Vector2 a = n.Center - Vector2.UnitX * 2000f;
				Vector2 right = n.Center + Vector2.UnitX * 2000f;
				bool inRangeOfStars = ((Rectangle)(ref shapeRectangle)).Intersects(n.Hitbox);
				if ((Collision.CheckLinevLine(a, right, cleanOldPositions[i], cleanOldPositions[(i + 1) % cleanOldPositions.Count]).Length != 0) & inRangeOfStars)
				{
					enemyIsInShape = true;
					break;
				}
			}
			if (enemyIsInShape)
			{
				SoundEngine.PlaySound(in SoundID.DD2_LightningBugZap, n.Center);
				CreateDustExplosionEffect(n.Center);
				if (Main.myPlayer == base.Projectile.owner)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), n.Center, Vector2.Zero, ModContent.ProjectileType<ArtAttackStrike>(), damage, 0f, base.Projectile.owner, n.whoAmI);
				}
			}
		}
	}

	public void CreateDustExplosionEffect(Vector2 dustSpawnPosition)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 60; i++)
		{
			Dust dust = Dust.NewDustPerfect(dustSpawnPosition + Main.rand.NextVector2Circular(12f, 12f), 267);
			dust.velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2f, 9.5f);
			dust.color = Main.hslToRgb(Main.rand.NextFloat(), 1f, Main.rand.NextFloat(0.5f, 0.9f));
			((Color)(ref dust.color)).A = 100;
			dust.scale = Main.rand.NextFloat(1f, 1.25f);
			dust.fadeIn = Main.rand.NextFloat(0.4f, 1f);
			dust.noGravity = true;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		CreateDustExplosionEffect(base.Projectile.Center);
	}

	public Color TrailColor(float completionRatio, Vector2 vertexPos)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		float hue = (Main.GlobalTimeWrappedHourly * -0.62f + completionRatio * 1.5f) % 1f;
		float brightness = MathHelper.SmoothStep(0.5f, 1f, Utils.GetLerpValue(0.3f, 0f, completionRatio, clamped: true));
		float opacity = Utils.GetLerpValue(1f, 0.8f, completionRatio, clamped: true) * base.Projectile.Opacity;
		Color color = Main.hslToRgb(hue, 1f, brightness) * opacity;
		((Color)(ref color)).A = (byte)(int)(Utils.GetLerpValue(0f, 0.2f, completionRatio) * 128f);
		return color;
	}

	public float TrailWidth(float completionRatio, Vector2 vertexPos)
	{
		float widthInterpolant = Utils.GetLerpValue(0f, 0.25f, completionRatio, clamped: true) * Utils.GetLerpValue(1.1f, 0.7f, completionRatio, clamped: true);
		return MathHelper.SmoothStep(8f, 20f, widthInterpolant);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY;
		Vector2 origin = value.Size() * 0.5f;
		Main.spriteBatch.EnterShaderRegion();
		GameShaders.Misc["CalamityMod:ArtAttack"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		GameShaders.Misc["CalamityMod:ArtAttack"].Apply();
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(TrailWidth, TrailColor, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ArtAttack"]), 180);
		Main.spriteBatch.ExitShaderRegion();
		Main.EntitySpriteDraw(value, drawPosition, null, base.Projectile.GetAlpha(Color.White), base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
