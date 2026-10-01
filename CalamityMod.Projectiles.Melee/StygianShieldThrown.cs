using System;
using System.Collections.Generic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class StygianShieldThrown : ModProjectile, ILocalizedModType, IModType
{
	public const int ReboundTime = 90;

	public const int MaxBounces = 1;

	public const float MaxHomingRange = 640f;

	public const float ReturnPiercingDamageMult = 0.6f;

	private List<int> PreviousNPCs = new List<int> { -1 };

	private SlotId LoopSoundSlot;

	public const float TotalTrailLength = 35f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float AirTime => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 35;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 50);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 180 * base.Projectile.MaxUpdates;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		if (AirTime == 0f)
		{
			SoundEngine.PlaySound(in StygianShield.ShieldThrowSound, base.Projectile.Center);
		}
		if ((AirTime + 40f) % 60f == 0f)
		{
			LoopSoundSlot = SoundEngine.PlaySound(in StygianShield.ThrowLoopSound, base.Projectile.Center);
		}
		if (SoundEngine.TryGetActiveSound(LoopSoundSlot, out ActiveSound LoopSound) && LoopSound.IsPlaying)
		{
			LoopSound.Position = base.Projectile.Center;
		}
		base.Projectile.rotation += (float)base.Projectile.direction * 0.4f;
		Vector2 squishFactor = default(Vector2);
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			if (base.Projectile.oldPos[i] != Vector2.Zero && i % 7 == 3)
			{
				float lengthRatio = (float)i / 35f;
				Vector2 position = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f;
				Color redGradient = Color.Lerp(Color.LightPink, Color.Red, lengthRatio);
				Color blackGradient = Color.Lerp(new Color(40, 40, 40), Color.Black, lengthRatio);
				float rotMotion = (float)base.Projectile.timeLeft * ((float)Math.PI * 2f) / 60f;
				float trueRot = base.Projectile.oldRot[i] + rotMotion;
				((Vector2)(ref squishFactor))._002Ector(0.8f, 1f);
				GeneralParticleHandler.SpawnParticle(new SemiCircularSmearVFX(position, redGradient, trueRot, 0.3f, squishFactor));
				GeneralParticleHandler.SpawnParticle(new SemiCircularSmearVFX(position, blackGradient, trueRot + (float)Math.PI, 0.3f, squishFactor));
			}
		}
		AirTime++;
		if (AirTime >= 90f)
		{
			ReturnToOwner();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in StygianShield.ShieldThrowHitSound, base.Projectile.Center);
		PreviousNPCs.Add(target.whoAmI);
		if (SeekNPC() == -1)
		{
			ReturnToOwner();
		}
		if (base.Projectile.numHits >= 1)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.6f);
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in StygianShield.ShieldThrowHitSound, base.Projectile.Center);
		if (SeekNPC() == -1)
		{
			ReturnToOwner();
		}
		base.Projectile.numHits++;
		return false;
	}

	public int SeekNPC()
	{
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.numHits >= 1)
		{
			return -1;
		}
		float range = 640f;
		int targetNPC = -1;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC target = enumerator.Current;
			if (target.CanBeChasedBy(base.Projectile) && !PreviousNPCs.Contains(target.whoAmI))
			{
				float distance = Vector2.Distance(target.Center, base.Projectile.Center);
				if (distance < range && Collision.CanHit(base.Projectile, target))
				{
					range = distance;
					targetNPC = target.whoAmI;
				}
			}
		}
		if ((float)targetNPC != -1f)
		{
			AirTime = 0f;
			base.Projectile.velocity = base.Projectile.SafeDirectionTo(Main.npc[targetNPC].Center) * 15f;
		}
		return targetNPC;
	}

	public void ReturnToOwner()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		AirTime = 90f;
		base.Projectile.numHits = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.velocity = base.Projectile.SafeDirectionTo(Owner.Center) * 20f;
		Rectangle hitbox = base.Projectile.Hitbox;
		if (((Rectangle)(ref hitbox)).Intersects(Owner.Hitbox) || Vector2.Distance(base.Projectile.Center, Owner.Center) >= 3000f)
		{
			SoundEngine.PlaySound(in StygianShield.ShieldCatchSound, Owner.Center);
			if (SoundEngine.TryGetActiveSound(LoopSoundSlot, out ActiveSound LoopSound))
			{
				LoopSound?.Stop();
			}
			base.Projectile.Kill();
		}
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = (height = 32);
		return true;
	}
}
