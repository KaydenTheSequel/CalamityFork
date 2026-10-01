using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace CalamityMod.UI.CalamitasEnchants;

public class CalamitasEnchantUI
{
	public static int NPCIndex = -1;

	public static int EnchantIndex = 0;

	public static Enchantment? SelectedEnchantment = null;

	public static Item CurrentlyHeldItem = new Item();

	public static float TopButtonClickCountdown = 0f;

	public static float BottomButtonClickCountdown = 0f;

	public static float ReforgeButtonClickCountdown = 0f;

	public static bool CurrentlyViewing = false;

	public static readonly float ResolutionRatio = (float)Main.screenHeight / 1440f;

	public static readonly SoundStyle EnchSound = new SoundStyle("CalamityMod/Sounds/Custom/WeaponEnchant");

	public static readonly SoundStyle EXSound = new SoundStyle("CalamityMod/Sounds/Custom/WeaponExhume");

	public static Vector2 ReforgeUITopLeft
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(68f, 320f) * Main.UIScale;
		}
	}

	public static Rectangle MouseScreenArea
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			return Utils.CenteredRectangle(Main.MouseScreen, Vector2.One * 2f);
		}
	}

	public static bool InRangeOfNPC()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.npc.IndexInRange(NPCIndex) || !Main.npc[NPCIndex].active)
		{
			return false;
		}
		Rectangle validTalkArea = Utils.CenteredRectangle(Main.LocalPlayer.Center, new Vector2((float)Player.tileRangeX * 3f, (float)Player.tileRangeY * 2f) * 16f);
		return ((Rectangle)(ref validTalkArea)).Intersects(Main.npc[NPCIndex].Hitbox);
	}

	public static void Draw(SpriteBatch spriteBatch)
	{
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		if (TopButtonClickCountdown > 0f)
		{
			TopButtonClickCountdown--;
		}
		if (BottomButtonClickCountdown > 0f)
		{
			BottomButtonClickCountdown--;
		}
		if (ReforgeButtonClickCountdown > 0f)
		{
			ReforgeButtonClickCountdown--;
		}
		if (!CurrentlyViewing)
		{
			if (!CurrentlyHeldItem.IsAir)
			{
				Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_Misc(CurrentlyHeldItem.Name), CurrentlyHeldItem, CurrentlyHeldItem.stack);
				CurrentlyHeldItem.TurnToAir();
			}
			EnchantIndex = 0;
			NPCIndex = -1;
			return;
		}
		if (Main.LocalPlayer.chest != -1 || Main.LocalPlayer.sign != -1 || Main.LocalPlayer.talkNPC == -1 || !Main.playerInventory || !InRangeOfNPC() || Main.InGuideCraftMenu)
		{
			CurrentlyViewing = false;
			Main.LocalPlayer.dropItemCheck();
			Recipe.FindRecipes();
			return;
		}
		Main.playerInventory = true;
		Main.npcChatText = string.Empty;
		Texture2D backgroundTexture = ModContent.Request<Texture2D>("CalamityMod/UI/CalamitasEnchantments/CalamitasCurseBackground", (AssetRequestMode)2).Value;
		Vector2 backgroundScale = Vector2.One * Main.UIScale;
		spriteBatch.Draw(backgroundTexture, ReforgeUITopLeft, (Rectangle?)null, Color.White, 0f, Vector2.Zero, backgroundScale, (SpriteEffects)0, 0f);
		DisableMouseWhenOverUI(backgroundTexture, backgroundScale);
		IEnumerable<Enchantment> possibleEnchantments = SelectEnchantment();
		int cost = 0;
		if (SelectedEnchantment.HasValue)
		{
			Point costDrawPositionTopLeft = (ReforgeUITopLeft + new Vector2(50f, 78f) * backgroundScale).ToPoint();
			cost = DrawEnchantmentCost(spriteBatch, costDrawPositionTopLeft);
			Point descriptionDrawPositionTopLeft = costDrawPositionTopLeft;
			descriptionDrawPositionTopLeft.Y += (int)(Main.UIScale * 70f);
			Vector2 iconDrawPositionTopLeft = costDrawPositionTopLeft.ToVector2() + new Vector2(270f, -24f) * Main.UIScale;
			DrawEnchantmentDescription(spriteBatch, descriptionDrawPositionTopLeft);
			if (!string.IsNullOrEmpty(SelectedEnchantment.Value.IconTexturePath))
			{
				Texture2D iconTexture = ModContent.Request<Texture2D>(SelectedEnchantment.Value.IconTexturePath, (AssetRequestMode)2).Value;
				DrawIcon(spriteBatch, iconDrawPositionTopLeft, iconTexture);
			}
		}
		Vector2 itemSlotDrawPosition = ReforgeUITopLeft + new Vector2(30f, 50f) * backgroundScale;
		Vector2 reforgeIconDrawPosition = ReforgeUITopLeft + new Vector2(84f, 60f) * backgroundScale;
		DrawItemIcon(spriteBatch, itemSlotDrawPosition, reforgeIconDrawPosition, backgroundScale, out var isHoveringOverItemIcon, out var isHoveringOverReforgeIcon);
		if (isHoveringOverItemIcon)
		{
			InteractWithItemSlot();
		}
		DrawAndInteractWithButtons(spriteBatch, possibleEnchantments, ReforgeUITopLeft + new Vector2(210f, 50f) * backgroundScale, ReforgeUITopLeft + new Vector2(210f, 90f) * backgroundScale, backgroundScale);
		if (SelectedEnchantment.HasValue)
		{
			DrawEnchantmentName(spriteBatch, ReforgeUITopLeft + new Vector2(216f, 66f) * backgroundScale);
		}
		if (isHoveringOverReforgeIcon && Main.mouseLeft && Main.mouseLeftRelease)
		{
			InteractWithEnchantIcon(cost);
			ReforgeButtonClickCountdown = 15f;
		}
	}

	public static void DisableMouseWhenOverUI(Texture2D backgroundTexture, Vector2 backgroundScale)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		Rectangle backgroundArea = default(Rectangle);
		((Rectangle)(ref backgroundArea))._002Ector((int)ReforgeUITopLeft.X, (int)ReforgeUITopLeft.Y, (int)((float)backgroundTexture.Width * backgroundScale.X), (int)((float)backgroundTexture.Width * backgroundScale.Y));
		Rectangle mouseScreenArea = MouseScreenArea;
		if (((Rectangle)(ref mouseScreenArea)).Intersects(backgroundArea))
		{
			Main.LocalPlayer.mouseInterface = false;
			Main.blockMouse = true;
		}
	}

	public static IEnumerable<Enchantment> SelectEnchantment()
	{
		IEnumerable<Enchantment> possibleEnchantments = EnchantmentManager.GetValidEnchantmentsForItem(CurrentlyHeldItem);
		SelectedEnchantment = null;
		if (possibleEnchantments.Any())
		{
			SelectedEnchantment = possibleEnchantments.ElementAt(EnchantIndex);
		}
		return possibleEnchantments;
	}

	public static int DrawEnchantmentCost(SpriteBatch spriteBatch, Point costDrawPositionTopLeft)
	{
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		if (CurrentlyHeldItem.IsAir)
		{
			return 0;
		}
		int cost = CurrentlyHeldItem.value * 2;
		if (SelectedEnchantment.HasValue && SelectedEnchantment.Value.Name == CalamityUtils.GetText("UI.Exhumed.DisplayName"))
		{
			cost = (int)MathHelper.Min((float)cost, (float)Item.buyPrice(5)) * 2;
			cost *= CurrentlyHeldItem.stack;
		}
		cost = (int)((double)cost * Main.LocalPlayer.currentShoppingSettings.PriceAdjustment);
		if (Main.LocalPlayer.discountAvailable)
		{
			cost = (int)((double)cost * 0.8);
		}
		string costText = CalamityUtils.GetTextValue("UI.Cost");
		Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, costText, costDrawPositionTopLeft.X, (float)costDrawPositionTopLeft.Y + 45f * Main.UIScale, Color.White * ((float)(int)Main.mouseTextColor / 255f), Color.Black, Vector2.Zero, Main.UIScale);
		costDrawPositionTopLeft.X += (int)((FontAssets.MouseText.Value.MeasureString(costText).X * 0.5f + 12f) * Main.UIScale);
		int[] coinsArray = Utils.CoinsSplit(cost);
		float y = (float)costDrawPositionTopLeft.Y + 54f * Main.UIScale;
		Vector2 drawPosition = default(Vector2);
		for (int i = 0; i < 4; i++)
		{
			((Vector2)(ref drawPosition))._002Ector((float)costDrawPositionTopLeft.X + (ChatManager.GetStringSize(FontAssets.MouseText.Value, costText, Vector2.One).X + ((float)(24 * i) - 24f)) * Main.UIScale, y);
			spriteBatch.Draw(TextureAssets.Item[74 - i].Value, drawPosition, (Rectangle?)null, Color.White, 0f, TextureAssets.Item[74 - i].Size() * 0.5f, Main.UIScale, (SpriteEffects)0, 0f);
			Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.ItemStack.Value, coinsArray[3 - i].ToString(), drawPosition.X - 11f, drawPosition.Y, Color.White, Color.Black, new Vector2(0.3f), 0.75f * Main.UIScale);
		}
		return cost;
	}

	public static void DrawEnchantmentDescription(SpriteBatch spriteBatch, Point descriptionDrawPositionTopLeft)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		Vector2 vectorDrawPosition = descriptionDrawPositionTopLeft.ToVector2();
		Vector2 scale = new Vector2(0.8f, 0.825f) * MathHelper.Clamp(ResolutionRatio, 0.825f, 1f) * Main.UIScale;
		string[] array = Utils.WordwrapString(SelectedEnchantment.Value.Description.ToString().Replace("\n", " "), FontAssets.MouseText.Value, 400, 16, out var _);
		foreach (string line in array)
		{
			if (!string.IsNullOrEmpty(line))
			{
				ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, line, vectorDrawPosition, Color.Orange, 0f, Vector2.Zero, scale);
				vectorDrawPosition.Y += Main.UIScale * 16f;
			}
		}
	}

	public static void DrawIcon(SpriteBatch spriteBatch, Vector2 drawPositionTopLeft, Texture2D texture)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		spriteBatch.Draw(texture, drawPositionTopLeft, (Rectangle?)null, Color.White, 0f, Vector2.Zero, Main.UIScale, (SpriteEffects)0, 0f);
	}

	public static void DrawItemIcon(SpriteBatch spriteBatch, Vector2 itemSlotDrawPosition, Vector2 reforgeIconDrawPosition, Vector2 scale, out bool isHoveringOverItemIcon, out bool isHoveringOverReforgeIcon)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		isHoveringOverReforgeIcon = false;
		Texture2D itemSlotTexture = ModContent.Request<Texture2D>("CalamityMod/UI/CalamitasEnchantments/CalamitasCurseItemSlot", (AssetRequestMode)2).Value;
		Texture2D reforgeIconTexture = ModContent.Request<Texture2D>("CalamityMod/UI/CalamitasEnchantments/CalamitasCurseUI_Button", (AssetRequestMode)2).Value;
		Rectangle reforgeIconArea = default(Rectangle);
		((Rectangle)(ref reforgeIconArea))._002Ector((int)reforgeIconDrawPosition.X, (int)reforgeIconDrawPosition.Y, (int)((float)reforgeIconTexture.Width * scale.X), (int)((float)reforgeIconTexture.Height * scale.Y));
		Rectangle mouseScreenArea = MouseScreenArea;
		if (((Rectangle)(ref mouseScreenArea)).Intersects(reforgeIconArea))
		{
			reforgeIconTexture = ModContent.Request<Texture2D>("CalamityMod/UI/CalamitasEnchantments/CalamitasCurseUI_ButtonHovered", (AssetRequestMode)2).Value;
			isHoveringOverReforgeIcon = true;
		}
		if (ReforgeButtonClickCountdown > 0f)
		{
			reforgeIconTexture = ModContent.Request<Texture2D>("CalamityMod/UI/CalamitasEnchantments/CalamitasCurseUI_ButtonClicked", (AssetRequestMode)2).Value;
		}
		mouseScreenArea = MouseScreenArea;
		isHoveringOverItemIcon = ((Rectangle)(ref mouseScreenArea)).Intersects(new Rectangle((int)itemSlotDrawPosition.X, (int)itemSlotDrawPosition.Y, (int)((float)itemSlotTexture.Width * scale.X), (int)((float)itemSlotTexture.Height * scale.Y)));
		spriteBatch.Draw(itemSlotTexture, itemSlotDrawPosition, (Rectangle?)null, Color.White, 0f, Vector2.Zero, scale, (SpriteEffects)0, 0f);
		if (!CurrentlyHeldItem.IsAir)
		{
			AttemptToDrawItemInIcon(spriteBatch, itemSlotDrawPosition);
		}
		spriteBatch.Draw(reforgeIconTexture, reforgeIconDrawPosition, (Rectangle?)null, Color.White, 0f, Vector2.Zero, scale, (SpriteEffects)0, 0f);
	}

	public static void AttemptToDrawItemInIcon(SpriteBatch spriteBatch, Vector2 drawPosition)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		float inventoryScale = Main.inventoryScale;
		Texture2D itemTexture = TextureAssets.Item[CurrentlyHeldItem.type].Value;
		Rectangle itemFrame = itemTexture.Frame();
		if (Main.itemAnimations[CurrentlyHeldItem.type] != null)
		{
			itemFrame = Main.itemAnimations[CurrentlyHeldItem.type].GetFrame(itemTexture);
		}
		float baseScale = Main.UIScale;
		Color _ = Color.White;
		ItemSlot.GetItemLight(ref _, ref baseScale, CurrentlyHeldItem);
		float itemScale = 1f;
		if (itemFrame.Width > 36 || itemFrame.Height > 36)
		{
			itemScale = 36f / MathHelper.Max((float)itemFrame.Width, (float)itemFrame.Height);
		}
		itemScale *= inventoryScale * baseScale;
		drawPosition += Vector2.One * 24f * baseScale;
		spriteBatch.Draw(itemTexture, drawPosition, (Rectangle?)itemFrame, CurrentlyHeldItem.GetAlpha(Color.White), 0f, itemFrame.Size() * 0.5f, itemScale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(itemTexture, drawPosition, (Rectangle?)itemFrame, CurrentlyHeldItem.GetColor(Color.White), 0f, itemFrame.Size() * 0.5f, itemScale, (SpriteEffects)0, 0f);
	}

	public static void InteractWithItemSlot()
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (!CurrentlyHeldItem.IsAir)
		{
			Main.HoverItem = CurrentlyHeldItem.Clone();
			Main.instance.MouseTextHackZoom(string.Empty);
		}
		if (Main.mouseLeftRelease && Main.mouseLeft && (Main.mouseItem.CanBeEnchantedBySomething() || Main.mouseItem.IsAir))
		{
			EnchantIndex = 0;
			Utils.Swap(ref Main.mouseItem, ref CurrentlyHeldItem);
			SoundEngine.PlaySound(in SoundID.Grab);
		}
	}

	public static void DrawAndInteractWithButtons(SpriteBatch spriteBatch, IEnumerable<Enchantment> possibleEnchantments, Vector2 topButtonTopLeft, Vector2 bottomButtonTopLeft, Vector2 scale)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		if (!possibleEnchantments.Any())
		{
			return;
		}
		Texture2D topArrowTexture = ModContent.Request<Texture2D>("CalamityMod/UI/CalamitasEnchantments/CalamitasCurseUI_ArrowUp", (AssetRequestMode)2).Value;
		Texture2D bottomArrowTexture = ModContent.Request<Texture2D>("CalamityMod/UI/CalamitasEnchantments/CalamitasCurseUI_ArrowDown", (AssetRequestMode)2).Value;
		if (TopButtonClickCountdown > 0f)
		{
			topArrowTexture = ModContent.Request<Texture2D>("CalamityMod/UI/CalamitasEnchantments/CalamitasCurseUI_ArrowUpClicked", (AssetRequestMode)2).Value;
		}
		if (BottomButtonClickCountdown > 0f)
		{
			bottomArrowTexture = ModContent.Request<Texture2D>("CalamityMod/UI/CalamitasEnchantments/CalamitasCurseUI_ArrowDownClicked", (AssetRequestMode)2).Value;
		}
		Rectangle topButtonArea = default(Rectangle);
		((Rectangle)(ref topButtonArea))._002Ector((int)topButtonTopLeft.X, (int)topButtonTopLeft.Y, (int)((float)topArrowTexture.Width * scale.X), (int)((float)topArrowTexture.Height * scale.Y));
		Rectangle bottomButtonArea = default(Rectangle);
		((Rectangle)(ref bottomButtonArea))._002Ector((int)bottomButtonTopLeft.X, (int)bottomButtonTopLeft.Y, (int)((float)bottomArrowTexture.Width * scale.X), (int)((float)bottomArrowTexture.Height * scale.Y));
		Rectangle mouseScreenArea = MouseScreenArea;
		bool hoveringOverTopArrow = ((Rectangle)(ref mouseScreenArea)).Intersects(topButtonArea);
		mouseScreenArea = MouseScreenArea;
		bool hoveringOverBottomArrow = ((Rectangle)(ref mouseScreenArea)).Intersects(bottomButtonArea);
		if (hoveringOverTopArrow)
		{
			topArrowTexture = ModContent.Request<Texture2D>("CalamityMod/UI/CalamitasEnchantments/CalamitasCurseUI_ArrowUpHovered", (AssetRequestMode)2).Value;
		}
		if (hoveringOverBottomArrow)
		{
			bottomArrowTexture = ModContent.Request<Texture2D>("CalamityMod/UI/CalamitasEnchantments/CalamitasCurseUI_ArrowDownHovered", (AssetRequestMode)2).Value;
		}
		if (EnchantIndex > 0)
		{
			spriteBatch.Draw(topArrowTexture, topButtonTopLeft, (Rectangle?)null, Color.White, 0f, Vector2.Zero, scale, (SpriteEffects)0, 0f);
		}
		if (EnchantIndex < possibleEnchantments.Count() - 1)
		{
			spriteBatch.Draw(bottomArrowTexture, bottomButtonTopLeft, (Rectangle?)null, Color.White, 0f, Vector2.Zero, scale, (SpriteEffects)0, 0f);
		}
		if (Main.mouseLeft && Main.mouseLeftRelease)
		{
			if (hoveringOverTopArrow && EnchantIndex > 0)
			{
				EnchantIndex--;
				TopButtonClickCountdown = 15f;
				SoundEngine.PlaySound(in SoundID.MenuTick);
			}
			if (hoveringOverBottomArrow && EnchantIndex < possibleEnchantments.Count() - 1)
			{
				EnchantIndex++;
				BottomButtonClickCountdown = 15f;
				SoundEngine.PlaySound(in SoundID.MenuTick);
			}
		}
	}

	public static void DrawEnchantmentName(SpriteBatch spriteBatch, Vector2 nameDrawCenter)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		Vector2 scale = new Vector2(0.8f, 0.745f) * Main.UIScale;
		string enchName = SelectedEnchantment.Value.Name.ToString();
		float textWidth = FontAssets.MouseText.Value.MeasureString(enchName).X * scale.X;
		Color drawColor = (SelectedEnchantment.Value.Equals(EnchantmentManager.ClearEnchantment) ? Color.White : Color.Orange);
		nameDrawCenter.X -= textWidth * 0.5f;
		ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, enchName, nameDrawCenter, drawColor, 0f, Vector2.Zero, scale);
	}

	public static void InteractWithEnchantIcon(int cost)
	{
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		if (!CurrentlyHeldItem.IsAir && SelectedEnchantment.HasValue && cost > 0 && Main.LocalPlayer.CanAfford(cost))
		{
			bool num = SelectedEnchantment.Value.Name == CalamityUtils.GetText("UI.Exhumed.DisplayName");
			int oldPrefix = CurrentlyHeldItem.prefix;
			int oldStack = CurrentlyHeldItem.stack;
			CurrentlyHeldItem.SetDefaults(CurrentlyHeldItem.type);
			CurrentlyHeldItem.Prefix(oldPrefix);
			CurrentlyHeldItem = CurrentlyHeldItem.Clone();
			if (num)
			{
				CurrentlyHeldItem.SetDefaults(EnchantmentManager.ItemUpgradeRelationship[CurrentlyHeldItem.type]);
				CurrentlyHeldItem.Prefix(oldPrefix);
				CurrentlyHeldItem.stack = oldStack;
			}
			else
			{
				CurrentlyHeldItem.Calamity().AppliedEnchantment = SelectedEnchantment.Value;
				SelectedEnchantment.Value.CreationEffect?.Invoke(CurrentlyHeldItem);
			}
			Main.LocalPlayer.BuyItem(cost);
			EnchantIndex = 0;
			if (num)
			{
				SoundEngine.PlaySound(in EXSound, Main.LocalPlayer.Center);
			}
			else
			{
				SoundEngine.PlaySound(in EnchSound, Main.LocalPlayer.Center);
			}
		}
	}
}
