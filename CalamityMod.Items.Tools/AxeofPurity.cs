using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

[LegacyName(new string[] { "PurityAxe" })]
public class AxeofPurity : ModItem, ILocalizedModType, IModType
{
	private static int AxePower = 25;

	private static float PowderSpeed = 21f;

	public new string LocalizationCategory => "Items.Tools";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 54;
		base.Item.damage = 55;
		base.Item.knockBack = 5f;
		base.Item.useTime = 15;
		base.Item.useAnimation = 15;
		base.Item.axe = AxePower;
		base.Item.shoot = 10;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = PowderSpeed;
	}

	public override bool CanShoot(Player player)
	{
		if (player.altFunctionUse != 2)
		{
			return false;
		}
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		int powderDamage = (int)(0.85f * (float)damage);
		int idx = Projectile.NewProjectile(source, position, velocity, type, powderDamage, knockback, player.whoAmI);
		Main.projectile[idx].DamageType = DamageClass.Melee;
		return false;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void UseAnimation(Player player)
	{
		base.Item.axe = AxePower;
		if (player.altFunctionUse == 2)
		{
			base.Item.axe = 0;
		}
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 58);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<FellerofEvergreens>().AddIngredient(66, 20).AddIngredient(501, 20)
			.AddIngredient(502, 10)
			.AddTile(16)
			.Register();
	}
}
