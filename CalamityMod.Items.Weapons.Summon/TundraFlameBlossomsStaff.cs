using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class TundraFlameBlossomsStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 60;
		base.Item.damage = 40;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item46;
		base.Item.buffType = ModContent.BuffType<TundraFlameBlossomsBuff>();
		base.Item.shoot = ModContent.ProjectileType<TundraFlameBlossom>();
		base.Item.DamageType = DamageClass.Summon;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] < 3;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		CalamityUtils.KillShootProjectiles(shouldBreak: false, type, player);
		for (int i = 0; i < 3; i++)
		{
			Projectile projectile = Projectile.NewProjectileDirect(source, player.Center, Vector2.Zero, type, damage, knockback, player.whoAmI);
			projectile.ai[1] = (int)((float)Math.PI * 2f / 3f * (float)i * 32f);
			projectile.originalDamage = base.Item.damage;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CinderBlossomStaff>().AddIngredient<FrostBlossomStaff>().AddRecipeGroup("AnyMythrilBar", 5)
			.AddTile(134)
			.Register();
	}
}
