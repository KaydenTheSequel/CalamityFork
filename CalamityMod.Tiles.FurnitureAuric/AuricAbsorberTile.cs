using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureAuric;

public class AuricAbsorberTile : ModTile
{
	public override void SetStaticDefaults()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		base.HitSound = SoundID.NPCHit34;
		AddMapEntry(new Color(192, 237, 255));
		base.DustType = 226;
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		Tile tileCache = Main.tile[i, j];
		if (!tileCache.IsTileActuallyInvisible())
		{
			TileFramingSystem.SlopedGlowmask(in tileCache, i, j, TextureAssets.Tile[base.Type].Value, null, CalamityUtils.ApplyPaint(Main.tile[i, j].TileColor, Color.White), default(Vector2));
		}
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		return TileFramingSystem.BetterGemsparkFraming(i, j, resetFrame);
	}
}
