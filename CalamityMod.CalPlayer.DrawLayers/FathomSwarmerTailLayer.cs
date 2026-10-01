using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class FathomSwarmerTailLayer : PlayerDrawLayer
{
	public override Position GetDefaultPosition()
	{
		return new BeforeParent(PlayerDrawLayers.Skin);
	}

	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		Player drawPlayer = drawInfo.drawPlayer;
		CalamityPlayer modPlayer = drawPlayer.Calamity();
		if (drawInfo.shadow == 0f && !drawPlayer.dead)
		{
			return modPlayer.fathomSwarmerTail;
		}
		return false;
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Armor/FathomSwarmer/FathomSwarmerArmor_Tail", (AssetRequestMode)2).Value;
		Player drawPlayer = drawInfo.drawPlayer;
		Rectangle frame = texture.Frame(1, 4, 0, drawPlayer.Calamity().tailFrame);
		Item[] dye = drawPlayer.dye;
		int dyeShader = ((dye != null) ? dye[2].dye : 0);
		int frameSizeY = texture.Height / 4;
		int drawX = (int)(drawInfo.Center.X - Main.screenPosition.X - (float)(3 * drawPlayer.direction));
		int drawY = (int)(drawInfo.Center.Y - Main.screenPosition.Y - 4f);
		DrawData drawData = new DrawData(texture, new Vector2((float)drawX, (float)drawY), frame, drawInfo.colorPants, 0f, new Vector2((float)texture.Width / 2f, (float)frameSizeY / 2f), 1f, drawInfo.playerEffect);
		drawData.shader = dyeShader;
		DrawData tailDrawData = drawData;
		drawInfo.DrawDataCache.Add(tailDrawData);
	}
}
