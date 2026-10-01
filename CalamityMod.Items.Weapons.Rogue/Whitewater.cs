using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

[LegacyName(new string[] { "BrackishFlask" })]
public class Whitewater : RogueWeapon
{
	public bool splitDirection;

	public override float StealthDamageMultiplier => 0.9f;

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 40;
		base.Item.damage = 80;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useTime = (base.Item.useAnimation = 34);
		base.Item.useStyle = 1;
		base.Item.knockBack = 6.5f;
		base.Item.UseSound = SoundID.Item106 with
		{
			Volume = 0.7f
		};
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.shoot = ModContent.ProjectileType<WhitewaterProj>();
		base.Item.shootSpeed = 6f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int stealth = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (stealth.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[stealth].Calamity().stealthStrike = true;
			}
		}
		else
		{
			int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			Main.projectile[proj].ai[1] = (splitDirection ? 1 : (-1));
			splitDirection = !splitDirection;
		}
		return false;
	}
}
