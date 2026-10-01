using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class OnyxChainBlaster : ModItem, ILocalizedModType, IModType
{
	public static int AmmoSavedPercent = 50;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 64;
		base.Item.height = 32;
		base.Item.damage = 58;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 15;
		base.Item.useAnimation = 15;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4.5f;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.UseSound = SoundID.Item36;
		base.Item.autoReuse = true;
		base.Item.shoot = 661;
		base.Item.shootSpeed = 24f;
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-5f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		int shardDamage = 2 * damage;
		float shardKB = 2f * knockback;
		Vector2 offset = default(Vector2);
		((Vector2)(ref offset))._002Ector((float)Main.rand.Next(-25, 26) * 0.05f, (float)Main.rand.Next(-25, 26) * 0.05f);
		Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity + offset, 661, shardDamage, shardKB, player.whoAmI);
		projectile.timeLeft = (int)((float)projectile.timeLeft * 1.25f);
		for (int i = 0; i < 4; i++)
		{
			((Vector2)(ref offset))._002Ector((float)Main.rand.Next(-45, 46) * 0.05f, (float)Main.rand.Next(-45, 46) * 0.05f);
			Projectile.NewProjectile(source, position, velocity + offset, type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return Main.rand.Next(100) >= AmmoSavedPercent;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3788).AddIngredient(1929).AddIngredient(3467, 5)
			.AddTile(134)
			.Register();
	}
}
