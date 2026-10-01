using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class SandSharknadoStaff : ModItem, ILocalizedModType, IModType
{
	public const float ProjSpeed = 30f;

	public const float FireSpeed = 50f;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 48;
		base.Item.height = 56;
		base.Item.damage = 71;
		base.Item.knockBack = 2f;
		base.Item.mana = 10;
		base.Item.buffType = ModContent.BuffType<Sandnado>();
		base.Item.shoot = ModContent.ProjectileType<SandnadoMinion>();
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.DamageType = DamageClass.Summon;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item44;
		base.Item.rare = 7;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.noMelee = true;
		base.Item.autoReuse = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ForgottenApexWand>().AddIngredient(3794, 5).AddIngredient<GrandScale>()
			.AddTile(134)
			.Register();
	}
}
