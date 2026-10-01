using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Cooldowns;
using CalamityMod.Items.BaseItems;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Wulfrum;

public class WulfrumFusionCannon : HeldOnlyItem, IHideFrontArm, ILocalizedModType, IModType
{
	public static readonly SoundStyle ShootSound = new SoundStyle("CalamityMod/Sounds/Item/WulfrumProsthesisShoot")
	{
		PitchVariance = 0.1f,
		Volume = 0.4f
	};

	public static int ArmorPenetration = 10;

	public bool noAnimation;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override string Texture => "CalamityMod/Items/Armor/Wulfrum/WulfrumFusionCannon";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ArmorPenetration);

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 42;
		base.Item.damage = 6;
		base.Item.ArmorPenetration = ArmorPenetration;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.rare = 2;
		base.Item.UseSound = ShootSound;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<WulfrumFusionBolt>();
		base.Item.shootSpeed = 18f;
		base.Item.holdStyle = 16;
		base.Item.useTime = 4;
		base.Item.useAnimation = 10;
		base.Item.reuseDelay = 17;
		base.Item.useLimitPerAnimation = 3;
		base.Item.noUseGraphic = false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		tooltips.FirstOrDefault((TooltipLine x) => x.Name == "ItemName" && x.Mod == "Terraria").OverrideColor = Color.Lerp(new Color(194, 255, 67), new Color(112, 244, 244), 0.5f + 0.5f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f));
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
		if (player.whoAmI == Main.myPlayer && !WulfrumHat.HasArmorSet(player))
		{
			base.Item.type = 0;
			base.Item.SetDefaults();
			base.Item.stack = 0;
			Main.mouseItem = new Item();
		}
		base.Item.noUseGraphic = false;
		if (!player.Calamity().cooldowns.TryGetValue(WulfrumBastion.ID, out var cd) || cd.timeLeft > WulfrumHat.BastionCooldown + WulfrumHat.BastionTime - WulfrumHat.BastionBuildTime)
		{
			base.Item.noUseGraphic = true;
		}
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		velocity = velocity.RotatedByRandom(0.07853981852531433);
	}

	public override bool CanUseItem(Player player)
	{
		if (player.Calamity().cooldowns.TryGetValue(WulfrumBastion.ID, out var cd))
		{
			return cd.timeLeft < WulfrumHat.BastionCooldown + WulfrumHat.BastionTime - WulfrumHat.BastionBuildTime;
		}
		return false;
	}

	public void SetItemInHand(Player player, Rectangle heldItemFrame)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().mouseWorld.X > player.Center.X)
		{
			player.ChangeDir(1);
		}
		else
		{
			player.ChangeDir(-1);
		}
		if (player.Calamity().cooldowns.TryGetValue(WulfrumBastion.ID, out var cd) && cd.timeLeft <= WulfrumHat.BastionCooldown + WulfrumHat.BastionTime - WulfrumHat.BastionBuildTime)
		{
			if (player.ItemTimeIsZero)
			{
				noAnimation = false;
			}
			if (player.itemAnimation > base.Item.useAnimation)
			{
				noAnimation = true;
			}
			float animProgress = 1f - (float)player.itemAnimation / (float)player.itemAnimationMax;
			if (noAnimation || float.IsNaN(animProgress))
			{
				animProgress = 1f;
			}
			Vector2 itemPosition = player.MountedCenter + new Vector2(-2f * (float)player.direction, -1f * player.gravDir);
			float itemRotation = (player.Calamity().mouseWorld - itemPosition).ToRotation();
			if (animProgress < 0.9f)
			{
				itemPosition -= itemRotation.ToRotationVector2() * (1f - (float)Math.Pow(1f - (0.9f - animProgress) / 0.9f, 4.0)) * 3f;
			}
			if (animProgress < 0.6f)
			{
				itemRotation += -0.3f * (float)Math.Pow((0.6f - animProgress) / 0.6f, 2.0) * (float)player.direction * player.gravDir;
			}
			itemPosition += Main.rand.NextVector2Circular(2f, 2f) * (1f - animProgress);
			Vector2 itemSize = default(Vector2);
			((Vector2)(ref itemSize))._002Ector(38f, 18f);
			Vector2 itemOrigin = default(Vector2);
			((Vector2)(ref itemOrigin))._002Ector(-12f, 0f);
			CalamityUtils.CleanHoldStyle(player, itemRotation, itemPosition, itemSize, itemOrigin, noSandstorm: true);
		}
	}

	public override void HoldStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetItemInHand(player, heldItemFrame);
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetItemInHand(player, heldItemFrame);
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		return false;
	}
}
