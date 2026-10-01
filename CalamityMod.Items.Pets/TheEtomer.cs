using CalamityMod.Buffs.Pets;
using CalamityMod.Items.Placeables.Crags;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.Pets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Pets;

[LegacyName(new string[] { "RotomRemote" })]
public class TheEtomer : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Pets";

	public override void SetDefaults()
	{
		base.Item.DefaultToVanitypet(ModContent.ProjectileType<RotomPet>(), ModContent.BuffType<ElectricTroublemaker>());
		base.Item.UseSound = SoundID.Item113;
		base.Item.value = Item.sellPrice(0, 2);
		base.Item.rare = 3;
		base.Item.Calamity().devItem = true;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
		{
			player.AddBuff(base.Item.buffType, 3600);
		}
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Pets/TheEtomerGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyGoldBar", 8).AddIngredient<PrismShard>(5).AddIngredient<ScorchedBone>(5)
			.AddTile(16)
			.Register();
	}
}
