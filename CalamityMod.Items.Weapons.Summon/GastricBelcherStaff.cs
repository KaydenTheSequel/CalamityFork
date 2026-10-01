using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class GastricBelcherStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 66;
		base.Item.height = 70;
		base.Item.damage = 50;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 1f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = SoundID.Item76;
		base.Item.buffType = ModContent.BuffType<GastricAberrationBuff>();
		base.Item.shoot = ModContent.ProjectileType<GastricBelcher>();
		base.Item.DamageType = DamageClass.Summon;
		base.Item.autoReuse = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Main.rand.NextVector2Circular(5f, 5f), type, damage, knockback, player.whoAmI, 0f, 1f).originalDamage = base.Item.damage;
		return false;
	}
}
