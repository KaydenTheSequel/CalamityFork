using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class EyeOfNight : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 36);
		base.Item.damage = 24;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 1f;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.UseSound = SoundID.NPCHit1;
		base.Item.autoReuse = true;
		base.Item.buffType = ModContent.BuffType<EyeOfNightBuff>();
		base.Item.shoot = ModContent.ProjectileType<EyeOfNightSummon>();
		base.Item.DamageType = DamageClass.Summon;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.UnitY * -3f, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<VileFeeder>().AddIngredient<StaffOfNecrosteocytes>().AddIngredient<BelladonnaSpiritStaff>()
			.AddIngredient(2365)
			.AddIngredient<PurifiedGel>(10)
			.AddTile(26)
			.Register();
		CreateRecipe().AddIngredient<ScabRipper>().AddIngredient<StaffOfNecrosteocytes>().AddIngredient<BelladonnaSpiritStaff>()
			.AddIngredient(2365)
			.AddIngredient<PurifiedGel>(10)
			.AddTile(26)
			.Register();
	}
}
