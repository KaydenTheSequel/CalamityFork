using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class FlowersOfMortality : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.StaffMinionSlotsRequired[base.Type] = 3f;
	}

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 36;
		base.Item.damage = 72;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.UseSound = SoundID.Item44;
		base.Item.buffType = ModContent.BuffType<FlowersOfMortalityBuff>();
		base.Item.shoot = ModContent.ProjectileType<FlowersOfMortalityPetal>();
		base.Item.shootSpeed = 10f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] < 5;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		CalamityUtils.KillShootProjectiles(shouldBreak: false, type, player);
		for (int i = 0; i < 5; i++)
		{
			Projectile projectile = Projectile.NewProjectileDirect(source, player.Center, Vector2.Zero, type, damage, knockback, player.whoAmI);
			projectile.ai[0] = (float)Math.PI * 2f * (float)i / 5f;
			projectile.rotation = projectile.ai[0];
			projectile.originalDamage = base.Item.damage;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<WitherBlossomsStaff>().AddIngredient(3467, 5).AddIngredient<LifeAlloy>(5)
			.AddIngredient(3459, 5)
			.AddTile(134)
			.Register();
	}
}
