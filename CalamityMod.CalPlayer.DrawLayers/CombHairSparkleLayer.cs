using CalamityMod.Items.Accessories.Vanity;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class CombHairSparkleLayer : PlayerDrawLayer
{
	public override bool IsHeadLayer => true;

	public override Position GetDefaultPosition()
	{
		return new BeforeParent(PlayerDrawLayers.LeinforsHairShampoo);
	}

	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		Player drawPlayer = drawInfo.drawPlayer;
		CalamityPlayer modPlayer = drawPlayer.Calamity();
		bool canReceiveHairSparkles = (drawInfo.fullHair || drawInfo.hatHair || drawInfo.drawsBackHairWithoutHeadgear || drawPlayer.head == -1 || drawPlayer.head == 0) && Main.rgbToHsl(drawInfo.colorHead).Z > 0.2f;
		bool baldHairStyles = drawPlayer.hair == 15 || drawPlayer.hair == 76;
		if ((drawInfo.shadow == 0f && !drawPlayer.dead && !drawInfo.headOnlyRender && modPlayer.combHair) & canReceiveHairSparkles)
		{
			return !baldHairStyles;
		}
		return false;
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		Player drawPlayer = drawInfo.drawPlayer;
		if (!Main.rand.NextBool(20))
		{
			return;
		}
		int dyeIndex = -1;
		for (int i = 0; i < 20; i++)
		{
			if (drawPlayer.armor[i].type == ModContent.ItemType<TheComb>())
			{
				dyeIndex = i;
				break;
			}
		}
		ArmorShaderData shader = GameShaders.Armor.GetSecondaryShader((dyeIndex != -1) ? drawPlayer.dye[dyeIndex % 10].dye : 0, drawPlayer);
		int num = Utils.Clamp((int)drawPlayer.MountedCenter.X / 16 - 50, 2, Main.maxTilesX - 2);
		int RightRange = Utils.Clamp((int)drawPlayer.MountedCenter.X / 16 + 50, 2, Main.maxTilesX - 2);
		int TopRange = Utils.Clamp((int)drawPlayer.MountedCenter.Y / 16 - 50, 2, Main.maxTilesY - 2);
		int BottomRange = Utils.Clamp((int)drawPlayer.MountedCenter.Y / 16 + 50, 2, Main.maxTilesY - 2);
		float range = 50000f;
		Vector2 ChestPosition = Vector2.Zero;
		for (int j = num; j <= RightRange; j++)
		{
			for (int k = TopRange; k <= BottomRange; k++)
			{
				Tile tile = Main.tile[j, k];
				if (!tile.HasTile || !TileID.Sets.IsAContainer[tile.TileType] || TileID.Sets.BasicDresser[tile.TileType])
				{
					continue;
				}
				int PotentialChest = Chest.FindChestByGuessing(j, k);
				if (PotentialChest != -1)
				{
					Chest chest = Main.chest[PotentialChest];
					Vector2 chestCenter = new Vector2((float)(chest.x + 1), (float)(chest.y + 1)) * 16f;
					float distance = Vector2.Distance(chestCenter, drawPlayer.MountedCenter);
					if (distance < range)
					{
						range = distance;
						ChestPosition = chestCenter;
					}
				}
			}
		}
		if (!drawInfo.hatHair || Main.rand.NextBool())
		{
			Rectangle area = (drawInfo.hatHair ? Utils.CenteredRectangle(drawInfo.Position + drawPlayer.Size * 0.5f + new Vector2((float)(drawPlayer.direction * -10), drawPlayer.gravDir * -10f), new Vector2(5f, 5f)) : Utils.CenteredRectangle(drawInfo.Position + drawPlayer.Size * 0.5f + new Vector2(0f, drawPlayer.gravDir * -20f), new Vector2(20f, 14f)));
			Dust sparkle = Dust.NewDustDirect(area.TopLeft(), area.Width, area.Height, 246, 0f, 0f, 150, default(Color), 0.3f);
			sparkle.fadeIn = 1f;
			sparkle.velocity = ((ChestPosition == Vector2.Zero) ? (sparkle.velocity * 0.1f) : ((ChestPosition - sparkle.position).SafeNormalize(Vector2.Zero) * 0.2f));
			sparkle.noLight = true;
			sparkle.shader = shader;
			drawInfo.DustCache.Add(sparkle.dustIndex);
		}
		if (drawPlayer.velocity.X != 0f && drawInfo.backHairDraw)
		{
			Rectangle areaB = Utils.CenteredRectangle(drawInfo.Position + drawPlayer.Size * 0.5f + new Vector2((float)(drawPlayer.direction * -14), 0f), new Vector2(4f, 30f));
			Dust sparkleB = Dust.NewDustDirect(areaB.TopLeft(), areaB.Width, areaB.Height, 246, 0f, 0f, 150, default(Color), 0.3f);
			sparkleB.fadeIn = 1f;
			sparkleB.velocity = ((ChestPosition == Vector2.Zero) ? (sparkleB.velocity * 0.1f) : ((ChestPosition - sparkleB.position).SafeNormalize(Vector2.Zero) * 0.2f));
			sparkleB.noLight = true;
			sparkleB.shader = shader;
			drawInfo.DustCache.Add(sparkleB.dustIndex);
		}
	}
}
