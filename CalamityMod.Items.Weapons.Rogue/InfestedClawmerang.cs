using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

[LegacyName(new string[] { "Shroomerang" })]
public class InfestedClawmerang : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 50;
		base.Item.damage = 18;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 1;
		base.Item.knockBack = 1.5f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.rare = 2;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.shoot = ModContent.ProjectileType<InfestedClawmerangProj>();
		base.Item.shootSpeed = 15f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int stealth = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (stealth.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[stealth].Calamity().stealthStrike = true;
			}
			return false;
		}
		return true;
	}
}
