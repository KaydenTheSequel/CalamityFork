using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class ForsakenSaber : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 56;
		base.Item.damage = 80;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 15);
		base.Item.useStyle = 1;
		base.Item.useTurn = true;
		base.Item.knockBack = 6f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.shoot = ModContent.ProjectileType<SandBlade>();
		base.Item.shootSpeed = 15f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		Vector2 spreadVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.8f, 1.2f);
		Projectile.NewProjectile(source, position, spreadVelocity, type, damage / 2, knockback * 0.5f, player.whoAmI);
		Projectile.NewProjectile(source, position, velocity, type, damage / 2, knockback * 0.5f, player.whoAmI);
		return false;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 159);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyAdamantiteBar", 5).AddIngredient(3783, 2).AddTile(134)
			.Register();
	}
}
