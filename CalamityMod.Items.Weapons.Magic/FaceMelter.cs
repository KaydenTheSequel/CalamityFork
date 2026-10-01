using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class FaceMelter : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 50;
		base.Item.damage = 130;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 10;
		base.Item.useTime = 5;
		base.Item.useAnimation = 10;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.Calamity().donorItem = true;
		base.Item.shoot = ModContent.ProjectileType<MelterNote1>();
		base.Item.UseSound = SoundID.Item47;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 20f;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-15f, 0f);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			base.Item.useTime = 20;
			base.Item.useAnimation = 20;
		}
		else
		{
			base.Item.useTime = 5;
			base.Item.useAnimation = 10;
		}
		return base.CanUseItem(player);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			Projectile.NewProjectile(source, player.ClampedMouseWorld(), Vector2.Zero, ModContent.ProjectileType<MelterAmp>(), damage, knockback, player.whoAmI);
			return false;
		}
		if (Main.rand.Next(2) == 0)
		{
			damage = (int)((float)damage * 1.5f);
			type = ModContent.ProjectileType<MelterNote1>();
		}
		else
		{
			velocity.X *= 1.5f;
			velocity.Y *= 1.5f;
			type = ModContent.ProjectileType<MelterNote2>();
		}
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1305).AddIngredient(4715).AddIngredient<CosmiliteBar>(8)
			.AddIngredient<NightmareFuel>(20)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
