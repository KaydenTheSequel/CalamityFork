using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class CadaverousCarrion : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 54;
		base.Item.height = 56;
		base.Item.damage = 384;
		base.Item.mana = 10;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.sentry = true;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.autoReuse = true;
		base.Item.knockBack = 4f;
		base.Item.UseSound = SoundID.NPCDeath13;
		base.Item.shoot = ModContent.ProjectileType<OldDukeHeadCorpse>();
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		player.FindSentryRestingSpot(type, out var XPosition, out var YPosition, out var YOffset);
		YOffset += 3;
		((Vector2)(ref position))._002Ector((float)XPosition, (float)(YPosition - YOffset));
		int p = Projectile.NewProjectile(source, position, Vector2.Zero, type, damage, knockback, player.whoAmI);
		if (Main.projectile.IndexInRange(p))
		{
			Main.projectile[p].originalDamage = base.Item.damage;
		}
		player.UpdateMaxTurrets();
		return false;
	}
}
