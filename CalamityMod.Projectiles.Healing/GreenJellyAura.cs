using System.Collections.Generic;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Items.Accessories;
using CalamityMod.Particles;
using CalamityMod.Systems.Collections;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Healing;

public class GreenJellyAura : ModProjectile, ILocalizedModType, IModType
{
	public int ShinkGrow;

	public int Framecounter;

	public int CleanseOnce = 1;

	public int PulseOnce = 1;

	public int PulseOnce2 = 1;

	public int PulseOnce3 = 1;

	public static readonly SoundStyle Spawnsound = new SoundStyle("CalamityMod/Sounds/Custom/OrbHeal3")
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
		base.Projectile.timeLeft = GrandGelatin.AuraLifetime + 10;
	}

	public override void AI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		Framecounter++;
		for (int playerIndex = 0; playerIndex < 255; playerIndex++)
		{
			Player player = Main.player[playerIndex];
			if (!(Vector2.Distance(player.Center, base.Projectile.Center) < 245f))
			{
				continue;
			}
			player.AddBuff(ModContent.BuffType<GreenJellyRegen>(), 480);
			if (cleanseList[playerIndex])
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
				int dust = Dust.NewDust(player.Center, player.width + 4, player.height + 4, 298, player.velocity.X * 0.2f, player.velocity.Y * 0.2f, 100, default(Color), 5.5f);
				Main.dust[dust].noGravity = true;
				Dust obj = Main.dust[dust];
				obj.velocity *= 1.5f;
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
				GeneralParticleHandler.SpawnParticle(new StaticPulseRing(base.Projectile.Center, Vector2.Zero, Color.Lime, new Vector2(1f, 1f), 0f, 0f, 0.225f, 10));
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
				GeneralParticleHandler.SpawnParticle(new StaticPulseRing(base.Projectile.Center, Vector2.Zero, Color.Lime, new Vector2(1f, 1f), 0f, 0.225f, 0.225f, 1790));
				PulseOnce2 = 0;
			}
			for (int j = 0; j < 2; j++)
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2CircularEdge(232f, 232f), 298);
				dust2.scale = Main.rand.NextFloat(2.2f, 3.3f);
				dust2.noGravity = true;
			}
			for (int k = 0; k < 1; k++)
			{
				Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(225f, 225f), 298);
				dust3.scale = Main.rand.NextFloat(0.8f, 1.3f);
				dust3.noGravity = true;
			}
			if (Framecounter == GrandGelatin.AuraLifetime)
			{
				ShinkGrow = 2;
			}
		}
		if (ShinkGrow == 2 && PulseOnce3 == 1)
		{
			GeneralParticleHandler.SpawnParticle(new StaticPulseRing(base.Projectile.Center, Vector2.Zero, Color.Lime, new Vector2(1f, 1f), 0f, 0.225f, 0f, 10));
			PulseOnce3 = 0;
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
