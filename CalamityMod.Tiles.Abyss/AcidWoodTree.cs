using CalamityMod.Gores.Trees;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.FurnitureAcidwood;
using CalamityMod.Items.Potions.Food;
using CalamityMod.Items.Tools;
using CalamityMod.NPCs.AcidRain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Abyss;

public class AcidWoodTree : ModPalmTree
{
	public override TreePaintingSettings TreeShaderSettings => new TreePaintingSettings
	{
		UseSpecialGroups = true,
		SpecialGroupMinimalHueValue = 0.153f,
		SpecialGroupMaximumHueValue = 0.25f,
		SpecialGroupMinimumSaturationValue = 0.8802f,
		SpecialGroupMaximumSaturationValue = 1f
	};

	public override void SetStaticDefaults()
	{
		base.GrowsOnTileId = new int[1] { ModContent.TileType<SulphurousSand>() };
	}

	public override Asset<Texture2D> GetTopTextures()
	{
		return ModContent.Request<Texture2D>("CalamityMod/Tiles/Abyss/AcidWoodTreeTops", (AssetRequestMode)2);
	}

	public override Asset<Texture2D> GetTexture()
	{
		return ModContent.Request<Texture2D>("CalamityMod/Tiles/Abyss/AcidWoodTree", (AssetRequestMode)2);
	}

	public override Asset<Texture2D> GetOasisTopTextures()
	{
		return ModContent.Request<Texture2D>("CalamityMod/Tiles/Abyss/AcidWoodTreeOasisTops", (AssetRequestMode)2);
	}

	public override int DropWood()
	{
		return ModContent.ItemType<Acidwood>();
	}

	public override int CreateDust()
	{
		return 75;
	}

	public override int SaplingGrowthType(ref int style)
	{
		style = 0;
		return ModContent.TileType<AcidWoodTreeSapling>();
	}

	public override int TreeLeaf()
	{
		return ModContent.GoreType<SulphurLeaf>();
	}

	public override bool Shake(int x, int y, ref bool createLeaves)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 worldPosition = Utils.ToWorldCoordinates(new Vector2((float)x, (float)y), 8f, 8f);
		Player nearestPlayer = Main.player[Player.FindClosest(worldPosition, 16, 16)];
		if (nearestPlayer.active && nearestPlayer.HeldItem.type == ModContent.ItemType<FellerofEvergreens>() && WorldGen.genRand.NextBool(3))
		{
			int treeDropItemType = (WorldGen.genRand.NextBool() ? ModContent.ItemType<Jackfruit>() : ModContent.ItemType<Salak>());
			Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), x * 16, y * 16, 16, 16, treeDropItemType);
		}
		int randAmt = Main.rand.Next(1, 3);
		if (Main.getGoodWorld && Main.rand.NextBool(15))
		{
			Projectile.NewProjectile(new EntitySource_ShakeTree(x, y), x * 16, y * 16, Main.rand.NextFloat(-100f, 100f) * 0.002f, 0f, 28, 0, 0f, Player.FindClosest(new Vector2((float)(x * 16), (float)(y * 16)), 16, 16));
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
			int type = ModContent.NPCType<BabyFlakCrab>();
			NPC.NewNPC(new EntitySource_ShakeTree(x, y), x * 16, y * 16, type);
		}
		else if (Main.rand.NextBool(15))
		{
			createLeaves = true;
			int type2 = -1;
			if (DownedBossSystem.downedEoCAcidRain)
			{
				type2 = ModContent.ItemType<SulphuricScale>();
			}
			if (DownedBossSystem.downedAquaticScourgeAcidRain && Main.rand.NextBool())
			{
				type2 = ModContent.ItemType<CorrodedFossil>();
			}
			if (type2 != -1)
			{
				Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2((float)x, (float)y) * 16f, type2, randAmt);
			}
		}
		else if (Main.rand.NextBool(12))
		{
			int fruitType = (Main.rand.NextBool() ? ModContent.ItemType<Jackfruit>() : ModContent.ItemType<Salak>());
			Item.NewItem(WorldGen.GetItemSource_FromTreeShake(x, y), new Vector2((float)x, (float)y) * 16f, fruitType);
		}
		return false;
	}
}
