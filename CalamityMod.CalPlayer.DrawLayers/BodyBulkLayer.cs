using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class BodyBulkLayer : PlayerDrawLayer
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
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		if (drawInfo.headOnlyRender)
		{
			return;
		}
		Player drawPlayer = drawInfo.drawPlayer;
		Item bodyItem = drawPlayer.armor[1];
		if (drawPlayer.armor[11].type > 0)
		{
			bodyItem = drawPlayer.armor[11];
		}
		if (ModContent.GetModItem(bodyItem.type) is IBulkyArmor chestplateBulkDrawer)
		{
			string equipSlotName = ((chestplateBulkDrawer.EquipSlotName(drawPlayer) != "") ? chestplateBulkDrawer.EquipSlotName(drawPlayer) : bodyItem.ModItem.Name);
			int equipSlot = EquipLoader.GetEquipSlot(base.Mod, equipSlotName, EquipType.Body);
			if (drawPlayer.body == equipSlot)
			{
				Item[] dye = drawPlayer.dye;
				int dyeShader = ((dye != null) ? dye[1].dye : 0);
				Vector2 drawPosition = drawInfo.Position - Main.screenPosition;
				drawPosition += new Vector2((float)(drawPlayer.width - drawPlayer.bodyFrame.Width) / 2f, (float)(drawPlayer.height - drawPlayer.bodyFrame.Height) + 4f);
				((Vector2)(ref drawPosition))._002Ector((float)(int)drawPosition.X, (float)(int)drawPosition.Y);
				drawPosition += drawPlayer.bodyPosition + drawInfo.bodyVect;
				Texture2D extraPieceTexture = ModContent.Request<Texture2D>(chestplateBulkDrawer.BulkTexture, (AssetRequestMode)2).Value;
				Rectangle frame = extraPieceTexture.Frame(1, 20, 0, drawPlayer.bodyFrame.Y / drawPlayer.bodyFrame.Height);
				DrawData drawData = new DrawData(extraPieceTexture, drawPosition, frame, drawInfo.colorArmorBody, drawPlayer.fullRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect);
				drawData.shader = dyeShader;
				DrawData pieceDrawData = drawData;
				drawInfo.DrawDataCache.Add(pieceDrawData);
			}
		}
	}
}
