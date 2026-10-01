using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class BarracudaGun : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 54;
		base.Item.height = 28;
		base.Item.damage = 52;
		base.Item.channel = true;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 1f;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.UseSound = new SoundStyle("CalamityMod/Sounds/Item/GunShotBig")
		{
			Volume = 0.5f,
			Pitch = Main.rand.NextFloat(0.2f, 0.3f)
		};
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 15f;
		base.Item.shoot = ModContent.ProjectileType<MechanicalBarracuda>();
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		int numProj = 4;
		float rotation = MathHelper.ToRadians(3f);
		for (int i = 0; i < numProj; i++)
		{
			Vector2 perturbedSpeed = (velocity * Main.rand.NextFloat(0.8f, 1.2f)).RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)i / (float)(numProj - 1)));
			Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1156).AddIngredient(3467, 5).AddIngredient(319, 2)
			.AddTile(134)
			.Register();
	}
}
