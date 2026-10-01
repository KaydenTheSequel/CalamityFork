using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class AstralStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 86;
		base.Item.height = 72;
		base.Item.damage = 245;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 26;
		base.Item.useTime = 18;
		base.Item.useAnimation = 18;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
		base.Item.rare = 9;
		base.Item.UseSound = SoundID.Item105;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<AstralCrystal>();
		base.Item.shootSpeed = 15f;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 15f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		Vector2 spawnPos = default(Vector2);
		((Vector2)(ref spawnPos))._002Ector(player.MountedCenter.X + (float)Main.rand.Next(-200, 201), player.MountedCenter.Y - 600f);
		Vector2 targetPos = Main.MouseWorld + new Vector2((float)Main.rand.Next(-30, 31), (float)Main.rand.Next(-30, 31));
		Vector2 velocityReal = targetPos - spawnPos;
		((Vector2)(ref velocityReal)).Normalize();
		velocityReal *= 13f;
		int p = Projectile.NewProjectile(source, spawnPos, velocityReal, type, damage, knockback, player.whoAmI);
		Main.projectile[p].ai[0] = targetPos.Y - 120f;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AstralBar>(6).AddTile(412).Register();
	}
}
