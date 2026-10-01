using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Miasma : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 64;
		base.Item.damage = 40;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 16;
		base.Item.useAnimation = (base.Item.useTime = 27);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item8;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<MiasmaGas>();
		base.Item.shootSpeed = 10f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		int cloudAmt = Main.rand.Next(3, 6);
		for (int i = 0; i < cloudAmt; i++)
		{
			Vector2 velocityReal = velocity * Main.rand.NextFloat(0.9f, 1.1f);
			float angle = Main.rand.NextFloat(-1f, 1f) * MathHelper.ToRadians(30f);
			Projectile.NewProjectile(source, position, velocityReal.RotatedBy(angle), type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1244).AddIngredient<AquamarineStaff>().AddIngredient<CorrodedFossil>(10)
			.AddTile(16)
			.Register();
	}
}
