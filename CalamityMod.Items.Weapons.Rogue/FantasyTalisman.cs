using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class FantasyTalisman : RogueWeapon
{
	public override float StealthDamageMultiplier => 0.75f;

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 30;
		base.Item.damage = 45;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useTime = (base.Item.useAnimation = 25);
		base.Item.useStyle = 1;
		base.Item.knockBack = 6f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.shoot = ModContent.ProjectileType<FantasyTalismanProj>();
		base.Item.shootSpeed = 18f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		for (int i = -1; i <= 1; i++)
		{
			Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.ToRadians((float)i * (player.Calamity().StealthStrikeAvailable() ? 5f : 6f)));
			int shootCard = (player.Calamity().StealthStrikeAvailable() ? ModContent.ProjectileType<FantasyTalismanStealth>() : type);
			int card = Projectile.NewProjectile(source, position, perturbedSpeed, shootCard, damage, knockback, player.whoAmI);
			if (card.WithinBounds(Main.maxProjectiles) && player.Calamity().StealthStrikeAvailable())
			{
				Main.projectile[card].Calamity().stealthStrike = true;
			}
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SolarVeil>(10).AddIngredient(225, 10).AddIngredient(1508, 5)
			.AddTile(134)
			.Register();
	}
}
