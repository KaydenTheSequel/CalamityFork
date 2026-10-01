using System;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class WulfrumKnife : RogueWeapon
{
	public static readonly SoundStyle Throw3Sound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumKnifeThrowFull")
	{
		Volume = 0.7f,
		PitchVariance = 0.4f
	};

	public static readonly SoundStyle Throw2Sound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumKnifeThrowTwo")
	{
		Volume = 0.7f,
		PitchVariance = 0.4f
	};

	public static readonly SoundStyle Throw1Sound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumKnifeThrowSingle")
	{
		Volume = 0.7f,
		PitchVariance = 0.4f
	};

	public static readonly SoundStyle TileHitSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumKnifeTileHit", 2)
	{
		Volume = 0.7f,
		PitchVariance = 0.4f,
		MaxInstances = 3
	};

	public int shootCount;

	public bool stealthStrikeStarted;

	public override float StealthDamageMultiplier => 1.5f;

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 38;
		base.Item.damage = 11;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.useTime = 4;
		base.Item.useAnimation = 10;
		base.Item.reuseDelay = 24;
		base.Item.useLimitPerAnimation = 3;
		base.Item.knockBack = 1f;
		base.Item.UseSound = Throw3Sound;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.shoot = ModContent.ProjectileType<WulfrumKnifeProj>();
		base.Item.shootSpeed = 4f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override void UseAnimation(Player player)
	{
		shootCount = 0;
		stealthStrikeStarted = false;
		base.Item.UseSound = Throw3Sound;
	}

	public override void HoldItem(Player player)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		if (!player.controlUseTile || player.mouseInterface || player.ItemAnimationActive)
		{
			return;
		}
		if (Main.rand.NextBool(7))
		{
			GeneralParticleHandler.SpawnParticle(new ManaDrainStreak(player, Main.rand.NextFloat(0.2f, 0.5f), Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(170f, 670f), Main.rand.NextFloat(30f, 44f), Color.GreenYellow, Color.DeepSkyBlue, Main.rand.Next(15, 30)));
		}
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile item = enumerator.Current;
			if (item.type == ModContent.ProjectileType<WulfrumKnifeProj>() && item.owner == player.whoAmI && (item.ai[0] > 0f || item.damage == 0))
			{
				item.ai[0] = -1f;
			}
		}
	}

	public override bool AltFunctionUse(Player player)
	{
		return false;
	}

	public override bool AdditionalStealthCheck()
	{
		return stealthStrikeStarted;
	}

	public override void ModifyStatsExtra(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		int num;
		float num2;
		if (!player.Calamity().StealthStrikeAvailable())
		{
			num = (stealthStrikeStarted ? 1 : 0);
			if (num == 0)
			{
				num2 = (float)Math.PI / 40f;
				goto IL_0025;
			}
		}
		else
		{
			num = 1;
		}
		num2 = (float)Math.PI / 100f;
		goto IL_0025;
		IL_0025:
		float spread = num2;
		float speedBoost = ((num != 0) ? 1.25f : 1f);
		velocity = velocity.RotatedByRandom((float)shootCount / 2f * spread) * speedBoost;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile item = enumerator.Current;
				if (item.type == ModContent.ProjectileType<WulfrumKnifeProj>() && item.owner == player.whoAmI && item.ai[0] > 0f)
				{
					item.ai[0] = -1f;
				}
			}
			return false;
		}
		if (player.Calamity().StealthStrikeAvailable() || stealthStrikeStarted)
		{
			stealthStrikeStarted = true;
			int p = Projectile.NewProjectile(source, position, velocity * 1.3f, ModContent.ProjectileType<WulfrumKnifeProj>(), damage, knockback, player.whoAmI);
			Projectile proj = Main.projectile[p];
			if (p.WithinBounds(Main.maxProjectiles))
			{
				proj.Calamity().stealthStrike = true;
				proj.penetrate = 2;
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<WulfrumMetalScrap>(10).AddTile(16).Register();
	}
}
