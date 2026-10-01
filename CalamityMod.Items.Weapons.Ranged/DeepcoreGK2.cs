using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class DeepcoreGK2 : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 142;
		base.Item.height = 64;
		base.Item.damage = 41;
		base.Item.ArmorPenetration = 10;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.noMelee = true;
		base.Item.useAnimation = (base.Item.useTime = 14);
		base.Item.useStyle = 5;
		base.Item.knockBack = 7f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item38;
		base.Item.autoReuse = true;
		base.Item.shoot = 14;
		base.Item.shootSpeed = 20f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.Calamity().donorItem = true;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-20f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 shootDirection = velocity.SafeNormalize(Vector2.UnitX * (float)player.direction);
		Vector2 gunTip = position + shootDirection * base.Item.scale * 120f;
		gunTip.Y -= 18f;
		int p = Projectile.NewProjectile(source, gunTip, velocity, type, damage, knockback, player.whoAmI);
		if (p.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[p].Calamity().deepcoreBullet = true;
			Main.projectile[p].scale = 2f;
		}
		return false;
	}
}
