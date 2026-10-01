using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class UrchinStinger : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 10;
		base.Item.height = 26;
		base.Item.damage = 13;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.useAnimation = (base.Item.useTime = 17);
		base.Item.knockBack = 1.5f;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.shoot = ModContent.ProjectileType<UrchinStingerProj>();
		base.Item.shootSpeed = 12f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(23f)) * 0.6f;
		Vector2 newVel2 = velocity.RotatedByRandom(MathHelper.ToRadians(23f)) * 0.8f;
		if (!player.Calamity().StealthStrikeAvailable())
		{
			Projectile.NewProjectile(source, position, newVel, type, damage / 2, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position, newVel2, type, damage / 2, knockback, player.whoAmI);
		}
		if (player.Calamity().StealthStrikeAvailable())
		{
			int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (player.Calamity().StealthStrikeAvailable())
			{
				Main.projectile[proj].Calamity().stealthStrike = true;
			}
			return false;
		}
		return true;
	}
}
