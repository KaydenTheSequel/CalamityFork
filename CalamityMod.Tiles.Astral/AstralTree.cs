using System;
using CalamityMod.Dusts;
using CalamityMod.Gores.Trees;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.FurnitureMonolith;
using CalamityMod.Items.Potions.Food;
using CalamityMod.Items.Tools;
using CalamityMod.NPCs.Astral;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Astral;

public class AstralTree : GlowMaskTree
{
	public override TreePaintingSettings TreeShaderSettings => new TreePaintingSettings
	{
		UseSpecialGroups = true,
		SpecialGroupMinimalHueValue = 11f / 72f,
		SpecialGroupMaximumHueValue = 0.25f,
		SpecialGroupMinimumSaturationValue = 0.88f,
		SpecialGroupMaximumSaturationValue = 1f
	};

	public override void SetStaticDefaults()
	{
		base.GrowsOnTileId = new int[1] { ModContent.TileType<AstralGrass>() };
	}

	public override Asset<Texture2D> GetTexture()
	{
		return ModContent.Request<Texture2D>("CalamityMod/Tiles/Astral/AstralTree", (AssetRequestMode)2);
	}

	public override Asset<Texture2D> GetGlowTexture()
	{
		return ModContent.Request<Texture2D>("CalamityMod/Tiles/Astral/AstralTreeGlow", (AssetRequestMode)2);
	}

	public override Asset<Texture2D> GetBranchTextures()
	{
		return ModContent.Request<Texture2D>("CalamityMod/Tiles/Astral/AstralTree_Branches", (AssetRequestMode)2);
	}

	public override Asset<Texture2D> GetBranchGlowTextures()
	{
		return ModContent.Request<Texture2D>("CalamityMod/Tiles/Astral/AstralTree_BranchesGlow", (AssetRequestMode)2);
	}

	public override Asset<Texture2D> GetTopTextures()
	{
		return ModContent.Request<Texture2D>("CalamityMod/Tiles/Astral/AstralTree_Tops", (AssetRequestMode)2);
	}

	public override Asset<Texture2D> GetTopGlowTextures()
	{
		return ModContent.Request<Texture2D>("CalamityMod/Tiles/Astral/AstralTree_TopsGlow", (AssetRequestMode)2);
	}

	public override Color GetGlowColor(int i, int j)
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		float brightness = 1f;
		float declareThisHereToPreventRunningTheSameCalculationMultipleTimes = (float)Main.GameUpdateCount * 0.012f;
		brightness *= MathF.Sin((float)i / 18f + declareThisHereToPreventRunningTheSameCalculationMultipleTimes);
		brightness *= MathF.Sin((float)j / 18f + declareThisHereToPreventRunningTheSameCalculationMultipleTimes);
		brightness *= MathF.Sin((float)i * 18f + declareThisHereToPreventRunningTheSameCalculationMultipleTimes);
		brightness *= MathF.Sin((float)j * 18f + declareThisHereToPreventRunningTheSameCalculationMultipleTimes);
		brightness = MathHelper.Clamp(brightness, 0f, 1f);
		return Color.White * MathHelper.Lerp(0.1f, 1f, brightness);
	}

	public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight)
	{
	}

	public override int DropWood()
	{
		return ModContent.ItemType<AstralMonolith>();
	}

	public override int CreateDust()
	{
		return ModContent.DustType<AstralBasic>();
	}

	public override int SaplingGrowthType(ref int style)
	{
		style = 0;
		return ModContent.TileType<AstralTreeSapling>();
	}

	public override int TreeLeaf()
	{
		return ModContent.GoreType<AstralLeaf>();
	}

	public override bool Shake(int x, int y, ref bool createLeaves)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		Vector2 worldPosition = Utils.ToWorldCoordinates(new Vector2((float)x, (float)y), 8f, 8f);
		Player nearestPlayer = Main.player[Player.FindClosest(worldPosition, 16, 16)];
		if (nearestPlayer.active && nearestPlayer.HeldItem.type == ModContent.ItemType<FellerofEvergreens>() && WorldGen.genRand.NextBool(3))
		{
			int treeDropItemType = (WorldGen.genRand.NextBool() ? ModContent.ItemType<Barberry>() : ModContent.ItemType<Lotus>());
			Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), x * 16, y * 16, 16, 16, treeDropItemType);
		}
		int randAmt = Main.rand.Next(1, 3);
		if (Main.getGoodWorld && Main.rand.NextBool(15))
		{
			Projectile.NewProjectile(new EntitySource_ShakeTree(x, y), x * 16, y * 16, Main.rand.NextFloat(-100f, 100f) * 0.002f, 0f, 28, 0, 0f, Player.FindClosest(new Vector2((float)(x * 16), (float)(y * 16)), 16, 16));
		}
		else if (Main.rand.NextBool(7))
		{
			createLeaves = true;
			Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), x * 16, y * 16, 16, 16, 27, randAmt);
		}
		else if (Main.rand.NextBool(35) && Main.halloween)
		{
			createLeaves = true;
			Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), x * 16, y * 16, 16, 16, 1809, randAmt);
		}
		else if (Main.rand.NextBool(12))
		{
			createLeaves = true;
			Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), x * 16, y * 16, 16, 16, DropWood(), Main.rand.Next(1, 4));
		}
		else if (Main.rand.NextBool(20))
		{
			createLeaves = true;
			int coin = 71;
			int amount = Main.rand.Next(50, 100);
			if (Main.rand.NextBool(30))
			{
				coin = 73;
				amount = 1;
				if (Main.rand.NextBool(5))
				{
					amount++;
				}
				if (Main.rand.NextBool(10))
				{
					amount++;
				}
			}
			else if (Main.rand.NextBool(10))
			{
				coin = 72;
				amount = Main.rand.Next(1, 21);
				if (Main.rand.NextBool(3))
				{
					amount += Main.rand.Next(1, 21);
				}
				if (Main.rand.NextBool(4))
				{
					amount += Main.rand.Next(1, 21);
				}
			}
			Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), x * 16, y * 16, 16, 16, coin, amount);
		}
		else if (Main.rand.NextBool(20))
		{
			createLeaves = true;
			int type = ModContent.NPCType<Twinkler>();
			if (Main.raining)
			{
				type = 484;
			}
			NPC.NewNPC(new EntitySource_ShakeTree(x, y), x * 16, y * 16, type);
		}
		else if (Main.rand.NextBool(15))
		{
			createLeaves = true;
			int type2 = ModContent.ItemType<StarblightSoot>();
			if (!Main.dayTime && Main.rand.NextBool())
			{
				type2 = 75;
			}
			Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2((float)x, (float)y) * 16f, type2, randAmt);
		}
		else if (Main.rand.NextBool(12))
		{
			int fruitType = (Main.rand.NextBool() ? ModContent.ItemType<Barberry>() : ModContent.ItemType<Lotus>());
			Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2((float)x, (float)y) * 16f, fruitType);
		}
		return false;
	}
}
