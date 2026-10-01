using CalamityMod.Enums;
using CalamityMod.Events;
using CalamityMod.NPCs.ExoMechs;
using CalamityMod.Packets;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class BossRushEffectThing : ModProjectile, ILocalizedModType, IModType
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
		base.Projectile.timeLeft = 120;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = Owner.Center;
		if (Time >= 70f)
		{
			MoonlordDeathDrama.RequestLight(Utils.GetLerpValue(70f, 85f, Time, clamped: true), Main.LocalPlayer.Center);
		}
		if (Time % 10f == 9f)
		{
			BossRushEvent.SyncStartTimer((int)Time);
		}
		float currentShakePower = MathHelper.Lerp(8f, 12f, Utils.GetLerpValue(72f, 120f, Time, clamped: true));
		currentShakePower *= 1f - Utils.GetLerpValue(1500f, 3700f, Main.LocalPlayer.Distance(base.Projectile.Center), clamped: true);
		Main.LocalPlayer.SetScreenshake(currentShakePower);
		Time++;
	}

	public override void OnKill(int timeLeft)
	{
		BossRushEvent.SyncStartTimer(120);
		for (int doom = 0; doom < Main.maxNPCs; doom++)
		{
			NPC n = Main.npc[doom];
			if (n.active && (n.boss || n.type == 13 || n.type == 14 || n.type == 15 || n.type == ModContent.NPCType<Draedon>()))
			{
				n.active = false;
				n.netUpdate = true;
			}
		}
		BossRushEvent.BossRushStage = 0;
		BossRushEvent.BossRushActive = true;
		BossRushDialogueSystem.StartDialogue(DownedBossSystem.startedBossRushAtLeastOnce ? BossRushDialoguePhase.StartRepeat : BossRushDialoguePhase.Start);
		CalamityNetcode.SyncWorld();
		if (Main.dedServ)
		{
			BossRushStagePacket.Send();
		}
	}
}
