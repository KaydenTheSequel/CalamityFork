using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class EternalBlizzard : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		base.Item.width = 72;
		base.Item.height = 36;
		base.Item.damage = 67;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 18;
		base.Item.useAnimation = 18;
		base.Item.useStyle = 5;
		base.Item.useTurn = false;
		base.Item.noMelee = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.useAmmo = AmmoID.Arrow;
		base.Item.UseSound = SoundID.Item5;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<IcicleArrowProj>();
		base.Item.shootSpeed = 11f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		float SpeedX = velocity.X + (float)Main.rand.Next(-10, 11) * 0.05f;
		float SpeedY = velocity.Y + (float)Main.rand.Next(-10, 11) * 0.05f;
		int index = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, ModContent.ProjectileType<IcicleArrowProj>(), (int)((float)damage * 0.7f), knockback, player.whoAmI);
		Main.projectile[index].noDropItem = true;
		return true;
	}
}
