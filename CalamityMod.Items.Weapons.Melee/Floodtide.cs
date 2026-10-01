using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Floodtide : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 80;
		base.Item.height = 92;
		base.Item.damage = 85;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTime = (base.Item.useAnimation = 24);
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 6f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<FloodtideShark>();
		base.Item.shootSpeed = 18f;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		player.itemLocation += Utils.RotatedBy(new Vector2(-4f * (float)player.direction, 2f * player.gravDir), (double)player.itemRotation, default(Vector2));
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 2; i++)
		{
			float SpeedX = velocity.X + (float)Main.rand.Next(-20, 21) * 0.05f;
			float SpeedY = velocity.Y + (float)Main.rand.Next(-20, 21) * 0.05f;
			Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI);
		}
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
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 217);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaRemains>(12).AddIngredient<DepthCells>(10).AddIngredient(319, 2)
			.AddTile(134)
			.Register();
	}
}
