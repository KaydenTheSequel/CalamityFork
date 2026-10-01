using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class StormfrontRazor : RogueWeapon
{
	public static readonly SoundStyle LightningStrikeSound = new SoundStyle("CalamityMod/Sounds/Custom/LightningStrike");

	public const float LightningDamageFactor = 1.5f;

	public override float StealthDamageMultiplier => 1.2f;

	public override float StealthVelocityMultiplier => 1.5f;

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(6, 4));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 64;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.useStyle = 1;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.useAnimation = 20;
		base.Item.useTime = 20;
		base.Item.damage = 38;
		base.Item.knockBack = 7f;
		base.Item.shoot = ModContent.ProjectileType<StormfrontRazorProjectile>();
		base.Item.shootSpeed = 8.2f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 8f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int p = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, 10f);
			if (p.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[p].Calamity().stealthStrike = true;
			}
			return false;
		}
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, 1f);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Cinquedea>().AddRecipeGroup("AnyMythrilBar", 6).AddIngredient<EssenceofSunlight>(4)
			.AddIngredient<SeaPrism>(15)
			.AddIngredient<StormlionMandible>(2)
			.AddTile(134)
			.Register();
	}
}
