using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class LiliesOfFinality : ModItem, ILocalizedModType, IModType
{
	public static float MaxEnemyDistanceDetection = 2000f;

	public static int CommonDustID = 114;

	public static float Elster_DistanceFromTarget = 280f;

	public static float Elster_TargettingFlySpeed = 35f;

	public static float Elster_BulletProjectileSpeed = 20f;

	public static int Elster_BulletMaxUpdates = 2;

	public static float Ariane_BoltFireRate = 60f;

	public static float Ariane_TargettingFlySpeed = 20f;

	public static float Ariane_BoltProjectileSpeed = 25f;

	public static int Ariane_BoltTimeHoming = 600;

	public static float Ariane_MinTurnRate = 0.05f;

	public static float Ariane_MaxTurnRate = 0.2f;

	public static int Ariane_AoESize = 1050;

	public static float Ariane_AoEDMGMultiplier = 0.4f;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.StaffMinionSlotsRequired[base.Type] = 2f;
	}

	public override void SetDefaults()
	{
		base.Item.damage = 512;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.buffType = ModContent.BuffType<LiliesOfFinalityBuff>();
		base.Item.shoot = ModContent.ProjectileType<LiliesOfFinalityElster>();
		base.Item.knockBack = 5f;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.width = 36;
		base.Item.height = 50;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.useStyle = 4;
		base.Item.UseSound = new SoundStyle("CalamityMod/Sounds/Item/LiliesOfFinalitySummonSpawn");
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] == 0)
		{
			return (float)player.maxMinions - player.slotsMinions >= 2f;
		}
		return false;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Vector2 mouse = player.ClampedMouseWorld();
		Projectile.NewProjectileDirect(source, mouse, new Vector2(-1f, -1f) * 3f, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		Projectile.NewProjectileDirect(source, mouse, new Vector2(1f, -1f) * 3f, ModContent.ProjectileType<LiliesOfFinalityAriane>(), damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		if (Main.dedServ)
		{
			return false;
		}
		int dustAmount = Main.rand.Next(30, 41);
		for (int i = 0; i < dustAmount; i++)
		{
			Vector2 dustVelocity = ((float)Math.PI * 2f / (float)dustAmount * (float)i).ToRotationVector2() * Main.rand.NextFloat(3f, 6f);
			int commonDustID = CommonDustID;
			Vector2? velocity2 = dustVelocity;
			float scale = Main.rand.NextFloat(1.2f, 1.5f);
			Dust.NewDustPerfect(mouse, commonDustID, velocity2, 0, default(Color), scale).noGravity = true;
		}
		return false;
	}
}
