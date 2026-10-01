using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Apotheosis : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 34;
		base.Item.damage = 222;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 42;
		base.Item.useAnimation = (base.Item.useTime = 167);
		base.Item.useStyle = 5;
		base.Item.useTurn = false;
		base.Item.noMelee = true;
		base.Item.knockBack = 6.9f;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
		base.Item.UseSound = SoundID.Item92;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<ApotheosisWorm>();
		base.Item.shootSpeed = 42f;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/ApotheosisGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(531).AddIngredient<CosmicDischarge>().AddIngredient<VoidEaterMarionette>(2)
			.AddIngredient<MawOfInfinity>(2)
			.AddIngredient<ShadowspecBar>(5)
			.AddIngredient<CosmiliteBar>(33)
			.AddIngredient<AscendantSpiritEssence>(11)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
