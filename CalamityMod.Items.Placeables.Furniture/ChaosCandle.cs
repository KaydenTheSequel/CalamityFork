using CalamityMod.Items.Materials;
using CalamityMod.Items.Potions;
using CalamityMod.Tiles.Furniture;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class ChaosCandle : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToTorch(ModContent.TileType<global::CalamityMod.Tiles.Furniture.ChaosCandle>(), 0);
		base.Item.value = Item.sellPrice(0, 0, 1);
		base.Item.rare = 1;
	}

	public override void HoldItem(Player player)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().chaosCandle = true;
		if (!Collision.DrownCollision(player.position, player.width, player.height, player.gravDir) && !base.Item.wet)
		{
			if (Main.rand.NextBool((player.itemAnimation > 0) ? 10 : 20))
			{
				Dust.NewDust(new Vector2(player.itemLocation.X + 12f * (float)player.direction, player.itemLocation.Y - 10f * player.gravDir), 4, 4, 235);
			}
			player.itemLocation.Y += 8f;
			Lighting.AddLight(player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * (float)player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), reverseRotation: true), 0.85f, 0.25f, 0.25f);
		}
	}

	public override void PostUpdate()
	{
		if (!base.Item.wet)
		{
			Lighting.AddLight((int)((base.Item.position.X + (float)(base.Item.width / 2)) / 16f), (int)((base.Item.position.Y + (float)(base.Item.height / 2)) / 16f), 0.85f, 0.25f, 0.25f);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(148).AddIngredient<ZergPotion>().AddIngredient<EssenceofHavoc>(2)
			.AddTile(18)
			.Register();
	}
}
