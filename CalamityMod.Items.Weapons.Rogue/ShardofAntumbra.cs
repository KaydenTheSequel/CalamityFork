using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

[LegacyName(new string[] { "XerocPitchfork" })]
public class ShardofAntumbra : RogueWeapon
{
	public override float StealthDamageMultiplier => 0.7f;

	public override void SetDefaults()
	{
		base.Item.width = 48;
		base.Item.height = 48;
		base.Item.damage = 111;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useTime = (base.Item.useAnimation = 17);
		base.Item.useStyle = 1;
		base.Item.knockBack = 8f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.shoot = ModContent.ProjectileType<AntumbraShardProjectile>();
		base.Item.shootSpeed = 14f;
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
				Main.projectile[stealth].extraUpdates = 4;
			}
		}
		return !player.Calamity().StealthStrikeAvailable();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MeldConstruct>(12).AddTile(412).Register();
	}
}
