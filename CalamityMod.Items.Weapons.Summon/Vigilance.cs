using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class Vigilance : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 32);
		base.Item.damage = 115;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 4f;
		base.Item.UseSound = SoundID.DD2_BetsySummon;
		base.Item.autoReuse = true;
		base.Item.buffType = ModContent.BuffType<SoulSeekerBuff>();
		base.Item.shoot = ModContent.ProjectileType<SeekerSummonProj>();
		base.Item.DamageType = DamageClass.Summon;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if ((float)player.maxMinions - player.slotsMinions >= 1f)
		{
			player.AddBuff(base.Item.buffType, 2);
			Projectile projectile = Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI);
			projectile.ai[0] = player.ownedProjectileCounts[type];
			projectile.originalDamage = base.Item.damage;
		}
		return false;
	}
}
