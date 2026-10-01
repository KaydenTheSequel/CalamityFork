using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class GammaHeart : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 60;
		base.Item.useStyle = 4;
		base.Item.noMelee = true;
		base.Item.UseSound = SoundID.Item42;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.mana = 10;
		base.Item.damage = 85;
		base.Item.knockBack = 3f;
		base.Item.autoReuse = true;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.buffType = ModContent.BuffType<GammaHydraBuff>();
		base.Item.shoot = ModContent.ProjectileType<GammaHead>();
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile.NewProjectileDirect(source, player.Center, Vector2.Zero, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		return false;
	}
}
