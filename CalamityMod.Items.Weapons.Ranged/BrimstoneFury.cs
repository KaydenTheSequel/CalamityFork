using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class BrimstoneFury : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 58;
		base.Item.damage = 21;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 22;
		base.Item.useAnimation = 22;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3.75f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item5;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<BrimstoneBolt>();
		base.Item.shootSpeed = 13f;
		base.Item.useAmmo = AmmoID.Arrow;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		int numProj = 3;
		float rotation = MathHelper.ToRadians(2f);
		for (int i = 0; i < numProj; i++)
		{
			Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(0f - rotation, rotation, (float)i / (float)(numProj - 1)));
			if (CalamityUtils.CheckWoodenAmmo(type, player))
			{
				Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<BrimstoneBolt>(), damage, knockback, player.whoAmI);
				continue;
			}
			int proj = Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
			Main.projectile[proj].noDropItem = true;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UnholyCore>(5).AddTile(134).Register();
	}
}
