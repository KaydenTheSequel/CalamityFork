using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class GemTechGemLayer : PlayerDrawLayer
{
	public override Position GetDefaultPosition()
	{
		return new AfterParent(PlayerDrawLayers.BackAcc);
	}

	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		Player drawPlayer = drawInfo.drawPlayer;
		CalamityPlayer modPlayer = drawPlayer.Calamity();
		if (drawInfo.shadow == 0f && !drawPlayer.dead && modPlayer.GemTechSet)
		{
			return drawPlayer.Calamity().andromedaState == AndromedaPlayerState.Inactive;
		}
		return false;
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = drawInfo.drawPlayer.Calamity();
		float opacity = MathHelper.Lerp(0.85f, 1.05f, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 2.3f) * 0.5f + 0.5f);
		float time = Main.GlobalTimeWrappedHourly * 0.61f;
		float gemTime = Main.GlobalTimeWrappedHourly * 3.41f;
		for (int i = 5; i >= 0; i--)
		{
			float pulseFactor = 1.8f;
			Texture2D gemTexture;
			GemTechArmorGemType gemType;
			switch (i)
			{
			default:
				gemTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/GemTechRedGem", (AssetRequestMode)2).Value;
				gemType = GemTechArmorGemType.Rogue;
				break;
			case 1:
				gemTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/GemTechYellowGem", (AssetRequestMode)2).Value;
				gemType = GemTechArmorGemType.Melee;
				break;
			case 2:
				gemTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/GemTechGreenGem", (AssetRequestMode)2).Value;
				gemType = GemTechArmorGemType.Ranged;
				break;
			case 3:
				gemTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/GemTechBlueGem", (AssetRequestMode)2).Value;
				gemType = GemTechArmorGemType.Summoner;
				break;
			case 4:
				gemTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/GemTechPurpleGem", (AssetRequestMode)2).Value;
				gemType = GemTechArmorGemType.Magic;
				break;
			case 5:
				gemTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/GemTechPinkGem", (AssetRequestMode)2).Value;
				gemType = GemTechArmorGemType.Base;
				pulseFactor = 2.5f;
				break;
			}
			if (modPlayer.GemTechState.GemIsActive(gemType))
			{
				float drawOffsetAngle = modPlayer.GemTechState.CalculateGemOffsetAngle(gemType, gemTime);
				float gemOpacity = opacity;
				pulseFactor *= (float)Math.Cos(time + drawOffsetAngle);
				gemOpacity *= Utils.GetLerpValue(-0.75f, -0.51f, (float)Math.Sin(drawOffsetAngle), clamped: true);
				Vector2 baseDrawPosition = modPlayer.GemTechState.CalculateGemPosition(gemType) - Main.screenPosition;
				float afterimageTime = Main.GlobalTimeWrappedHourly * 0.47f;
				float backAfterImageOpacity = gemOpacity * 0.24f;
				Vector2 origin = gemTexture.Size() * 0.5f;
				for (int j = 0; j < 5; j++)
				{
					Color backAfterimageColor = Main.hslToRgb((afterimageTime + (float)j / 5f) % 1f, 1f, 0.67f);
					backAfterimageColor = Color.Lerp(backAfterimageColor, Color.White, 0.64f) * backAfterImageOpacity;
					Vector2 drawPosition = baseDrawPosition + ((float)Math.PI * 2f * (float)j / 5f).ToRotationVector2() * pulseFactor;
					DrawData gemBackDrawData = new DrawData(gemTexture, drawPosition, null, backAfterimageColor, 0f, origin, 1f, (SpriteEffects)0);
					drawInfo.DrawDataCache.Add(gemBackDrawData);
				}
				Color baseGemColor = Main.hslToRgb(Main.GlobalTimeWrappedHourly * 0.51f % 1f, 1f, 0.67f);
				baseGemColor = Color.Lerp(baseGemColor, Color.White, 0.56f);
				((Color)(ref baseGemColor)).A = 105;
				baseGemColor *= gemOpacity;
				DrawData gemDrawData = new DrawData(gemTexture, baseDrawPosition, null, baseGemColor, 0f, gemTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
				drawInfo.DrawDataCache.Add(gemDrawData);
			}
		}
	}
}
