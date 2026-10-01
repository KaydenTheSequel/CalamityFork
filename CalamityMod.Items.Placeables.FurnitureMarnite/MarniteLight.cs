using CalamityMod.Tiles.FurnitureMarnite;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.FurnitureMarnite;

public class MarniteLight : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.Torches[base.Type] = true;
		ItemID.Sets.SingleUseInGamepad[base.Type] = true;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = 5353;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToTorch(ModContent.TileType<global::CalamityMod.Tiles.FurnitureMarnite.MarniteLight>(), 0);
		base.Item.value = Item.sellPrice(0, 0, 0, 20);
	}

	public override void HoldItem(Player player)
	{
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool((player.itemAnimation > 0) ? 10 : 20))
		{
			Dust.NewDust(new Vector2(player.itemLocation.X + 16f * (float)player.direction, player.itemLocation.Y - 14f * player.gravDir), 4, 4, 180);
		}
		Lighting.AddLight(player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * (float)player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), reverseRotation: true), 19f / 85f, 82f / 85f, 1f);
	}

	public override void PostUpdate()
	{
		Lighting.AddLight((int)((base.Item.position.X + (float)(base.Item.width / 2)) / 16f), (int)((base.Item.position.Y + (float)(base.Item.height / 2)) / 16f), 19f / 85f, 82f / 85f, 1f);
	}

	public override void AddRecipes()
	{
		CreateRecipe(3).AddIngredient(8, 3).AddIngredient<PolishedMarniteBlock>().Register();
	}
}
