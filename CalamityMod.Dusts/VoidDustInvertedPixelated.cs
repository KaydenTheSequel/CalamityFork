using CalamityMod.Enums;
using CalamityMod.Systems.Graphic.PixelationSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Dusts;

public class VoidDustInvertedPixelated : VoidDustInverted
{
	public override string Texture => "CalamityMod/Dusts/VoidDustInverted";

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
		Main.spriteBatch.Draw(VoidDustInverted.SmallBloomCircle.Value, dust.position - Main.screenPosition, (Rectangle?)null, Color.Black * 0.4f * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, VoidDustInverted.SmallBloomCircle.Size() * 0.5f, dust.scale * 0.068f, (SpriteEffects)0, 0f);
		if (dust.alpha < 1)
		{
			Main.spriteBatch.Draw(VoidDustInverted.SmallBloomCircle.Value, dust.position - Main.screenPosition, (Rectangle?)null, Color.Black * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, VoidDustInverted.SmallBloomCircle.Size() * 0.5f, dust.scale * 0.057f, (SpriteEffects)0, 0f);
		}
		SpriteBatch spriteBatch = Main.spriteBatch;
		Texture2D value = VoidDustInverted.BloomCircle.Value;
		Vector2 val = dust.position - Main.screenPosition;
		Color color = dust.color;
		((Color)(ref color)).A = 0;
		spriteBatch.Draw(value, val, (Rectangle?)null, color * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, VoidDustInverted.BloomCircle.Size() * 0.5f, dust.scale * 0.07f, (SpriteEffects)0, 0f);
		if (!dust.noLight)
		{
			SpriteBatch spriteBatch2 = Main.spriteBatch;
			Texture2D value2 = VoidDustInverted.SolidCircle.Value;
			Vector2 val2 = dust.position - Main.screenPosition;
			color = dust.color;
			((Color)(ref color)).A = 0;
			spriteBatch2.Draw(value2, val2, (Rectangle?)null, color * 0.75f * Utils.GetLerpValue(255f, 0f, dust.alpha), dust.rotation, VoidDustInverted.SolidCircle.Size() * 0.5f, dust.scale * 0.075f, (SpriteEffects)0, 0f);
		}
	}
}
