using System.Collections.Generic;
using System.Linq;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Cooldowns;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "ElementalBlaster" })]
public class SuperradiantSlaughterer : ModItem, ILocalizedModType, IModType
{
	public const float ShootSpeed = 24f;

	public const int DashCooldown = 360;

	public bool hasDashed;

	public int rightClickDelay;

	public static int doubleRightFrameWindow = 23;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		if (tooltips != null && Main.LocalPlayer != null)
		{
			TooltipLine mainTooltip = tooltips.FirstOrDefault((TooltipLine x) => x.Text.Contains("[MAIN]") && x.Mod == "Terraria");
			if (mainTooltip != null)
			{
				mainTooltip.Text = Lang.SupportGlyphs(this.GetLocalizedValue("MainInfo"));
				mainTooltip.OverrideColor = Color.Chartreuse;
			}
			TooltipLine altTooltip = tooltips.FirstOrDefault((TooltipLine x) => x.Text.Contains("[ALT]") && x.Mod == "Terraria");
			if (altTooltip != null)
			{
				altTooltip.Text = Lang.SupportGlyphs(this.GetLocalization("AltInfo").Format(6));
				altTooltip.OverrideColor = Color.SpringGreen;
			}
		}
	}

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 84;
		base.Item.height = 46;
		base.Item.damage = 100;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 10;
		base.Item.useAnimation = 10;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.knockBack = 1.75f;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<SuperradiantSlaughtererHoldout>();
		base.Item.shootSpeed = 24f;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 21f;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void HoldItem(Player player)
	{
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		if (player.whoAmI == Main.myPlayer)
		{
			player.Calamity().mouseWorldListener = true;
			player.Calamity().rightClickListener = true;
			if (rightClickDelay > 0)
			{
				rightClickDelay--;
			}
			if (!player.HasCooldown(SuperradiantSawBoost.ID))
			{
				hasDashed = false;
			}
			if (player.Calamity().mouseRight && CanUseItem(player) && !Main.mapFullscreen && !Main.blockMouse && !player.HasCooldown(SuperradiantSawBoost.ID) && !Main.projectile.Any((Projectile n) => n.active && n.type == base.Item.shoot && n.owner == player.whoAmI))
			{
				int damage = (int)player.GetTotalDamage<MeleeDamageClass>().ApplyTo(base.Item.damage);
				float kb = player.GetTotalKnockback<MeleeDamageClass>().ApplyTo(base.Item.knockBack);
				Projectile.NewProjectile(base.Item.GetSource_FromThis(), player.Center, player.SafeDirectionTo(player.Calamity().mouseWorld), base.Item.shoot, damage * 2, kb, player.whoAmI, 0f, 2f);
			}
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			if (rightClickDelay > 0 && !hasDashed && player.Calamity().DashID != SuperradiantSawDash.ID)
			{
				hasDashed = true;
				player.Calamity().sBlasterDashActivated = true;
			}
			rightClickDelay = doubleRightFrameWindow;
			return false;
		}
		Projectile.NewProjectile(source, position, velocity, base.Item.shoot, damage * 2, knockback, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Buzzkill>().AddIngredient<SpeedBlaster>().AddIngredient(3467, 5)
			.AddIngredient<LifeAlloy>(5)
			.AddIngredient(3456, 5)
			.AddTile(134)
			.Register();
	}
}
