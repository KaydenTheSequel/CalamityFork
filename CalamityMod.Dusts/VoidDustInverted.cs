using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts;

public class VoidDustInverted : ModDust
{
	public static Asset<Texture2D> SolidCircle { get; private set; }

	public static Asset<Texture2D> BloomCircle { get; private set; }

	public static Asset<Texture2D> SmallBloomCircle { get; private set; }

	public override void Load()
	{
		if (!Main.dedServ)
		{
			SolidCircle = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/BasicCircle", (AssetRequestMode)2);
			BloomCircle = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
			SmallBloomCircle = ModContent.Request<Texture2D>("CalamityMod/Particles/SmallBloom", (AssetRequestMode)2);
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
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.Draw(SmallBloomCircle.Value, dust.position - Main.screenPosition, (Rectangle?)null, Color.Black * 0.4f * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, SmallBloomCircle.Size() * 0.5f, dust.scale * 0.068f, (SpriteEffects)0, 0f);
		if (dust.alpha < 1)
		{
			Main.spriteBatch.Draw(SmallBloomCircle.Value, dust.position - Main.screenPosition, (Rectangle?)null, Color.Black * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, SmallBloomCircle.Size() * 0.5f, dust.scale * 0.057f, (SpriteEffects)0, 0f);
		}
		SpriteBatch spriteBatch = Main.spriteBatch;
		Texture2D value = BloomCircle.Value;
		Vector2 val = dust.position - Main.screenPosition;
		Color color = dust.color;
		((Color)(ref color)).A = 0;
		spriteBatch.Draw(value, val, (Rectangle?)null, color * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, BloomCircle.Size() * 0.5f, dust.scale * 0.07f, (SpriteEffects)0, 0f);
		if (!dust.noLight)
		{
			SpriteBatch spriteBatch2 = Main.spriteBatch;
			Texture2D value2 = SolidCircle.Value;
			Vector2 val2 = dust.position - Main.screenPosition;
			color = dust.color;
			((Color)(ref color)).A = 0;
			spriteBatch2.Draw(value2, val2, (Rectangle?)null, color * 0.75f * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, SolidCircle.Size() * 0.5f, dust.scale * 0.075f, (SpriteEffects)0, 0f);
		}
		return false;
	}
}
