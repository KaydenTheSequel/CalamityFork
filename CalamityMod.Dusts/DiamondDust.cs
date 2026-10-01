using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts;

public class DiamondDust : ModDust
{
	public static Asset<Texture2D> GlowDiamond { get; private set; }

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void Load()
	{
		if (!Main.dedServ)
		{
			GlowDiamond = ModContent.Request<Texture2D>("CalamityMod/Particles/SquareRotated", (AssetRequestMode)2);
		}
	}

	public override void OnSpawn(Dust dust)
	{
		dust.scale *= Main.rand.NextFloat(0.8f, 1f);
	}

	public override bool Update(Dust dust)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		dust.rotation = dust.velocity.ToRotation() + (float)Math.PI / 2f;
		dust.velocity *= 0.96f;
		if (dust.noGravity)
		{
			dust.scale -= 0.045f;
		}
		else
		{
			dust.scale -= 0.03f;
			dust.velocity.Y += Main.rand.NextFloat(0.1f, 0.35f);
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
		dust.position += dust.velocity;
		dust.fadeIn++;
		return false;
	}

	public override bool PreDraw(Dust dust)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		float squashLerp = Utils.GetLerpValue(10f, 25f, dust.fadeIn, clamped: true);
		Vector2 squash = default(Vector2);
		((Vector2)(ref squash))._002Ector(MathHelper.Lerp(1f, 0.3f, squashLerp), MathHelper.Lerp(1f, 7f, squashLerp));
		SpriteBatch spriteBatch = Main.spriteBatch;
		Texture2D value = GlowDiamond.Value;
		Vector2 val = dust.position - Main.screenPosition;
		Color val2 = dust.color;
		((Color)(ref val2)).A = 0;
		spriteBatch.Draw(value, val, (Rectangle?)null, val2 * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, GlowDiamond.Size() * 0.5f, squash * dust.scale * 0.1f, (SpriteEffects)0, 0f);
		if (dust.alpha < 1)
		{
			SpriteBatch spriteBatch2 = Main.spriteBatch;
			Texture2D value2 = GlowDiamond.Value;
			Vector2 val3 = dust.position - Main.screenPosition;
			val2 = Color.Lerp(dust.color, Color.White, 0.2f);
			((Color)(ref val2)).A = 0;
			spriteBatch2.Draw(value2, val3, (Rectangle?)null, val2 * 0.85f * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, GlowDiamond.Size() * 0.5f, squash * dust.scale * 0.09f, (SpriteEffects)0, 0f);
		}
		if (!dust.noLight)
		{
			for (int i = 0; i < 2; i++)
			{
				SpriteBatch spriteBatch3 = Main.spriteBatch;
				Texture2D value3 = GlowDiamond.Value;
				Vector2 val4 = dust.position - Main.screenPosition;
				val2 = Color.Lerp(dust.color, Color.White, 0.4f);
				((Color)(ref val2)).A = 0;
				spriteBatch3.Draw(value3, val4, (Rectangle?)null, val2 * Utils.GetLerpValue(255f, 0f, dust.alpha) * ((i == 0) ? 1f : 0.7f), dust.rotation, GlowDiamond.Size() * 0.5f, squash * dust.scale * 0.08f * ((i == 0) ? 0.7f : 1f), (SpriteEffects)0, 0f);
			}
		}
		return false;
	}
}
