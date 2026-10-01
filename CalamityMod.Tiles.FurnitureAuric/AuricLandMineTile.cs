using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalamityMod.Tiles.FurnitureAuric;

public class AuricLandMineTile : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileLighted[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		TileID.Sets.PressurePlate[base.Type] = 0;
		TileID.Sets.PreventsTileRemovalIfOnTopOfIt[base.Type] = true;
		TileID.Sets.PreventsTileHammeringIfOnTopOfIt[base.Type] = true;
		TileID.Sets.PreventsSandfall[base.Type] = true;
		Main.tileNoAttach[base.Type] = true;
		Main.tileLavaDeath[base.Type] = false;
		Main.tileWaterDeath[base.Type] = false;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
		TileObjectData.newTile.CoordinateHeights = new int[1] { 18 };
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.addTile(base.Type);
		base.MinPick = 250;
	}

	public override void HitSwitch(int i, int j)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		Vector2 tileCenter = new Vector2((float)i, (float)j) * 16f + Vector2.One * 8f;
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DudFire");
		style.Pitch = 0.8f;
		SoundEngine.PlaySound(in style, tileCenter);
		GeneralParticleHandler.SpawnParticle(new GenericSparkle(tileCenter, Vector2.Zero, Color.Goldenrod, Color.Gold, 2.5f, 9, Main.rand.NextFloat(-0.01f, 0.01f), 2.68f));
		WorldGen.KillTile(i, j, fail: false, effectOnly: false, noItem: true);
		NetMessage.SendTileSquare(-1, i, j);
		Main.LocalPlayer.RemoveAllIFrames();
		Projectile.NewProjectile(new EntitySource_TileInteraction(Main.LocalPlayer, i, j), tileCenter, Vector2.Zero, ModContent.ProjectileType<AuricLandMineExplosion>(), 40000, 0f);
	}

	public override void HitWire(int i, int j)
	{
		Wiring.HitSwitchAndSync(i, j);
	}

	public override bool IsTileDangerous(int i, int j, Player player)
	{
		return true;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 226);
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
