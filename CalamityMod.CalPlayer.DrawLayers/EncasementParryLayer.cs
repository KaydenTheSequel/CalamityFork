using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class EncasementParryLayer : PlayerDrawLayer
{
	public enum EncasementType
	{
		BlazingCore,
		FlameLickedShell
	}

	public override Position GetDefaultPosition()
	{
		return new BeforeParent(PlayerDrawLayers.FrontAccFront);
	}

	public EncasementType GetEncasementTypeFor(CalamityPlayer modPlayer)
	{
		if (!modPlayer.blazingCore)
		{
			return EncasementType.FlameLickedShell;
		}
		return EncasementType.BlazingCore;
	}

	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		Player drawPlayer = drawInfo.drawPlayer;
		CalamityPlayer modPlayer = drawPlayer.Calamity();
		EncasementType encasement = GetEncasementTypeFor(modPlayer);
		bool visible = drawInfo.shadow == 0f && !drawPlayer.dead;
		return encasement switch
		{
			EncasementType.BlazingCore => visible && modPlayer.blazingCoreParry > 0, 
			EncasementType.FlameLickedShell => visible && modPlayer.flameLickedShellParry > 0, 
			_ => false, 
		};
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		Player drawPlayer = drawInfo.drawPlayer;
		CalamityPlayer calPlayer = drawPlayer.Calamity();
		EncasementType encasement = GetEncasementTypeFor(calPlayer);
		string tex = "CalamityMod/CalPlayer/DrawLayers/";
		int currentParry;
		float defaultOpacity;
		float scale;
		switch (encasement)
		{
		case EncasementType.BlazingCore:
			tex += "BlazingCoreCrystal";
			currentParry = calPlayer.blazingCoreParry;
			defaultOpacity = 0.725f;
			scale = 1.15f;
			break;
		case EncasementType.FlameLickedShell:
			tex += "PetrifiedRoseBud";
			currentParry = calPlayer.flameLickedShellParry;
			defaultOpacity = 0.875f;
			scale = 1.2f;
			break;
		default:
			tex += "BlazingCoreCrystal";
			currentParry = 0;
			defaultOpacity = 0f;
			scale = 0f;
			break;
		}
		Texture2D texture = ModContent.Request<Texture2D>(tex, (AssetRequestMode)2).Value;
		Vector2 drawPos = drawInfo.Center - Main.screenPosition + new Vector2(0f, drawPlayer.gfxOffY);
		drawPos.Y += 15f;
		drawPos.X += 15f;
		int maxParry = 30;
		float colorIntensity = ((currentParry >= 12) ? defaultOpacity : (1f - Utils.GetLerpValue(maxParry, 0f, currentParry, clamped: true)));
		SpriteEffects spriteEffects = (SpriteEffects)(drawPlayer.direction == -1);
		drawInfo.DrawDataCache.Add(new DrawData(texture, drawPos, null, Color.White * colorIntensity, 0f, texture.Size() * 0.75f, scale, spriteEffects));
	}
}
