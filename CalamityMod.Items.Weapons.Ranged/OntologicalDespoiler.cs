using System.Linq;
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

[LegacyName(new string[] { "MolecularManipulator" })]
public class OntologicalDespoiler : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ChargeLV1 = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserChargeLV1");

	public static readonly SoundStyle ChargeLV2 = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserChargeLV2");

	public static readonly SoundStyle ChargeStart = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserChargeStart");

	public static readonly SoundStyle ChargeLoop = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserChargeLoop");

	internal static readonly int ChargeLoopSoundFrames = 151;

	public static readonly SoundStyle SmallShot = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserSmallShot");

	public static readonly SoundStyle BigShot = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserBigShot");

	public static readonly SoundStyle BigShot2 = new SoundStyle("CalamityMod/Sounds/Item/OntologicalDespoilerLargeShot");

	public static readonly SoundStyle SmallImpact = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserChargeLV1");

	public static readonly SoundStyle LargeImpact = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserChargeLV2");

	public bool shotType = true;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 88;
		base.Item.height = 34;
		base.Item.damage = 445;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 8);
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.useStyle = 5;
		base.Item.knockBack = 6f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.UseSound = null;
		base.Item.autoReuse = false;
		base.Item.shoot = ModContent.ProjectileType<OntologicalDespoilerHoldout>();
		base.Item.shootSpeed = 12f;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 25f;
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
		if (Main.myPlayer == player.whoAmI)
		{
			player.Calamity().rightClickListener = true;
		}
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		if (Main.projectile.Any((Projectile n) => n.active && n.type == base.Item.shoot && n.owner == player.whoAmI))
		{
			return false;
		}
		if (player.Calamity().mouseRight && player.whoAmI == Main.myPlayer && !Main.mapFullscreen && !Main.blockMouse)
		{
			int aiType = 10 + (shotType ? 5 : 0);
			if (!player.Calamity().despoilerNerf)
			{
				aiType = 20;
			}
			Projectile.NewProjectileDirect(source, player.Center, Vector2.Zero, base.Item.shoot, player.HeldItem.damage, 0f, player.whoAmI, 0f, 0f, aiType).velocity = (player.Calamity().mouseWorld - player.MountedCenter).SafeNormalize(Vector2.Zero);
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DudFire");
			style.Volume = 0.7f;
			style.Pitch = ((aiType == 20) ? (-0.7f) : (-0.5f + (shotType ? 0.5f : 0f)));
			SoundEngine.PlaySound(in style, player.Center);
			if (aiType != 20)
			{
				shotType = !shotType;
			}
		}
		else
		{
			Projectile.NewProjectileDirect(source, player.MountedCenter, Vector2.Zero, type, damage, knockback, player.whoAmI, 0f, 0f, (!shotType) ? 5 : 0).velocity = player.Calamity().mouseWorld - player.RotatedRelativePoint(player.MountedCenter);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ArcNovaDiffuser>().AddIngredient<NullificationPistol>().AddIngredient<DarkPlasma>(3)
			.AddTile(134)
			.Register();
	}
}
