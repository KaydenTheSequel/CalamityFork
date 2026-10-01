using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Dusts;

public class SquashDustTileTouch : SquashDust
{
	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void OnSpawn(Dust dust)
	{
		dust.scale *= Main.rand.NextFloat(0.8f, 1f);
	}

	public override bool Update(Dust dust)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		bool touchedTiles = false;
		int size = 2;
		if (Collision.SolidCollision(dust.position, size, size))
		{
			touchedTiles = true;
			dust.velocity = Vector2.Zero;
		}
		float fadeSpeed = dust.fadeIn + 1f;
		dust.rotation = dust.velocity.ToRotation() + (float)Math.PI / 2f;
		dust.velocity *= 0.96f;
		if (dust.noGravity)
		{
			dust.scale -= 0.045f * (touchedTiles ? 0.4f : 1f) * fadeSpeed;
		}
		else
		{
			dust.scale -= 0.03f * (touchedTiles ? 0.4f : 1f);
			dust.velocity.Y += Main.rand.NextFloat(0.1f, 0.35f) * fadeSpeed;
		}
		float light = MathHelper.Clamp(dust.scale * 0.8f, 0f, 1f);
		if (!dust.noLightEmittence)
		{
			Lighting.AddLight(dust.position, ((Color)(ref dust.color)).ToVector3() * light);
		}
		if (dust.scale <= 0f)
		{
			dust.active = false;
		}
		if (!touchedTiles)
		{
			dust.position += dust.velocity;
		}
		return false;
	}
}
