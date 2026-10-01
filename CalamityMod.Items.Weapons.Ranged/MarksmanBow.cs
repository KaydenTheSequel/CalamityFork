using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class MarksmanBow : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 110;
		base.Item.damage = 39;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 5;
		base.Item.useAnimation = 15;
		base.Item.useLimitPerAnimation = 3;
		base.Item.reuseDelay = 10;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 6f;
		base.Item.UseSound = SoundID.Item5;
		base.Item.autoReuse = true;
		base.Item.shoot = 5;
		base.Item.shootSpeed = 10f;
		base.Item.useAmmo = AmmoID.Arrow;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.Calamity().donorItem = true;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 10f;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-4f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityUtils.CheckWoodenAmmo(type, player))
		{
			type = 5;
		}
		float SpeedX = velocity.X + Main.rand.NextFloat(-0.5f, 0.5f);
		float SpeedY = velocity.Y + Main.rand.NextFloat(-0.5f, 0.5f);
		int arrow = Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), type, damage, knockback, player.whoAmI);
		Main.projectile[arrow].noDropItem = true;
		if (type == 5)
		{
			Main.projectile[arrow].localNPCHitCooldown = 12 * Main.projectile[arrow].MaxUpdates;
			Main.projectile[arrow].usesLocalNPCImmunity = true;
			Main.projectile[arrow].usesIDStaticNPCImmunity = false;
			Main.projectile[arrow].tileCollide = false;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1508, 31).AddTile(134).Register();
	}
}
