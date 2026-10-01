using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class FrontARmOverShoulderpadLayer : PlayerDrawLayer
{
	public override Position GetDefaultPosition()
	{
		return new AfterParent(PlayerDrawLayers.ArmOverItem);
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
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		Player drawPlayer = drawInfo.drawPlayer;
		Item bodyItem = drawPlayer.armor[1];
		if (drawPlayer.armor[11].type > 0)
		{
			bodyItem = drawPlayer.armor[11];
		}
		if (ModContent.GetModItem(bodyItem.type) is IDrawArmOverShoulderpad frontArmDrawer)
		{
			string equipSlotName = ((frontArmDrawer.EquipSlotName(drawPlayer) != "") ? frontArmDrawer.EquipSlotName(drawPlayer) : bodyItem.ModItem.Name);
			int equipSlot = EquipLoader.GetEquipSlot(base.Mod, equipSlotName, EquipType.Body);
			if (drawPlayer.body == equipSlot)
			{
				Item[] dye = drawPlayer.dye;
				int dyeShader = ((dye != null) ? dye[1].dye : 0);
				Vector2 drawPosition = drawInfo.Position - Main.screenPosition;
				drawPosition += new Vector2((float)(drawPlayer.width - drawPlayer.bodyFrame.Width) / 2f, (float)(drawPlayer.height - drawPlayer.bodyFrame.Height) + 4f);
				((Vector2)(ref drawPosition))._002Ector((float)(int)drawPosition.X, (float)(int)drawPosition.Y);
				drawPosition += drawPlayer.bodyPosition + drawInfo.bodyVect;
				Texture2D extraPieceTexture = ModContent.Request<Texture2D>(frontArmDrawer.FrontArmTexture, (AssetRequestMode)2).Value;
				Rectangle frame = extraPieceTexture.Frame(1, 20, 0, drawPlayer.bodyFrame.Y / drawPlayer.bodyFrame.Height);
				DrawData drawData = new DrawData(extraPieceTexture, drawPosition, frame, drawInfo.colorArmorBody, drawPlayer.fullRotation, drawInfo.bodyVect, 1f, drawInfo.playerEffect);
				drawData.shader = dyeShader;
				DrawData pieceDrawData = drawData;
				drawInfo.DrawDataCache.Add(pieceDrawData);
			}
		}
	}
}
