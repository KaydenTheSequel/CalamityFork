using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class FleshOfInfidelity : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 42;
		base.Item.damage = 20;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.buffType = ModContent.BuffType<FleshBallBuff>();
		base.Item.shoot = ModContent.ProjectileType<FleshBallMinion>();
		base.Item.knockBack = 1f;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.mana = 10;
		base.Item.noMelee = true;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Zombie24;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.UnitY * -1f, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		return false;
	}
}
