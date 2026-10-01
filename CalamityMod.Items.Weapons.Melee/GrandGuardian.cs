using CalamityMod.Items.BaseItems;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class GrandGuardian : CustomUseProjItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 130;
		base.Item.height = 130;
		base.Item.damage = 515;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useAnimation = 35;
		base.Item.useTime = 35;
		base.Item.useTurn = true;
		base.Item.knockBack = 9f;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<GrandGuardianHoldout>();
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.useStyle = 5;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/GrandGuardianGlow", (AssetRequestMode)2).Value);
	}

	public override bool MeleePrefix()
	{
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MajesticGuard>().AddIngredient(3457, 12).AddTile(412)
			.Register();
	}
}
