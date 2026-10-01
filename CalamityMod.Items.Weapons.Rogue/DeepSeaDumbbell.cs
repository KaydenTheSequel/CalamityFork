using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class DeepSeaDumbbell : RogueWeapon
{
	private const float FlexMultMax = 5f;

	private float flexMult = 1f;

	public override float StealthDamageMultiplier => 0.9f;

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 24;
		base.Item.damage = 466;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.useTime = 25;
		base.Item.useAnimation = 25;
		base.Item.knockBack = 8f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.useTurn = false;
		base.Item.shoot = ModContent.ProjectileType<DeepSeaDumbbell1>();
		base.Item.shootSpeed = 20f;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.Calamity().donorItem = true;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			base.Item.useStyle = 4;
			base.Item.noMelee = false;
			base.Item.noUseGraphic = false;
			base.Item.autoReuse = false;
			base.Item.UseSound = SoundID.Item1;
		}
		else
		{
			base.Item.useStyle = 1;
			base.Item.noMelee = true;
			base.Item.noUseGraphic = true;
			base.Item.autoReuse = true;
			base.Item.UseSound = SoundID.Item1;
		}
		return base.CanUseItem(player);
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.altFunctionUse != 2)
		{
			return 1f;
		}
		return 5f / 9f;
	}

	public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	{
		damage *= flexMult;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		flexMult = 1f;
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		flexMult = 1f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			flexMult = MathHelper.Clamp(flexMult + 1f, 1f, 5f);
			return false;
		}
		int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (player.Calamity().StealthStrikeAvailable() && proj.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[proj].Calamity().stealthStrike = true;
		}
		flexMult = 1f;
		return false;
	}
}
