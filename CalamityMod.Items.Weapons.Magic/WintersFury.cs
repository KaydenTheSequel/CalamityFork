using System;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class WintersFury : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 40;
		base.Item.damage = 65;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 7;
		base.Item.useTime = 12;
		base.Item.useAnimation = 12;
		base.Item.useStyle = 5;
		base.Item.useTurn = false;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.UseSound = SoundID.Item9;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<Icicle>();
		base.Item.shootSpeed = 16f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.rand.NextBool(4))
		{
			Vector2 speed = velocity.RotatedByRandom(0.2617993950843811);
			speed.Y -= Math.Abs(speed.X) * 0.2f;
			int p = Projectile.NewProjectile(source, position, speed, ModContent.ProjectileType<FrostShardFriendly>(), damage, knockback, player.whoAmI);
			if (p.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[p].DamageType = DamageClass.Magic;
			}
		}
		if (Main.rand.NextBool(4))
		{
			SoundEngine.PlaySound(in SoundID.Item1, position);
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X * 1.2f, velocity.Y * 1.2f, ModContent.ProjectileType<Snowball>(), damage, knockback * 2f, player.whoAmI);
		}
		velocity.X += Main.rand.NextFloat(-2f, 2f);
		velocity.Y += Main.rand.NextFloat(-2f, 2f);
		return true;
	}
}
