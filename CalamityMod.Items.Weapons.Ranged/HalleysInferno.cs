using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class HalleysInferno : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ShootSound = new SoundStyle("CalamityMod/Sounds/Item/HalleysInfernoShoot")
	{
		Volume = 0.68f
	};

	public static readonly SoundStyle Hit = new SoundStyle("CalamityMod/Sounds/Item/HalleysInfernoHit")
	{
		Volume = 0.75f
	};

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public static float MaxStarburstPerComet => 1f;

	public static float MaxStarburstPerStar => 0.5f;

	public static float LostAccuracyPerMiss => 4f;

	public static float MaxAccuracy => 50f;

	public static float StarburstDmgMult => 2.5f;

	public static float StarburstVelMult => 0.75f;

	public override void SetDefaults()
	{
		base.Item.width = 84;
		base.Item.height = 34;
		base.Item.damage = 444;
		base.Item.knockBack = 5.5f;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 5;
		base.Item.useAnimation = 25;
		base.Item.reuseDelay = 39;
		base.Item.useLimitPerAnimation = 5;
		base.Item.autoReuse = true;
		base.Item.useAmmo = AmmoID.Gel;
		base.Item.consumeAmmoOnFirstShotOnly = true;
		base.Item.shootSpeed = 12f;
		base.Item.shoot = ModContent.ProjectileType<HalleysInfernoHoldout>();
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.UseSound = ShootSound;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 20f;
	}

	public override void HoldItem(Player player)
	{
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		if (player.mount.Type != 8)
		{
			if (Main.LocalPlayer == player && !Main.projectile.Any((Projectile x) => x.active && x.owner == player.whoAmI && x.type == base.Item.shoot))
			{
				Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), player.Center, Vector2.Zero, base.Item.shoot, 0, 0f, player.whoAmI);
			}
			player.Calamity().StratusStarburstResetTimer = (int)MathHelper.Max((float)player.Calamity().StratusStarburstResetTimer, 600f);
			player.Calamity().ammoCost *= 0.5f;
		}
	}

	public override bool CanUseItem(Player player)
	{
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1910).AddIngredient<Lumenyl>(6).AddIngredient<RuinousSoul>(4)
			.AddIngredient<ExodiumCluster>(12)
			.AddTile(134)
			.Register();
	}
}
