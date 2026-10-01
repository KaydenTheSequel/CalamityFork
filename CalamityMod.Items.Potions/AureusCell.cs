using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

[LegacyName(new string[] { "AstralJelly" })]
public class AureusCell : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.FoodParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(187, 220, 237),
			new Color(237, 93, 83),
			new Color(123, 99, 130)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(22, 38, 7, CalamityUtils.MinutesToFrames(8));
		base.Item.healMana = 200;
		base.Item.UseSound = SoundID.Item3;
		base.Item.value = Item.sellPrice(0, 0, 50);
		base.Item.rare = 7;
	}

	public override void OnConsumeItem(Player player)
	{
		if (PlayerInput.Triggers.JustPressed.QuickBuff)
		{
			player.statMana += base.Item.healMana;
			if (player.statMana > player.statManaMax2)
			{
				player.statMana = player.statManaMax2;
			}
			player.AddBuff(94, Player.manaSickTime);
			if (Main.myPlayer == player.whoAmI)
			{
				player.ManaEffect(base.Item.healMana);
			}
		}
		player.AddBuff(7, base.Item.buffTime);
		player.AddBuff(6, base.Item.buffTime);
	}
}
