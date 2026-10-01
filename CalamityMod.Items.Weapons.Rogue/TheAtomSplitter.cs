using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class TheAtomSplitter : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 128);
		base.Item.damage = 296;
		base.Item.knockBack = 7f;
		base.Item.useAnimation = (base.Item.useTime = 25);
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 24f;
		base.Item.shoot = ModContent.ProjectileType<TheAtomSplitterProjectile>();
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		int javelin = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, -1f);
		if (player.Calamity().StealthStrikeAvailable() && Main.projectile.IndexInRange(javelin))
		{
			Main.projectile[javelin].Calamity().stealthStrike = true;
		}
		return false;
	}
}
