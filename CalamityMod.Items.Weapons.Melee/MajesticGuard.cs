using CalamityMod.Items.BaseItems;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class MajesticGuard : CustomUseProjItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 100;
		base.Item.height = 100;
		base.Item.damage = 365;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useAnimation = 50;
		base.Item.useTime = 50;
		base.Item.useTurn = true;
		base.Item.knockBack = 12f;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<MajesticGuardHoldout>();
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.useStyle = 5;
	}

	public override bool MeleePrefix()
	{
		return true;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/MajesticGuardGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3520).AddRecipeGroup("AnyMythrilBar", 15).AddIngredient<EssenceofSunlight>(3)
			.AddIngredient<EssenceofHavoc>(3)
			.AddIngredient<EssenceofEleum>(3)
			.AddTile(134)
			.Register();
		CreateRecipe().AddIngredient(3484).AddRecipeGroup("AnyMythrilBar", 15).AddIngredient<EssenceofSunlight>(3)
			.AddIngredient<EssenceofHavoc>(3)
			.AddIngredient<EssenceofEleum>(3)
			.AddTile(134)
			.Register();
	}
}
