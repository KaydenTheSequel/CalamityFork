using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class IchorSpear : RogueWeapon
{
	public static readonly SoundStyle ThrowSound = new SoundStyle("CalamityMod/Sounds/Item/IchorSpearThrow")
	{
		Volume = 0.2f,
		PitchVariance = 0.4f
	};

	public override float StealthDamageMultiplier => 0.5f;

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 52);
		base.Item.damage = 120;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 39);
		base.Item.useStyle = 1;
		base.Item.knockBack = 7f;
		base.Item.UseSound = ThrowSound;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.shoot = ModContent.ProjectileType<IchorSpearProj>();
		base.Item.shootSpeed = 17f;
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
				Main.projectile[stealth].usesLocalNPCImmunity = true;
			}
			return false;
		}
		return true;
	}
}
