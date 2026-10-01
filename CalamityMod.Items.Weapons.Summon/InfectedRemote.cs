using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class InfectedRemote : ModItem, ILocalizedModType, IModType
{
	public const int DefaultIframes = 10;

	public const int ChargeIframes = 2;

	public const int RocketShootRate = 6;

	public const int BeeShootRate = 22;

	public const int MaxUpdatesWhenCharging = 2;

	public const float RegularChargeSpeed = 40f;

	public const float HorizontalRocketChargeSpeed = 22f;

	public const float RocketDamageFactor = 0.7f;

	public const float BeeDamageFactor = 0.65f;

	public const float MinionSlotRequirement = 3f;

	public const float EnemyTargetingRange = 1300f;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.StaffMinionSlotsRequired[base.Type] = 3f;
	}

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 28;
		base.Item.damage = 50;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.useStyle = 4;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.UseSound = SoundID.Item15;
		base.Item.buffType = ModContent.BuffType<ViriliBuff>();
		base.Item.shoot = ModContent.ProjectileType<PlaguePrincess>();
		base.Item.DamageType = DamageClass.Summon;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.Calamity().donorItem = true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		CalamityUtils.KillShootProjectiles(shouldBreak: true, type, player);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		return false;
	}
}
