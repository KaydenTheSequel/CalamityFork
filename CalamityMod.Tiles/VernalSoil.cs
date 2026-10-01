using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles;

public class VernalSoil : ModTile
{
	public Asset<Texture2D> GrassTexture;

	private int extraFrameHeight = 36;

	private int extraFrameWidth = 90;

	public override void SetStaticDefaults()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		GrassTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/VernicFernGrass", (AssetRequestMode)2);
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = false;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Organic"]);
		CalamityUtils.MergeWithGeneral(base.Type);
		TileID.Sets.CanBeDugByShovel[base.Type] = true;
		base.DustType = 38;
		AddMapEntry(new Color(80, 120, 0));
		base.HitSound = SoundID.Dig;
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(1);
		this.RegisterBlendMergeWith(59);
	}

	public override void PostTileFrame(int i, int j, int up, int down, int left, int right, int upLeft, int upRight, int downLeft, int downRight)
	{
		if (Main.tile[i - 1, j - 1].TileType != base.Type || Main.tile[i, j - 1].TileType != base.Type || Main.tile[i + 1, j - 1].TileType != base.Type || Main.tile[i - 1, j - 2].TileType != base.Type || Main.tile[i, j - 2].TileType != base.Type || Main.tile[i + 1, j - 2].TileType != base.Type)
		{
			Main.tile[i, j].Get<TileSpecialDrawData>().HasSpecialPoint = true;
		}
		else
		{
			Main.tile[i, j].Get<TileSpecialDrawData>().HasSpecialPoint = false;
		}
	}

	public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
	{
		if (Main.tile[i, j].Get<TileSpecialDrawData>().HasSpecialPoint)
		{
			Main.instance.TilesRenderer.AddSpecialLegacyPoint(i, j);
		}
	}

	public override void RandomUpdate(int i, int j)
	{
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		if (j < (int)Main.worldSurface - 1 || j >= Main.maxTilesY - 20)
		{
			return;
		}
		int j2 = j - 1;
		if (j2 < 10)
		{
			j2 = 10;
		}
		if (Main.tile[i, j2].LiquidAmount != 0 || !WorldGen.genRand.NextBool(15))
		{
			return;
		}
		ushort tileTypeToPlace = (ushort)ModContent.TileType<GiantPlanteraBulb>();
		int tileTypeToPlaceThickness = 5;
		bool placeBulb = true;
		int minDistanceFromOtherBulbs = 10;
		for (int k = i - minDistanceFromOtherBulbs; k < i + minDistanceFromOtherBulbs; k += 2)
		{
			for (int l = j - minDistanceFromOtherBulbs; l < j + minDistanceFromOtherBulbs; l += 2)
			{
				if (k > tileTypeToPlaceThickness && k < Main.maxTilesX - tileTypeToPlaceThickness && l > tileTypeToPlaceThickness && l < Main.maxTilesY - tileTypeToPlaceThickness && Main.tile[k, l].HasTile && Main.tile[k, l].TileType == tileTypeToPlace)
				{
					placeBulb = false;
					break;
				}
			}
		}
		if (!placeBulb || i < tileTypeToPlaceThickness || i > Main.maxTilesX - tileTypeToPlaceThickness || j2 < tileTypeToPlaceThickness || j2 > Main.maxTilesY - tileTypeToPlaceThickness)
		{
			return;
		}
		bool placeTile = true;
		for (int m = i - 2; m < i + 3; m++)
		{
			for (int n = j2 - 4; n < j2 + 1; n++)
			{
				if (Main.tile[m, n] == null)
				{
					return;
				}
				if (Main.tile[m, n].HasTile)
				{
					placeTile = false;
				}
			}
			if (Main.tile[m, j2 + 1] == null)
			{
				return;
			}
			if (!WorldGen.SolidTile2(m, j2 + 1))
			{
				placeTile = false;
			}
		}
		if (!placeTile)
		{
			return;
		}
		WorldGen.PlaceObject(i, j2, tileTypeToPlace, mute: true);
		NetMessage.SendObjectPlacement(-1, i, j2, tileTypeToPlace, 0, 0, -1, -1);
		if (!WorldGen.PlayerLOS(i, j2))
		{
			return;
		}
		float projectileVelocity = 6f;
		int projType = 228;
		int npcType = 265;
		Vector2 spawn = default(Vector2);
		((Vector2)(ref spawn))._002Ector((float)(i * 16 + 8), (float)(j2 * 16 + 8));
		Vector2 dustSpawn = default(Vector2);
		((Vector2)(ref dustSpawn))._002Ector((float)(i * 16), (float)(j2 * 16));
		SoundEngine.PlaySound(in SoundID.Item73, spawn);
		Vector2 destination = new Vector2((float)(i * 16 + 8), (float)((j2 - 2) * 16 + 8)) - spawn;
		((Vector2)(ref destination)).Normalize();
		destination *= projectileVelocity;
		int numProj = 15;
		int numNPCs = 5;
		float rotation = MathHelper.ToRadians(100f);
		for (int projIndex = 0; projIndex < numProj; projIndex++)
		{
			Vector2 perturbedSpeed = destination.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)projIndex / (float)(numProj - 1))) * (Main.rand.NextFloat() + 0.25f);
			if (Main.netMode != 1)
			{
				Projectile.NewProjectile(new EntitySource_TileUpdate(i, j2), spawn, perturbedSpeed, projType, 0, 0f, Player.FindClosest(new Vector2((float)(i * 16), (float)(j2 * 16)), 16, 16));
			}
			perturbedSpeed *= 2f;
			Dust.NewDustDirect(dustSpawn, 16, 16, 44, perturbedSpeed.X, perturbedSpeed.Y, 250).fadeIn = 0.7f;
			Dust.NewDustDirect(dustSpawn, 16, 16, (!WorldGen.genRand.NextBool(3) && Main.hardMode) ? 166 : 167, perturbedSpeed.X, perturbedSpeed.Y);
		}
		if (!Main.hardMode)
		{
			return;
		}
		for (int npcIndex = 0; npcIndex < numNPCs; npcIndex++)
		{
			Vector2 perturbedSpeed2 = destination.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)npcIndex / (float)(numNPCs - 1))) * (Main.rand.NextFloat() + 0.5f) * 0.5f;
			if (Main.netMode != 1)
			{
				int spore = NPC.NewNPC(new EntitySource_TileUpdate(i, j2), (int)spawn.X, (int)spawn.Y, npcType, 0, -1f);
				Main.npc[spore].velocity.X = perturbedSpeed2.X;
				Main.npc[spore].velocity.Y = perturbedSpeed2.Y;
				Main.npc[spore].netUpdate = true;
			}
			perturbedSpeed2 *= 2f;
			Dust.NewDustDirect(dustSpawn, 16, 16, 44, perturbedSpeed2.X, perturbedSpeed2.Y, 250).fadeIn = 0.7f;
			Dust.NewDustDirect(dustSpawn, 16, 16, 166, perturbedSpeed2.X, perturbedSpeed2.Y);
		}
	}

	public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = Main.tile[i, j];
		if (!tile.IsTileActuallyInvisible())
		{
			Tile uptile = Main.tile[i, j - 1];
			if (!uptile.HasTile || !uptile.IsTileFull())
			{
				Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
				Vector2 drawOffset = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + zero;
				Color drawColour = CalamityUtils.ApplyPaint(tile.TileColor, Lighting.GetColor(i, j));
				Texture2D leaves = GrassTexture.Value;
				DrawExtraTop(i, j, leaves, drawOffset, drawColour);
				DrawExtraDrapes(i, j, leaves, drawOffset, drawColour);
			}
		}
	}

	private void DrawExtraTop(int i, int j, Texture2D extras, Vector2 drawOffset, Color drawColour)
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		if (CheckTile(base.Type, equal: false, 0, 1, i, j) || (CheckTile(base.Type, equal: true, 0, 1, i, j) && CheckTile(base.Type, equal: false, 1, 1, i, j) && CheckTile(base.Type, equal: false, -1, 1, i, j) && CheckTile(base.Type, equal: true, 1, 0, i, j) && CheckTile(base.Type, equal: true, -1, 0, i, j)))
		{
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(GetExtraState("middle") + GetExtraVariant(i, j), GetExtraPattern(i), 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(0f, 16f), (Rectangle?)new Rectangle(GetExtraState("middle") + GetExtraVariant(i, j), GetExtraPattern(i) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			DrawExtraOverhang(i, j, extras, drawOffset, drawColour);
		}
	}

	private void DrawExtraWallEnds(int i, int j, Texture2D extras, Vector2 drawOffset, Color drawColour)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		if (CheckTile(base.Type, equal: true, 1, 0, i, j) && CheckTile(base.Type, equal: false, 1, 1, i, j) && CheckTile(base.Type, equal: true, 0, 1, i, j) && (CheckTile(base.Type, equal: true, -1, 1, i, j) || CheckTile(base.Type, equal: false, -1, 0, i, j)))
		{
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(GetExtraState("wallEndLeft") + GetExtraVariant(i + 1, j), GetExtraPattern(i), 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(0f, 16f), (Rectangle?)new Rectangle(GetExtraState("wallEndLeft") + GetExtraVariant(i + 1, j), GetExtraPattern(i) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		if (CheckTile(base.Type, equal: true, -1, 0, i, j) && CheckTile(base.Type, equal: false, -1, 1, i, j) && CheckTile(base.Type, equal: true, 0, 1, i, j) && (CheckTile(base.Type, equal: true, 1, 1, i, j) || CheckTile(base.Type, equal: false, 1, 0, i, j)))
		{
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(GetExtraState("wallEndRight") + GetExtraVariant(i - 1, j), GetExtraPattern(i), 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(0f, 16f), (Rectangle?)new Rectangle(GetExtraState("wallEndRight") + GetExtraVariant(i - 1, j), GetExtraPattern(i) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
	}

	private void DrawExtraOverhang(int i, int j, Texture2D extras, Vector2 drawOffset, Color drawColour)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		if (CheckTile(base.Type, equal: false, -1, 0, i, j))
		{
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(-16f, 0f), (Rectangle?)new Rectangle(GetExtraState("overhangLeft") + GetExtraVariant(i, j), GetExtraPattern(i - 1), 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(-16f, 16f), (Rectangle?)new Rectangle(GetExtraState("overhangLeft") + GetExtraVariant(i, j), GetExtraPattern(i - 1) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		if (CheckTile(base.Type, equal: false, 1, 0, i, j))
		{
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(16f, 0f), (Rectangle?)new Rectangle(GetExtraState("overhangRight") + GetExtraVariant(i, j), GetExtraPattern(i + 1), 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(extras, drawOffset + new Vector2(16f, 16f), (Rectangle?)new Rectangle(GetExtraState("overhangRight") + GetExtraVariant(i, j), GetExtraPattern(i + 1) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
	}

	private void DrawExtraDrapes(int i, int j, Texture2D extras, Vector2 drawOffset, Color drawColour)
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		if ((CheckTile(base.Type, equal: true, 0, 1, i, j) && CheckTile(base.Type, equal: false, 0, 2, i, j)) || (CheckTile(base.Type, equal: true, 0, 2, i, j) && CheckTile(base.Type, equal: false, 1, 2, i, j) && CheckTile(base.Type, equal: false, -1, 2, i, j) && CheckTile(base.Type, equal: true, 1, 1, i, j) && CheckTile(base.Type, equal: true, -1, 1, i, j)))
		{
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(GetExtraState("middle") + GetExtraVariant(i, j - 1), GetExtraPattern(i) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		if (CheckTile(base.Type, equal: true, 1, 1, i, j) && CheckTile(base.Type, equal: false, 1, 2, i, j) && CheckTile(base.Type, equal: true, 0, 2, i, j) && (CheckTile(base.Type, equal: true, -1, 2, i, j) || CheckTile(base.Type, equal: false, -1, 1, i, j)))
		{
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(GetExtraState("wallEndLeft") + GetExtraVariant(i + 1, j - 1), GetExtraPattern(i) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		if (CheckTile(base.Type, equal: true, -1, 1, i, j) && CheckTile(base.Type, equal: false, -1, 2, i, j) && CheckTile(base.Type, equal: true, 0, 2, i, j) && (CheckTile(base.Type, equal: true, 1, 2, i, j) || CheckTile(base.Type, equal: false, 1, 1, i, j)))
		{
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(GetExtraState("wallEndRight") + GetExtraVariant(i - 1, j - 1), GetExtraPattern(i) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		if (CheckTile(base.Type, equal: true, 1, 1, i, j) && CheckTile(base.Type, equal: false, 0, 1, i, j) && CheckTile(base.Type, equal: false, 0, 2, i, j) && CheckTile(base.Type, equal: false, 1, 2, i, j))
		{
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(GetExtraState("overhangLeft") + GetExtraVariant(i + 1, j - 1), GetExtraPattern(i) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		if (CheckTile(base.Type, equal: true, -1, 1, i, j) && CheckTile(base.Type, equal: false, 0, 1, i, j) && CheckTile(base.Type, equal: false, 0, 2, i, j) && CheckTile(base.Type, equal: false, -1, 2, i, j))
		{
			Main.spriteBatch.Draw(extras, drawOffset, (Rectangle?)new Rectangle(GetExtraState("overhangRight") + GetExtraVariant(i - 1, j - 1), GetExtraPattern(i) + 18, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
	}

	private bool CheckTile(int type, bool equal, int x, int y, int i, int j)
	{
		return Main.tile[i + x, j - y].TileType == type == equal;
	}

	private int GetExtraState(string type)
	{
		switch (type)
		{
		case "middle":
			return 36;
		case "overhangLeft":
			return 18;
		case "overhangRight":
			return 54;
		case "wallEndLeft":
			return 0;
		case "wallEndRight":
			return 72;
		default:
			Main.NewText(type.ToString() + " is not a valid Extra sheet state");
			return 0;
		}
	}

	private int GetExtraPattern(int i)
	{
		return i % 3 * extraFrameHeight;
	}

	private int GetExtraVariant(int i, int j)
	{
		return Main.tile[i, j].TileFrameNumber * extraFrameWidth;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
