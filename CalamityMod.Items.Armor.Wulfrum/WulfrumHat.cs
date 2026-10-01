using System;
using CalamityMod.Cooldowns;
using CalamityMod.Items.Accessories.Vanity;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Wulfrum;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "WulfrumHelmet" })]
[LegacyName(new string[] { "WulfrumHeadSummon" })]
public class WulfrumHat : ModItem, IExtendedHat, ILocalizedModType, IModType
{
	public static readonly SoundStyle SetActivationSound = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/WulfrumBastionActivate");

	public static readonly SoundStyle SetBreakSound = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/WulfrumBastionBreak");

	public static readonly SoundStyle SetBreakSoundSafe = new SoundStyle("CalamityMod/Sounds/Custom/AbilitySounds/WulfrumBastionBreakSafely");

	public static float SummonDamageBoost = 0.05f;

	public static int BastionDefenseBoost = 12;

	public static float BastionDRBoost = 0.1f;

	public static int BastionBuildTime = CalamityUtils.SecondsToFrames(0.55f);

	public static int BastionTime = CalamityUtils.SecondsToFrames(30);

	public static int TimeLostPerHit = CalamityUtils.SecondsToFrames(2);

	public static int BastionCooldown = CalamityUtils.SecondsToFrames(20);

	internal static Item DummyCannon = new Item();

	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public string ExtensionTexture => "CalamityMod/Items/Armor/Wulfrum/WulfrumHat_HeadExtension";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(SummonDamageBoost.ToPercent());

	public Vector2 ExtensionSpriteOffset(PlayerDrawSet drawInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return -Vector2.UnitY * 2f;
	}

	public string EquipSlotName(Player drawPlayer)
	{
		if (!drawPlayer.Male)
		{
			return "WulfrumHatFemale";
		}
		return Name;
	}

	public static bool PowerModeEngaged(Player player, out CooldownInstance cd)
	{
		cd = null;
		if (player.Calamity().cooldowns.TryGetValue(WulfrumBastion.ID, out cd))
		{
			return cd.timeLeft > BastionCooldown;
		}
		return false;
	}

