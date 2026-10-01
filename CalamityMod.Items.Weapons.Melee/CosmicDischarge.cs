using System;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class CosmicDischarge : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 52;
		base.Item.damage = 450;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.autoReuse = true;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.useTime = (base.Item.useAnimation = 15);
		base.Item.useStyle = 5;
		base.Item.knockBack = 0.5f;
		base.Item.UseSound = SoundID.Item122;
		base.Item.shootSpeed = 24f;
		base.Item.shoot = ModContent.ProjectileType<CosmicDischargeFlail>();
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		float ai3 = (Main.rand.NextFloat() - 0.75f) * ((float)Math.PI / 4f);
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, ai3);
		return false;
	}
}
