using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Back })]
[LegacyName(new string[] { "MarniteBayonet" })]
public class MarniteRepulsionShield : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 30;
		base.Item.rare = 1;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.accessory = true;
		base.Item.defense = 3;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		player.GetModPlayer<MarniteRepulsionShieldPlayer>().shieldEquipped = true;
		if (player.whoAmI == Main.myPlayer)
		{
			IEntitySource source = player.GetSource_Accessory(base.Item);
			if (player.ownedProjectileCounts[ModContent.ProjectileType<MarniteRepulsionHitbox>()] < 1)
			{
				Projectile.NewProjectileDirect(source, player.Center, Vector2.Zero, ModContent.ProjectileType<MarniteRepulsionHitbox>(), 5, 12f, Main.myPlayer).originalDamage = 5;
			}
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyGoldBar", 5).AddIngredient(3086, 15).AddIngredient(3081, 15)
			.AddTile(16)
			.Register();
	}
}
