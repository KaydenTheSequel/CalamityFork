using System;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class Lionfish : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 40;
		base.Item.damage = 45;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 26);
		base.Item.useStyle = 1;
		base.Item.knockBack = 2.5f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.shoot = ModContent.ProjectileType<LionfishProj>();
		base.Item.shootSpeed = 12f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int stealth = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (stealth.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[stealth].Calamity().stealthStrike = true;
			}
			for (int s = 0; s < 5; s++)
			{
				Vector2 spikeVel = velocity;
				spikeVel *= Main.rand.NextFloat(0.85f, 1.25f);
				spikeVel = spikeVel.RotatedBy((Main.rand.NextDouble() - 0.5) * Math.PI * 0.25);
				int spike = Projectile.NewProjectile(source, position, spikeVel, ModContent.ProjectileType<UrchinSpikeFugu>(), (int)((double)damage * 0.5), (int)(knockback * 0.5f), player.whoAmI, -10f, 1f);
				if (spike.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[spike].DamageType = RogueDamageClass.Instance;
				}
			}
			return false;
		}
		return true;
	}
}
