using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class UnstableCastersGauntletLayer : PlayerDrawLayer
{
	public override Position GetDefaultPosition()
	{
		return new AfterParent(PlayerDrawLayers.HandOnAcc);
	}

	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		return drawInfo.drawPlayer.HeldItem.ModItem is UnstableCastersGauntlet;
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = new Vector2((float)(int)(drawInfo.Position.X - Main.screenPosition.X - (float)(drawInfo.drawPlayer.bodyFrame.Width / 2) + (float)(drawInfo.drawPlayer.width / 2)), (float)(int)(drawInfo.Position.Y - Main.screenPosition.Y + (float)drawInfo.drawPlayer.height - (float)drawInfo.drawPlayer.bodyFrame.Height + 4f)) + drawInfo.drawPlayer.bodyPosition + new Vector2((float)(drawInfo.drawPlayer.bodyFrame.Width / 2), (float)(drawInfo.drawPlayer.bodyFrame.Height / 2));
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/UnstableCastersGauntlet_HandsOn", (AssetRequestMode)2).Value;
		Vector2 position = val + drawInfo.frontShoulderOffset + new Vector2((float)(-5 * ((!((Enum)drawInfo.playerEffect).HasFlag((Enum)(object)(SpriteEffects)1)) ? 1 : (-1))), 0f);
		Vector2 value = Main.OffsetsPlayerHeadgear[drawInfo.drawPlayer.bodyFrame.Y / drawInfo.drawPlayer.bodyFrame.Height];
		value.Y -= 2f;
		position += value * (float)(-((Enum)drawInfo.playerEffect).HasFlag((Enum)(object)(SpriteEffects)2).ToDirectionInt());
		Vector2 origin = drawInfo.bodyVect + drawInfo.frontShoulderOffset + new Vector2((float)(-5 * ((!((Enum)drawInfo.playerEffect).HasFlag((Enum)(object)(SpriteEffects)1)) ? 1 : (-1))), 0f);
		if (drawInfo.compFrontArmFrame.X / drawInfo.compFrontArmFrame.Width >= 7)
		{
			position += new Vector2((float)((!((Enum)drawInfo.playerEffect).HasFlag((Enum)(object)(SpriteEffects)1)) ? 1 : (-1)), (float)((!((Enum)drawInfo.playerEffect).HasFlag((Enum)(object)(SpriteEffects)2)) ? 1 : (-1)));
		}
		DrawData drawData = new DrawData(texture, position, drawInfo.compFrontArmFrame, Color.White * (1f - drawInfo.shadow), drawInfo.compositeFrontArmRotation, origin, 1f, drawInfo.playerEffect);
		drawInfo.DrawDataCache.Add(drawData);
	}
}
