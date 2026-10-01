using System;
using CalamityMod.Effects;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class PhalanxSurgeLaser : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 7;
		base.Projectile.height = 7;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 7;
		base.Projectile.timeLeft = 600;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref ArsenalEffects.ArsenalLaserColor)).ToVector3() * 0.4f);
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (time > 15 && targetDist < 1400f)
		{
			if (base.Projectile.timeLeft % 3 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center, base.Projectile.velocity * 0.01f, affectedByGravity: false, 8, 1.7f * base.Projectile.ai[0], ArsenalEffects.ArsenalLaserColor));
			}
			if (base.Projectile.timeLeft % 2 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, base.Projectile.velocity * 0.01f, affectedByGravity: false, 3, 0.7f * base.Projectile.ai[0], Color.White));
			}
		}
		time++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 3; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ArsenalEffects.ArsenalLaserDust, (base.Projectile.velocity * 4f).RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(0.3f, 0.8f), 0, default(Color), Main.rand.NextFloat(0.7f, 1.3f));
			dust.noGravity = true;
			dust.color = ArsenalEffects.ArsenalLaserColor;
			dust.alpha = 100;
			dust.fadeIn = -3f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		if (time < 1)
		{
			return false;
		}
		Texture2D pointTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowBlade", (AssetRequestMode)2).Value;
		float fade = Utils.GetLerpValue(0f, 15f, base.Projectile.timeLeft, clamped: true);
		for (int i = 0; i < 4; i++)
		{
			Vector2 position = base.Projectile.Center - Main.screenPosition;
			Color val = ArsenalEffects.ArsenalLaserColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(pointTexture, position, null, val * fade * 0.4f, base.Projectile.rotation, pointTexture.Size() * 0.5f, new Vector2(0.7f - (float)i * 0.1f, 1f + (float)i * 0.15f) * 0.018f * base.Projectile.ai[0], (SpriteEffects)0);
			Vector2 position2 = base.Projectile.Center - Main.screenPosition;
			val = Color.White;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(pointTexture, position2, null, val * fade * 0.2f, base.Projectile.rotation, pointTexture.Size() * 0.5f, new Vector2(0.7f - (float)i * 0.1f, 1f + (float)i * 0.15f) * 0.013f * base.Projectile.ai[0], (SpriteEffects)0);
		}
		return false;
	}
}
