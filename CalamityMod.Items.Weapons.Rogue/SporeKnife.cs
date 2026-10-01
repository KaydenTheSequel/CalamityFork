using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class SporeKnife : RogueWeapon
{
	public static readonly SoundStyle ThrowSound = new SoundStyle("CalamityMod/Sounds/Item/SporeKnifeThrow", 1, 2)
	{
		PitchVariance = 0.2f,
		MaxInstances = 2
	};

	public static readonly SoundStyle ImpactSound = new SoundStyle("CalamityMod/Sounds/Item/SporeKnifeImpact")
	{
		PitchVariance = 0.25f,
		MaxInstances = 10
	};

	public static readonly SoundStyle StealthImpactSound = new SoundStyle("CalamityMod/Sounds/Item/SporeKnifeStealthImpact");

	public static readonly SoundStyle ChompSound = new SoundStyle("CalamityMod/Sounds/Item/SporeKnifeChomp", 1, 3)
	{
		PitchVariance = 0.25f,
		MaxInstances = 10
	};

	public override float StealthDamageMultiplier => 2f;

	public override void SetDefaults()
	{
		base.Item.width = 12;
		base.Item.height = 40;
		base.Item.damage = 18;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 1;
		base.Item.useTime = 20;
		base.Item.knockBack = 1.75f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.shoot = ModContent.ProjectileType<SporeKnifeProj>();
		base.Item.shootSpeed = 15f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			SoundEngine.PlaySound(in ThrowSound, position);
			int stealth = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (stealth.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[stealth].Calamity().stealthStrike = true;
			}
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(331, 12).AddIngredient(209, 8).AddTile(16)
			.Register();
	}
}
