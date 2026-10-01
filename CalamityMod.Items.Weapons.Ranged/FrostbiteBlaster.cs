using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class FrostbiteBlaster : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 22;
		base.Item.damage = 50;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 7;
		base.Item.useAnimation = 21;
		base.Item.reuseDelay = 54;
		base.Item.useLimitPerAnimation = 3;
		base.Item.useStyle = 5;
		base.Item.useTurn = false;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.UseSound = SoundID.Item36;
		base.Item.autoReuse = true;
		base.Item.shoot = 337;
		base.Item.shootSpeed = 9f;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-14f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item36, position);
		for (int i = 0; i < 2; i++)
		{
			float newSpeedX = velocity.X + (float)Main.rand.Next(-40, 41) * 0.06f;
			float newSpeedY = velocity.Y + (float)Main.rand.Next(-40, 41) * 0.06f;
			if (type == 14)
			{
				int p = Projectile.NewProjectile(source, position.X, position.Y, newSpeedX, newSpeedY, 337, damage, knockback, player.whoAmI);
				Main.projectile[p].DamageType = DamageClass.Ranged;
			}
			else
			{
				Projectile.NewProjectile(source, position.X, position.Y, newSpeedX, newSpeedY, type, damage, knockback, player.whoAmI);
			}
		}
		return false;
	}
}
