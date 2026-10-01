using System;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

[LegacyName(new string[] { "NanoblackReaperMelee", "NanoblackReaperRogue" })]
public class NanoblackReaper : RogueWeapon, IHoldShiftTooltipItem
{
	internal const float PiOver3 = (float)Math.PI / 3f;

	internal const float TwoPiOver3 = (float)Math.PI * 2f / 3f;

	internal static readonly Color NanoblackSlashColor1;

	internal static readonly Color NanoblackSlashColor2;

	internal static readonly Color NanoblackDustColor1;

	internal static readonly Color TesselationParticleColor;

	internal static readonly Color ZeroPointLineColor;

	internal static readonly Color ZeroPointImpactColor;

	internal static readonly Color PiercingStrikeColor;

	internal static readonly Color LightspeedCarveColor1;

	internal static readonly Color LightspeedCarveColor2;

	public static float Knockback;

	public static float Speed;

	public static int FocusFlurryAttacks;

	public static int PerfectLightspeedCarveFrames;

	public static int ImperfectLightspeedCarveFrames;

	public static float LightspeedCarveKnockback;

	public static int ArmorPenetration;

	public static int ZeroPointArmorPenetration;

	public static int LightspeedCarveArmorPenetration;

	public static float TesselationDamageRatio;

	public static float TesselationKnockback;

	public bool ShowExtensionIndicator => false;

	public bool HasFlavorTooltip => true;

	public Color? TooltipExtensionColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(31, 223, 128);
		}
	}

	public Color? FlavorTooltipColor => TooltipExtensionColor;

	public override float StealthDamageMultiplier => 1f;

	public override void SetDefaults()
	{
		base.Item.width = 78;
		base.Item.height = 64;
		base.Item.damage = 315;
		base.Item.knockBack = Knockback;
		base.Item.ArmorPenetration = ArmorPenetration;
		base.Item.useTime = (base.Item.useAnimation = 19);
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item18;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.shoot = ModContent.ProjectileType<NanoblackMain>();
		base.Item.shootSpeed = Speed;
	}

	public override void HoldItem(Player player)
	{
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = player.Calamity();
		modPlayer.mouseWorldListener = true;
		if (modPlayer.focusFlurryAttackCount > FocusFlurryAttacks)
		{
			modPlayer.focusFlurryAttackCount = FocusFlurryAttacks;
		}
		if (player.whoAmI != Main.myPlayer)
		{
			return;
		}
		if (modPlayer.mouseRight && modPlayer.StealthStrikeAvailable())
		{
			modPlayer.ConsumeStealthByAttacking();
			modPlayer.focusFlurryAttackCount = FocusFlurryAttacks;
			SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Item/StygianDash");
			SoundStyle flurryActivationSound2 = new SoundStyle("CalamityMod/Sounds/Item/HeliumFlashCoreImpact");
			float sound2Pitch = Main.rand.NextFloat(0.08f, 0.2f);
			SoundStyle style = soundStyle with
			{
				Volume = 1f
			};
			SoundEngine.PlaySound(in style, player.Center);
			style = flurryActivationSound2 with
			{
				Volume = 0.3f,
				Pitch = sound2Pitch
			};
			SoundEngine.PlaySound(in style, player.Center);
			Color color = NanoblackSlashColor1;
			Vector2 slashDir = (Main.rand.NextBool() ? (-1f) : 1f) * Vector2.UnitX;
			Vector2 vel = 0.01f * slashDir.RotatedByRandom(0.39269909262657166);
			GeneralParticleHandler.SpawnParticle(new VoidSparkParticle(scale: 0.33f / 0.357f, relativePosition: player.Center, velocity: vel, affectedByGravity: false, lifetime: 12, color: color));
			float glowScale = 0.33f * 0.333f;
			Vector2 squashStretch = default(Vector2);
			((Vector2)(ref squashStretch))._002Ector(1.3333f, 0.8f);
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(player.Center, vel, affectedByGravity: false, 11, glowScale, color, squashStretch, quickShrink: true));
		}
		if (!Main.mouseLeft || !Main.mouseLeftRelease)
		{
			return;
		}
		int scytheID = ModContent.ProjectileType<NanoblackMain>();
		for (int i = 0; i < Main.maxProjectiles; i++)
		{
			Projectile p = Main.projectile[i];
			if (p.active && p.type == scytheID && p.owner == player.whoAmI)
			{
				(p.ModProjectile as NanoblackMain).AttemptLightspeedCarve();
			}
		}
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.Calamity().focusFlurryAttackCount <= 0)
		{
			return 1f;
		}
		return 3f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = player.Calamity();
		if (modPlayer.focusFlurryAttackCount > 0)
		{
			SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Item/DemonSwordSwing2");
			float pitch = Main.rand.NextFloat(-0.24f, -0.12f) + (float)modPlayer.focusFlurryAttackCount * 0.01f;
			SoundStyle style = soundStyle with
			{
				Volume = 0.2f,
				Pitch = pitch,
				MaxInstances = 12
			};
			SoundEngine.PlaySound(in style, player.Center);
			Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI).Calamity().stealthStrike = true;
			modPlayer.focusFlurryAttackCount--;
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MoltenAmputator>().AddIngredient<GhoulishGouger>().AddIngredient<ShadowspecBar>(5)
			.AddIngredient<EndothermicEnergy>(40)
			.AddIngredient<PlagueCellCanister>(20)
			.AddIngredient(1346, 400)
			.AddTile<DraedonsForge>()
			.Register();
	}

	static NanoblackReaper()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		NanoblackSlashColor1 = new Color(47, 248, 211);
		NanoblackSlashColor2 = new Color(15, 15, 15);
		NanoblackDustColor1 = new Color(52, 239, 184);
		TesselationParticleColor = new Color(79, 240, 168);
		ZeroPointLineColor = new Color(24, 191, 160);
		ZeroPointImpactColor = new Color(31, 223, 128, 96);
		PiercingStrikeColor = new Color(36, 252, 212);
		LightspeedCarveColor1 = new Color(68, 242, 242);
		LightspeedCarveColor2 = new Color(66, 219, 173);
		Knockback = 9f;
		Speed = 16f;
		FocusFlurryAttacks = 12;
		PerfectLightspeedCarveFrames = 4;
		ImperfectLightspeedCarveFrames = 8;
		LightspeedCarveKnockback = 7f;
		ArmorPenetration = 30;
		ZeroPointArmorPenetration = 120;
		LightspeedCarveArmorPenetration = 120;
		TesselationDamageRatio = 0.25f;
		TesselationKnockback = 1.5f;
	}
}
