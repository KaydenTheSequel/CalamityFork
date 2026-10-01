using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class ExecutionersBlade : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 64;
		base.Item.height = 64;
		base.Item.damage = 165;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useTime = 3;
		base.Item.useAnimation = 9;
		base.Item.useLimitPerAnimation = 3;
		base.Item.useStyle = 1;
		base.Item.knockBack = 6.75f;
		base.Item.UseSound = SoundID.Item73;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.shoot = ModContent.ProjectileType<ExecutionersBladeProj>();
		base.Item.shootSpeed = 24f;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/ExecutionersBladeGlow", (AssetRequestMode)2).Value);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		bool num = player.Calamity().StealthStrikeAvailable();
		int stealth = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (num && stealth.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[stealth].Calamity().stealthStrike = true;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmiliteBar>(12).AddTile<CosmicAnvil>().Register();
	}
}
