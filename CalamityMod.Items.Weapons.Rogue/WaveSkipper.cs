using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

[LegacyName(new string[] { "DuneHopper" })]
public class WaveSkipper : RogueWeapon
{
	public static int SpreadAngle = 8;

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 44;
		base.Item.damage = 80;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 22);
		base.Item.useStyle = 1;
		base.Item.knockBack = 4f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.shoot = ModContent.ProjectileType<WaveSkipperProjectile>();
		base.Item.shootSpeed = 12f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			for (int i = -SpreadAngle; i < SpreadAngle * 2; i += SpreadAngle)
			{
				Vector2 spreadVelocity = player.SafeDirectionTo(Main.MouseWorld).RotatedBy(MathHelper.ToRadians((float)i)) * base.Item.shootSpeed;
				int stealth = Projectile.NewProjectile(source, position, spreadVelocity, ModContent.ProjectileType<WaveSkipperProjectile>(), damage, knockback, player.whoAmI);
				if (stealth.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[stealth].Calamity().stealthStrike = true;
				}
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ScourgeoftheDesert>().AddIngredient<MolluskHusk>(5).AddIngredient<SeaPrism>(15)
			.AddIngredient<PrismShard>(20)
			.AddTile(16)
			.Register();
	}
}
