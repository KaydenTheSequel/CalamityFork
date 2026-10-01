using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class RefractionRotor : RogueWeapon
{
	public override float StealthDamageMultiplier => 0.3f;

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 120);
		base.Item.damage = 240;
		base.Item.knockBack = 8.5f;
		base.Item.useAnimation = (base.Item.useTime = 40);
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 18f;
		base.Item.shoot = ModContent.ProjectileType<RefractionRotorProjectile>();
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (Main.LocalPlayer.Calamity().StealthStrikeAvailable())
		{
			int spread = 20;
			for (int i = -1; i <= 1; i++)
			{
				Vector2 perturbedspeed = velocity.RotatedBy(MathHelper.ToRadians((float)(spread * i)));
				int proj = Projectile.NewProjectile(source, position, perturbedspeed, type, damage, knockback, player.whoAmI, 0f, 1f);
				if (proj.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[proj].Calamity().stealthStrike = true;
				}
			}
			return false;
		}
		return true;
	}
}
