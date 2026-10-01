using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class CausticCroakerStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 42;
		base.Item.damage = 8;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.sentry = true;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.knockBack = 0.25f;
		base.Item.shoot = ModContent.ProjectileType<EXPLODINGFROG>();
		base.Item.shootSpeed = 10f;
		base.Item.UseSound = SoundID.Item44;
		base.Item.useStyle = 1;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse != 2)
		{
			player.FindSentryRestingSpot(type, out var XPosition, out var YPosition, out var YOffset);
			YOffset -= 13;
			((Vector2)(ref position))._002Ector((float)XPosition, (float)(YPosition - YOffset));
			int p = Projectile.NewProjectile(source, position, Vector2.Zero, type, damage, knockback, player.whoAmI);
			if (Main.projectile.IndexInRange(p))
			{
				Main.projectile[p].originalDamage = base.Item.damage;
			}
			player.UpdateMaxTurrets();
		}
		return false;
	}
}
