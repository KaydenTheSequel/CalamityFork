using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class SoulPiercer : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 64;
		base.Item.height = 64;
		base.Item.damage = 235;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 19;
		base.Item.useAnimation = (base.Item.useTime = 25);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 8f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.UseSound = SoundID.Item73;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<SoulPiercerBeam>();
		base.Item.shootSpeed = 6f;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/SoulPiercerGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmiliteBar>(12).AddTile<CosmicAnvil>().Register();
	}
}
