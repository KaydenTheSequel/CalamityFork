using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class InsidiousImpaler : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 66;
		base.Item.height = 70;
		base.Item.damage = 250;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.noMelee = true;
		base.Item.useTurn = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 13);
		base.Item.useStyle = 1;
		base.Item.knockBack = 12f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<InsidiousHarpoon>();
		base.Item.shootSpeed = 18f;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		return false;
	}
}
