using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class AquaticHeartIceShieldLayer : PlayerDrawLayer
{
	public override Position GetDefaultPosition()
	{
		return new AfterParent(PlayerDrawLayers.BackAcc);
	}

	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		if (drawInfo.drawPlayer.Calamity().aquaticHeartIce)
		{
			return drawInfo.shadow == 0f;
		}
		return false;
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/CalPlayer/DrawLayers/IceShield", (AssetRequestMode)2).Value;
		int drawX = (int)(drawInfo.Center.X - Main.screenPosition.X);
		int drawY = (int)(drawInfo.Center.Y - Main.screenPosition.Y);
		drawInfo.DrawDataCache.Add(new DrawData(texture, new Vector2((float)drawX, (float)drawY), null, Color.White, 0f, texture.Size() * 0.5f, 1f, (SpriteEffects)0));
	}
}
