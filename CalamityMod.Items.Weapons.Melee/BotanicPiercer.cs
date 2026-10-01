using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee.Spears;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "TerraLance" })]
public class BotanicPiercer : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 112;
		base.Item.height = 112;
		base.Item.damage = 110;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.noMelee = true;
		base.Item.useTurn = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 17;
		base.Item.useStyle = 5;
		base.Item.useTime = 17;
		base.Item.knockBack = 8.5f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.shoot = ModContent.ProjectileType<BotanicPiercerProjectile>();
		base.Item.shootSpeed = 11f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/BotanicPiercerGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1228).AddIngredient(274).AddIngredient(550)
			.AddIngredient<LivingShard>(12)
			.AddTile(134)
			.Register();
	}
}
