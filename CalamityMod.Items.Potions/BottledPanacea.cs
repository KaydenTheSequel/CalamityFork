using CalamityMod.Cooldowns;
using CalamityMod.DataStructures;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

public class BottledPanacea : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[2]
		{
			new Color(84, 215, 254),
			new Color(35, 101, 192)
		};
	}

	public override void SetDefaults()
	{
		base.Item.UseSound = SoundID.Item3;
		base.Item.useStyle = 9;
		base.Item.useTurn = true;
		base.Item.useTime = (base.Item.useAnimation = 17);
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 1;
	}

	public override bool CanUseItem(Player player)
	{
		return !player.HasCooldown(PanaceaCooldown.ID);
	}

	public override bool? UseItem(Player player)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		if (player.itemAnimation > 0 && player.itemTime == 0)
		{
			player.itemTime = base.Item.useTime;
			SoundEngine.PlaySound(in SoundID.Item4, player.Center);
			for (int i = Player.MaxBuffs - 1; i >= 0; i--)
			{
				int buffType = player.buffType[i];
				if (BuffDatasets.DebuffDataset[buffType] != null && (BuffDatasets.DebuffDataset[buffType].SicknessDebuffScaling > 0f || BuffDatasets.DebuffDataset[buffType].ElectricDebuffScaling > 0f))
				{
					player.DelBuff(i);
				}
			}
			Vector2 plusVel = default(Vector2);
			for (int j = 0; j < 4; j++)
			{
				((Vector2)(ref plusVel))._002Ector(Main.rand.NextFloat(-0.55f, 0.55f), Main.rand.NextFloat(-6f, 0f));
				GeneralParticleHandler.SpawnParticle(new HealingPlus(Main.rand.NextVector2FromRectangle(player.Hitbox), Main.rand.NextFloat(1.25f, 1.5f), plusVel, Color.SkyBlue, Color.SkyBlue, 20));
			}
			for (int d = 0; d < 10; d++)
			{
				Dust.NewDust(player.position, player.width, player.height, 59);
			}
			player.AddCooldown(PanaceaCooldown.ID, CalamityUtils.SecondsToFrames(30));
		}
		return true;
	}
}
