using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts;

public class VoidDust : ModDust
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
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		if (!dust.noLight)
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			Texture2D value = BloomCircle.Value;
			Vector2 val = dust.position - Main.screenPosition;
			Color color = dust.color;
			((Color)(ref color)).A = 0;
			spriteBatch.Draw(value, val, (Rectangle?)null, color * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, BloomCircle.Size() * 0.5f, dust.scale * 0.1f, (SpriteEffects)0, 0f);
			if (dust.alpha < 1)
			{
				SpriteBatch spriteBatch2 = Main.spriteBatch;
				Texture2D value2 = BloomCircle.Value;
				Vector2 val2 = dust.position - Main.screenPosition;
				color = dust.color;
				((Color)(ref color)).A = 0;
				spriteBatch2.Draw(value2, val2, (Rectangle?)null, color * 0.85f * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, BloomCircle.Size() * 0.5f, dust.scale * 0.04f, (SpriteEffects)0, 0f);
			}
		}
		Main.spriteBatch.Draw(SolidCircle.Value, dust.position - Main.screenPosition, (Rectangle?)null, Color.Black * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, SolidCircle.Size() * 0.5f, dust.scale * 0.075f, (SpriteEffects)0, 0f);
		return false;
	}
}
