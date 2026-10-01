using CalamityMod.Events;
using CalamityMod.Items.Placeables;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Events;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class BossRushEndEffectThing : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Time => ref base.Projectile.ai[0];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.aiStyle = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 340;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		if (Time == 25f)
		{
			SoundEngine.PlaySound(in BossRushEvent.VictorySound, Main.LocalPlayer.Center);
		}
		base.Projectile.Center = Owner.Center;
		BossRushEvent.SyncEndTimer((int)Time);
		float currentShakePower = MathHelper.Lerp(1f, 20f, Utils.GetLerpValue(140f, 180f, Time, clamped: true) * Utils.GetLerpValue(10f, 40f, base.Projectile.timeLeft, clamped: true));
		if (base.Projectile.timeLeft > 5)
		{
			Main.LocalPlayer.SetScreenshake(currentShakePower);
		}
		MoonlordDeathDrama.RequestLight(Utils.GetLerpValue(220f, 265f, Time, clamped: true) * Utils.GetLerpValue(10f, 30f, base.Projectile.timeLeft, clamped: true), Main.LocalPlayer.Center);
		if (base.Projectile.timeLeft < 5 && BossRushDialogueSystem.CurrentDialogueDelay != 0)
		{
			base.Projectile.timeLeft = 5;
		}
		Time++;
	}

	public override void OnKill(int timeLeft)
	{
		BossRushEvent.End();
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player p = enumerator.Current;
			int rock = Item.NewItem(p.GetSource_Misc("CalamityMod_BossRushRock"), (int)p.position.X, (int)p.position.Y, p.width, p.height, ModContent.ItemType<Rock>());
			if (rock < Main.maxItems && Main.dedServ)
			{
				Main.timeItemSlotCannotBeReusedFor[rock] = 54000;
				NetMessage.SendData(90, p.whoAmI, -1, null, rock);
			}
		}
	}
}
