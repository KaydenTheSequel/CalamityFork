using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class GlacialEmbraceOverlayLayer : PlayerDrawLayer
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
			return modPlayer.GlacialEmbrace;
		}
		return false;
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/GlacialEmbraceBody", (AssetRequestMode)2).Value;
		int drawX = (int)(drawInfo.Center.X - Main.screenPosition.X);
		int drawY = (int)(drawInfo.Center.Y - Main.screenPosition.Y);
		SpriteEffects spriteEffects = (SpriteEffects)(drawInfo.drawPlayer.direction == -1);
		drawInfo.DrawDataCache.Add(new DrawData(texture, new Vector2((float)drawX, (float)drawY), null, new Color(53, Main.DiscoG, 255) * 0.5f, 0f, texture.Size() * 0.5f, 1.15f, spriteEffects));
	}
}
