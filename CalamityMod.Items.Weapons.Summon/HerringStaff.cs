using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class HerringStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 48);
		base.Item.damage = 15;
		base.Item.knockBack = 1.25f;
		base.Item.mana = 10;
		base.Item.buffType = ModContent.BuffType<Herring>();
		base.Item.shoot = ModContent.ProjectileType<HerringAI>();
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.DamageType = DamageClass.Summon;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item21;
		base.Item.rare = 3;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		return false;
	}
}
