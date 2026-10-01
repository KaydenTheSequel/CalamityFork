using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class VileFeeder : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 36;
		base.Item.damage = 15;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.buffType = ModContent.BuffType<VileFeederBuff>();
		base.Item.shoot = ModContent.ProjectileType<VileFeederSummon>();
		base.Item.knockBack = 0.5f;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.mana = 10;
		base.Item.noMelee = true;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item2;
		base.Item.shootSpeed = 1f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Main.rand.NextVector2CircularEdge(5f, 5f), type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(57, 5).AddIngredient(86, 9).AddIngredient(619, 20)
			.AddTile(16)
			.Register();
	}
}
