using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class ResurrectionButterfly : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 46;
		base.Item.damage = 66;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 1f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.Calamity().donorItem = true;
		base.Item.UseSound = SoundID.Item44;
		base.Item.autoReuse = true;
		base.Item.buffType = ModContent.BuffType<ResurrectionButterflyBuff>();
		base.Item.shoot = ModContent.ProjectileType<PinkButterfly>();
		base.Item.DamageType = DamageClass.Summon;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Vector2 clampedMouse = player.ClampedMouseWorld();
		Vector2 mouseDirection = Vector2.Normalize(clampedMouse - player.Center) * 10f;
		Projectile.NewProjectileDirect(source, clampedMouse, mouseDirection.RotatedBy(1.5707963705062866), ModContent.ProjectileType<PinkButterfly>(), damage, knockback, Main.myPlayer).originalDamage = base.Item.damage;
		Projectile.NewProjectileDirect(source, clampedMouse, mouseDirection.RotatedBy(-1.5707963705062866), ModContent.ProjectileType<PurpleButterfly>(), damage, knockback, Main.myPlayer).originalDamage = base.Item.damage;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<LifeAlloy>(5).AddIngredient(1611, 2).AddIngredient(225, 40)
			.AddIngredient(1508, 20)
			.AddTile(86)
			.Register();
	}
}
