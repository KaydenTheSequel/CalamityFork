using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class BloodBoiler : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle Heartbeat = new SoundStyle("CalamityMod/Sounds/Item/Heartbeat")
	{
		PitchVariance = 0.2f,
		Volume = 0.55f
	};

	public bool shotReturn;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 60;
		base.Item.height = 30;
		base.Item.damage = 120;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 5;
		base.Item.useAnimation = 25;
		base.Item.autoReuse = true;
		base.Item.UseSound = Heartbeat;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.shootSpeed = 9.5f;
		base.Item.shoot = ModContent.ProjectileType<BloodBoilerFire>();
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-5f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		shotReturn = !shotReturn;
		if (Main.rand.NextFloat() > 0.2f)
		{
			player.statLife--;
		}
		if (player.statLife <= 0)
		{
			PlayerDeathReason pdr = PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.BloodBoiler" + Main.rand.Next(1, 3)).ToNetworkText(player.name));
			player.KillMe(pdr, 1000.0, 0);
			return false;
		}
		Vector2 newVel = velocity.RotatedBy(shotReturn ? 0.03f : (-0.03f));
		Projectile.NewProjectile(source, position, newVel, type, damage, knockback, player.whoAmI, 0f, 0f, shotReturn ? 5 : 0);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BloodstoneCore>(6).AddTile(134).Register();
	}
}
