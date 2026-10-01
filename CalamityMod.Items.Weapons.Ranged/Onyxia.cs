using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Onyxia : ModItem, ILocalizedModType, IModType
{
	public static int AmmoSavedPercent = 50;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 84;
		base.Item.height = 34;
		base.Item.damage = 90;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 10);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4.5f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.UseSound = SoundID.Item36;
		base.Item.autoReuse = true;
		base.Item.shoot = 661;
		base.Item.shootSpeed = 28f;
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-11f, 3f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		int shardDamage = (int)(1.45f * (float)damage);
		float shardKB = 2f * knockback;
		Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, 661, shardDamage, shardKB, player.whoAmI);
		projectile.timeLeft = (int)((float)projectile.timeLeft * 1.4f);
		for (int i = 0; i < 3; i++)
		{
			float randAngle = Main.rand.NextFloat(0.035f);
			float randVelMultiplier = Main.rand.NextFloat(0.92f, 1.08f);
			Vector2 ccwVelocity = velocity.RotatedBy(0f - randAngle) * randVelMultiplier;
			Vector2 cwVelocity = velocity.RotatedBy(randAngle) * randVelMultiplier;
			Projectile.NewProjectile(source, position, ccwVelocity, type, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position, cwVelocity, type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return Main.rand.Next(100) >= AmmoSavedPercent;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<OnyxChainBlaster>().AddIngredient<CosmiliteBar>(8).AddIngredient<DarksunFragment>(8)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
