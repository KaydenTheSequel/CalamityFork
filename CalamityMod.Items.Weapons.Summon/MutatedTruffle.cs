using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class MutatedTruffle : ModItem, ILocalizedModType, IModType
{
	public static float EnemyDistanceDetection = 8000f;

	public static int ToothballFireRate = 60;

	public static int ToothballsUntilNextState = 5;

	public static float ToothballSpeed = 25f;

	public static float ToothballSpikeSpeed = 30f;

	public static float DashSpeed = 50f;

	public static int DashTime = 240;

	public static int VortexTimeUntilNextState = 300;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.StaffMinionSlotsRequired[base.Type] = 3f;
	}

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 26;
		base.Item.damage = 250;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.buffType = ModContent.BuffType<MutatedTruffleBuff>();
		base.Item.shoot = ModContent.ProjectileType<MutatedTruffleMinion>();
		base.Item.knockBack = 5f;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.mana = 10;
		base.Item.noMelee = true;
		base.Item.useStyle = 4;
		base.Item.UseSound = SoundID.NPCHit14;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] < 1)
		{
			return player.maxMinions >= 3;
		}
		return false;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		CalamityUtils.KillShootProjectiles(shouldBreak: true, type, player);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Main.rand.NextVector2Circular(2f, 2f), type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		return false;
	}
}
