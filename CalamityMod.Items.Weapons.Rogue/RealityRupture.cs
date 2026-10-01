using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

[LegacyName(new string[] { "LuminousStriker" })]
public class RealityRupture : RogueWeapon
{
	public static readonly SoundStyle ThrowSound = new SoundStyle("CalamityMod/Sounds/Item/RealityRupture")
	{
		Volume = 0.3f,
		PitchVariance = 0.3f
	};

	public static readonly SoundStyle ThrowSound2 = new SoundStyle("CalamityMod/Sounds/Item/LanceofDestinyStrong")
	{
		Volume = 0.4f,
		PitchVariance = 0.3f
	};

	public static readonly SoundStyle ThrowSound3 = new SoundStyle("CalamityMod/Sounds/Item/RealityRuptureStealth")
	{
		Volume = 0.5f,
		PitchVariance = 0.3f
	};

	private bool BigSpear;

	public override float StealthDamageMultiplier => 2.25f;

	public override void SetDefaults()
	{
		base.Item.width = 86;
		base.Item.height = 102;
		base.Item.damage = 420;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 37;
		base.Item.useStyle = 1;
		base.Item.useTime = 37;
		base.Item.knockBack = 9f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.shoot = ModContent.ProjectileType<RealityRuptureMini>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int stealth = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<RealityRuptureStealth>(), damage, knockback, player.whoAmI);
			SoundEngine.PlaySound(in ThrowSound3, player.Center);
			if (stealth.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[stealth].Calamity().stealthStrike = true;
				Main.projectile[stealth].usesLocalNPCImmunity = true;
			}
			return false;
		}
		int projType = (BigSpear ? ModContent.ProjectileType<RealityRuptureLance>() : type);
		SoundEngine.PlaySound(BigSpear ? ThrowSound2 : ThrowSound, player.Center);
		if (BigSpear)
		{
			Projectile.NewProjectile(source, position, velocity, projType, damage * 4, knockback * 1.5f, player.whoAmI);
		}
		else
		{
			int index = 4;
			for (int i = -index; i <= index; i += index)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.ToRadians((float)i));
				int spear = Projectile.NewProjectile(source, position, perturbedSpeed, projType, damage, knockback, player.whoAmI);
				if (spear.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[spear].Calamity().stealthStrike = player.Calamity().StealthStrikeAvailable();
				}
			}
		}
		BigSpear = !BigSpear;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SpearofDestiny>().AddIngredient<ArmoredShell>().AddIngredient<TwistingNether>()
			.AddIngredient<DarkPlasma>()
			.AddTile(134)
			.Register();
	}
}
