using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class RefractivePrismTorch : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.Torches[base.Type] = true;
		ItemID.Sets.SingleUseInGamepad[base.Type] = true;
		ItemID.Sets.WaterTorches[base.Type] = true;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = 5353;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToTorch(ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.RefractivePrismTorch>(), 0, allowWaterPlacement: true);
	}

	public override void HoldItem(Player player)
	{
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool((player.itemAnimation > 0) ? 10 : 20))
		{
			Dust.NewDust(new Vector2(player.itemLocation.X + 16f * (float)player.direction, player.itemLocation.Y - 14f * player.gravDir), 4, 4, Main.rand.Next(68, 71));
		}
		Lighting.AddLight(player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * (float)player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), reverseRotation: true), 1f, 0.9f, 1.2f);
	}

	public override void PostUpdate()
	{
		Lighting.AddLight((int)((base.Item.position.X + (float)(base.Item.width / 2)) / 16f), (int)((base.Item.position.Y + (float)(base.Item.height / 2)) / 16f), 1f, 0.9f, 1.2f);
	}

	public override void AddRecipes()
	{
		CreateRecipe(3).AddIngredient(8, 3).AddIngredient<PrismShard>().Register();
	}
}
