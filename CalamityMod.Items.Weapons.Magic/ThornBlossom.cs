using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class ThornBlossom : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 66;
		base.Item.height = 68;
		base.Item.damage = 120;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 10;
		base.Item.useTime = 23;
		base.Item.useAnimation = 23;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.UseSound = SoundID.Item109;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<BeamingBolt>();
		base.Item.shootSpeed = 20f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		player.statLife -= 3;
		if (player.statLife <= 0)
		{
			player.KillMe(PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.ThornBlossom").ToNetworkText(player.name)), 1000.0, 0);
		}
		for (int index = 0; index < 3; index++)
		{
			Projectile.NewProjectile(source, position, velocity.RotatedByRandom(MathHelper.ToRadians(30f)) * 1.5f, ModContent.ProjectileType<NettleRight>(), (int)((double)damage * 1.35), knockback, player.whoAmI);
		}
		Projectile.NewProjectile(source, position, velocity * 0.66f, type, damage, knockback, player.whoAmI, 1f);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ArchAmaryllis>().AddIngredient<UelibloomBar>(10).AddIngredient<UnholyEssence>(10)
			.AddTile(134)
			.Register();
	}
}
