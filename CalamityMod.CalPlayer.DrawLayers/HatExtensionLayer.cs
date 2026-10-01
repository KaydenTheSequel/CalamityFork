using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class HatExtensionLayer : PlayerDrawLayer
{
	public override Position GetDefaultPosition()
	{
		return new AfterParent(PlayerDrawLayers.Head);
	}

	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		if (drawInfo.shadow != 0f)
		{
			return !drawInfo.drawPlayer.dead;
		}
		return true;
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		Player drawPlayer = drawInfo.drawPlayer;
		if (drawPlayer.head == -1 || EquipLoader.GetEquipTexture(EquipType.Head, drawPlayer.head) == null)
		{
			return;
		}
		ModItem headItem = EquipLoader.GetEquipTexture(EquipType.Head, drawPlayer.head).Item;
		if (headItem is IExtendedHat extendedHatDrawer)
		{
			string equipSlotName = ((extendedHatDrawer.EquipSlotName(drawPlayer) != "") ? extendedHatDrawer.EquipSlotName(drawPlayer) : headItem.Name);
			int equipSlot = EquipLoader.GetEquipSlot(base.Mod, equipSlotName, EquipType.Head);
			if (extendedHatDrawer.PreDrawExtension(drawInfo) && !drawInfo.drawPlayer.dead && equipSlot == drawPlayer.head)
			{
				Item[] dye = drawPlayer.dye;
				int dyeShader = ((dye != null) ? dye[0].dye : 0);
				Vector2 headDrawPosition = drawInfo.Position - Main.screenPosition;
				headDrawPosition += new Vector2((float)(drawPlayer.width - drawPlayer.bodyFrame.Width) / 2f, (float)(drawPlayer.height - drawPlayer.bodyFrame.Height) + 4f);
				((Vector2)(ref headDrawPosition))._002Ector((float)(int)headDrawPosition.X, (float)(int)headDrawPosition.Y);
				headDrawPosition += drawPlayer.headPosition + drawInfo.headVect;
				headDrawPosition += extendedHatDrawer.ExtensionSpriteOffset(drawInfo);
				Texture2D extraPieceTexture = ModContent.Request<Texture2D>(extendedHatDrawer.ExtensionTexture, (AssetRequestMode)2).Value;
				Rectangle frame = extraPieceTexture.Frame(1, 20, 0, drawPlayer.bodyFrame.Y / drawPlayer.bodyFrame.Height);
				DrawData drawData = new DrawData(extraPieceTexture, headDrawPosition, frame, drawInfo.colorArmorHead, drawPlayer.headRotation, drawInfo.headVect, 1f, drawInfo.playerEffect);
				drawData.shader = dyeShader;
				DrawData pieceDrawData = drawData;
				drawInfo.DrawDataCache.Add(pieceDrawData);
			}
		}
	}
}
