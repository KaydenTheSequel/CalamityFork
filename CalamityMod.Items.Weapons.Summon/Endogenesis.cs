using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class Endogenesis : ModItem, ILocalizedModType, IModType
{
	public static int AttackMode;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.StaffMinionSlotsRequired[base.Type] = 10f;
	}

	public override void SetDefaults()
	{
		base.Item.width = 80;
		base.Item.height = 80;
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.UseSound = SoundID.Item78;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.mana = 80;
		base.Item.damage = 1300;
		base.Item.knockBack = 4f;
		base.Item.autoReuse = true;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.buffType = ModContent.BuffType<EndoCooperBuff>();
		base.Item.shoot = ModContent.ProjectileType<EndoCooperBody>();
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
	}

	public override bool CanUseItem(Player player)
	{
		return (float)player.maxMinions >= 10f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.KillShootProjectileMany(player, type, ModContent.ProjectileType<EndoCooperLimbs>(), ModContent.ProjectileType<EndoBeam>());
		player.AddBuff(base.Item.buffType, 2);
		SummonEndoCooper(source, AttackMode, player.ClampedMouseWorld(), damage, base.Item.damage, knockback, player, out var _, out var _);
		AttackMode++;
		if (AttackMode > 3)
		{
			AttackMode = 0;
		}
		return false;
	}

	public static void SummonEndoCooper(IEntitySource source, int attackMode, Vector2 spawnPosition, int damage, int baseDamage, float knockback, Player owner, out int bodyIndex, out int limbsIndex)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		bodyIndex = (limbsIndex = -1);
		if (Main.myPlayer == owner.whoAmI)
		{
			float dmgMult = 1f;
			if (attackMode == 0)
			{
				dmgMult = 0.65f;
			}
			if (attackMode == 1)
			{
				dmgMult = 1f;
			}
			if (attackMode == 2)
			{
				dmgMult = 0.95f;
			}
			if (attackMode == 3)
			{
				dmgMult = 0.9f;
			}
			bodyIndex = Projectile.NewProjectile(source, spawnPosition, Vector2.Zero, ModContent.ProjectileType<EndoCooperBody>(), (int)((float)damage * dmgMult), knockback, owner.whoAmI, attackMode);
			limbsIndex = Projectile.NewProjectile(source, spawnPosition, Vector2.Zero, ModContent.ProjectileType<EndoCooperLimbs>(), (int)((float)damage * dmgMult), knockback, owner.whoAmI, attackMode, bodyIndex);
			Main.projectile[bodyIndex].ai[1] = limbsIndex;
			Main.projectile[bodyIndex].originalDamage = (int)((float)baseDamage * dmgMult);
			Main.projectile[limbsIndex].originalDamage = (int)((float)baseDamage * dmgMult);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CryogenicStaff>().AddIngredient(1931).AddIngredient<ShadowspecBar>(5)
			.AddIngredient<EndothermicEnergy>(100)
			.AddIngredient<EssenceofEleum>(15)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
