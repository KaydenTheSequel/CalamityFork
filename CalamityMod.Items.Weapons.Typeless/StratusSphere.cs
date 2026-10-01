using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Typeless;

public class StratusSphere : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle CastSound = new SoundStyle("CalamityMod/Sounds/Item/StratusSphereCast")
	{
		Volume = 0.75f
	};

	public new string LocalizationCategory => "Items.Weapons.Typeless";

	public override void SetDefaults()
	{
		base.Item.width = 48;
		base.Item.height = 30;
		base.Item.damage = 400;
		base.Item.DamageType = AverageDamageClass.Instance;
		base.Item.useTime = 30;
		base.Item.useAnimation = 30;
		base.Item.useLimitPerAnimation = 1;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5.5f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.UseSound = CastSound;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 20f;
		base.Item.shoot = ModContent.ProjectileType<StratusBlackHole>();
		base.Item.noUseGraphic = true;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3476).AddIngredient<Lumenyl>(6).AddIngredient<RuinousSoul>(4)
			.AddIngredient<ExodiumCluster>(12)
			.AddTile(134)
			.Register();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		List<Projectile> holes = new List<Projectile>();
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile item = enumerator.Current;
			if (item.type == type && item.owner == player.whoAmI && item.timeLeft > 30)
			{
				holes.Add(item);
			}
		}
		List<Projectile> orderedholes = holes.OrderBy((Projectile x) => x.timeLeft).ToList();
		while (orderedholes.Count >= 1)
		{
			orderedholes.First().timeLeft = 30;
			orderedholes.Remove(orderedholes.First());
		}
		if (player.altFunctionUse == 2)
		{
			if (player.Calamity().Starshield > 45)
			{
				player.Calamity().Starshield = 30;
			}
			else
			{
				player.Calamity().Starshield = CalamityUtils.MinutesToFrames(10);
			}
		}
		else
		{
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			player.Calamity().Starshield = (int)MathHelper.Min(30f, (float)player.Calamity().Starshield);
		}
		return false;
	}
}
