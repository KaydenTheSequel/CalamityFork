using CalamityMod.Items.Placeables.FurnitureNavystone.FurnitureAncientNavystone;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.FurnitureNavystone.FurnitureAncientNavystone;

[LegacyName(new string[] { "EutrophicChandelier" })]
public class AncientNavystoneChandelier : ModTile
{
	public override void SetStaticDefaults()
	{
		this.SetUpChandelier(ModContent.ItemType<global::CalamityMod.Items.Placeables.FurnitureNavystone.FurnitureAncientNavystone.AncientNavystoneChandelier>());
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		return CalamityUtils.DrawSwayingMultiTile(i, j);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 51, 0f, 0f, 1, new Color(54, 69, 72));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.tile[i, j].TileFrameX < 18)
		{
			r = 0.5f;
			g = 1f;
			b = 1f;
		}
		else
		{
			r = 0f;
			g = 0f;
			b = 0f;
		}
	}

	public override void HitWire(int i, int j)
	{
		FurnitureCommon.LightHitWire(base.Type, i, j, 3, 3);
	}
}
