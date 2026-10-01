using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class StarSputter : ModItem, ILocalizedModType, IModType
{
	private int counter;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 80;
		base.Item.height = 26;
		base.Item.damage = 138;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 8;
		base.Item.useAnimation = 24;
		base.Item.reuseDelay = 15;
		base.Item.useLimitPerAnimation = 3;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 15f;
		base.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
		base.Item.rare = 9;
		base.Item.UseSound = SoundID.Item92;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<SputterComet>();
		base.Item.shootSpeed = 15f;
		base.Item.useAmmo = AmmoID.FallenStar;
		base.Item.consumeAmmoOnFirstShotOnly = true;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-5f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		counter++;
		if (counter == 10)
		{
			Projectile.NewProjectile(source, position, velocity * 0.8f, ModContent.ProjectileType<SputterCometBig>(), (int)((float)damage * 1.5f), knockback, player.whoAmI);
		}
		if (counter >= 12)
		{
			counter = 0;
		}
		return true;
	}
}
