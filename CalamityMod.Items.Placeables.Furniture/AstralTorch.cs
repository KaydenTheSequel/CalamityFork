using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Astral;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture;

public class AstralTorch : ModItem, ILocalizedModType, IModType
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
		base.Item.DefaultToTorch(ModContent.TileType<global::CalamityMod.Tiles.Astral.AstralTorch>(), 0);
	}

	public override void HoldItem(Player player)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		int num;
		if (!Collision.DrownCollision(player.position, player.width, player.height, player.gravDir))
		{
			num = (base.Item.wet ? 1 : 0);
			if (num == 0 && Main.rand.NextBool((player.itemAnimation > 0) ? 10 : 20))
			{
				Dust.NewDust(new Vector2(player.itemLocation.X + 16f * (float)player.direction, player.itemLocation.Y - 14f * player.gravDir), 4, 4, ModContent.DustType<AstralOrange>());
			}
		}
		else
		{
			num = 1;
		}
		Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * (float)player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), reverseRotation: true);
		if (num == 0)
		{
			Lighting.AddLight(position, 1.6f, 0.6f, 0.3f);
		}
	}

	public override void PostUpdate()
	{
		if (!base.Item.wet)
		{
			Lighting.AddLight((int)((base.Item.position.X + (float)(base.Item.width / 2)) / 16f), (int)((base.Item.position.Y + (float)(base.Item.height / 2)) / 16f), 1.6f, 0.6f, 0.3f);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe(3).AddIngredient(8, 3).AddIngredient<StarblightSoot>().Register();
	}
}
