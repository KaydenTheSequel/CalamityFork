using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class SpineOfThanatos : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 28);
		base.Item.damage = 128;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.autoReuse = true;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.useStyle = 5;
		base.Item.knockBack = 8f;
		base.Item.UseSound = SoundID.Item68;
		base.Item.shootSpeed = 1f;
		base.Item.shoot = ModContent.ProjectileType<SpineOfThanatosProjectile>();
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 2; i++)
		{
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, ((float)i == 0f).ToDirectionInt());
		}
		return true;
	}
}
