using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

[LegacyName(new string[] { "LanternoftheSoul" })]
public class GuidelightofOblivion : ModItem, ILocalizedModType, IModType
{
	public const int ActiveFlameLimit = 15;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 60;
		base.Item.damage = 75;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.sentry = true;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<LanternSoul>();
		base.Item.UseSound = SoundID.Item44;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		int p = Projectile.NewProjectile(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI);
		if (Main.projectile.IndexInRange(p))
		{
			Main.projectile[p].originalDamage = base.Item.damage;
		}
		player.UpdateMaxTurrets();
		return false;
	}
}
