using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class FuelCellBundle : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 32;
		base.Item.mana = 10;
		base.Item.damage = 25;
		base.Item.useStyle = 1;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.noMelee = true;
		base.Item.knockBack = 7f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.UseSound = SoundID.Item106;
		base.Item.autoReuse = true;
		base.Item.noUseGraphic = true;
		base.Item.shoot = ModContent.ProjectileType<PlaguebringerMK2>();
		base.Item.shootSpeed = 11f;
		base.Item.DamageType = DamageClass.Summon;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse != 2)
		{
			int p = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<MK2FlaskSummon>(), damage, knockback, player.whoAmI);
			if (Main.projectile.IndexInRange(p))
			{
				Main.projectile[p].originalDamage = base.Item.damage;
			}
		}
		return false;
	}
}
