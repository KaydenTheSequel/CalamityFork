using System.Collections.Generic;
using CalamityMod.Items.Accessories;
using CalamityMod.Particles;
using CalamityMod.Systems.Collections;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Healing;

public class BlueJellyAura : ModProjectile, ILocalizedModType, IModType
{
	public int ShinkGrow;

	public int Framecounter;

	public int CleanseOnce = 1;

	public int PulseOnce = 1;

	public int PulseOnce2 = 1;

	public int PulseOnce3 = 1;

	public static readonly SoundStyle Spawnsound = new SoundStyle("CalamityMod/Sounds/Custom/OrbHeal1")
	{
		Volume = 0.5f
	};

	public List<bool> cleanseList = new List<bool>(new bool[255]);

	public new string LocalizationCategory => "Projectiles.Healing";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref int CleansingEffect => ref Main.player[base.Projectile.owner].Calamity().CleansingEffect;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 336);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = CleansingJelly.AuraLifetime + 10;
	}

	public override void AI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		Framecounter++;
		for (int playerIndex = 0; playerIndex < 255; playerIndex++)
		{
			Player player = Main.player[playerIndex];
			if (!(Vector2.Distance(player.Center, base.Projectile.Center) < 165f) || cleanseList[playerIndex])
			{
				continue;
			}
			cleanseList[playerIndex] = true;
			CleansingEffect = 1;
			for (int l = 0; l < Player.MaxBuffs; l++)
			{
				int buffID = player.buffType[l];
				if (player.buffTime[l] > 2 && CalamityBuffSets.IsDebuff[buffID])
				{
					player.buffTime[l] = 0;
				}
			}
			for (int i = 0; i < 55; i++)
			{
				int dust = Dust.NewDust(player.Center, player.width + 4, player.height + 4, 187, player.velocity.X * 0.2f, player.velocity.Y * 0.2f, 100, default(Color), 5.5f);
				Main.dust[dust].noGravity = true;
				Dust obj = Main.dust[dust];
				obj.velocity *= 1.2f;
				Main.dust[dust].velocity.Y -= 0.5f;
			}
			SoundStyle style = Spawnsound with
			{
				Pitch = -0.9f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (ShinkGrow == 0)
		{
			if (PulseOnce == 1)
			{
				GeneralParticleHandler.SpawnParticle(new StaticPulseRing(base.Projectile.Center, Vector2.Zero, Color.RoyalBlue, new Vector2(1f, 1f), 0f, 0f, 0.152f, 10));
				PulseOnce = 0;
			}
			if (Framecounter == 10)
			{
				ShinkGrow = 1;
			}
		}
		if (ShinkGrow == 1)
		{
			if (PulseOnce2 == 1)
			{
				GeneralParticleHandler.SpawnParticle(new StaticPulseRing(base.Projectile.Center, Vector2.Zero, Color.RoyalBlue, new Vector2(1f, 1f), 0f, 0.152f, 0.152f, 1790));
				PulseOnce2 = 0;
			}
			for (int j = 0; j < 1; j++)
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2CircularEdge(155f, 155f), 187);
				dust2.scale = Main.rand.NextFloat(2.2f, 3.3f);
				dust2.noGravity = true;
			}
			for (int k = 0; k < 1; k++)
			{
				Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(150f, 150f), 187);
				dust3.scale = Main.rand.NextFloat(0.8f, 1.3f);
				dust3.noGravity = true;
			}
			if (Framecounter == CleansingJelly.AuraLifetime)
			{
				ShinkGrow = 2;
			}
		}
		if (ShinkGrow == 2 && PulseOnce3 == 1)
		{
			GeneralParticleHandler.SpawnParticle(new StaticPulseRing(base.Projectile.Center, Vector2.Zero, Color.RoyalBlue, new Vector2(1f, 1f), 0f, 0.152f, 0f, 10));
			PulseOnce3 = 0;
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
