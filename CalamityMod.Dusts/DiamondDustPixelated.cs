using CalamityMod.Enums;
using CalamityMod.Systems.Graphic.PixelationSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Dusts;

public class DiamondDustPixelated : DiamondDust
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

	public static void DrawPixelated(Dust dust)
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
		Texture2D value = DiamondDust.GlowDiamond.Value;
		Vector2 val = dust.position - Main.screenPosition;
		Color val2 = dust.color;
		((Color)(ref val2)).A = 0;
		spriteBatch.Draw(value, val, (Rectangle?)null, val2 * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, DiamondDust.GlowDiamond.Size() * 0.5f, squash * dust.scale * 0.1f, (SpriteEffects)0, 0f);
		if (dust.alpha < 1)
		{
			SpriteBatch spriteBatch2 = Main.spriteBatch;
			Texture2D value2 = DiamondDust.GlowDiamond.Value;
			Vector2 val3 = dust.position - Main.screenPosition;
			val2 = Color.Lerp(dust.color, Color.White, 0.2f);
			((Color)(ref val2)).A = 0;
			spriteBatch2.Draw(value2, val3, (Rectangle?)null, val2 * 0.85f * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, DiamondDust.GlowDiamond.Size() * 0.5f, squash * dust.scale * 0.09f, (SpriteEffects)0, 0f);
		}
		if (!dust.noLight)
		{
			for (int i = 0; i < 2; i++)
			{
				SpriteBatch spriteBatch3 = Main.spriteBatch;
				Texture2D value3 = DiamondDust.GlowDiamond.Value;
				Vector2 val4 = dust.position - Main.screenPosition;
				val2 = Color.Lerp(dust.color, Color.White, 0.4f);
				((Color)(ref val2)).A = 0;
				spriteBatch3.Draw(value3, val4, (Rectangle?)null, val2 * Utils.GetLerpValue(255f, 0f, dust.alpha) * ((i == 0) ? 1f : 0.7f), dust.rotation, DiamondDust.GlowDiamond.Size() * 0.5f, squash * dust.scale * 0.08f * ((i == 0) ? 0.7f : 1f), (SpriteEffects)0, 0f);
			}
		}
	}
}
