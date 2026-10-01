using System;
using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Environment;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.Abyss;

[LegacyName(new string[] { "SteamGeyser" })]
public class SteamGeyser1 : ModTile
{
	private int steamTimer;

	public override void SetStaticDefaults()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		Main.tileFrameImportant[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile(base.Type);
		AddMapEntry(new Color(103, 65, 64), CalamityUtils.GetText(LocalizationCategory + ".SteamGeyser.MapEntry"));
		base.DustType = 75;
		base.SetStaticDefaults();
	}

	public override bool IsTileDangerous(int i, int j, Player player)
	{
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 2);
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		Tile t = CalamityUtils.ParanoidTileRetrieval(i, j);
		Vector2 spawnPosition = default(Vector2);
		((Vector2)(ref spawnPosition))._002Ector((float)i * 16f + 24f, (float)j * 16f - 4f);
		if (!Main.gamePaused && t.TileFrameX % 36 == 0 && t.TileFrameY % 36 == 0 && Collision.CanHitLine(spawnPosition, 1, 1, spawnPosition - Vector2.UnitY * 100f, 1, 1))
		{
			steamTimer += Main.rand.Next(0, 2);
			if (steamTimer >= 240)
			{
				steamTimer = 0;
			}
		}
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		Tile t = CalamityUtils.ParanoidTileRetrieval(i, j);
		Vector2 spawnPosition = default(Vector2);
		((Vector2)(ref spawnPosition))._002Ector((float)i * 16f + 24f, (float)j * 16f - 4f);
		if (!Main.gamePaused && !CalamityPlayer.areThereAnyDamnBosses && t.TileFrameX % 36 == 0 && t.TileFrameY % 36 == 0 && Collision.CanHitLine(spawnPosition, 1, 1, spawnPosition - Vector2.UnitY * 100f, 1, 1) && steamTimer > 180 && Main.rand.NextBool(3))
		{
			float positionInterpolant = (float)(i + j) * 0.041f % 1f;
			Vector2 smokeVelocity = -Vector2.UnitY.RotatedByRandom(0.10999999940395355) * MathHelper.Lerp(4.8f, 8.1f, positionInterpolant);
			smokeVelocity.X += (float)Math.Cos((float)Math.PI * 2f * positionInterpolant) * 1.1f;
			smokeVelocity.Y -= Main.rand.Next(3, 6);
			Projectile.NewProjectile(new EntitySource_WorldEvent(), spawnPosition, smokeVelocity, ModContent.ProjectileType<HotSteam>(), Main.expertMode ? 15 : 23, 0f);
		}
	}
}
