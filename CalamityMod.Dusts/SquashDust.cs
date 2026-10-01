using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts;

public class SquashDust : ModDust
{
	public static Asset<Texture2D> SolidCircle { get; private set; }

	public static Asset<Texture2D> BloomCircle { get; private set; }

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void Load()
	{
		if (!Main.dedServ)
		{
			SolidCircle = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/BasicCircle", (AssetRequestMode)2);
			BloomCircle = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		}
	}

	public override void OnSpawn(Dust dust)
	{
		dust.scale *= Main.rand.NextFloat(0.8f, 1f);
	}

	public override bool Update(Dust dust)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		float fadeSpeed = dust.fadeIn + 1f;
		dust.rotation = dust.velocity.ToRotation() + (float)Math.PI / 2f;
		dust.velocity *= 0.96f;
		if (dust.noGravity)
		{
			dust.scale -= 0.045f * fadeSpeed;
		}
		else
		{
			dust.scale -= 0.03f * fadeSpeed;
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
		dust.position += dust.velocity;
		return false;
	}

	public override bool PreDraw(Dust dust)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 squash = default(Vector2);
		((Vector2)(ref squash))._002Ector(Utils.Remap(((Vector2)(ref dust.velocity)).Length(), 2f, 7f, 1f, 0.5f), Utils.Remap(((Vector2)(ref dust.velocity)).Length(), 2f, 7f, 1f, 2.5f));
		SpriteBatch spriteBatch = Main.spriteBatch;
		Texture2D value = BloomCircle.Value;
		Vector2 val = dust.position - Main.screenPosition;
		Color val2 = dust.color;
		((Color)(ref val2)).A = 0;
		spriteBatch.Draw(value, val, (Rectangle?)null, val2 * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, BloomCircle.Size() * 0.5f, squash * dust.scale * 0.1f, (SpriteEffects)0, 0f);
		if (dust.alpha < 1)
		{
			SpriteBatch spriteBatch2 = Main.spriteBatch;
			Texture2D value2 = BloomCircle.Value;
			Vector2 val3 = dust.position - Main.screenPosition;
			val2 = dust.color;
			((Color)(ref val2)).A = 0;
			spriteBatch2.Draw(value2, val3, (Rectangle?)null, val2 * 0.85f * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, BloomCircle.Size() * 0.5f, squash * dust.scale * 0.04f, (SpriteEffects)0, 0f);
		}
		if (!dust.noLight)
		{
			SpriteBatch spriteBatch3 = Main.spriteBatch;
			Texture2D value3 = SolidCircle.Value;
			Vector2 val4 = dust.position - Main.screenPosition;
			val2 = Color.Lerp(dust.color, Color.White, 0.3f);
			((Color)(ref val2)).A = 0;
			spriteBatch3.Draw(value3, val4, (Rectangle?)null, val2 * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, SolidCircle.Size() * 0.5f, squash * dust.scale * 0.075f, (SpriteEffects)0, 0f);
		}
		return false;
	}
}
