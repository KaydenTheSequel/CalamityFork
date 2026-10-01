using System;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class WulfrumManaDrain : ModProjectile, ILocalizedModType, IModType
{
	private SlotId SuccSoundSlot;

	public new string LocalizationCategory => "Projectiles.Magic";

	public Player Owner => Main.player[base.Projectile.owner];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float Timer => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 2;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void AI()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		if (!Owner.Calamity().mouseRight || Owner.dead || Owner.frozen || !Owner.active || Owner.statMana == Owner.statManaMax2)
		{
			return;
		}
		if (!SoundEngine.TryGetActiveSound(SuccSoundSlot, out ActiveSound idleSoundOut) || !idleSoundOut.IsPlaying)
		{
			SoundStyle style = WulfrumProsthesis.SuckSound with
			{
				Volume = WulfrumProsthesis.SuckSound.Volume * 0.01f,
				IsLooped = true
			};
			SuccSoundSlot = SoundEngine.PlaySound(in style, Owner.Center);
		}
		else if (idleSoundOut != null)
		{
			idleSoundOut.Position = Owner.Center;
			idleSoundOut.Volume = Math.Clamp(Timer / 30f + 0.001f, 0f, 1f) * 100f;
		}
		base.Projectile.timeLeft = 2;
		base.Projectile.Center = Owner.MountedCenter;
		base.Projectile.velocity = (Owner.Calamity().mouseWorld - Owner.MountedCenter).SafeNormalize(Vector2.One);
		if (Main.rand.NextBool(6))
		{
			GeneralParticleHandler.SpawnParticle(new ManaDrainStreak(Owner, Main.rand.NextFloat(0.2f, 0.5f), base.Projectile.velocity.RotatedByRandom(0.9424778819084167) * Main.rand.NextFloat(70f, 150f), Main.rand.NextFloat(30f, 44f), Color.GreenYellow, Color.DeepSkyBlue, Main.rand.Next(13, 20)));
		}
		NPC target = GetSuccTarget();
		if (target != null)
		{
			Owner.GetModPlayer<WulfrumProsthesisPlayer>().ManaDrainActive = true;
			if (!Main.rand.NextBool(3))
			{
				Vector2 center = target.Center;
				center.X += (float)Main.rand.Next(-100, 100) * 0.1f;
				center.Y += (float)Main.rand.Next(-100, 100) * 0.1f;
				center += target.velocity;
				GeneralParticleHandler.SpawnParticle(new ManaDrainBlob(Owner, center, Main.rand.NextVector2Circular(4f, 4f), Main.rand.NextFloat(0.7f, 0.9f), Color.DeepSkyBlue));
			}
		}
		Timer++;
	}

	public NPC GetSuccTarget()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC struckNPC = enumerator.Current;
			if (!struckNPC.townNPC && !struckNPC.friendly)
			{
				float distance = struckNPC.Distance(base.Projectile.Center);
				float extraDistance = struckNPC.width / 2 + struckNPC.height / 2;
				if (distance - extraDistance < 400f && Collision.CheckAABBvLineCollision(struckNPC.Hitbox.TopLeft(), struckNPC.Hitbox.Size(), Owner.MountedCenter, Owner.MountedCenter + base.Projectile.velocity * 400f, 110f, ref collisionPoint) && (Collision.CanHit(base.Projectile.Center, 1, 1, struckNPC.Center, 1, 1) || !(extraDistance < distance)))
				{
					return struckNPC;
				}
			}
		}
		return null;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		if (Timer > 2f)
		{
			NPC target = GetSuccTarget();
			if (target != null)
			{
				int particlesCount = Main.rand.Next(5, 10);
				for (int i = 0; i < particlesCount; i++)
				{
					Vector2 center = target.Center;
					center.X += (float)Main.rand.Next(-100, 100) * 0.1f;
					center.Y += (float)Main.rand.Next(-100, 100) * 0.1f;
					center += target.velocity;
					GeneralParticleHandler.SpawnParticle(new ManaDrainBlob(Owner, center, Main.rand.NextVector2Circular(4f, 4f), Main.rand.NextFloat(0.76f, 1f), Color.DeepSkyBlue));
				}
			}
		}
		if (SoundEngine.TryGetActiveSound(SuccSoundSlot, out ActiveSound soundOut))
		{
			soundOut.Stop();
			SoundStyle style = WulfrumProsthesis.SuckStopSound with
			{
				Volume = WulfrumProsthesis.SuckStopSound.Volume * Timer / 30f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
	}
}
