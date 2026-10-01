using System;
using CalamityMod.Cooldowns;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.DesertProwler;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class DesertProwlerHat : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle SmokeBombSound = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/DesertProwlerSmokeBomb");

	public static readonly SoundStyle SmokeBombEndSound = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/DesertProwlerSmokeBombEnd");

	public static readonly SoundStyle CDResetSound = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/DesertProwlerCDReset");

	public static int RogueCritBoost = 4;

	public static float SetBonusRogueStealth = 0.5f;

	public static float SmokeMoveSpeedMult = 1.5f;

	public static float SmokeDefenseMult = 0.75f;

	public static float SmokeAggroMult = 0.5f;

	public static int SmokeCooldown = CalamityUtils.SecondsToFrames(25);

	public static int SmokeDuration = CalamityUtils.SecondsToFrames(5);

	public static int LightsOutReset = CalamityUtils.SecondsToFrames(2);

	public static int FreeCrit = 200;

	public static int BonusDamageCap = 200;

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueCritBoost);

	public static bool ShroudedInSmoke(Player player, out CooldownInstance cd)
	{
		cd = null;
		if (player.Calamity().cooldowns.TryGetValue(SandsmokeBomb.ID, out cd))
		{
			return cd.timeLeft > SmokeCooldown;
		}
		return false;
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.defense = 2;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<DesertProwlerShirt>())
		{
			return legs.type == ModContent.ItemType<DesertProwlerPants>();
		}
		return false;
	}

	public static bool HasArmorSet(Player player)
	{
		if (player.armor[0].type == ModContent.ItemType<DesertProwlerHat>() && player.armor[1].type == ModContent.ItemType<DesertProwlerShirt>())
		{
			return player.armor[2].type == ModContent.ItemType<DesertProwlerPants>();
		}
		return false;
	}

	public bool IsPartOfSet(Item item)
	{
		if (item.type != ModContent.ItemType<DesertProwlerHat>() && item.type != ModContent.ItemType<DesertProwlerShirt>())
		{
			return item.type == ModContent.ItemType<DesertProwlerPants>();
		}
		return true;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		Color AbilityBriefColor = Color.Lerp(new Color(255, 229, 156), new Color(233, 225, 198), 0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 3f));
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusRogueStealth.ToStealth(), AbilityBriefColor.Hex3(), CalamityUtils.GetArmorSetBonusKey(), FreeCrit, BonusDamageCap, LightsOutReset.FramesToSeconds());
		player.Calamity().wearingRogueArmor = true;
		player.Calamity().rogueStealthMax += SetBonusRogueStealth;
		DesertProwlerPlayer armorPlayer = player.GetModPlayer<DesertProwlerPlayer>();
		armorPlayer.desertProwlerSet = true;
		if (!ShroudedInSmoke(player, out var cd))
		{
			return;
		}
		if (cd.timeLeft == SmokeCooldown + SmokeDuration)
		{
			armorPlayer.SetBonusStartEffect();
		}
		player.moveSpeed *= SmokeMoveSpeedMult;
		player.invis = true;
		player.aggro = (int)((float)player.aggro * SmokeAggroMult);
		player.noKnockback = true;
		for (int i = 0; i < 2; i++)
		{
			Vector2 dustDisplace = Main.rand.NextVector2Circular(80f, 50f);
			Vector2 position = player.MountedCenter + dustDisplace;
			Vector2 dustSpeed = Main.rand.NextVector2Circular(0.5f, 0.5f) + player.velocity / 8f - Vector2.UnitY.RotatedByRandom(0.7853981852531433) * 0.06f;
			dustSpeed.X += 1.4f * (float)Math.Sin((dustDisplace.X + 80f) / 160f * (float)Math.PI) * (float)((!Main.rand.NextBool()) ? 1 : (-1));
			GeneralParticleHandler.SpawnParticle(new SandyDustParticle(position, dustSpeed, Color.White, Main.rand.NextFloat(0.7f, 1.2f), Main.rand.Next(20, 50), 0.03f, Vector2.UnitY * 0.03f));
		}
		int sandSmokeCount = Main.rand.Next(2, 3);
		Color startColor = default(Color);
		Color endColor = default(Color);
		for (int j = 0; j < sandSmokeCount; j++)
		{
			((Color)(ref startColor))._002Ector(173, 156, 112);
			((Color)(ref endColor))._002Ector(143, 120, 63);
			if (Main.rand.NextBool())
			{
				((Color)(ref startColor))._002Ector(173, 139, 100);
				((Color)(ref endColor))._002Ector(149, 106, 50);
			}
			Vector3 hslStartColor = Main.rgbToHsl(startColor);
			Vector3 hslEndColor = Main.rgbToHsl(endColor);
			float valueShift = Main.rand.NextFloat(0f, 0.5f);
			float satShift = Main.rand.NextFloat(-0.1f, 0f);
			float hueShiftPercent = Main.rand.NextFloat();
			hslStartColor.Z = Math.Clamp(hslStartColor.Z + valueShift, 0f, 1f);
			hslEndColor.Z = Math.Clamp(hslEndColor.Z + valueShift, 0f, 1f);
			hslStartColor.Y = Math.Clamp(hslStartColor.Y + satShift, 0f, 1f);
			hslEndColor.Y = Math.Clamp(hslEndColor.Y + satShift, 0f, 1f);
			hslStartColor.X = MathHelper.Lerp(hslStartColor.X, 0.16862746f, hueShiftPercent);
			hslEndColor.X = MathHelper.Lerp(hslEndColor.X, 0.16862746f, hueShiftPercent);
			startColor = Main.hslToRgb(hslStartColor);
			endColor = Main.hslToRgb(hslEndColor);
			Vector2 smokeRandomPos = Main.rand.NextVector2Circular(40f, player.height);
			Vector2 position2 = player.MountedCenter + smokeRandomPos;
			float burstAngle = (float)Math.PI - (smokeRandomPos.X + 40f) / 80f * (float)Math.PI;
			Vector2 smokeSpeed = Main.rand.NextVector2Circular(1f, 0.5f) - Vector2.UnitY * 0.05f + player.velocity * 0.5f + burstAngle.ToRotationVector2() * ((1f - (float)Math.Sin(burstAngle)) * 0.9f + 0.1f) * 1.5f;
			smokeSpeed.X += (float)Math.Sin((smokeRandomPos.X + 40f) / 80f * (float)Math.PI) * (float)((!Main.rand.NextBool()) ? 1 : (-1));
			GeneralParticleHandler.SpawnParticle(new TimedSmokeParticle(position2, smokeSpeed, startColor, endColor, Main.rand.NextFloat(0.7f, 1.6f), Main.rand.NextFloat(0.4f, 0.55f), Main.rand.Next(20, 36), 0.01f));
		}
		Vector2 dustDirection = Main.rand.NextVector2CircularEdge(1f, 1f);
		float dustDistance = Main.rand.NextFloat(30f);
		Vector2 position3 = player.MountedCenter + dustDirection * dustDistance;
		int dustType = (Main.rand.NextBool() ? 32 : 31);
		Dust dust = Dust.NewDustPerfect(position3, dustType);
		dust.noGravity = true;
		dust.fadeIn = 1f;
		Vector2 dustVelocity = dustDirection.RotatedBy(1.5707963705062866) * 0.04f * dustDistance;
		dust.velocity = dustVelocity;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetCritChance<ThrowingDamageClass>() += RogueCritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StormlionMandible>(2).AddIngredient(225, 8).AddTile(86)
			.Register();
	}
}
