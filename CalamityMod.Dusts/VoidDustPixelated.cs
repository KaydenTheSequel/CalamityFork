using CalamityMod.Enums;
using CalamityMod.Systems.Graphic.PixelationSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Dusts;

public class VoidDustPixelated : VoidDust
{
	public override string Texture => "CalamityMod/Dusts/VoidDust";

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
			Texture2D value = VoidDust.BloomCircle.Value;
			Vector2 val = dust.position - Main.screenPosition;
			Color color = dust.color;
			((Color)(ref color)).A = 0;
			spriteBatch.Draw(value, val, (Rectangle?)null, color * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, VoidDust.BloomCircle.Size() * 0.5f, dust.scale * 0.1f, (SpriteEffects)0, 0f);
			if (dust.alpha < 1)
			{
				SpriteBatch spriteBatch2 = Main.spriteBatch;
				Texture2D value2 = VoidDust.BloomCircle.Value;
				Vector2 val2 = dust.position - Main.screenPosition;
				color = dust.color;
				((Color)(ref color)).A = 0;
				spriteBatch2.Draw(value2, val2, (Rectangle?)null, color * 0.85f * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, VoidDust.BloomCircle.Size() * 0.5f, dust.scale * 0.04f, (SpriteEffects)0, 0f);
			}
		}
		Main.spriteBatch.Draw(VoidDust.SolidCircle.Value, dust.position - Main.screenPosition, (Rectangle?)null, Color.Black * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, VoidDust.SolidCircle.Size() * 0.5f, dust.scale * 0.075f, (SpriteEffects)0, 0f);
	}
}
