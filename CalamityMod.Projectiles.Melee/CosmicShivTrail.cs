using System;
using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class CosmicShivTrail : ModProjectile, ILocalizedModType, IModType
{
	public NPC target;

	public const float MaxDistanceToTarget = 540f;

	public const float ExplosionDamageMultiplier = 2f;

	public const int FadeInTime = 12;

	public static List<Color> DustColors;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 15;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 220;
		base.Projectile.scale = 0.7f;
		base.Projectile.Opacity = 0f;
		base.Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		int QuarterSpriteWidth = 16;
		int QuarterSpriteHeight = 16;
		int QuarterProjWidth = base.Projectile.width / 3;
		int QuarterProjHeight = base.Projectile.height / 3;
		base.DrawOriginOffsetX = 0f;
		base.DrawOffsetX = -(QuarterSpriteWidth - QuarterProjWidth);
		base.DrawOriginOffsetY = -(QuarterSpriteHeight - QuarterProjHeight);
		Vector2 center = base.Projectile.Center;
		Color alpha = base.Projectile.GetAlpha(Color.White);
		Lighting.AddLight(center, ((Color)(ref alpha)).ToVector3());
		base.Projectile.rotation += Utils.GetLerpValue(-8f, 12f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
		if (base.Projectile.ai[0] > 2f)
		{
			base.Projectile.Opacity = Utils.GetLerpValue(2f, 12f, base.Projectile.ai[0], clamped: true);
			if (Main.rand.Next(0, 2) == 0)
			{
				float scale = Main.rand.NextFloat(1.6f, 2.1f);
				Color randomColor = DustColors[Main.rand.Next(0, DustColors.Count)];
				Dust.NewDustDirect(base.Projectile.position, 1, 1, 267, 0f, 0f, 255, randomColor * 0.6f, scale).noGravity = true;
			}
		}
		if (base.Projectile.ai[0] % 30f == 16f)
		{
			target = base.Projectile.Center.ClosestNPCAt(540f);
		}
		if (target != null)
		{
			Vector2 targetVelocity = (target.Center - base.Projectile.Center).SafeNormalize(Vector2.Zero);
			float multiplier = ((((Vector2)(ref base.Projectile.velocity)).Length() < 4f) ? (((Vector2)(ref base.Projectile.velocity)).Length() + 1f) : 1f);
			targetVelocity *= ((Vector2)(ref base.Projectile.velocity)).Length() + multiplier;
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, targetVelocity, 0.12f);
		}
		base.Projectile.ai[0]++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.position, Vector2.Zero, ModContent.ProjectileType<CosmicShivAura>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, target.whoAmI);
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 60);
	}

	private void CircularDamage(float radius)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		Player owner = Main.player[base.Projectile.owner];
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC target = enumerator.Current;
			if (!target.dontTakeDamage && !target.friendly)
			{
				float num = Vector2.Distance(base.Projectile.Center, target.Hitbox.TopLeft());
				float d2 = Vector2.Distance(base.Projectile.Center, target.Hitbox.TopRight());
				float d3 = Vector2.Distance(base.Projectile.Center, target.Hitbox.BottomLeft());
				float d4 = Vector2.Distance(base.Projectile.Center, target.Hitbox.BottomRight());
				if (MathHelper.Min(MathHelper.Min(MathHelper.Min(num, d2), d3), d4) <= radius)
				{
					int damage = (int)((float)base.Projectile.damage * 2f);
					Projectile.NewProjectileDirect(owner.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), damage, 0f, owner.whoAmI, target.whoAmI).DamageType = DamageClass.Melee;
				}
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = sourceRectangle.Size() / 2f;
		float rotation = base.Projectile.rotation;
		Color color = base.Projectile.GetAlpha(Color.White);
		float scaleMult = 1f;
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			Vector2 drawPos = base.Projectile.oldPos[i] + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
			color *= (float)(base.Projectile.oldPos.Length - i / 4) / (float)base.Projectile.oldPos.Length;
			Main.spriteBatch.Draw(texture, drawPos, (Rectangle?)sourceRectangle, color * 0.4f, rotation * -1f, origin, base.Projectile.scale * 1.8f * scaleMult, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(texture, drawPos, (Rectangle?)sourceRectangle, color, rotation, origin, base.Projectile.scale * scaleMult, (SpriteEffects)0, 0f);
			scaleMult *= 0.9f;
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		float rand2PI = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
		int rosePetalCount = Main.rand.Next(5, 8);
		int bigWavyPetalCount = Main.rand.Next(7, 11);
		int wavyPetalCount = Main.rand.Next(6, 10);
		float speed = Main.rand.Next(6, 11);
		switch (Main.rand.Next(0, 10))
		{
		case 0:
		{
			for (float k2 = 0f; k2 < (float)Math.PI * 2f; k2 += 0.03f)
			{
				float scale2 = Main.rand.NextFloat(1.1f, 1.4f);
				float randomWhitingValue2 = Main.rand.NextFloat(0f, 0.2f);
				Color color2 = Color.Lerp(DustColors[Main.rand.Next(0, DustColors.Count)], Color.White, randomWhitingValue2);
				Vector2 velocity2 = k2.ToRotationVector2() * (2f + (float)(Math.Sin(rand2PI + k2 * (float)rosePetalCount) + 1.0) * speed) * Main.rand.NextFloat(0.95f, 1.05f);
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 278, velocity2, 0, color2, scale2);
				dust2.noGravity = true;
				dust2.fadeIn = -1f;
				Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center, 278, velocity2 * 0.75f, 0, color2, scale2);
				dust3.noGravity = true;
				dust3.fadeIn = -1f;
			}
			break;
		}
		case 1:
		{
			for (float k3 = 0f; k3 < (float)Math.PI * 2f; k3 += 0.08f)
			{
				float scale3 = Main.rand.NextFloat(1.2f, 1.6f);
				float randomWhitingValue3 = Main.rand.NextFloat(0f, 0.2f);
				Color color3 = Color.Lerp(DustColors[Main.rand.Next(0, DustColors.Count)], Color.White, randomWhitingValue3);
				Vector2 velocity3 = k3.ToRotationVector2() * (float)(Math.Cos((double)(k3 * (float)bigWavyPetalCount) + (double)rand2PI) + 5.099999904632568) * speed / 4f;
				Dust dust4 = Dust.NewDustPerfect(base.Projectile.Center, 278, velocity3, 0, color3, scale3);
				dust4.noGravity = true;
				dust4.fadeIn = -1f;
				Dust dust5 = Dust.NewDustPerfect(base.Projectile.Center, 278, velocity3 * 0.75f, 0, color3, scale3);
				dust5.noGravity = true;
				dust5.fadeIn = -1f;
			}
			break;
		}
		default:
		{
			for (float k = 0f; k < (float)Math.PI * 2f; k += 0.2f)
			{
				float scale = Main.rand.NextFloat(1f, 1.2f);
				float randomWhitingValue = Main.rand.NextFloat(0.5f, 0.7f);
				Color color = Color.Lerp(DustColors[Main.rand.Next(0, DustColors.Count)], Color.White, randomWhitingValue);
				Vector2 velocity = k.ToRotationVector2() * (0.4f * (float)(Math.Sin((double)(k * (float)wavyPetalCount) + (double)rand2PI) + 2.0999999046325684) * (speed / 2f));
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 278, velocity, 0, color * 0.3f, scale);
				dust.noGravity = true;
				dust.fadeIn = -1f;
			}
			break;
		}
		}
		CircularDamage(80f);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		return new Color(238, 171, 255, 25) * base.Projectile.Opacity;
	}

	static CosmicShivTrail()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		DustColors = new List<Color>
		{
			new Color(69, 69, 222),
			new Color(99, 66, 212),
			new Color(130, 64, 214),
			new Color(154, 75, 219),
			new Color(165, 62, 201)
		};
	}
}
