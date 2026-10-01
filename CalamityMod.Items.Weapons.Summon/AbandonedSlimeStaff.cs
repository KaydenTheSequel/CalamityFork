using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class AbandonedSlimeStaff : ModItem, ILocalizedModType, IModType
{
	private int slimeSlots;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 62;
		base.Item.height = 62;
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.UseSound = SoundID.Item44;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.mana = 40;
		base.Item.damage = 56;
		base.Item.knockBack = 3f;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.buffType = ModContent.BuffType<AbandonedSlimeBuff>();
		base.Item.shoot = ModContent.ProjectileType<AstrageldonSummon>();
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.Calamity().donorItem = true;
	}

	public override void HoldItem(Player player)
	{
		player.jumpSpeedBoost += 0.5f;
		double minionCount = 0.0;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile projectile = enumerator.Current;
			if (projectile.owner == player.whoAmI && projectile.minion && projectile.type != ModContent.ProjectileType<AstrageldonSummon>())
			{
				minionCount += (double)projectile.minionSlots;
			}
		}
		slimeSlots = (int)((double)player.maxMinions - minionCount);
	}

	public override bool CanUseItem(Player player)
	{
		return slimeSlots >= 1;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		CalamityUtils.KillShootProjectiles(shouldBreak: true, type, player);
		float damageMult = (float)Math.Log(slimeSlots, 8.0) + 1f;
		Projectile projectile = Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, type, (int)((float)damage * damageMult), knockback, player.whoAmI);
		projectile.originalDamage = (int)((float)base.Item.damage * damageMult);
		projectile.minionSlots = slimeSlots;
		return false;
	}
}
