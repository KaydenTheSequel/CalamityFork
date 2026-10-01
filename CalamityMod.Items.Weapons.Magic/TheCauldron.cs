using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class TheCauldron : ModItem, ILocalizedModType, IModType
{
	private float manaReductionMult = 0.2f;

	public static Asset<Texture2D> Glow;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			Glow = ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 46;
		base.Item.damage = 56;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.useTime = (base.Item.useAnimation = 60);
		base.Item.knockBack = 8f;
		base.Item.mana = 18;
		base.Item.UseSound = SoundID.DD2_MonkStaffSwing;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.shoot = ModContent.ProjectileType<CauldronHoldout>();
		base.Item.shootSpeed = 12f;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.channel = true;
		base.Item.Calamity().donorItem = true;
	}

	public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
	{
		if (player.lavaWet || player.ZoneUnderworldHeight)
		{
			mult = manaReductionMult;
		}
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, Glow.Value);
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<CauldronHoldout>(), damage, knockback, player.whoAmI, 46f);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(207).AddIngredient(175, 8).AddIngredient(173, 20)
			.AddIngredient(172, 20)
			.AddTile(16)
			.Register();
	}
}
