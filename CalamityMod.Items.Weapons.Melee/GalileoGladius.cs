using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Melee.Shortswords;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class GalileoGladius : BaseSwordHoldoutItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override int ProjectileType => ModContent.ProjectileType<GalileoGladiusProj>();

	public override bool SizeModifiers => true;

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 44;
		base.Item.useStyle = 13;
		base.Item.damage = 600;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 8);
		base.Item.knockBack = 10f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.shootSpeed = 10f;
		base.SetDefaults();
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().StratusStarburstResetTimer = (int)MathHelper.Max((float)player.Calamity().StratusStarburstResetTimer, 600f);
	}

	public override bool AltFunctionUse(Player player)
	{
		return player.Calamity().AvaliableStarburst >= 10;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		if (player.ownedProjectileCounts[ModContent.ProjectileType<GalileoGladiusThrown>()] > 0)
		{
			Projectile proj = Main.projectile.First((Projectile x) => x.active && x.type == ModContent.ProjectileType<GalileoGladiusThrown>() && x.owner == player.whoAmI);
			if (proj.ai[0] > 0f)
			{
				if (player.altFunctionUse == 2 && player.Calamity().AvaliableStarburst >= 20)
				{
					proj.ai[1] = 3f;
				}
				else
				{
					proj.ai[1] = 2f;
				}
			}
			proj.netUpdate = true;
			return false;
		}
		if (player.altFunctionUse == 2)
		{
			Projectile.NewProjectile(source, position, velocity * 2f, ModContent.ProjectileType<GalileoGladiusThrown>(), damage, knockback, player.whoAmI);
			return false;
		}
		return base.Shoot(player, source, position, velocity, type, damage, knockback);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(4463).AddIngredient<Lumenyl>(8).AddIngredient<RuinousSoul>(5)
			.AddIngredient<ExodiumCluster>(15)
			.AddTile(134)
			.Register();
	}
}
