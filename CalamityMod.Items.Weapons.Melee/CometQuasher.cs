using CalamityMod.Items.BaseItems;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class CometQuasher : CustomUseProjItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 92;
		base.Item.height = 94;
		base.Item.damage = 48;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useAnimation = (base.Item.useTime = 38);
		base.Item.knockBack = 2.75f;
		base.Item.autoReuse = true;
		base.Item.useTurn = true;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<CometQuasherHoldout>();
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
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/CometQuasherGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(117, 20).AddIngredient<EssenceofSunlight>(4).AddIngredient<StarblightSoot>(12)
			.AddIngredient(75, 5)
			.AddTile(16)
			.Register();
	}
}
