using System;
using System.Linq;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses.BrainOfCthulhu;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss.BrainOfCthulhu;

public class TelekineticBlast : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	private Player target => Main.player[(int)base.Projectile.ai[0]];

	private float debuffMultiplier
	{
		get
		{
			if (Main.npc[NPCSource].type != 266)
			{
				return 1f;
			}
			return 2f;
		}
	}

	private int delay
	{
		get
		{
			return (int)base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = value;
		}
	}

	private int NPCSource => (int)base.Projectile.ai[2];

	public override void SetDefaults()
	{
		base.Projectile.width = 1;
		base.Projectile.height = 1;
		base.Projectile.penetrate = -1;
		base.Projectile.Opacity = 1f;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 10;
		base.Projectile.damage = 0;
		base.Projectile.scale = 1f;
		base.Projectile.hostile = true;
		base.Projectile.netImportant = true;
	}

	public override void OnSpawn(IEntitySource source)
	{
		base.Projectile.netUpdate = true;
	}

	public override void AI()
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		if (Main.npc[NPC.crimsonBoss].AIOverride<BrainOfCthulhuAI>().AttackFlag || Main.npc[NPC.crimsonBoss].AIOverride<BrainOfCthulhuAI>().AIState == BrainOfCthulhuAI.BrainAIState.DeathAnimation)
		{
			base.Projectile.active = false;
		}
		else if (--delay <= 0)
		{
			if (Main.npc[NPCSource].ModNPC is FalseBrain illusion)
			{
				illusion.BeenHit = true;
				Main.npc[NPCSource].netUpdate = true;
			}
			for (int i = 0; i < 6; i++)
			{
				Vector2 dir = target.Center - base.Projectile.Center;
				int lifeTime = 24;
				dir /= (float)lifeTime / 2f * 5f;
				dir *= (float)i;
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, dir, (i % 2 == 0) ? Color.Red : Color.Orange, new Vector2(0.5f, 1f), dir.ToRotation(), 0f, (float)i / 5f, lifeTime + 8));
			}
			SoundEngine.PlaySound(in BrainOfCthulhuAI.Laugh, base.Projectile.Center);
			target.AddBuff(22, (int)Math.Round(900f * debuffMultiplier));
			target.AddBuff(30, (int)Math.Round(900f * debuffMultiplier));
			target.AddBuff(31, (int)Math.Round(60f * debuffMultiplier));
			int timeToAdd = (int)Math.Round(300f * debuffMultiplier);
			int bbIndex = target.buffType.ToList().IndexOf(ModContent.BuffType<BurningBlood>());
			if (bbIndex != -1)
			{
				timeToAdd += target.buffTime[bbIndex];
			}
			if (timeToAdd > 3600)
			{
				timeToAdd = 3600;
			}
			target.AddBuff(ModContent.BuffType<BurningBlood>(), timeToAdd);
			target.Hurt(PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.BrainIllusion" + Main.rand.Next(1, 4)).ToNetworkText(target.name)), 100, (!(Main.npc[NPC.crimsonBoss].Center.X > target.Center.X)) ? 1 : (-1), pvp: false, quiet: false, 0, dodgeable: false, 0f, 1f);
			target.Calamity().adrenaline = 0f;
			base.Projectile.active = false;
		}
	}
}
