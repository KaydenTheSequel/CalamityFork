using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class TheSevensStriker : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public static readonly SoundStyle RouletteSound = new SoundStyle("CalamityMod/Sounds/Item/SevensStrikerRoulette")
	{
		Volume = 0.6f,
		SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
	};

	public static readonly SoundStyle RouletteTickSound = new SoundStyle("CalamityMod/Sounds/Item/SevensStrikerRouletteTick")
	{
		Volume = 0.5f
	};

	public static readonly SoundStyle BustSound = new SoundStyle("CalamityMod/Sounds/Item/SevensStrikerBust");

	public static readonly SoundStyle BustGFB = new SoundStyle("CalamityMod/Sounds/Item/SevensStrikerBustGFB");

	public static readonly SoundStyle DoublesSound = new SoundStyle("CalamityMod/Sounds/Item/SevensStrikerDoubles");

	public static readonly SoundStyle TriplesSound = new SoundStyle("CalamityMod/Sounds/Item/SevensStrikerTriples");

	public static readonly SoundStyle JackpotSound = new SoundStyle("CalamityMod/Sounds/Item/SevensStrikerJackpot");

	public static readonly SoundStyle JackpotGFB = new SoundStyle("CalamityMod/Sounds/Item/SevensStrikerJackpotGFB");

	public static readonly SoundStyle CoinSound = new SoundStyle("CalamityMod/Sounds/Item/SevensStrikerCoinShot")
	{
		MaxInstances = 0,
		PitchVariance = 0.5f
	};

	public static int ShotCoin = 0;

	public static readonly float RightClickCopperMultiplier = 0.04f;

	public static readonly float RightClickSilverMultiplier = 0.08f;

	public static readonly float RightClickGoldMultiplier = 0.16f;

	public static int RightClickAmmoSavedPercent = 80;

	public static readonly float DoublesMultiplier = 1f;

	public static readonly float TriplesCherryMultiplier = 1f;

	public static readonly float TriplesCherrySplitMultiplier = 0.333f;

	public static readonly float TriplesGrapeMultiplier = 0.333f;

	public static readonly float JackpotMultiplier = 0.5f;

	public static readonly float JackpotMultiplierGFB = 7f;

	public bool ShowExtensionIndicator => false;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RightClickAmmoSavedPercent);

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 170;
		base.Item.height = 56;
		base.Item.damage = 777;
		base.Item.knockBack = 9f;
		base.Item.useTime = 30;
		base.Item.useAnimation = 30;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.useTurn = true;
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.useAmmo = AmmoID.Coin;
		base.Item.shootSpeed = 24f;
		base.Item.shoot = 161;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.Calamity().donorItem = true;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool? UseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			bool num = Main.rand.Next(100) >= RightClickAmmoSavedPercent;
			long coinCount = Utils.CoinsCount(out var overflow, player.inventory);
			int price;
			if (overflow || coinCount > 10000)
			{
				price = 10000;
				ShotCoin = 160;
			}
			else if (coinCount > 100)
			{
				price = 100;
				ShotCoin = 159;
			}
			else
			{
				price = 1;
				ShotCoin = 158;
			}
			if (num)
			{
				player.BuyItem(price);
			}
		}
		else
		{
			base.Item.shoot = ModContent.ProjectileType<SevensStrikerHoldout>();
		}
		return base.UseItem(player);
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-30f, -11f);
	}

	public override void UseAnimation(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			base.Item.UseSound = CoinSound;
			base.Item.noUseGraphic = false;
		}
		else
		{
			base.Item.UseSound = null;
			base.Item.noUseGraphic = true;
		}
	}

	public override float UseTimeMultiplier(Player player)
	{
		if (player.altFunctionUse != 2)
		{
			return 1f;
		}
		return 0.1f;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return player.altFunctionUse != 2;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[ModContent.ProjectileType<SevensStrikerHoldout>()] <= 0)
		{
			return true;
		}
		if (player.altFunctionUse == 2)
		{
			Utils.CoinsCount(out var overflow, player.inventory);
			return overflow;
		}
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			Vector2 shootDirection = velocity.SafeNormalize(Vector2.UnitX * (float)player.direction);
			Vector2 gunTip = position + shootDirection * base.Item.scale * 90f;
			gunTip.Y -= 20f;
			float randAngle = Main.rand.NextFloat(-0.05f, 0.05f);
			float randVelMultiplier = Main.rand.NextFloat(0.92f, 1.08f);
			Vector2 finalVelocity = velocity.RotatedBy(randAngle) * randVelMultiplier;
			float damageMult = ((ShotCoin == 160) ? RightClickGoldMultiplier : ((ShotCoin == 159) ? RightClickSilverMultiplier : RightClickCopperMultiplier));
			int finalDamage = (int)((float)damage * damageMult);
			Projectile.NewProjectile(source, gunTip, finalVelocity, ShotCoin, finalDamage, knockback, player.whoAmI);
		}
		else
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SevensStrikerHoldout>(), damage, knockback, player.whoAmI, type);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(905).AddIngredient(74, 7).AddIngredient(73, 77)
			.AddIngredient(3467, 12)
			.AddIngredient<TwistingNether>(3)
			.AddTile(134)
			.Register();
	}
}
