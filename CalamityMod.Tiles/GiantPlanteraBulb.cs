using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ObjectData;

namespace CalamityMod.Tiles;

public class GiantPlanteraBulb : GlowMaskTile
{
	public override void SetupStatic()
	{
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		GlowMaskPaintInteraction = PaintColorTint.None;
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		TileMaterials.SetForTileId(base.Type, TileMaterials._materialsByName["Plant"]);
		TileID.Sets.PreventsTileRemovalIfOnTopOfIt[base.Type] = true;
		TileID.Sets.PreventsTileHammeringIfOnTopOfIt[base.Type] = true;
		TileID.Sets.PreventsTileReplaceIfOnTopOfIt[base.Type] = true;
		TileID.Sets.PreventsSandfall[base.Type] = true;
		TileObjectData.newTile.Width = 5;
		TileObjectData.newTile.Height = 5;
		TileObjectData.newTile.Origin = new Point16(2, 4);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.CoordinateHeights = new int[5] { 16, 16, 16, 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.WaterDeath = false;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(107, 125, 33), CalamityUtils.GetText("Tiles.GiantBulb"));
		AddMapEntry(new Color(243, 82, 171), CalamityUtils.GetText("Tiles.GiantBulb"));
		base.AnimationFrameHeight = 90;
		base.MineResist = 3f;
		base.DustType = 168;
		base.HitSound = SoundID.Grass;
	}

	public override ushort GetMapOption(int i, int j)
	{
		return Main.hardMode ? ((ushort)1) : ((ushort)0);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		type = ((!WorldGen.genRand.NextBool(3) && Main.hardMode) ? 166 : 167);
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 2);
	}

	public override bool CanKillTile(int i, int j, ref bool blockDamaged)
	{
		return Main.hardMode;
	}

	public override bool CanExplode(int i, int j)
	{
		return Main.hardMode;
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		float x = i * 16;
		float y = j * 16;
		float distanceFromPlayer = -1f;
		int player = 0;
		for (int playerIndex = 0; playerIndex < 255; playerIndex++)
		{
			float dist = Math.Abs(Main.player[playerIndex].position.X - x) + Math.Abs(Main.player[playerIndex].position.Y - y);
			if (dist < distanceFromPlayer || distanceFromPlayer == -1f)
			{
				player = playerIndex;
				distanceFromPlayer = dist;
			}
		}
		if (distanceFromPlayer / 16f >= 50f)
		{
			return;
		}
		float projectileVelocity = 6f;
		int projType = 228;
		int npcType = 265;
		Vector2 spawn = default(Vector2);
		((Vector2)(ref spawn))._002Ector((float)((i + 2) * 16 + 8), (float)((j + 4) * 16 + 8));
		Vector2 dustSpawn = default(Vector2);
		((Vector2)(ref dustSpawn))._002Ector((float)((i + 2) * 16), (float)((j + 4) * 16));
		SoundEngine.PlaySound(in SoundID.Item74, spawn);
		Vector2 destination = new Vector2((float)((i + 2) * 16 + 8), (float)(j * 16 + 8)) - spawn;
		((Vector2)(ref destination)).Normalize();
		destination *= projectileVelocity;
		int numProj = 30;
		int numNPCs = 10;
		float rotation = MathHelper.ToRadians(100f);
		for (int projIndex = 0; projIndex < numProj; projIndex++)
		{
			Vector2 perturbedSpeed = destination.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)projIndex / (float)(numProj - 1))) * (Main.rand.NextFloat() + 0.25f);
			if (Main.netMode != 1)
			{
				Projectile.NewProjectile(new EntitySource_TileBreak(i, j), spawn, perturbedSpeed, projType, 0, 0f, Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16));
			}
			perturbedSpeed *= 2f;
			Dust.NewDustDirect(dustSpawn, 16, 16, 44, perturbedSpeed.X, perturbedSpeed.Y, 250).fadeIn = 0.7f;
			Dust.NewDustDirect(dustSpawn, 16, 16, (!WorldGen.genRand.NextBool(3) && Main.hardMode) ? 166 : 167, perturbedSpeed.X, perturbedSpeed.Y);
		}
		for (int npcIndex = 0; npcIndex < numNPCs; npcIndex++)
		{
			Vector2 perturbedSpeed2 = destination.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)npcIndex / (float)(numNPCs - 1))) * (Main.rand.NextFloat() + 0.5f) * 0.5f;
			if (Main.netMode != 1)
			{
				int spore = NPC.NewNPC(new EntitySource_TileBreak(i, j), (int)spawn.X, (int)spawn.Y, npcType, 0, -1f);
				Main.npc[spore].velocity.X = perturbedSpeed2.X;
				Main.npc[spore].velocity.Y = perturbedSpeed2.Y;
				Main.npc[spore].netUpdate = true;
			}
			perturbedSpeed2 *= 2f;
			Dust.NewDustDirect(dustSpawn, 16, 16, 44, perturbedSpeed2.X, perturbedSpeed2.Y, 250).fadeIn = 0.7f;
			Dust.NewDustDirect(dustSpawn, 16, 16, 166, perturbedSpeed2.X, perturbedSpeed2.Y);
		}
		NPC.SpawnOnPlayer(player, 262);
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		if (!Main.hardMode)
		{
			frame = 0;
			return;
		}
		frameCounter++;
		if (frameCounter > 25)
		{
			frameCounter = 0;
			frame++;
			if (frame > 6)
			{
				frame = 1;
			}
		}
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.hardMode)
		{
			r = 0.486f;
			g = 0.164f;
			b = 0.342f;
		}
	}

	public override Color GetGlowMaskColor(int i, int j, TileDrawInfo drawData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.Yellow;
	}
}
