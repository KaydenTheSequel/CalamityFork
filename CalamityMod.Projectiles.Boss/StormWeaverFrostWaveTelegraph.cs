using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class StormWeaverFrostWaveTelegraph : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 4;
		base.Projectile.Opacity = 1f;
	}

	public override void AI()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref base.Projectile.velocity)).Length() < base.Projectile.ai[1])
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.01f;
			if (((Vector2)(ref base.Projectile.velocity)).Length() > base.Projectile.ai[1])
			{
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= base.Projectile.ai[1];
			}
		}
		if (base.Projectile.timeLeft < 60)
		{
			base.Projectile.Opacity = (float)base.Projectile.timeLeft / 60f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		Texture2D pulseTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/SmallGreyscaleCircle", (AssetRequestMode)2).Value;
		for (int i = 0; i < 5; i++)
		{
			Vector2 offset = ((float)i / 5f * ((float)Math.PI * 2f)).ToRotationVector2() * 24f;
			float time = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 1.8f);
			float angle = time * (float)Math.PI + Main.GlobalTimeWrappedHourly * 2.1f;
			float scale = 1.1f + time * 0.2f;
			Main.EntitySpriteDraw(pulseTexture, base.Projectile.Center + offset - Main.screenPosition, null, Color.LightCyan * 0.3f * base.Projectile.Opacity, angle, pulseTexture.Size() * 0.5f, scale, (SpriteEffects)0);
		}
		return false;
	}
}
