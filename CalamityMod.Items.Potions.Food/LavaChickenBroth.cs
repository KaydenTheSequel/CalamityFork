using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Rarities;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions.Food;

[LegacyName(new string[] { "Fabsoup" })]
public class LavaChickenBroth : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/SoupConsumption");

	public SlotId DrinkSoundSlot;

	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Type, new DrawAnimationVertical(int.MaxValue, 3));
		ItemID.Sets.IsFood[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 50;
		base.Item.value = 0;
		base.Item.rare = ModContent.RarityType<CalamityRed>();
		base.Item.maxStack = 1;
		base.Item.consumable = false;
		base.Item.useAnimation = 901;
		base.Item.useTime = 901;
		base.Item.UseSound = null;
		base.Item.useStyle = 2;
		base.Item.useTurn = true;
	}

	public override bool? UseItem(Player player)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		DrinkSoundSlot = SoundEngine.PlaySound(in UseSound, player.Center);
		return true;
	}

	public override void UseItemFrame(Player player)
	{
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		int time = CalamityUtils.MinutesToFrames(25) + CalamityUtils.SecondsToFrames(25);
		if (player.itemAnimation == 180)
		{
			player.AddBuff(207, time);
		}
		if (player.itemAnimation == 60)
		{
			player.AddBuff(207, time);
			player.AddBuff(24, time);
			player.AddBuff(44, time);
			player.AddBuff(39, time);
			player.AddBuff(ModContent.BuffType<Shadowflame>(), time);
			player.AddBuff(ModContent.BuffType<BrimstoneFlames>(), time);
			player.AddBuff(ModContent.BuffType<HolyFlames>(), time);
			player.AddBuff(ModContent.BuffType<GodSlayerInferno>(), time);
			player.AddBuff(ModContent.BuffType<Dragonfire>(), time);
			player.AddBuff(ModContent.BuffType<VulnerabilityHex>(), time);
		}
		if (SoundEngine.TryGetActiveSound(DrinkSoundSlot, out ActiveSound drinkSound) && drinkSound.IsPlaying)
		{
			drinkSound.Position = player.Center;
		}
	}
}