	public override void Load()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		if (!Main.dedServ)
		{
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/Items/Armor/Wulfrum/WulfrumHat_FemaleHead", EquipType.Head, null, "WulfrumHatFemale");
		}
		On_Main.DrawPendingMouseText += new hook_DrawPendingMouseText(SpoofMouseItem);
	}

	public override void Unload()
	{
		DummyCannon.TurnToAir();
		DummyCannon = null;
	}

	private void SpoofMouseItem(orig_DrawPendingMouseText orig)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.LocalPlayer;
		if (DummyCannon.IsAir && !Main.gameMenu)
		{
			DummyCannon.SetDefaults(ModContent.ItemType<WulfrumFusionCannon>());
		}
		if (IsPartOfSet(Main.HoverItem) && HasArmorSet(player) && Main.keyState.PressingShift())
		{
			Main.HoverItem = DummyCannon.Clone();
			Main.hoverItemName = DummyCannon.Name;
		}
		orig.Invoke();
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.defense = 1;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<WulfrumJacket>())
		{
			return legs.type == ModContent.ItemType<WulfrumOveralls>();
		}
		return false;
	}

	public static bool HasArmorSet(Player player)
	{
		if (player.armor[0].type == ModContent.ItemType<WulfrumHat>() && player.armor[1].type == ModContent.ItemType<WulfrumJacket>())
		{
			return player.armor[2].type == ModContent.ItemType<WulfrumOveralls>();
		}
		return false;
	}

	public bool IsPartOfSet(Item item)
	{
		if (item.type != ModContent.ItemType<WulfrumHat>() && item.type != ModContent.ItemType<WulfrumJacket>())
		{
			return item.type == ModContent.ItemType<WulfrumOveralls>();
		}
		return true;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		WulfrumArmorPlayer modPlayer = player.GetModPlayer<WulfrumArmorPlayer>();
		player.GetModPlayer<WulfrumTransformationPlayer>();
		modPlayer.wulfrumSet = true;
		Color AbilityBriefColor = Color.Lerp(new Color(194, 255, 67), new Color(112, 244, 244), 0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 3f));
		player.setBonus = this.GetLocalization("SetBonus").Format(AbilityBriefColor.Hex3(), CalamityUtils.GetArmorSetBonusKey(), BastionTime.FramesToSeconds(), TimeLostPerHit.FramesToSeconds());
		if (PowerModeEngaged(player, out var cd))
		{
			if (cd.timeLeft == BastionCooldown + BastionTime)
			{
				ActivationEffects(player);
			}
			player.statDefense += BastionDefenseBoost;
			player.endurance += BastionDRBoost;
			bool num = player.Transformation().Type == ModContent.ItemType<AbandonedWulfrumHelmet>();
			Item headItem = ((player.armor[10].type != 0) ? player.armor[10] : player.armor[0]);
			bool hatVisible = !num && headItem.type == ModContent.ItemType<WulfrumHat>();
			if ((cd.timeLeft == BastionCooldown + BastionTime - (int)((float)BastionBuildTime * 0.9f)) & hatVisible)
			{
				GeneralParticleHandler.SpawnParticle(new WulfrumHatParticle(player, -Vector2.UnitY.RotatedByRandom(0.7853981852531433) * Main.rand.NextFloat(3f, 7f), 25));
			}
			if (cd.timeLeft < BastionCooldown + BastionTime - BastionBuildTime)
			{
				player.Transformation().Type = ModContent.ItemType<AbandonedWulfrumHelmet>();
			}
			if (DummyCannon.IsAir)
			{
				DummyCannon.SetDefaults(ModContent.ItemType<WulfrumFusionCannon>());
			}
			if (Main.myPlayer == player.whoAmI)
			{
				if (Main.mouseItem.type != DummyCannon.type && !Main.mouseItem.IsAir)
				{
					Main.LocalPlayer.QuickSpawnItem(null, Main.mouseItem, Main.mouseItem.stack);
				}
				Main.mouseItem = DummyCannon;
			}
			player.inventory[58] = DummyCannon;
			player.selectedItem = 58;
		}
		else if (Main.myPlayer == player.whoAmI)
		{
			if (Main.mouseItem.type == ModContent.ItemType<WulfrumFusionCannon>())
			{
				Main.mouseItem = new Item();
			}
			DummyCannon.TurnToAir();
			if (player.Transformation().Type == ModContent.ItemType<AbandonedWulfrumHelmet>() && player.Transformation().currentTransformation.IsForced)
			{
				player.Transformation().currentTransformation.IsForced = false;
			}
		}
	}

	public void ActivationEffects(Player player)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SetActivationSound);
		bool transformedAlready = player.Transformation().Type == ModContent.ItemType<AbandonedWulfrumHelmet>();
		if (!transformedAlready)
		{
			player.controlUseItem = false;
			player.controlUseTile = false;
			player.controlThrow = false;
			for (int i = 0; i < 5; i++)
			{
				GeneralParticleHandler.SpawnParticle(new WulfrumBastionPartsParticle(player, i, BastionBuildTime + 2));
			}
		}
		Particle gun = new WulfrumBastionPartsParticle(player, 5, BastionBuildTime + 2);
		if (transformedAlready)
		{
			(gun as WulfrumBastionPartsParticle).TimeOffset = 0f;
			(gun as WulfrumBastionPartsParticle).AnimationTime = BastionBuildTime + 2;
		}
		GeneralParticleHandler.SpawnParticle(gun);
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<SummonDamageClass>() += SummonDamageBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<WulfrumMetalScrap>(5).AddIngredient<EnergyCore>().AddTile(16)
			.Register();
	}
}
