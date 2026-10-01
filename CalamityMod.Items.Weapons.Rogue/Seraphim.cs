using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class Seraphim : RogueWeapon
{
	public const int SplitDaggerCount = 6;

	public const int StealthStrikeLightCount = 7;

	public override void SetDefaults()
	{
		base.Item.width = 82;
		base.Item.height = 82;
		base.Item.damage = 320;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 13;
		base.Item.useTime = 13;
		base.Item.useStyle = 1;
		base.Item.knockBack = 6f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.shoot = ModContent.ProjectileType<SeraphimProjectile>();
		base.Item.shootSpeed = 64f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		int knife = Projectile.NewProjectile(source, position + velocity, velocity, type, damage, knockback, player.whoAmI);
		if (Main.projectile.IndexInRange(knife))
		{
			Main.projectile[knife].Calamity().stealthStrike = player.Calamity().StealthStrikeAvailable();
		}
		if (player.Calamity().StealthStrikeAvailable())
		{
			for (int i = 0; i < 7; i++)
			{
				float offsetAngle = MathHelper.Lerp(-0.97f, 0.97f, (float)i / 6f);
				Vector2 lightShootVelocity = velocity.SafeNormalize(Vector2.UnitY).RotatedBy(offsetAngle) * 23f;
				Projectile.NewProjectile(source, position, lightShootVelocity, ModContent.ProjectileType<SeraphimAngelicLight2>(), damage, knife, player.whoAmI, 1f);
			}
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ShatteredDawn>().AddIngredient<AuricBar>(5).AddTile<CosmicAnvil>()
			.Register();
	}
}
