using System;
using System.Collections.Generic;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class SHPV : ModProjectile, ILocalizedModType, IModType
{
	private static Asset<Texture2D> Smoke = ModContent.Request<Texture2D>("CalamityMod/Particles/MediumMist", (AssetRequestMode)2);

	private static Asset<Texture2D> SoulSquare = ModContent.Request<Texture2D>("CalamityMod/Particles/Square", (AssetRequestMode)2);

	private SlotId VacuumSound;

	private const int VacuumStartFrames = 78;

	private const int VacuumLoopFrames = 132;

	private bool PlayedEndSound;

	private const float Size = 576f;

	private const float Spread = (float)Math.PI * 50f / 333f;

	private const int MaxSoulsRing = 10;

	public List<float> SoulColors = new List<float>();

	private bool FiringLasers;

	private bool ConsumeSoul;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	private Player Owner => Main.player[base.Projectile.owner];

	public ref float Timer => ref base.Projectile.ai[0];

	private static Vector2 Offset
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(27f, -10f);
		}
	}

	public Vector2 TipPosition
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center + Vector2.UnitX.RotatedBy(base.Projectile.rotation) * 62f + Vector2.UnitY * Offset.Y;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 70);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.CantUseHoldout(needsToHold: false))
		{
			base.Projectile.Kill();
			return;
		}
		if (Timer == 0f)
		{
			VacuumSound = SoundEngine.PlaySound(in SHPC.VacuumStart, Owner.Center);
		}
		if (SoundEngine.TryGetActiveSound(VacuumSound, out ActiveSound ChargeSound) && ChargeSound.IsPlaying)
		{
			ChargeSound.Position = Owner.Center;
		}
		Timer++;
		if (!Owner.Calamity().mouseRight)
		{
			FiringLasers = true;
		}
		if (FiringLasers)
		{
			if (!PlayedEndSound)
			{
				if (SoundEngine.TryGetActiveSound(VacuumSound, out ActiveSound Vacuum))
				{
					Vacuum?.Stop();
				}
				VacuumSound = SoundEngine.PlaySound(in SHPC.VacuumEnd, Owner.Center);
				PlayedEndSound = true;
			}
			if (SoulColors.Count > 0 && Timer % 5f == 0f)
			{
				if (Owner.HeldItem.ModItem is SHPC shpc)
				{
					SoundEngine.PlaySound(in CommonCalamitySounds.ELRFireSound, Owner.Center);
					Vector2 laserPos = TipPosition + Vector2.UnitY.RotatedBy(base.Projectile.rotation) * Main.rand.NextFloat(-7f, 7f);
					Vector2 laserVel = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld) * 20f;
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), laserPos, laserVel, ModContent.ProjectileType<SHPL>(), (int)((float)base.Projectile.damage * 1.4f), 3f, base.Projectile.owner, SoulColors[0]);
					if (ConsumeSoul)
					{
						SoulColors.RemoveAt(0);
						if (shpc.storedSoulpower > 0)
						{
							shpc.storedSoulpower--;
						}
						else if (SHPC.FindSoulForAmmo(Owner) != -1)
						{
							int soulType = SHPC.FindSoulForAmmo(Owner);
							Owner.ConsumeItem(soulType);
							shpc.storedSoulType = soulType;
							shpc.storedSoulpower = 50;
						}
						else
						{
							SoulColors.Clear();
						}
					}
					ConsumeSoul = !ConsumeSoul;
				}
				else
				{
					SoulColors.Clear();
				}
			}
		}
		else
		{
			if (Timer % 2f == 0f)
			{
				Vector2 val = Owner.Center + Owner.Center.DirectionTo(Owner.Calamity().mouseWorld).RotatedByRandom(0.4717106223106384) * 576f * Main.rand.NextFloat(0.4f, 1f);
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(val, val.DirectionTo(TipPosition) * 4.5f, Color.Gray, Color.DarkGray, 1.25f, 96f));
			}
			if ((Timer - 78f) % 132f == 0f)
			{
				VacuumSound = SoundEngine.PlaySound(in SHPC.VacuumLoop, Owner.Center);
			}
		}
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.ChangeDir(Math.Sign((Owner.Calamity().mouseWorld - Owner.Center).X));
		Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, (Owner.Center - Owner.Calamity().mouseWorld).ToRotation() * Owner.gravDir + (float)Math.PI / 2f);
		Owner.SetDummyItemTime(2);
		base.Projectile.rotation = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld).ToRotation();
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.Center = Owner.Center;
		if (!FiringLasers || SoulColors.Count > 0)
		{
			base.Projectile.timeLeft = 48;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(VacuumSound, out ActiveSound ChargeSound))
		{
			ChargeSound?.Stop();
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		if (FiringLasers)
		{
			return false;
		}
		bool withinAngle = Math.Abs(TipPosition.DirectionTo(Owner.Calamity().mouseWorld).ToRotation() - TipPosition.DirectionTo(((Rectangle)(ref targetHitbox)).Center.ToVector2()).ToRotation()) <= (float)Math.PI * 50f / 333f;
		Rectangle extraSafetyHitbox = default(Rectangle);
		((Rectangle)(ref extraSafetyHitbox))._002Ector((int)TipPosition.X - base.Projectile.width / 2, (int)TipPosition.Y - base.Projectile.height / 2, base.Projectile.width, base.Projectile.height);
		return (CalamityUtils.CircularHitboxCollision(TipPosition, 576f, targetHitbox) & withinAngle) || ((Rectangle)(ref targetHitbox)).Intersects(extraSafetyHitbox);
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		Vector2 farthestPos = Owner.Center + Owner.Center.DirectionTo(Owner.Calamity().mouseWorld) * 576f;
		float rotation = TipPosition.DirectionTo(farthestPos).ToRotation();
		if (!FiringLasers)
		{
			Texture2D tex = Smoke.Value;
			Rectangle frame = tex.Frame(1, 3, 0, Main.rand.Next(3));
			Main.spriteBatch.SetBlendState(BlendState.Additive);
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					float distRatio = 1f - (float)((Main.GameUpdateCount + j * 10) % 30) / 30f;
					Vector2 posOffset = Utils.RotatedBy(new Vector2(MathF.Sin((float)Math.PI / 3f * (float)i) * 25f * distRatio, MathF.Cos((float)Main.GameUpdateCount * (float)Math.PI / 30f + (float)Math.PI / 3f * (float)i) * 240f * distRatio), (double)rotation, default(Vector2));
					float colorMult = 0.5f * Utils.GetLerpValue(1f, 0.8f, distRatio, clamped: true);
					Main.EntitySpriteDraw(tex, Vector2.Lerp(TipPosition, farthestPos, distRatio) + posOffset - Main.screenPosition, frame, Color.Gray * colorMult, rotation + (float)Math.PI, frame.Size() / 2f, 1.5f * distRatio, (SpriteEffects)0);
				}
			}
			Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		}
		Texture2D shpc = TextureAssets.Item[ModContent.ItemType<SHPC>()].Value;
		Vector2 position = Owner.Center - Main.screenPosition + Vector2.UnitX.RotatedBy(rotation) * Offset.X + Vector2.UnitY * Offset.Y;
		SpriteEffects sp = (SpriteEffects)((Owner.Calamity().mouseWorld.X < Owner.Center.X) ? 2 : 0);
		Main.EntitySpriteDraw(shpc, position, null, lightColor, rotation, shpc.Size() / 2f, 1f, sp);
		if (SoulColors.Count > 0)
		{
			Texture2D orbitingSoulTexture = SoulSquare.Value;
			for (int k = 0; k < SoulColors.Count; k++)
			{
				int soulsInRing = ((SoulColors.Count <= 10) ? SoulColors.Count : ((SoulColors.Count - k > SoulColors.Count % 10) ? 10 : (SoulColors.Count % 10)));
				int ringsBack = k / 10;
				Vector2 soulPosition = base.Projectile.Center + Vector2.UnitX.RotatedBy(rotation) * (68f - (float)ringsBack * 9f) + Vector2.UnitY * -12.5f;
				Vector2 posOffset2 = Utils.RotatedBy(new Vector2(MathF.Sin((float)Main.GameUpdateCount * (float)Math.PI / 30f + (float)Math.PI * 2f / (float)soulsInRing * (float)(k % 10)) * 3f, MathF.Cos((float)Main.GameUpdateCount * (float)Math.PI / 30f + (float)Math.PI * 2f / (float)soulsInRing * (float)(k % 10)) * 25f), (double)rotation, default(Vector2));
				if (posOffset2.RotatedBy(0f - rotation).X <= 2.7f)
				{
					Main.EntitySpriteDraw(orbitingSoulTexture, soulPosition + posOffset2 - Main.screenPosition, null, SHPB.FindColorForSoul((int)SoulColors[k]), 0f, orbitingSoulTexture.Size() * 0.5f, 1f, (SpriteEffects)0);
					Main.EntitySpriteDraw(orbitingSoulTexture, soulPosition + posOffset2 - Main.screenPosition, null, Color.White, 0f, orbitingSoulTexture.Size() * 0.5f, 0.5f, (SpriteEffects)0);
				}
			}
		}
		return false;
	}
}
