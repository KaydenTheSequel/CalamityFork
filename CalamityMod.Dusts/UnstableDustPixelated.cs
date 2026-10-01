using CalamityMod.Enums;
using CalamityMod.Systems.Graphic.PixelationSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Dusts;

public class UnstableDustPixelated : UnstableDust
{
	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override bool PreDraw(Dust dust)
	{
		PixelationManager.AddPixelatedDrawer(delegate
		{
			DrawPixelated(dust);
		}, GeneralDrawLayer.AfterDusts);
		return false;
	}

	private static void DrawPixelated(Dust dust)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		Vector2 squash = default(Vector2);
		((Vector2)(ref squash))._002Ector(0.4f, 1f);
		float texScaling = 6f;
		SpriteBatch spriteBatch = Main.spriteBatch;
		Texture2D value = UnstableDust.GlowStar.Value;
		Vector2 val = dust.position - Main.screenPosition;
		Color val2 = dust.color;
		((Color)(ref val2)).A = 0;
		spriteBatch.Draw(value, val, (Rectangle?)null, val2 * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, UnstableDust.GlowStar.Size() * 0.5f, squash * dust.scale * 0.1f * texScaling, (SpriteEffects)0, 0f);
		if (dust.alpha < 1)
		{
			SpriteBatch spriteBatch2 = Main.spriteBatch;
			Texture2D value2 = UnstableDust.GlowStar.Value;
			Vector2 val3 = dust.position - Main.screenPosition;
			val2 = dust.color;
			((Color)(ref val2)).A = 0;
			spriteBatch2.Draw(value2, val3, (Rectangle?)null, val2 * 0.85f * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, UnstableDust.GlowStar.Size() * 0.5f, squash * dust.scale * 0.04f * texScaling, (SpriteEffects)0, 0f);
		}
		if (!dust.noLight)
		{
			SpriteBatch spriteBatch3 = Main.spriteBatch;
			Texture2D value3 = UnstableDust.GlowStar.Value;
			Vector2 val4 = dust.position - Main.screenPosition;
			val2 = Color.Lerp(dust.color, Color.White, 0.3f);
			((Color)(ref val2)).A = 0;
			spriteBatch3.Draw(value3, val4, (Rectangle?)null, val2 * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, UnstableDust.GlowStar.Size() * 0.5f, squash * dust.scale * 0.025f * texScaling, (SpriteEffects)0, 0f);
		}
	}
}
