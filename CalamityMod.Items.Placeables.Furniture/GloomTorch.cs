using CalamityMod.Items.Placeables.Crags;
using CalamityMod.Tiles.Crags;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class GloomTorch : ModItem, ILocalizedModType, IModType
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
		base.Item.DefaultToTorch(ModContent.TileType<global::CalamityMod.Tiles.Crags.GloomTorch>(), 0);
	}

	public override void HoldItem(Player player)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		bool num = Collision.DrownCollision(player.position, player.width, player.height, player.gravDir) || base.Item.wet;
		Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * (float)player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), reverseRotation: true);
		if (!num)
		{
			Lighting.AddLight(position, 0.5f, 0.75f, 1.2f);
		}
	}

	public override void PostUpdate()
	{
		if (!base.Item.wet)
		{
			Lighting.AddLight((int)((base.Item.position.X + (float)(base.Item.width / 2)) / 16f), (int)((base.Item.position.Y + (float)(base.Item.height / 2)) / 16f), 0.5f, 0.75f, 1.2f);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe(3).AddIngredient(8, 3).AddIngredient<global::CalamityMod.Items.Placeables.Crags.ScorchedBone>().Register();
	}
}
