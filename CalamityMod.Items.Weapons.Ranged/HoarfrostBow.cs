using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "EffluviumBow" })]
public class HoarfrostBow : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 70;
		base.Item.damage = 36;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 29;
		base.Item.useAnimation = 29;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item5;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<MistArrow>();
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.Arrow;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		for (int index = 0; index < 2; index++)
		{
			float SpeedX = velocity.X + (float)Main.rand.Next(-25, 26) * 0.05f;
			float SpeedY = velocity.Y + (float)Main.rand.Next(-25, 26) * 0.05f;
			if (CalamityUtils.CheckWoodenAmmo(type, player))
			{
				Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, ModContent.ProjectileType<MistArrow>(), damage, knockback, player.whoAmI);
				continue;
			}
			int proj = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI);
			Main.projectile[proj].noDropItem = true;
		}
		return false;
	}
}
