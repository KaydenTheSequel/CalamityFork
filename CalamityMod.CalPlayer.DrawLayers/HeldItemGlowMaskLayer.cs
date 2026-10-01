using System;
using System.Collections.Generic;
using CalamityMod.Items.SummonItems;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace CalamityMod.CalPlayer.DrawLayers;

public class HeldItemGlowMaskLayer : PlayerDrawLayer
{
	public override Position GetDefaultPosition()
	{
		return new AfterParent(PlayerDrawLayers.HeldItem);
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0705: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a50: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a22: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0909: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0957: Unknown result type (might be due to invalid IL or missing references)
		//IL_0977: Unknown result type (might be due to invalid IL or missing references)
		//IL_0981: Unknown result type (might be due to invalid IL or missing references)
		//IL_0999: Unknown result type (might be due to invalid IL or missing references)
		//IL_099e: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0923: Unknown result type (might be due to invalid IL or missing references)
		//IL_092e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0869: Unknown result type (might be due to invalid IL or missing references)
		//IL_0891: Unknown result type (might be due to invalid IL or missing references)
		//IL_0896: Unknown result type (might be due to invalid IL or missing references)
		//IL_089d: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0825: Unknown result type (might be due to invalid IL or missing references)
		//IL_082d: Unknown result type (might be due to invalid IL or missing references)
		//IL_083c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0811: Unknown result type (might be due to invalid IL or missing references)
		//IL_0816: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		Player drawPlayer = drawInfo.drawPlayer;
		List<DrawData> existingDrawData = drawInfo.DrawDataCache;
		if (drawPlayer.JustDroppedAnItem)
		{
			return;
		}
		Item heldItem = drawInfo.heldItem;
		int itemType = heldItem.type;
		if (itemType < ItemID.Count)
		{
			return;
		}
		Color color = default(Color);
		((Color)(ref color))._002Ector(250, 250, 250, heldItem.alpha);
		Texture2D glowMask = null;
		if (itemType == ModContent.ItemType<AbyssShocker>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/AbyssShocker_mask", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<Apotheosis>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/ApotheosisGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<Auralis>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/AuralisGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<AuroraBlazer>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/AuroraBlazerGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<ChromaticEruption>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/ChromaticEruptionGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<CleansingBlaze>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/CleansingBlazeGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<CosmicImmaterializer>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Summon/CosmicImmaterializerGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<HyperdeathRiftScepter>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/HyperdeathRiftScepterGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<ThreadOfEradication>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/ThreadOfEradicationGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<EssenceFlayer>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/EssenceFlayerGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<EtherealSubjugator>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Summon/EtherealSubjugatorGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<MawOfInfinity>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/MawOfInfinityGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<FatesReveal>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/FatesRevealGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<GreatswordofJudgement>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/GreatswordofJudgementGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<GalactusBlade>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/GalactusBladeGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<LegionofCelestia>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Summon/LegionofCelestiaGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<NecroplasmicBeacon>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/SummonItems/NecroplasmicBeaconGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<Orderbringer>())
		{
			Color val = Color.Lerp(Color.White, drawPlayer.Calamity().lightRGB, 0.75f);
			((Color)(ref val)).A = 0;
			color = val;
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/OrderbringerGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<Photosynthesis>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/PhotosynthesisGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<PlantationStaff>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Summon/PlantationStaffGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<PrismaticBreaker>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/PrismaticBreakerGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<PulseRifle>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/DraedonsArsenal/PulseRifleGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<SoulPiercer>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/SoulPiercerGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<Lightspeed>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/LightspeedGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<SubsumingVortex>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/SubsumingVortexSmallGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<TerrorBlade>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/TerrorBladeGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<VernalBolter>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/VernalBolterGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<VividClarity>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/VividClarityGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<TelluricGlare>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/TelluricGlareGlow", (AssetRequestMode)2).Value;
		}
		else if (itemType == ModContent.ItemType<BlissfulBombardier>())
		{
			glowMask = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/BlissfulBombardierGlow", (AssetRequestMode)2).Value;
		}
		if (glowMask == null)
		{
			return;
		}
		if (drawPlayer.heldProj >= 0 && drawInfo.shadow == 0f && !drawInfo.heldProjOverHand)
		{
			drawInfo.projectileDrawPosition = existingDrawData.Count;
		}
		float adjustedItemScale = drawPlayer.GetAdjustedItemScale(heldItem);
		Vector2 position = default(Vector2);
		((Vector2)(ref position))._002Ector((float)(int)(drawInfo.ItemLocation.X - Main.screenPosition.X), (float)(int)(drawInfo.ItemLocation.Y - Main.screenPosition.Y));
		Rectangle itemDrawFrame = drawPlayer.GetItemDrawFrame(itemType);
		drawInfo.itemColor = Lighting.GetColor((int)((double)drawInfo.Position.X + (double)drawPlayer.width * 0.5) / 16, (int)(((double)drawInfo.Position.Y + (double)drawPlayer.height * 0.5) / 16.0));
		if (drawPlayer.shroomiteStealth && heldItem.CountsAsClass<RangedDamageClass>())
		{
			float stealth = drawPlayer.stealth;
			if ((double)stealth < 0.03)
			{
				stealth = 0.03f;
			}
			float stealthColorScale = (1f + stealth * 10f) / 11f;
			drawInfo.itemColor = new Color((int)(byte)((float)(int)((Color)(ref drawInfo.itemColor)).R * stealth), (int)(byte)((float)(int)((Color)(ref drawInfo.itemColor)).G * stealth), (int)(byte)((float)(int)((Color)(ref drawInfo.itemColor)).B * stealthColorScale), (int)(byte)((float)(int)((Color)(ref drawInfo.itemColor)).A * stealth));
		}
		if (drawPlayer.setVortex && heldItem.CountsAsClass<RangedDamageClass>())
		{
			float stealth2 = drawPlayer.stealth;
			if ((double)stealth2 < 0.03)
			{
				stealth2 = 0.03f;
			}
			drawInfo.itemColor = drawInfo.itemColor.MultiplyRGBA(new Color(Vector4.Lerp(Vector4.One, new Vector4(0f, 0.12f, 0.16f, 0f), 1f - stealth2)));
		}
		bool inUse = drawPlayer.itemAnimation > 0 && heldItem.useStyle != 0;
		bool visuallyHeld = heldItem.holdStyle != 0 && !drawPlayer.pulley;
		if (!drawPlayer.CanVisuallyHoldItem(heldItem))
		{
			visuallyHeld = false;
		}
		if (drawInfo.shadow != 0f || drawPlayer.frozen || !(inUse | visuallyHeld) || itemType <= 0 || drawPlayer.dead || heldItem.noUseGraphic || (drawPlayer.wet && heldItem.noWet) || (drawPlayer.happyFunTorchTime && drawPlayer.inventory[drawPlayer.selectedItem].createTile == 4 && drawPlayer.itemAnimation == 0))
		{
			return;
		}
		Vector2 originOffset = Vector2.Zero;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)itemDrawFrame.Width * 0.5f - (float)itemDrawFrame.Width * 0.5f * (float)drawPlayer.direction, (float)itemDrawFrame.Height);
		if (heldItem.useStyle == 9 && drawPlayer.itemAnimation > 0)
		{
			Vector2 vector2 = default(Vector2);
			((Vector2)(ref vector2))._002Ector(0.5f, 0.4f);
			origin = itemDrawFrame.Size() * vector2;
		}
		if (drawPlayer.gravDir == -1f)
		{
			origin.Y = (float)itemDrawFrame.Height - origin.Y;
		}
		origin += originOffset;
		float itemRotation = drawPlayer.itemRotation;
		if (heldItem.useStyle == 8)
		{
			ref float x = ref position.X;
			float xOffset = x;
			x = xOffset - 0f;
			itemRotation -= (float)Math.PI / 2f * (float)drawPlayer.direction;
			origin.Y = 2f;
			origin.X += 2 * drawPlayer.direction;
		}
		ItemSlot.GetItemLight(ref drawInfo.itemColor, heldItem);
		if (heldItem.useStyle == 5)
		{
			if (Item.staff[itemType])
			{
				float staffRotation = drawPlayer.itemRotation + (float)Math.PI / 4f * (float)drawPlayer.direction;
				float staffXOffset = 0f;
				float staffYOffset = 0f;
				Vector2 staffOrigin = default(Vector2);
				((Vector2)(ref staffOrigin))._002Ector(0f, (float)itemDrawFrame.Height);
				if (drawPlayer.gravDir == -1f)
				{
					if (drawPlayer.direction == -1)
					{
						staffRotation += (float)Math.PI / 2f;
						((Vector2)(ref staffOrigin))._002Ector((float)itemDrawFrame.Width, 0f);
						staffXOffset -= (float)itemDrawFrame.Width;
					}
					else
					{
						staffRotation -= (float)Math.PI / 2f;
						staffOrigin = Vector2.Zero;
					}
				}
				else if (drawPlayer.direction == -1)
				{
					((Vector2)(ref staffOrigin))._002Ector((float)itemDrawFrame.Width, (float)itemDrawFrame.Height);
					staffXOffset -= (float)itemDrawFrame.Width;
				}
				ItemLoader.HoldoutOrigin(drawPlayer, ref staffOrigin);
				DrawData item = new DrawData(glowMask, new Vector2((float)(int)(drawInfo.ItemLocation.X - Main.screenPosition.X + staffOrigin.X + staffXOffset), (float)(int)(drawInfo.ItemLocation.Y - Main.screenPosition.Y + staffYOffset)), itemDrawFrame, color, staffRotation, staffOrigin, adjustedItemScale, drawInfo.itemEffect);
				existingDrawData.Add(item);
			}
			else
			{
				int xOffset2 = 10;
				Vector2 offset = default(Vector2);
				((Vector2)(ref offset))._002Ector((float)(itemDrawFrame.Width / 2), (float)(itemDrawFrame.Height / 2));
				Vector2 directionalOffset = Main.DrawPlayerItemPos(drawPlayer.gravDir, itemType);
				xOffset2 = (int)directionalOffset.X;
				offset.Y = directionalOffset.Y;
				Vector2 drawOrigin = default(Vector2);
				((Vector2)(ref drawOrigin))._002Ector((float)(-xOffset2), (float)(itemDrawFrame.Height / 2));
				if (drawPlayer.direction == -1)
				{
					((Vector2)(ref drawOrigin))._002Ector((float)(itemDrawFrame.Width + xOffset2), (float)(itemDrawFrame.Height / 2));
				}
				DrawData item = new DrawData(glowMask, new Vector2((float)(int)(drawInfo.ItemLocation.X - Main.screenPosition.X + offset.X), (float)(int)(drawInfo.ItemLocation.Y - Main.screenPosition.Y + offset.Y)) - new Vector2((float)glowMask.Width * 0.5f, 0f), itemDrawFrame, new Color(250, 250, 250, heldItem.alpha), drawPlayer.itemRotation, drawOrigin, adjustedItemScale, drawInfo.itemEffect);
				existingDrawData.Add(item);
			}
		}
		else if (drawPlayer.gravDir == -1f)
		{
			DrawData item = new DrawData(glowMask, position, itemDrawFrame, new Color(250, 250, 250, heldItem.alpha), itemRotation, origin, adjustedItemScale, drawInfo.itemEffect);
			existingDrawData.Add(item);
		}
		else
		{
			DrawData item = new DrawData(glowMask, position, itemDrawFrame, color, itemRotation, origin, adjustedItemScale, drawInfo.itemEffect);
			existingDrawData.Add(item);
		}
	}
}
