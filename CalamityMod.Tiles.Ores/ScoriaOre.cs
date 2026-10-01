using CalamityMod.Projectiles.Environment;
using CalamityMod.Tiles.Abyss;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Ores;

[LegacyName(new string[] { "ChaoticOre" })]
public class ScoriaOre : GlowMaskTile
{
	public override void SetupStatic()
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileOreFinderPriority[base.Type] = 850;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithAbyss(base.Type);
		TileID.Sets.Ore[base.Type] = true;
		base.DustType = 105;
		AddMapEntry(new Color(167, 80, 22), CreateMapEntryName());
		base.MineResist = 3f;
		base.MinPick = 210;
		base.HitSound = SoundID.Tink;
		this.RegisterBlendMergeWith(ModContent.TileType<AbyssGravel>());
		this.RegisterBlendMergeWith(ModContent.TileType<PyreMantle>());
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		Tile up = Main.tile[i, j - 1];
		Tile up2 = Main.tile[i, j - 2];
		if (closer && Main.rand.NextBool(30) && !up.HasTile && !up2.HasTile)
		{
			Dust obj = Main.dust[Dust.NewDust(new Vector2((float)i * 16f, (float)j * 16f), 16, 16, 127, 0f, -10f, 47, new Color(255, 255, 255), 1.0465117f)];
			obj.noGravity = true;
			obj.fadeIn = 1.2209302f;
			Dust obj2 = Main.dust[Dust.NewDust(new Vector2((float)i * 16f, (float)j * 16f), 16, 16, 31, 0f, -1.9069767f, 195, new Color(255, 255, 255))];
			obj2.noGravity = false;
			obj2.fadeIn = 1.4209301f;
		}
		if (!Main.gamePaused && closer && Main.rand.NextBool(400))
		{
			int tileLocationY = j + 1;
			if (Main.tile[i, tileLocationY] != null && !Main.tile[i, tileLocationY].HasTile && Main.netMode != 1)
			{
				Projectile.NewProjectile(new EntitySource_WorldEvent(), i * 16 + 16, tileLocationY * 16 + 16, 0f, 0.1f, ModContent.ProjectileType<LavaChunk>(), 25, 2f, Main.myPlayer);
			}
		}
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		global::CalamityMod.World.Abyss.FillTileWithWater(i, j);
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.04f;
		g = 0f;
		b = 0f;
	}

	public override Color GetGlowMaskColor(int i, int j, TileDrawInfo drawData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return Color.White * 0.195f;
	}
}
