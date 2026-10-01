using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class ShatteredCommunityPlayer : ModPlayer
{
	internal ShatteredCommunity sc;

	public override void ResetEffects()
	{
		sc = null;
	}

	internal void AccumulateRageDamage(long damage)
	{
		if (sc != null)
		{
			sc.totalRageDamage += damage;
			if (sc.level < 25 && sc.totalRageDamage > ShatteredCommunity.CumulativeLevelCost(sc.level + 1))
			{
				sc.level++;
				LevelUpEffects(sc.Item);
			}
		}
	}

	private void LevelUpEffects(Item item)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		IEntitySource source_Accessory = base.Player.GetSource_Accessory(item);
		int projID = 672;
		Vector2 offset = default(Vector2);
		((Vector2)(ref offset))._002Ector(0f, 800f);
		Projectile projectile = Projectile.NewProjectileDirect(source_Accessory, base.Player.Center + offset, Vector2.Zero, projID, 0, 0f, base.Player.whoAmI);
		projectile.friendly = false;
		projectile.hostile = false;
		projectile.timeLeft = 107;
		projectile.MaxUpdates = 2;
		SoundStyle extraSound = SoundID.DD2_EtherianPortalDryadTouch with
		{
			Volume = SoundID.DD2_EtherianPortalDryadTouch.Volume * 1.4f
		};
		SoundEngine.PlaySound(in extraSound, base.Player.Center);
		Rectangle location = new Rectangle((int)base.Player.Center.X, (int)base.Player.Center.Y, 1, 1);
		Color textColor = default(Color);
		((Color)(ref textColor))._002Ector(236, 209, 236);
		CombatText.NewText(location, textColor, CalamityUtils.GetTextValueFromModItem<ShatteredCommunity>("LevelUpText"));
	}
}
