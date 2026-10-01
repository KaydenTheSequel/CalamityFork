using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Boss;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class KingofConstellationsTenryu : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.StaffMinionSlotsRequired[base.Type] = 4f;
	}

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 62;
		base.Item.mana = 10;
		base.Item.damage = 187;
		base.Item.useStyle = 4;
		base.Item.buffType = ModContent.BuffType<KingofConstellationsBuff>();
		base.Item.shoot = ModContent.ProjectileType<BlackDragonHead>();
		base.Item.UseSound = Flare.FlareSound;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.Calamity().donorItem = true;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.autoReuse = true;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-20f, 0f);
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] <= 0)
		{
			return player.maxMinions >= 4;
		}
		return false;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 spawnPos = default(Vector2);
		((Vector2)(ref spawnPos))._002Ector(player.position.X - 200f, player.position.Y - 200f);
		Vector2 spawnPos2 = default(Vector2);
		((Vector2)(ref spawnPos2))._002Ector(player.position.X + 200f, player.position.Y - 200f);
		player.AddBuff(base.Item.buffType, 2);
		SpawnDragon(ModContent.ProjectileType<WhiteDragonHead>(), ModContent.ProjectileType<WhiteDragonBody>(), ModContent.ProjectileType<WhiteDragonTail>(), spawnPos2, player, source, damage, base.Item.damage, knockback);
		SpawnDragon(ModContent.ProjectileType<BlackDragonHead>(), ModContent.ProjectileType<BlackDragonBody>(), ModContent.ProjectileType<BlackDragonTail>(), spawnPos, player, source, damage, base.Item.damage, knockback);
		return false;
	}

	public static void SpawnDragon(int headType, int bodyType, int tailType, Vector2 spawnPos, Player player, EntitySource_ItemUse_WithAmmo source, int damage, int originalDamage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(source, spawnPos, player.DirectionTo(Main.MouseWorld) * 3f, headType, damage, knockback, player.whoAmI).originalDamage = originalDamage;
		Projectile.NewProjectileDirect(source, spawnPos, Vector2.Zero, tailType, damage, knockback, player.whoAmI).originalDamage = originalDamage;
		for (int i = 0; i < 20; i++)
		{
			Projectile.NewProjectileDirect(source, spawnPos, Vector2.Zero, bodyType, damage, knockback, player.whoAmI).originalDamage = originalDamage;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3531).AddIngredient(527).AddIngredient(528)
			.AddIngredient<TwistingNether>(3)
			.AddTile(101)
			.Register();
	}
}
