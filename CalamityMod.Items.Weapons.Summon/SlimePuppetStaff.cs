using CalamityMod.Items.Materials;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class SlimePuppetStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 34;
		base.Item.damage = 10;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 29);
		base.Item.useStyle = 4;
		base.Item.noMelee = true;
		base.Item.knockBack = 3.6f;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.UseSound = SlimeGodCore.PossessionSound;
		base.Item.shoot = ModContent.ProjectileType<SlimePuppet>();
		base.Item.shootSpeed = 10f;
		base.Item.autoReuse = true;
		base.Item.DamageType = DamageClass.Summon;
	}

	public override Vector2? HoldoutOrigin()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(12f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse != 2)
		{
			int p = Projectile.NewProjectile(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, Main.myPlayer);
			if (Main.projectile.IndexInRange(p))
			{
				Main.projectile[p].originalDamage = base.Item.damage;
			}
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PurifiedGel>(18).AddIngredient<BlightedGel>(18).AddTile(220)
			.Register();
	}
}
