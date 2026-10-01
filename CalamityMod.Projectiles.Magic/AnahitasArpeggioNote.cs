using System;
using System.IO;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.NPCs;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

[PierceResistException(true)]
public class AnahitasArpeggioNote : ModProjectile, ILocalizedModType, IModType
{
	public int LingeringTime;

	public int FadeOutTime;

	public bool HasSetFadeOutVelocity;

	public Vector2 ReleaseCenterPoint;

	public float _randomReleaseRotationOffset;

	public SlotId StupidEasterEggSlot;

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float Timer => ref base.Projectile.ai[0];

	public ref float AIState => ref base.Projectile.ai[1];

	public ref float NoteSequence => ref base.Projectile.ai[2];

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 5;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 16;
	}

	public override void AI()
	{
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0635: Unknown result type (might be due to invalid IL or missing references)
		//IL_063a: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_0658: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_066d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		Timer++;
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.scale += 0.02f;
			if (base.Projectile.scale >= 1.15f)
			{
				base.Projectile.localAI[0] = 1f;
			}
		}
		else if (base.Projectile.localAI[0] == 1f)
		{
			base.Projectile.scale -= 0.02f;
			if (base.Projectile.scale <= 0.85f)
			{
				base.Projectile.localAI[0] = 0f;
			}
		}
		if (Timer == 1f)
		{
			if (Main.zenithWorld)
			{
				if (NoteSequence == 0f)
				{
					StupidEasterEggSlot = SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/GFB/SevenTrebleClefSouls"), Owner.Center);
				}
			}
			else
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HarpLV" + Math.Clamp((int)NoteSequence + 1, 1, 6));
				style.Volume = 0.8f;
				SoundEngine.PlaySound(in style, Owner.Center);
			}
		}
		if (Main.zenithWorld && NoteSequence == 0f && Timer % 2428f == 0f && AIState == 0f)
		{
			StupidEasterEggSlot = SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Custom/GFB/SevenTrebleClefSouls"), Owner.Center);
		}
		if (SoundEngine.TryGetActiveSound(StupidEasterEggSlot, out ActiveSound TrebleSoul) && TrebleSoul.IsPlaying)
		{
			TrebleSoul.Position = Owner.Center;
		}
		if (Main.zenithWorld)
		{
			Lighting.AddLight(base.Projectile.Center, 1.25f, 1.25f, 1.25f);
		}
		else
		{
			Lighting.AddLight(base.Projectile.Center, 0f, 0f, 1.25f);
		}
		if (AIState == 0f)
		{
			base.Projectile.timeLeft = LingeringTime + FadeOutTime;
			float rotationSpeed = (Main.zenithWorld ? 0.857142f : 1f) * (60f / (float)Owner.HeldItem.useTime);
			base.Projectile.Center = Owner.Center + Utils.RotatedBy(new Vector2(80f, 0f), (double)MathHelper.ToRadians(Timer * rotationSpeed), default(Vector2));
			if (Owner.releaseUseItem || !Owner.CheckMana(Owner.HeldItem.mana))
			{
				Owner.Calamity().arpeggioCooldown = 45;
				AIState = 1f;
				base.Projectile.netUpdate = true;
			}
		}
		else if (AIState == 1f)
		{
			Vector2 playerDirection = base.Projectile.Center - Owner.Center;
			if (!HasSetFadeOutVelocity)
			{
				((Vector2)(ref playerDirection)).Normalize();
				playerDirection *= 5.5f;
				base.Projectile.velocity = playerDirection;
				HasSetFadeOutVelocity = true;
			}
			base.Projectile.alpha += (int)Math.Ceiling(255f / (float)FadeOutTime);
			if (base.Projectile.alpha < 255)
			{
				return;
			}
			base.Projectile.alpha = 255;
			float degreesAmt = (Main.zenithWorld ? 51.428f : 60f);
			Vector2 musicNoteRotationOffset = Vector2.UnitY.RotatedBy(MathHelper.ToRadians(degreesAmt * NoteSequence) + _randomReleaseRotationOffset);
			Vector2 mouse = Owner.ClampedMouseWorld();
			base.Projectile.Center = mouse + musicNoteRotationOffset * 220f;
			ReleaseCenterPoint = mouse;
			playerDirection = base.Projectile.Center - mouse;
			((Vector2)(ref playerDirection)).Normalize();
			playerDirection *= -13f;
			base.Projectile.velocity = playerDirection;
			if (Main.zenithWorld)
			{
				if (SoundEngine.TryGetActiveSound(StupidEasterEggSlot, out ActiveSound Flowey))
				{
					Flowey?.Stop();
				}
			}
			else
			{
				SoundStyle style = AnahitasArpeggio.EndSound with
				{
					Volume = 0.8f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			AIState = 2f;
		}
		else
		{
			if (AIState != 2f)
			{
				return;
			}
			if (base.Projectile.timeLeft > 30)
			{
				base.Projectile.alpha -= 17;
				if (base.Projectile.alpha < 0)
				{
					base.Projectile.alpha = 0;
				}
			}
			else
			{
				base.Projectile.alpha += 9;
				if (base.Projectile.alpha > 255)
				{
					base.Projectile.alpha = 255;
				}
			}
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 0.5f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.925f;
				return;
			}
			base.Projectile.velocity = Vector2.Zero;
			Vector2 centerPointDirection = (Owner.Calamity().mouseWorld - ReleaseCenterPoint).SafeNormalize(Vector2.Zero);
			float distToMove = MathF.Min(5.75f, Vector2.Distance(Owner.Calamity().mouseWorld, ReleaseCenterPoint));
			ReleaseCenterPoint += centerPointDirection * distToMove;
			Projectile projectile2 = base.Projectile;
			projectile2.Center += centerPointDirection * distToMove;
			base.Projectile.Center = ReleaseCenterPoint + ReleaseCenterPoint.DirectionTo(base.Projectile.Center).RotatedBy(0.03141592815518379) * Vector2.Distance(ReleaseCenterPoint, base.Projectile.Center);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld)
		{
			Color stupidEasterEggColor = default(Color);
			float noteSequence = NoteSequence;
			if (noteSequence != 0f)
			{
				if (noteSequence != 1f)
				{
					if (noteSequence != 2f)
					{
						if (noteSequence != 3f)
						{
							if (noteSequence != 4f)
							{
								if (noteSequence != 5f)
								{
									if (noteSequence == 6f)
									{
										((Color)(ref stupidEasterEggColor))._002Ector(128, 0, 255);
									}
								}
								else
								{
									((Color)(ref stupidEasterEggColor))._002Ector(0, 0, 255);
								}
							}
							else
							{
								((Color)(ref stupidEasterEggColor))._002Ector(0, 255, 255);
							}
						}
						else
						{
							((Color)(ref stupidEasterEggColor))._002Ector(0, 255, 0);
						}
					}
					else
					{
						((Color)(ref stupidEasterEggColor))._002Ector(255, 255, 0);
					}
				}
				else
				{
					((Color)(ref stupidEasterEggColor))._002Ector(255, 128, 0);
				}
			}
			else
			{
				((Color)(ref stupidEasterEggColor))._002Ector(255, 0, 0);
			}
			return stupidEasterEggColor;
		}
		return base.GetAlpha(lightColor);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override bool? CanDamage()
	{
		return AIState == 2f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(103, 900);
		target.AddBuff(31, 300);
		if (!SoundEngine.TryGetActiveSound(SingularSoundInstanceSystem.SoundSlot, out ActiveSound _))
		{
			SingularSoundInstanceSystem.PlaySingleInstance(AnahitasArpeggio.HitSound, 60, 60, Owner);
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(_randomReleaseRotationOffset);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		_randomReleaseRotationOffset = reader.ReadSingle();
	}

	public AnahitasArpeggioNote()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		LingeringTime = 300;
		FadeOutTime = 20;
		ReleaseCenterPoint = Vector2.Zero;
		base._002Ector();
	}
}
