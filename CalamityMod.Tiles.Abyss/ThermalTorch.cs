using System.Linq;
using CalamityMod.Graphics;
using CalamityMod.Items.Placeables.Furniture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Abyss;

public class ThermalTorch : ModTile
{
	public Asset<Texture2D> FlameTexture;

	public override void SetStaticDefaults()
	{
		this.SetUpTorch(ModContent.ItemType<global::CalamityMod.Items.Placeables.Furniture.ThermalTorch>(), waterImmune: true, lavaImmune: true);
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 1, 0f, 0f, 1, new Color(200, 0, 0));
		return false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = ModContent.ItemType<global::CalamityMod.Items.Placeables.Furniture.ThermalTorch>();
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.tile[i, j].TileFrameX < 66)
		{
			r = 2f;
			g = 0.5f;
			b = 0.5f;
		}
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = 0;
		if (WorldGen.SolidTile(i, j - 1))
		{
			offsetY = 2;
			if (WorldGen.SolidTile(i - 1, j + 1) || WorldGen.SolidTile(i + 1, j + 1))
			{
				offsetY = 4;
			}
		}
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 pos = Utils.ToWorldCoordinates(new Point(i, j), 8f, 8f);
		if (closer || !Main.LocalPlayer.Calamity().ZoneAbyss || Main.gamePaused)
		{
			return;
		}
		if (EnhancedDarknessSystem.lights.Any(delegate(EnhancedDarknessSystem.LightSource x)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			return x.center == pos;
		}))
		{
			EnhancedDarknessSystem.lights.First(delegate(EnhancedDarknessSystem.LightSource x)
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0007: Unknown result type (might be due to invalid IL or missing references)
				return x.center == pos;
			}).lifetime = 5;
		}
		else
		{
			EnhancedDarknessSystem.lights.Add(new EnhancedDarknessSystem.LightSource(pos, null, 2f)
			{
				lifetime = 5
			});
		}
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		if (FlameTexture == null)
		{
			FlameTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Abyss/ThermalTorchFlame", (AssetRequestMode)2);
		}
		CalamityUtils.DrawFlameEffect(FlameTexture.Value, i, j, 2);
	}

	public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
	{
		if (Main.tile[i, j].TileFrameX < 66)
		{
			CalamityUtils.DrawFlameSparks(60, 5, i, j);
		}
	}

	public override bool RightClick(int i, int j)
	{
		FurnitureCommon.RightClickBreak(i, j);
		return true;
	}

	public override float GetTorchLuck(Player player)
	{
		if (!player.Calamity().ZoneAbyss)
		{
			return -1f;
		}
		return 1f;
	}
}
