using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class DragonsBreath : ModItem, ILocalizedModType, IModType
{
	public static int AmmoSavedPercent = 80;

	public static readonly SoundStyle FireballSound = new SoundStyle("CalamityMod/Sounds/Custom/Yharon/YharonFireball", 3)
	{
		PitchVariance = 0.3f,
		Volume = 0.75f
	};

	public static readonly SoundStyle WeldingStart = new SoundStyle("CalamityMod/Sounds/Item/DragonsBreathStrongStart")
	{
		Volume = 1.75f
	};

	public static readonly SoundStyle WeldingBurn = new SoundStyle("CalamityMod/Sounds/Item/WeldingBurn")
	{
		Volume = 0.65f
	};

	public static readonly SoundStyle WeldingShoot = new SoundStyle("CalamityMod/Sounds/Item/WeldingShoot")
	{
		Volume = 0.45f
	};

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 124;
		base.Item.height = 72;
		base.Item.damage = 328;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 30;
		base.Item.useAnimation = 30;
		base.Item.autoReuse = true;
		base.Item.channel = true;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.knockBack = 4.5f;
		base.Item.UseSound = null;
		base.Item.shoot = ModContent.ProjectileType<DragonsBreathHoldout>();
		base.Item.shootSpeed = 3.5f;
		base.Item.useAmmo = AmmoID.Gel;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] > 0)
		{
			return Main.rand.Next(100) >= AmmoSavedPercent;
		}
		return false;
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(source, position, Vector2.Zero, ModContent.ProjectileType<DragonsBreathHoldout>(), damage, knockback, player.whoAmI).velocity = (player.Calamity().mouseWorld - player.MountedCenter).SafeNormalize(Vector2.Zero);
		return false;
	}
}
