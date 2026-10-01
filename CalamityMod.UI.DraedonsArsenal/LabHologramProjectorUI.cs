using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.TileEntities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;

namespace CalamityMod.UI.DraedonsArsenal;

public class LabHologramProjectorUI
{
	public const float MaxPlayerDistance = 120f;

	public const float TextPadding = 170f;

	public const float TextAreaWidth = 800f;

	public const float YOffsetPerLine = 30f;

	public static string ChooseDialogue()
	{
		List<string> dialogueOptions = new List<string>
		{
			"Text1", "Text2", "Text3", "Text4", "Text5", "Text6", "Text7", "Text8", "Text9", "Text10",
			"Text11"
		};
		if (NPC.downedAncientCultist)
		{
			dialogueOptions.Add("PostCultistText");
		}
		if (!Main.rand.NextBool(5000))
		{
			return Main.rand.Next(dialogueOptions.ToArray());
		}
		return "EasterEgg";
	}

	public static void Draw(SpriteBatch spriteBatch)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		Player p = Main.LocalPlayer;
		CalamityPlayer mp = p.Calamity();
		int projectorID = mp.CurrentlyViewedHologramID;
		if (projectorID == -1 || p.talkNPC > 0 || Main.npcShop > 0)
		{
			return;
		}
		if (TileEntity.ByID.TryGetValue(projectorID, out var te) && te is TELabHologramProjector cast)
		{
			TELabHologramProjector projector = cast;
			Vector2 projectorWorldCenter = projector.Center;
			if (p.DistanceSQ(projectorWorldCenter) > 14400f)
			{
				SoundEngine.PlaySound(in SoundID.MenuClose);
				mp.CurrentlyViewedHologramID = -1;
				mp.CurrentlyViewedHologramText = string.Empty;
				return;
			}
			if (string.IsNullOrEmpty(mp.CurrentlyViewedHologramText))
			{
				mp.CurrentlyViewedHologramText = CalamityUtils.GetText("UI.Hologram." + ChooseDialogue()).ToString();
			}
			Color backgroundColor = default(Color);
			((Color)(ref backgroundColor))._002Ector(200, 200, 200, 200);
			string[] dialogLines = Utils.WordwrapString(mp.CurrentlyViewedHologramText, FontAssets.MouseText.Value, 460, 10, out var lineCount);
			spriteBatch.Draw(TextureAssets.ChatBack.Value, new Vector2((float)(Main.screenWidth / 2 - TextureAssets.ChatBack.Value.Width / 2), 100f), (Rectangle?)new Rectangle(0, 0, TextureAssets.ChatBack.Value.Width, (lineCount + 2) * 30), backgroundColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			spriteBatch.Draw(TextureAssets.ChatBack.Value, new Vector2((float)(Main.screenWidth / 2 - TextureAssets.ChatBack.Value.Width / 2), (float)(100 + (lineCount + 2) * 30)), (Rectangle?)new Rectangle(0, TextureAssets.ChatBack.Value.Height - 30, TextureAssets.ChatBack.Value.Width, 30), backgroundColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			float x = 170 + (int)((float)Main.screenWidth - 800f) / 2;
			for (int i = 0; i < lineCount + 1; i++)
			{
				if (dialogLines[i] != null)
				{
					Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, dialogLines[i], x, 120f + (float)i * 30f, Color.Cyan, Color.Black, Vector2.Zero);
				}
			}
		}
		else
		{
			mp.CurrentlyViewedHologramID = -1;
			mp.CurrentlyViewedHologramText = string.Empty;
		}
	}
}
