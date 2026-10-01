using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace CalamityMod.UI;

public static class DefenseDamageDisplayUI
{
	public static void Draw(SpriteBatch spriteBatch)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		int defenseDamage = Main.LocalPlayer.Calamity().CurrentDefenseDamage;
		if (defenseDamage > 0)
		{
			string defenseDamageText = (-defenseDamage).ToString();
			Texture2D defenseDamageIcon = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/DefenseDamage", (AssetRequestMode)2).Value;
			Vector2 defenseDamageIconCenter = new Vector2((float)Main.screenWidth - Main.UIScale * 328f, Main.UIScale * 16f) + defenseDamageIcon.Size() * 0.5f;
			Rectangle defenseDamageIconArea = Utils.CenteredRectangle(defenseDamageIconCenter, defenseDamageIcon.Size() * Main.UIScale);
			Vector2 defenseDamageTextArea = FontAssets.MouseText.Value.MeasureString(defenseDamageText);
			Vector2 defenseDamageTextDrawPosition = defenseDamageIconCenter + new Vector2(6f, 16f) - defenseDamageTextArea * 0.5f;
			Rectangle mouseArea = default(Rectangle);
			((Rectangle)(ref mouseArea))._002Ector(Main.mouseX, Main.mouseY, 2, 2);
			bool num = ((Rectangle)(ref mouseArea)).Intersects(defenseDamageIconArea);
			if (num)
			{
				defenseDamageIcon = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/DefenseDamageHover", (AssetRequestMode)2).Value;
			}
			spriteBatch.Draw(defenseDamageIcon, defenseDamageIconCenter, (Rectangle?)null, Color.White, 0f, defenseDamageIcon.Size() * 0.5f, Main.UIScale, (SpriteEffects)0, 0f);
			ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, defenseDamageText, defenseDamageTextDrawPosition, Color.IndianRed, 0f, Vector2.Zero, Vector2.One * Main.UIScale * 0.75f);
			if (num)
			{
				Main.hoverItemName = $"{Main.LocalPlayer.statDefense} {Language.GetTextValue("LegacyInterface.10")}\n{defenseDamage} {CalamityUtils.GetTextValue("UI.DefenseDamage")}";
			}
		}
	}
}
