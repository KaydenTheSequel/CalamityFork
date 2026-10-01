using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class WildfireBloomFlare : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/ExtraTextures/SmallGreyscaleCircle";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 18;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = 90 * base.Projectile.MaxUpdates;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 640f, 15f, 20f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 8; i++)
		{
			Vector2 smokeVel = Main.rand.NextVector2Circular(8f, 8f);
			Color smokeColor = (Main.rand.NextBool() ? Color.Lime : Color.Turquoise);
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, smokeVel, smokeColor, Color.Black, Main.rand.NextFloat(0.4f, 0.8f), 200 - Main.rand.Next(60), 0.1f));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		Texture2D lightTexture = TextureAssets.Projectile[base.Type].Value;
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			float colorInterpolation = MathF.Cos((float)base.Projectile.timeLeft / 16f + Main.GlobalTimeWrappedHourly / 20f + (float)i / (float)base.Projectile.oldPos.Length * (float)Math.PI) * 0.5f + 0.5f;
			Color val = Color.Lerp(Color.Lime, Color.Turquoise, colorInterpolation);
			Vector2 drawPosition = base.Projectile.oldPos[i] + lightTexture.Size() * 0.5f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY) + new Vector2(-15f, -15f);
			Color outerColor = val;
			Color innerColor = Color.Lerp(val, Color.Gold, 0.8f) * 0.5f;
			float intensity = 0.9f + 0.15f * MathF.Cos(Main.GlobalTimeWrappedHourly % 60f * ((float)Math.PI * 2f));
			intensity *= MathHelper.Lerp(0.15f, 1f, 1f - (float)i / (float)base.Projectile.oldPos.Length);
			Vector2 outerScale = new Vector2(1.25f) * intensity;
			Vector2 innerScale = new Vector2(1.25f) * intensity * 0.7f;
			outerColor *= intensity * base.Projectile.scale;
			innerColor *= intensity * base.Projectile.scale;
			Main.EntitySpriteDraw(lightTexture, drawPosition, null, outerColor, 0f, lightTexture.Size() * 0.5f, outerScale * 0.6f, (SpriteEffects)0);
			Main.EntitySpriteDraw(lightTexture, drawPosition, null, innerColor, 0f, lightTexture.Size() * 0.5f, innerScale * 0.6f, (SpriteEffects)0);
		}
		return false;
	}
}
