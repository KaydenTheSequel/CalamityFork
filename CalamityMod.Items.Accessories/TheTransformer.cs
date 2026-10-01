using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class TheTransformer : ModItem, ILocalizedModType, IModType
{
	public static int blobCap = 30;

	public static int blobDamage = 50;

	public new string LocalizationCategory => "Items.Accessories";

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.FindAndReplace("[GFB]", Lang.SupportGlyphs(this.GetLocalizedValue(Main.zenithWorld ? "TooltipGFB" : "TooltipNormal")));
		list.IntegrateHotkey(CalamityKeybinds.TransformerHotKey);
	}

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(6, 16));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 56;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.transformer = true;
		calamityPlayer.transformerVisual = !hideVisual;
		if (player.ownedProjectileCounts[ModContent.ProjectileType<TransformerAura>()] < 1 && !hideVisual && !player.dead && player.Calamity().transformerCooldown == 0)
		{
			Projectile.NewProjectileDirect(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<TransformerAura>(), 0, 0f, player.whoAmI);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaPrism>(10).AddRecipeGroup("AnyMythrilBar", 5).AddIngredient<EssenceofSunlight>(2)
			.AddIngredient<EssenceofHavoc>(2)
			.AddIngredient<EssenceofEleum>(2)
			.AddIngredient(520, 3)
			.AddIngredient(521, 3)
			.AddTile(134)
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
		CalamityUtils.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[base.Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.95f, new Vector2(0f, 0f), (SpriteEffects)0);
		return false;
	}
}
