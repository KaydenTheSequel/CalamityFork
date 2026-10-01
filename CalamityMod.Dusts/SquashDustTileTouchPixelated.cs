using CalamityMod.Enums;
using CalamityMod.Systems.Graphic.PixelationSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Dusts;

public class SquashDustTileTouchPixelated : SquashDustTileTouch
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
		Texture2D value = SquashDust.BloomCircle.Value;
		Vector2 val = dust.position - Main.screenPosition;
		Color val2 = dust.color;
		((Color)(ref val2)).A = 0;
		spriteBatch.Draw(value, val, (Rectangle?)null, val2 * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, SquashDust.BloomCircle.Size() * 0.5f, squash * dust.scale * 0.1f, (SpriteEffects)0, 0f);
		if (dust.alpha < 1)
		{
			SpriteBatch spriteBatch2 = Main.spriteBatch;
			Texture2D value2 = SquashDust.BloomCircle.Value;
			Vector2 val3 = dust.position - Main.screenPosition;
			val2 = dust.color;
			((Color)(ref val2)).A = 0;
			spriteBatch2.Draw(value2, val3, (Rectangle?)null, val2 * 0.85f * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, SquashDust.BloomCircle.Size() * 0.5f, squash * dust.scale * 0.04f, (SpriteEffects)0, 0f);
		}
		if (!dust.noLight)
		{
			SpriteBatch spriteBatch3 = Main.spriteBatch;
			Texture2D value3 = SquashDust.SolidCircle.Value;
			Vector2 val4 = dust.position - Main.screenPosition;
			val2 = Color.Lerp(dust.color, Color.White, 0.3f);
			((Color)(ref val2)).A = 0;
			spriteBatch3.Draw(value3, val4, (Rectangle?)null, val2 * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, SquashDust.SolidCircle.Size() * 0.5f, squash * dust.scale * 0.075f, (SpriteEffects)0, 0f);
		}
	}
}
