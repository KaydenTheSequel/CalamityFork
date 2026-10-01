using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee.Spears;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class StreamGouge : ModItem, ILocalizedModType, IModType
{
	public const int SpinTime = 45;

	public const int SpearFireTime = 24;

	public const int PortalLifetime = 30;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 100;
		base.Item.height = 100;
		base.Item.damage = 470;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.noMelee = true;
		base.Item.useTurn = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.useAnimation = 19;
		base.Item.useTime = 19;
		base.Item.useStyle = 5;
		base.Item.knockBack = 9.75f;
		base.Item.UseSound = SoundID.Item20;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.shoot = ModContent.ProjectileType<StreamGougeProj>();
		base.Item.shootSpeed = 15f;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/StreamGougeGlow", (AssetRequestMode)2).Value);
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmiliteBar>(12).AddTile<CosmicAnvil>().Register();
	}
}
