using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class SpearofDestiny : RogueWeapon
{
	public static readonly SoundStyle ThrowSound = new SoundStyle("CalamityMod/Sounds/Item/SpearofDestiny")
	{
		Volume = 0.3f,
		PitchVariance = 0.3f
	};

	public static readonly SoundStyle ThrowSound2 = new SoundStyle("CalamityMod/Sounds/Item/LanceofDestiny")
	{
		Volume = 0.3f,
		PitchVariance = 0.3f
	};

	public static readonly SoundStyle ThrowSound3 = new SoundStyle("CalamityMod/Sounds/Item/LanceofDestinyStrong")
	{
		Volume = 0.5f,
		PitchVariance = 0.3f
	};

	private bool BigSpear;

	public override float StealthDamageMultiplier => 2.8f;

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 52;
		base.Item.damage = 70;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 41);
		base.Item.useStyle = 1;
		base.Item.knockBack = 2f;
		base.Item.UseSound = ThrowSound;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLightPurpleBuyPrice;
		base.Item.rare = 6;
		base.Item.shoot = ModContent.ProjectileType<SpearofDestinyProjectile>();
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
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int stealth = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SpearofDestinyStealth>(), damage, knockback, player.whoAmI);
			SoundEngine.PlaySound(in ThrowSound3, player.Center);
			if (stealth.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[stealth].Calamity().stealthStrike = true;
				Main.projectile[stealth].usesLocalNPCImmunity = true;
			}
			return false;
		}
		int projType = (BigSpear ? ModContent.ProjectileType<LanceofDestiny>() : type);
		SoundEngine.PlaySound(BigSpear ? ThrowSound2 : ThrowSound, player.Center);
		if (BigSpear)
		{
			Projectile.NewProjectile(source, position, velocity, projType, damage * 3, knockback, player.whoAmI);
		}
		else
		{
			int index = 5;
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
		CreateRecipe().AddIngredient<CursedDagger>().AddIngredient(1225, 7).AddIngredient(547, 5)
			.AddIngredient(548, 5)
			.AddIngredient(549, 5)
			.AddTile(134)
			.Register();
		CreateRecipe().AddIngredient<IchorSpear>().AddIngredient(1225, 7).AddIngredient(547, 5)
			.AddIngredient(548, 5)
			.AddIngredient(549, 5)
			.AddTile(134)
			.Register();
	}
}
