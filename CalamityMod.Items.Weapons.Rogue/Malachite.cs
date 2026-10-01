using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class Malachite : RogueWeapon, IHoldShiftTooltipItem
{
	public string ExtensionIndicatorKey => "Items.Misc.LegendaryShortTooltip";

	public Color? ExtensionIndicatorColor => null;

	public string TooltipExtensionKey => "LegendaryText";

	public Color? TooltipExtensionColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lime;
		}
	}

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 58;
		base.Item.damage = 52;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 14);
		base.Item.useStyle = 1;
		base.Item.knockBack = 1.25f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<MalachiteProj>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.Calamity().StealthStrikeAvailable())
		{
			base.Item.UseSound = SoundID.Item109;
			base.Item.shoot = ModContent.ProjectileType<MalachiteStealth>();
		}
		else if (player.altFunctionUse == 2)
		{
			base.Item.UseSound = SoundID.Item109;
			base.Item.shoot = ModContent.ProjectileType<MalachiteBolt>();
		}
		else
		{
			base.Item.UseSound = SoundID.Item1;
			base.Item.shoot = ModContent.ProjectileType<MalachiteProj>();
		}
		return base.CanUseItem(player);
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.Calamity().StealthStrikeAvailable() || player.altFunctionUse == 2)
		{
			return 1f;
		}
		return 2f;
	}

	public override void ModifyStatsExtra(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (player.altFunctionUse == 2 && !player.Calamity().StealthStrikeAvailable())
		{
			damage = (int)((float)damage * 1.75f);
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			for (float i = -6.5f; i <= 6.5f; i += 6.5f)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.ToRadians(i));
				int stealth = Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
				if (stealth.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[stealth].Calamity().stealthStrike = true;
				}
			}
			return false;
		}
		return true;
	}
}
