using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class PhantasmalFury : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 62;
		base.Item.height = 60;
		base.Item.damage = 190;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 80;
		base.Item.useTime = 3;
		base.Item.useAnimation = 45;
		base.Item.reuseDelay = 75;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 20f;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.UseSound = new SoundStyle("CalamityMod/Sounds/Item/PhantasmalFuryShoot")
		{
			Volume = 0.6f,
			PitchVariance = 0.15f
		};
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<PhantasmalFuryProj>();
		base.Item.shootSpeed = 6f;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position + velocity * 13f, velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.8f, 1.1f), ModContent.ProjectileType<PhantasmalFuryProj>(), damage, 0f, player.whoAmI);
		for (int i = 0; i < 2; i++)
		{
			Projectile.NewProjectile(source, position + velocity * 13f, velocity.RotatedByRandom(0.4000000059604645), ModContent.ProjectileType<Phantom>(), damage / 2, 0f, player.whoAmI);
		}
		for (int j = 0; j < 2; j++)
		{
			Dust dust = Dust.NewDustPerfect(position + velocity * 13f, 267);
			dust.velocity = velocity.RotatedByRandom(0.25) * Main.rand.NextFloat(1f, 4f);
			dust.scale = Main.rand.NextFloat(0.5f, 0.9f);
			dust.noGravity = true;
			dust.color = Color.Lerp(Color.White, Color.Aqua, 0.3f);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1446).AddIngredient<RuinousSoul>(2).AddIngredient<DarkPlasma>()
			.AddTile(134)
			.Register();
	}
}
