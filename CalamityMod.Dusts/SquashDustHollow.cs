using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts;

public class SquashDustHollow : ModDust
{
	public static Asset<Texture2D> BloomRing { get; private set; }

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void Load()
	{
		if (!Main.dedServ)
		{
			BloomRing = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomRing", (AssetRequestMode)2);
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
		return false;
	}

	public override bool PreDraw(Dust dust)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		Vector2 squash = Vector2.Lerp(new Vector2(Utils.Remap(((Vector2)(ref dust.velocity)).Length(), 2f, 7f, 1f, 0.5f), Utils.Remap(((Vector2)(ref dust.velocity)).Length(), 2f, 7f, 1f, 2.5f)), Vector2.One, dust.fadeIn);
		Texture2D value = BloomRing.Value;
		Vector2 position = dust.position - Main.screenPosition;
		Color val = dust.color;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(value, position, null, val * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, BloomRing.Size() * 0.5f, squash * dust.scale * 0.1f, (SpriteEffects)0);
		if (dust.alpha < 1)
		{
			Texture2D value2 = BloomRing.Value;
			Vector2 position2 = dust.position - Main.screenPosition;
			val = Color.Lerp(dust.color, Color.White, 0.2f);
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value2, position2, null, val * 0.85f * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, BloomRing.Size() * 0.5f, squash * dust.scale * 0.09f, (SpriteEffects)0);
		}
		if (!dust.noLight)
		{
			for (int i = 0; i < 2; i++)
			{
				Texture2D value3 = BloomRing.Value;
				Vector2 position3 = dust.position - Main.screenPosition;
				val = Color.Lerp(dust.color, Color.White, 0.4f);
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(value3, position3, null, val * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, BloomRing.Size() * 0.5f, squash * dust.scale * 0.075f, (SpriteEffects)0);
			}
		}
		return false;
	}
}
