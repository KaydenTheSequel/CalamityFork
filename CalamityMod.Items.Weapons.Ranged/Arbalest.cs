using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Arbalest : ModItem, ILocalizedModType, IModType
{
	private int totalProjectiles = 1;

	private float arrowScale = 0.5f;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 82;
		base.Item.height = 34;
		base.Item.damage = 28;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 7;
		base.Item.useAnimation = 21;
		base.Item.reuseDelay = 30;
		base.Item.useLimitPerAnimation = 3;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.shoot = 10;
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.Arrow;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item5, player.Center);
		if (totalProjectiles > 3)
		{
			totalProjectiles = 1;
			if (arrowScale < 1.5f)
			{
				arrowScale += 0.05f;
			}
		}
		float spreadScale = arrowScale * arrowScale;
		int spread = (int)(30f * spreadScale);
		for (int i = 0; i < totalProjectiles; i++)
		{
			float SpeedX = velocity.X + (float)Main.rand.Next(-spread, spread + 1) * 0.05f;
			float SpeedY = velocity.Y + (float)Main.rand.Next(-spread, spread + 1) * 0.05f;
			int proj = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((float)damage * arrowScale), knockback * arrowScale, player.whoAmI);
			Main.projectile[proj].scale = arrowScale;
			Main.projectile[proj].extraUpdates++;
			Main.projectile[proj].noDropItem = true;
		}
		totalProjectiles++;
		if (arrowScale >= 1.5f)
		{
			arrowScale = 0.5f;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyMythrilBar", 10).AddIngredient(1344, 15).AddIngredient(549, 10)
			.AddTile(134)
			.Register();
	}
}
