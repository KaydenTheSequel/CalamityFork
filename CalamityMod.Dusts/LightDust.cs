using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts;

public class LightDust : ModDust
{
	public static Asset<Texture2D> SolidCircle { get; private set; }

	public static Asset<Texture2D> BloomCircle { get; private set; }

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
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		dust.rotation += MathF.Sign(dust.velocity.X);
		dust.velocity *= 0.98f;
		if (dust.noGravity)
		{
			dust.scale += 0.02f;
		}
		else
		{
			dust.scale -= 0.01f;
		}
		float light = MathHelper.Clamp(dust.scale * 0.8f, 0f, 1f);
		if (!dust.noLightEmittence)
		{
			Lighting.AddLight(dust.position, ((Color)(ref dust.color)).ToVector3() * light);
		}
		return true;
	}

	public override bool PreDraw(Dust dust)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		SpriteBatch spriteBatch = Main.spriteBatch;
		Texture2D value = BloomCircle.Value;
		Vector2 val = dust.position - Main.screenPosition;
		Color val2 = dust.color;
		((Color)(ref val2)).A = 0;
		spriteBatch.Draw(value, val, (Rectangle?)null, val2 * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, BloomCircle.Size() * 0.5f, dust.scale * 0.1f, (SpriteEffects)0, 0f);
		if (dust.alpha < 1)
		{
			SpriteBatch spriteBatch2 = Main.spriteBatch;
			Texture2D value2 = BloomCircle.Value;
			Vector2 val3 = dust.position - Main.screenPosition;
			val2 = dust.color;
			((Color)(ref val2)).A = 0;
			spriteBatch2.Draw(value2, val3, (Rectangle?)null, val2 * 0.85f * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, BloomCircle.Size() * 0.5f, dust.scale * 0.04f, (SpriteEffects)0, 0f);
		}
		if (!dust.noLight)
		{
			SpriteBatch spriteBatch3 = Main.spriteBatch;
			Texture2D value3 = SolidCircle.Value;
			Vector2 val4 = dust.position - Main.screenPosition;
			val2 = Color.Lerp(dust.color, Color.White, 0.3f);
			((Color)(ref val2)).A = 0;
			spriteBatch3.Draw(value3, val4, (Rectangle?)null, val2 * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, SolidCircle.Size() * 0.5f, dust.scale * 0.075f, (SpriteEffects)0, 0f);
		}
		return false;
	}
}
