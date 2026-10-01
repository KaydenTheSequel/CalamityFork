using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class Perdition : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 56);
		base.Item.damage = 375;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.buffType = ModContent.BuffType<PerditionBuff>();
		base.Item.shoot = ModContent.ProjectileType<PerditionBeacon>();
		base.Item.sentry = true;
		base.Item.knockBack = 4f;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.mana = 10;
		base.Item.noMelee = true;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.DD2_EtherianPortalOpen;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] < 1;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		CalamityUtils.KillShootProjectiles(shouldBreak: true, type, player);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		player.UpdateMaxTurrets();
		return false;
	}
}
