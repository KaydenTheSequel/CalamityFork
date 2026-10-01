using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class DarkSunRing : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public new string LocalizationCategory => "Items.Accessories";

	public bool HasFlavorTooltip => true;

	public Color? TooltipExtensionColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(195, 223, 255);
		}
	}

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(6, 7));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 60;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.defense = 4;
		base.Item.lifeRegen = 1;
		base.Item.accessory = true;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().darkSunRing = true;
		player.noKnockback = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UelibloomBar>(10).AddIngredient<DarksunFragment>(20).AddTile<CosmicAnvil>()
			.Register();
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[base.Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.8f, new Vector2(0f, 0f), (SpriteEffects)0);
		return false;
	}
}
