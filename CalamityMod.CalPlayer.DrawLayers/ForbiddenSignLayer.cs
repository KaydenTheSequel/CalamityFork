using System;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class ForbiddenSignLayer : PlayerDrawLayer
{
	public override Position GetDefaultPosition()
	{
		return new BeforeParent(PlayerDrawLayers.ForbiddenSetRing);
	}

	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		Player drawPlayer = drawInfo.drawPlayer;
		CalamityPlayer modPlayer = drawPlayer.Calamity();
		if (drawInfo.shadow == 0f && !drawPlayer.dead)
		{
			return modPlayer.forbiddenCirclet;
		}
		return false;
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		Player drawPlayer = drawInfo.drawPlayer;
		Color color = Color.Lerp(drawInfo.colorArmorBody, Color.White, 0.7f);
		Texture2D texture = TextureAssets.Extra[74].Value;
		Texture2D glowmask = TextureAssets.GlowMask[217].Value;
		int offsetY = (int)(MathF.Sin((float)drawPlayer.miscCounter / 300f * ((float)Math.PI * 2f)) * 6f);
		float shadowMult = MathF.Cos((float)drawPlayer.miscCounter / 75f * ((float)Math.PI * 2f)) * 4f;
		Color afterimageColor = new Color(80, 70, 40, 0) * (shadowMult * 0.125f + 0.5f) * 0.8f;
		if (drawPlayer.ownedProjectileCounts[ModContent.ProjectileType<CircletTornado>()] > 1)
		{
			offsetY = 0;
			shadowMult = 2f;
			afterimageColor = new Color(80, 70, 40, 0) * 0.3f;
			color = color.MultiplyRGB(new Color(0.5f, 0.5f, 1f));
		}
		Vector2 drawPos = drawInfo.Position + drawPlayer.bodyPosition - Main.screenPosition;
		drawPos += new Vector2((float)drawPlayer.width * 0.5f - (float)drawPlayer.direction * 10f, (float)drawPlayer.height - (float)drawPlayer.bodyFrame.Height * 0.5f + 4f + drawPlayer.gravDir * ((float)offsetY - 20f));
		DrawData drawData = new DrawData(texture, drawPos, null, color, drawPlayer.bodyRotation, texture.Size() * 0.5f, 1f, drawInfo.playerEffect);
		drawData.shader = drawInfo.cBody;
		DrawData drawData2 = drawData;
		drawInfo.DrawDataCache.Add(drawData2);
		Vector2 origin = texture.Size() * 0.5f;
		for (float i = 0f; i < 4f; i++)
		{
			float angle = (float)Math.PI / 2f * i;
			drawData2 = new DrawData(glowmask, drawPos + angle.ToRotationVector2() * shadowMult, null, afterimageColor, drawPlayer.bodyRotation, origin, 1f, drawInfo.playerEffect);
			drawInfo.DrawDataCache.Add(drawData2);
		}
	}
}
