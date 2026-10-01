using System;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class MeowCreature : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 36;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 24);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = 90 * base.Projectile.MaxUpdates;
		base.Projectile.DamageType = DamageClass.Ranged;
	}

	public override void AI()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: false, 400f, 15f, 20f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(Main.rand.NextBool() ? SoundID.Item58 : SoundID.Item57, base.Projectile.Center);
		for (int i = 0; i < 12; i++)
		{
			Vector2 smokeVel = Main.rand.NextVector2Circular(16f, 16f);
			Color smokeColor = Main.hslToRgb(Main.rand.NextFloat(), 1f, 0.8f);
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, smokeVel, smokeColor, Color.Black, Main.rand.NextFloat(0.6f, 1.6f), 220 - Main.rand.Next(60), 0.1f));
		}
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return (1f - completionRatio) * base.Projectile.scale * 9f;
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		return Main.hslToRgb(0.5f + 0.5f * completionRatio * MathF.Sin(Main.GlobalTimeWrappedHourly * 5f), 1f, 0.8f) * base.Projectile.Opacity;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}), 30);
		Texture2D glow = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(glow, base.Projectile.Center - Main.screenPosition, null, Color.White, base.Projectile.rotation, glow.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
	}
}
