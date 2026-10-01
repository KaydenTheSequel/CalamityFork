using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "MagnaStriker" })]
public class ArcNovaDiffuser : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ChargeLV1;

	public static readonly SoundStyle ChargeLV2;

	public static readonly SoundStyle ChargeStart;

	public static readonly SoundStyle ChargeLoop;

	internal static readonly int ChargeLoopSoundFrames;

	public static readonly SoundStyle SmallShot;

	public static readonly SoundStyle BigShot;

	public static int AftershotCooldownFrames;

	public static int Charge1Frames;

	public static int Charge2Frames;

	public static Color mainColor;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 28;
		base.Item.damage = 125;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = AftershotCooldownFrames);
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.useStyle = 5;
		base.Item.knockBack = 4f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.UseSound = null;
		base.Item.autoReuse = false;
		base.Item.shoot = ModContent.ProjectileType<ArcNovaDiffuserHoldout>();
		base.Item.shootSpeed = 12f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(source, player.MountedCenter, Vector2.Zero, ModContent.ProjectileType<ArcNovaDiffuserHoldout>(), damage, knockback, player.whoAmI, 0f, 1f).velocity = player.Calamity().mouseWorld - player.RotatedRelativePoint(player.MountedCenter);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<OpalStriker>().AddIngredient<MagnaCannon>().AddIngredient<CoreofCalamity>()
			.AddTile(134)
			.Register();
	}

	static ArcNovaDiffuser()
	{
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		ChargeLV1 = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserChargeLV1")
		{
			Volume = 0.6f
		};
		ChargeLV2 = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserChargeLV2")
		{
			Volume = 0.6f
		};
		ChargeStart = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserChargeStart")
		{
			Volume = 0.6f
		};
		ChargeLoop = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserChargeLoop")
		{
			Volume = 0.6f
		};
		ChargeLoopSoundFrames = 151;
		SmallShot = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserSmallShot")
		{
			PitchVariance = 0.3f,
			Volume = 0.5f
		};
		BigShot = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserBigShot")
		{
			PitchVariance = 0.3f,
			Volume = 0.8f
		};
		AftershotCooldownFrames = 9;
		Charge1Frames = 156;
		Charge2Frames = 308;
		mainColor = new Color(116, 225, 0);
	}
}
