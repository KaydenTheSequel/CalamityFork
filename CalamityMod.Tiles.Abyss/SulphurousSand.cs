using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Enemy;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Abyss;

[LegacyName(new string[] { "SulphurousSandNoWater" })]
public class SulphurousSand : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Sand"]);
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithAbyss(base.Type);
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
		base.DustType = 32;
		AddMapEntry(new Color(150, 100, 50));
		base.HitSound = SoundID.Dig;
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(1);
		this.RegisterBlendMergeWith(ModContent.TileType<SulphurousSandstone>());
		this.RegisterBlendMergeWith(53);
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		if (CalamityUtils.ParanoidTileRetrieval(i, j + 1).HasTile && CalamityUtils.ParanoidTileRetrieval(i, j + 1).TileType == (ushort)ModContent.TileType<SulphurousVines>())
		{
			WorldGen.KillTile(i, j + 1);
		}
	}

	public override void RandomUpdate(int i, int j)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		int tileLocationY = j - 1;
		if (Main.tile[i, tileLocationY] != null && !Main.tile[i, tileLocationY].HasTile)
		{
			if (Main.netMode != 1 && !CalamityPlayer.areThereAnyDamnBosses)
			{
				bool nearby = false;
				Vector2 tileWorldPos = Utils.ToWorldCoordinates(new Vector2((float)i, (float)tileLocationY), 8f, 8f);
				ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Player player = enumerator.Current;
					if (!player.dead && !player.ghost && player.DistanceSQ(tileWorldPos) < 4000000f)
					{
						nearby = true;
						break;
					}
				}
				if (nearby && Main.tile[i, tileLocationY].LiquidAmount == byte.MaxValue && Main.tile[i, tileLocationY - 1].LiquidAmount == byte.MaxValue && Main.tile[i, tileLocationY - 2].LiquidAmount == byte.MaxValue && !Main.tile[i, tileLocationY - 2].HasTile)
				{
					Projectile.NewProjectile(new EntitySource_WorldEvent(), i * 16 + 16, tileLocationY * 16 + 16, 0f, -0.1f, ModContent.ProjectileType<SulphuricAcidBubble>(), 0, 2f, Main.myPlayer);
				}
			}
			if ((i < 250 || i > Main.maxTilesX - 250) && Main.rand.NextBool(400))
			{
				if (Main.tile[i, tileLocationY].LiquidAmount == byte.MaxValue)
				{
					int ambientObjectDetectRadius = 7;
					int ambientObjectMax = 6;
					int ambientObjectAmt = 0;
					for (int l = i - ambientObjectDetectRadius; l <= i + ambientObjectDetectRadius; l++)
					{
						for (int m = tileLocationY - ambientObjectDetectRadius; m <= tileLocationY + ambientObjectDetectRadius; m++)
						{
							if (Main.tile[l, m].HasTile && Main.tile[l, m].TileType == 81)
							{
								ambientObjectAmt++;
							}
						}
					}
					if (ambientObjectAmt < ambientObjectMax && Main.tile[i, tileLocationY - 1].LiquidAmount == byte.MaxValue && Main.tile[i, tileLocationY - 2].LiquidAmount == byte.MaxValue && Main.tile[i, tileLocationY - 3].LiquidAmount == byte.MaxValue && Main.tile[i, tileLocationY - 4].LiquidAmount == byte.MaxValue)
					{
						WorldGen.PlaceTile(i, tileLocationY, 81, mute: true);
						if (Main.dedServ && Main.tile[i, tileLocationY].HasTile)
						{
							NetMessage.SendTileSquare(-1, i, tileLocationY, 1);
						}
					}
				}
				else if (Main.tile[i, tileLocationY].LiquidAmount == 0)
				{
					int ambientObjectDetectRadius2 = 7;
					int ambientObjectMax2 = 6;
					int ambientObjectAmt2 = 0;
					for (int k = i - ambientObjectDetectRadius2; k <= i + ambientObjectDetectRadius2; k++)
					{
						for (int n = tileLocationY - ambientObjectDetectRadius2; n <= tileLocationY + ambientObjectDetectRadius2; n++)
						{
							if (Main.tile[k, n].HasTile && Main.tile[k, n].TileType == 324)
							{
								ambientObjectAmt2++;
							}
						}
					}
					if (ambientObjectAmt2 < ambientObjectMax2)
					{
						WorldGen.PlaceTile(i, tileLocationY, 324, mute: true, forced: false, -1, Main.rand.Next(2));
						if (Main.dedServ && Main.tile[i, tileLocationY].HasTile)
						{
							NetMessage.SendTileSquare(-1, i, tileLocationY, 1);
						}
					}
				}
			}
		}
		int vineLength = WorldGen.genRand.Next((int)Main.rockLayer, (int)(Main.rockLayer + (double)Main.maxTilesY * 0.143));
		int nearbyVineCount = 0;
		for (int x = i - 15; x <= i + 15; x++)
		{
			for (int y = j - 15; y <= j + 15; y++)
			{
				if (WorldGen.InWorld(x, y) && CalamityUtils.ParanoidTileRetrieval(x, y).HasTile && CalamityUtils.ParanoidTileRetrieval(x, y).TileType == (ushort)ModContent.TileType<SulphurousVines>())
				{
					nearbyVineCount++;
				}
			}
		}
		if (!(Main.tile[i, j + 1] != null) || nearbyVineCount >= 5 || j < SulphurousSea.VineGrowTopLimit || Main.tile[i, j + 1].HasTile || Main.tile[i, j + 1].TileType == (ushort)ModContent.TileType<SulphurousVines>() || Main.tile[i, j + 1].LiquidAmount != byte.MaxValue || Main.tile[i, j + 1].LiquidType == 1)
		{
			return;
		}
		bool canGrowVine = false;
		for (int k2 = vineLength; k2 > vineLength - 10; k2--)
		{
			if (Main.tile[i, k2].BottomSlope)
			{
				canGrowVine = false;
				break;
			}
			if (Main.tile[i, k2].HasTile && !Main.tile[i, k2].BottomSlope)
			{
				canGrowVine = true;
				break;
			}
		}
		if (canGrowVine)
		{
			int vineY = j + 1;
			Main.tile[i, vineY].TileType = (ushort)ModContent.TileType<SulphurousVines>();
			Main.tile[i, vineY].Get<TileWallWireStateData>().HasTile = true;
			WorldGen.SquareTileFrame(i, vineY);
			if (Main.dedServ)
			{
				NetMessage.SendTileSquare(-1, i, vineY, 3);
			}
		}
		Main.tile[i, j].Get<TileWallWireStateData>().Slope = SlopeType.Solid;
		Main.tile[i, j].Get<TileWallWireStateData>().IsHalfBlock = false;
	}
}
