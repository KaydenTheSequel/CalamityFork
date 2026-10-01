using System;
using System.Collections.Generic;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ExorcismProj : ModProjectile, ILocalizedModType, IModType
{
	public bool flung;

	public int fallTime;

	public NPC targeted;

	public Vector2 impaleDist;

	public int hitRate;

	public float randpitch;

	public Vector2 storedStealthVel;

	public Color mainColor;

	public SlotId AudSlot;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Exorcism";

	public ref float time => ref base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public bool hitTiles => base.Projectile.ai[1] == -5f;

	public bool inSky => base.Projectile.ai[1] == 5f;

	public bool falling => base.Projectile.ai[1] == 10f;

	public bool stealth => base.Projectile.Calamity().stealthStrike;

	public Vector2 crossCenter
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center + (Vector2.UnitY * -11f).RotatedBy(base.Projectile.rotation);
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 25;
		base.Projectile.height = 80;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 0;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override bool ShouldUpdatePosition()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		if (flung && !inSky && !hitTiles)
		{
			if (!(impaleDist == Vector2.Zero))
			{
				return stealth;
			}
			return true;
		}
		return false;
	}

	public override bool? CanDamage()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		if (!flung || inSky || hitTiles || !(impaleDist == Vector2.Zero))
		{
			return false;
		}
		return null;
	}

	public override void AI()
	{
		//IL_0c76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0efa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f81: Unknown result type (might be due to invalid IL or missing references)
		//IL_1082: Unknown result type (might be due to invalid IL or missing references)
		//IL_1087: Unknown result type (might be due to invalid IL or missing references)
		//IL_108c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1090: Unknown result type (might be due to invalid IL or missing references)
		//IL_109a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1002: Unknown result type (might be due to invalid IL or missing references)
		//IL_1012: Unknown result type (might be due to invalid IL or missing references)
		//IL_1017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d99: Unknown result type (might be due to invalid IL or missing references)
		//IL_083e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0843: Unknown result type (might be due to invalid IL or missing references)
		//IL_084d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0852: Unknown result type (might be due to invalid IL or missing references)
		//IL_066c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0baf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_0705: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_076c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0771: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_07be: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_080e: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08da: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0906: Unknown result type (might be due to invalid IL or missing references)
		//IL_090c: Unknown result type (might be due to invalid IL or missing references)
		//IL_090e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0933: Unknown result type (might be due to invalid IL or missing references)
		//IL_0938: Unknown result type (might be due to invalid IL or missing references)
		//IL_0957: Unknown result type (might be due to invalid IL or missing references)
		//IL_0966: Unknown result type (might be due to invalid IL or missing references)
		//IL_0970: Unknown result type (might be due to invalid IL or missing references)
		//IL_0976: Unknown result type (might be due to invalid IL or missing references)
		//IL_0978: Unknown result type (might be due to invalid IL or missing references)
		//IL_097d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a52: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a62: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a69: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b21: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.dead && !flung)
		{
			base.Projectile.Kill();
			return;
		}
		if (stealth && base.Projectile.ai[2] != 5f)
		{
			base.Projectile.ai[2] = 5f;
			base.Projectile.ForceNetUpdate();
		}
		if (base.Projectile.ai[2] == 5f)
		{
			base.Projectile.Calamity().stealthStrike = true;
		}
		if (!flung && time == 0f)
		{
			randpitch = Main.rand.NextFloat(-0.1f, 0.1f);
		}
		Color newColor;
		if (flung)
		{
			if (hitTiles)
			{
				holySound();
				if (base.Projectile.timeLeft > 300)
				{
					base.Projectile.timeLeft = 300;
				}
			}
			else if (inSky)
			{
				base.Projectile.Center = new Vector2(Owner.ClampedMouseWorld().X, Owner.Center.Y) + new Vector2(0f, -700f);
				base.Projectile.Opacity = 0f;
				base.Projectile.extraUpdates = 0;
				base.Projectile.timeLeft++;
				fallTime--;
				if (fallTime <= 0)
				{
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						base.Projectile.localNPCImmunity[i] = 0;
					}
					base.Projectile.Opacity = 1f;
					base.Projectile.velocity = base.Projectile.Center.DirectionTo(Owner.Calamity().mouseWorld) * 7f;
					base.Projectile.numHits = 0;
					base.Projectile.extraUpdates = 25;
					base.Projectile.ai[1] = 10f;
				}
				if (fallTime == 10)
				{
					for (int j = 0; j < 2; j++)
					{
						SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MissileNearing");
						style.Volume = 0.5f;
						style.Pitch = 0.8f;
						style.MaxInstances = 2;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
					}
				}
			}
			else if (falling)
			{
				if (base.Projectile.numHits == 0)
				{
					base.Projectile.timeLeft++;
					if (base.Projectile.Center.Y + 80f > Owner.Calamity().mouseWorld.Y && Collision.SolidCollision(base.Projectile.Center, 20, 20))
					{
						base.Projectile.netUpdate = true;
						base.Projectile.extraUpdates = 3;
						base.Projectile.ai[1] = -5f;
						SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/ExoHit2");
						style.Volume = 0.6f;
						style.Pitch = Main.rand.NextFloat(0f, 0.1f);
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						style = new SoundStyle("CalamityMod/Sounds/NPCHit/RavagerRockPillarHit", 3);
						style.Volume = 0.7f;
						style.Pitch = -0.3f;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
					}
					base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
					float squash = Utils.GetLerpValue(1f, 3f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 35f, base.Projectile.velocity * 0.01f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 43, 0.4f, mainColor * 0.4f * squash, new Vector2(1f - 0.15f * squash, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.07f * squash));
				}
				else
				{
					base.Projectile.extraUpdates = 3;
					base.Projectile.Center = targeted.Center + impaleDist;
					if (stealth)
					{
						base.Projectile.rotation += (0.08f - (float)hitRate * 0.001f) * (float)base.Projectile.direction * 0.3f;
					}
					holySound();
					if (time >= (float)hitRate)
					{
						impaleDist.Y *= 0.93f;
						Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), targeted.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), (int)((float)base.Projectile.damage * Utils.Remap(hitRate, 50f, 5f, 0.05f, 0.1f)), 0f, Owner.whoAmI, targeted.whoAmI, 1f);
						projectile.ArmorPenetration = 30;
						projectile.DamageType = RogueDamageClass.Instance;
						if (hitRate > 10)
						{
							hitRate -= 10;
						}
						time = 0f;
					}
				}
			}
			else if (stealth)
			{
				if (base.Projectile.timeLeft > 240)
				{
					if (base.Projectile.soundDelay == 0)
					{
						base.Projectile.soundDelay = 5 * base.Projectile.MaxUpdates;
						SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/SwooshMid");
						style.MaxInstances = -1;
						style.Volume = 0.5f;
						style.Pitch = -0.2f;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
					}
					base.Projectile.rotation += 0.07f * (float)base.Projectile.direction;
				}
				if (time > 80f)
				{
					if (((Vector2)(ref base.Projectile.velocity)).Length() < ((Vector2)(ref storedStealthVel)).Length())
					{
						Projectile projectile2 = base.Projectile;
						projectile2.velocity += base.Projectile.Center.DirectionTo(Owner.Center) * 0.1f;
					}
					Projectile projectile3 = base.Projectile;
					projectile3.velocity *= 0.99f;
					if (time == 180f)
					{
						for (int k = 0; k < Main.maxNPCs; k++)
						{
							base.Projectile.localNPCImmunity[k] = 0;
						}
					}
					if (base.Projectile.timeLeft <= 240)
					{
						impaleDist = Vector2.One;
						base.Projectile.extraUpdates = 3;
						float endLerp = Utils.GetLerpValue(240f, 90f, base.Projectile.timeLeft, clamped: true);
						Vector2 endPos = Vector2.Lerp(Owner.Center + Vector2.UnitY * (-50f - 90f * endLerp), Owner.ClampedMouseWorld(), (float)Math.Pow(endLerp, 5.0));
						base.Projectile.velocity = (endPos - base.Projectile.Center) / 25f;
						base.Projectile.rotation = base.Projectile.rotation.AngleLerp(0f, endLerp);
					}
				}
				else
				{
					storedStealthVel = -base.Projectile.velocity * 1.1f;
				}
				if (base.Projectile.timeLeft > 240)
				{
					int numParts = 2;
					for (int l = 0; l < numParts; l++)
					{
						float fade = Utils.GetLerpValue(5f, 2f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true) * 3f + 1f;
						float rot = base.Projectile.rotation + (float)Math.PI * 2f * (float)l / (float)numParts;
						Vector2 vel = (-base.Projectile.velocity).MoveTowards(Utils.RotatedBy(new Vector2(0f, -130f), (double)rot, default(Vector2)).RotatedBy(-1.3f * (float)base.Projectile.direction), Utils.GetLerpValue(5f, 2f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true));
						if (time % 5f == 0f)
						{
							Vector2 position = base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, -70f), (double)rot, default(Vector2));
							int type = (Main.rand.NextBool(4) ? 278 : ModContent.DustType<LightDust>());
							newColor = default(Color);
							Dust dust = Dust.NewDustPerfect(position, type, null, 0, newColor);
							dust.noGravity = dust.type != 278;
							dust.scale = ((dust.type == 278) ? 0.95f : 1.2f) * 0.6f;
							dust.color = Color.Red;
							dust.velocity = (vel * 2f).RotatedByRandom(0.4000000059604645) * fade;
						}
						if (time % 2f == 0f)
						{
							Vector2 position2 = base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, -70f), (double)rot, default(Vector2));
							int type2 = (Main.rand.NextBool(4) ? 278 : ModContent.DustType<LightDust>());
							newColor = default(Color);
							Dust dust2 = Dust.NewDustPerfect(position2, type2, null, 0, newColor);
							dust2.noGravity = dust2.type != 278;
							dust2.scale = ((dust2.type == 278) ? 0.75f : 0.9f) * 0.6f;
							dust2.color = (Main.rand.NextBool(4) ? Color.Khaki : Color.Goldenrod);
							dust2.velocity = (vel * 2f).RotatedByRandom(0.4000000059604645) * fade;
						}
					}
				}
				holySound();
			}
			else
			{
				base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
				if (time >= 120f)
				{
					base.Projectile.ai[1] = 5f;
				}
				if (time % 6f == 0f && time > 12f)
				{
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 35f + Main.rand.NextVector2Circular(10f, 10f), -base.Projectile.velocity * 0.01f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 13, 0.2f, mainColor * 0.6f, new Vector2(0.6f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.6f));
				}
			}
		}
		else
		{
			Vector2 projToSky = base.Projectile.Center.DirectionTo(new Vector2(MathHelper.Lerp(Owner.ClampedMouseWorld().X, Owner.Center.X, 0.35f), Owner.Center.Y) + new Vector2(0f, -500f));
			base.Projectile.velocity = projToSky;
			float completion = time / ((float)Owner.HeldItem.useAnimation * 0.7f);
			if (completion >= 1f)
			{
				base.Projectile.Center = Owner.Center;
				base.Projectile.extraUpdates = (stealth ? 9 : 6);
				if (stealth)
				{
					base.Projectile.timeLeft = 560;
				}
				Vector2 velocity = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
				float speed = 9f;
				base.Projectile.velocity = (stealth ? velocity : projToSky) * speed;
				time = -1f;
				SoundStyle w = new SoundStyle("CalamityMod/Sounds/Item/SwooshMid");
				for (int m = 0; m < 2; m++)
				{
					SoundEngine.PlaySound(w with
					{
						Volume = 1f,
						Pitch = -0.4f + 0.2f * (float)m,
						MaxInstances = 6
					}, base.Projectile.Center);
				}
				flung = true;
			}
			else
			{
				Owner.direction = Math.Sign(Owner.Center.DirectionTo(Owner.Calamity().mouseWorld).X);
				float crossRot = 0f;
				if (completion >= 0.7f)
				{
					float completionLerp = (float)Math.Pow(Utils.GetLerpValue(0.7f, 1f, completion, clamped: true), 7.0);
					crossRot = MathHelper.ToRadians(MathHelper.Lerp(-45f, 130f, completionLerp) * (float)Owner.direction);
				}
				else
				{
					float completionLerp2 = (float)Math.Pow(Utils.GetLerpValue(0f, 0.7f, completion, clamped: true), 2.0);
					crossRot = MathHelper.ToRadians(MathHelper.Lerp(120f, -45f, completionLerp2) * (float)Owner.direction);
				}
				crossRot += Owner.Center.DirectionTo(Owner.Calamity().mouseWorld).ToRotation();
				Vector2 crossPos = Owner.MountedCenter + Utils.RotatedBy(new Vector2(0f, (float)(-24 * Owner.direction)), (double)crossRot, default(Vector2));
				Math.Pow(Utils.GetLerpValue(0f, 0.7f, completion, clamped: true), 2.0);
				base.Projectile.Center = crossPos;
				if (stealth)
				{
					base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.Pow(completion, 3.0) * 12f * (float)Owner.direction;
				}
				else
				{
					base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
				}
				Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, Owner.Center.DirectionTo(Owner.Calamity().mouseWorld).ToRotation() - MathHelper.ToRadians(90f));
				Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, crossRot - ((Owner.direction == 1) ? MathHelper.ToRadians(180f) : MathHelper.ToRadians(0f)));
			}
		}
		time++;
		if (!inSky)
		{
			Vector2 center = base.Projectile.Center;
			newColor = Color.Gold;
			Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.5f);
		}
		if (targeted != null && ((targeted.life <= 0 && targeted.realLife == -1) || targeted.lifeMax == 1))
		{
			base.Projectile.Kill();
		}
	}

	public void holySound()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(AudSlot, out ActiveSound holy) && holy.IsPlaying)
		{
			holy.Position = base.Projectile.Center;
			holy.Pitch = Utils.Remap(base.Projectile.timeLeft, 300f, 0f, -0.4f + randpitch, (stealth ? (-0.15f) : (-0.3f)) + randpitch);
			holy.Volume = Utils.Remap(base.Projectile.timeLeft, 300f, 0f, 0f, 0.5f) * 100f;
		}
		else if (base.Projectile.timeLeft > 1)
		{
			SoundStyle choir = new SoundStyle("CalamityMod/Sounds/Item/HolyLoop");
			SoundStyle style = choir with
			{
				Volume = 0.01f,
				Pitch = 0f
			};
			AudSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (stealth)
		{
			float minMult = 0.35f;
			int hitsToMinMult = 10;
			float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
			modifiers.SourceDamage *= damageMult / 2f;
		}
		else if (!falling || base.Projectile.numHits != 0)
		{
			modifiers.SourceDamage *= 0.1f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		if ((target.life > 0 || target.realLife != -1) && target.lifeMax != 1 && falling)
		{
			if (!stealth)
			{
				time = 0f;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/ExoHit4");
				style.Volume = 0.7f;
				style.Pitch = Main.rand.NextFloat(0.15f, 0.25f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				style = new SoundStyle("CalamityMod/Sounds/NPCHit/ExoHit3");
				style.Volume = 0.5f;
				style.Pitch = Main.rand.NextFloat(-0.1f, 0f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				Owner.SetScreenshake(5.5f);
				base.Projectile.timeLeft = 300;
				targeted = target;
				impaleDist = base.Projectile.Center - targeted.Center;
			}
		}
		else
		{
			base.Projectile.numHits--;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_062d: Unknown result type (might be due to invalid IL or missing references)
		//IL_063a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0640: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(AudSlot, out ActiveSound holy))
		{
			holy?.Stop();
		}
		SoundStyle style;
		if (stealth)
		{
			if (Main.zenithWorld)
			{
				for (int index = 0; index < Main.npc.Length; index++)
				{
					Main.npc[index].active = false;
				}
				for (int x = 0; x < Main.maxProjectiles; x++)
				{
					Projectile projectile = Main.projectile[x];
					if (projectile.active && projectile.type != ModContent.ProjectileType<ExorcismShockwave>() && projectile.type != ModContent.ProjectileType<ExorcismProj>())
					{
						projectile.active = false;
					}
				}
				for (int i = 0; i < Main.maxItems; i++)
				{
					Item item = Main.item[i];
					if (item.active)
					{
						item.active = false;
					}
				}
				for (int j = 0; j < 6000; j++)
				{
					Dust dust = Main.dust[j];
					if (dust.active)
					{
						dust.active = false;
					}
				}
				for (int k = 0; k < 600; k++)
				{
					Gore gore = Main.gore[k];
					if (gore.active)
					{
						gore.active = false;
					}
				}
				SoundStyle gong = new SoundStyle("CalamityMod/Sounds/Custom/GFB/Jesus");
				for (int l = 0; l < 2; l++)
				{
					SoundEngine.PlaySound(gong with
					{
						Volume = 1f,
						MaxInstances = 2
					}, base.Projectile.Center);
				}
			}
			else
			{
				Owner.SetScreenshake(7f);
				SoundStyle soundBurst = new SoundStyle("CalamityMod/Sounds/Item/HolyBurst");
				for (int m = 0; m < 3; m++)
				{
					SoundEngine.PlaySound(soundBurst with
					{
						Volume = 0.8f,
						Pitch = 0.2f * (float)m,
						MaxInstances = 3
					}, base.Projectile.Center);
				}
				style = new SoundStyle("CalamityMod/Sounds/Item/HolyColliderProjectileHit");
				style.Volume = 0.65f;
				style.Pitch = 0.6f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int n = 0; n < 4; n++)
				{
					for (int y = 0; y < 24; y++)
					{
						bool red = Main.rand.NextBool(5);
						float variance = Main.rand.NextFloat(0.7f, 1f);
						float placementVariance = 35f;
						Vector2 fxVel = Vector2.UnitY.RotatedBy((float)Math.PI / 2f * (float)n) * Main.rand.NextFloat((float)y * 1.2f, (float)y * 1.5f) * ((n == 0) ? 1.7f : 1f) * variance;
						Vector2 fxPos = crossCenter + Main.rand.NextVector2CircularEdge(placementVariance - variance * placementVariance, placementVariance - variance * placementVariance);
						Dust dust2 = Dust.NewDustPerfect(fxPos, ModContent.DustType<LightDust>(), fxVel * 1.7f, 0, default(Color), Main.rand.NextFloat(1.2f, 1.6f));
						dust2.noGravity = true;
						dust2.scale = (red ? 1.4f : 1.2f) * Main.rand.NextFloat(0.7f, 0.9f);
						dust2.color = (red ? Color.Red : mainColor);
						if (y % 2 == 0)
						{
							GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(fxPos, fxVel, affectedByGravity: false, 24, 0.065f, Main.rand.NextBool() ? Color.Khaki : mainColor, new Vector2(0.8f, 0.3f), quickShrink: true, glow: false, 0.5f));
						}
					}
					Vector2 fxVel2 = Vector2.UnitY.RotatedBy((float)Math.PI / 2f * (float)n) * ((n == 0) ? 1.7f : 1f);
					GeneralParticleHandler.SpawnParticle(new CustomSpark(crossCenter + fxVel2 * 145f, fxVel2, "CalamityMod/Particles/BloomLineFade", affectedByGravity: false, 15, 0.15f, mainColor, new Vector2(1.8f, 1.2f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.8f, 0.8f, 0.8f));
				}
			}
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), crossCenter, Vector2.Zero, ModContent.ProjectileType<ExorcismShockwave>(), (int)((float)base.Projectile.damage * 1.5f), 0f, Owner.whoAmI).Calamity().stealthStrike = true;
			return;
		}
		style = new SoundStyle("CalamityMod/Sounds/Item/HolyBurst");
		style.Volume = 1f;
		style.Pitch = 0f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), crossCenter, Vector2.Zero, ModContent.ProjectileType<ExorcismShockwave>(), (int)((float)base.Projectile.damage * 0.35f), 0f, Owner.whoAmI);
		for (int num = 0; num < 4; num++)
		{
			for (int num2 = 0; num2 < 14; num2++)
			{
				bool red2 = Main.rand.NextBool(5);
				float variance2 = Main.rand.NextFloat(0.7f, 1f);
				float placementVariance2 = 27f;
				Vector2 fxVel3 = Vector2.UnitY.RotatedBy((float)Math.PI / 2f * (float)num) * Main.rand.NextFloat((float)num2 * 0.8f, num2) * ((num == 0) ? 1.7f : 1f) * variance2;
				Vector2 fxPos2 = crossCenter + Main.rand.NextVector2CircularEdge(placementVariance2 - variance2 * placementVariance2, placementVariance2 - variance2 * placementVariance2);
				Dust dust3 = Dust.NewDustPerfect(fxPos2, ModContent.DustType<LightDust>(), fxVel3 * 1.5f, 0, default(Color), Main.rand.NextFloat(1.2f, 1.6f));
				dust3.noGravity = true;
				dust3.scale = (red2 ? 1.2f : 1f) * Main.rand.NextFloat(0.7f, 0.9f);
				dust3.color = (red2 ? Color.Red : mainColor);
				if (num2 % 2 == 0)
				{
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(fxPos2, fxVel3, affectedByGravity: false, 18, 0.05f, Main.rand.NextBool() ? Color.Khaki : mainColor, new Vector2(0.8f, 0.3f), quickShrink: true, glow: false, 0.6f));
				}
			}
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (stealth)
		{
			return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 120f, targetHitbox);
		}
		return base.Colliding(projHitbox, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		float glowUp = 1f + Utils.GetLerpValue(250f, 0f, base.Projectile.timeLeft, clamped: true);
		float throwCompletion = (flung ? 1f : ((float)Math.Pow(Math.Min(time / ((float)Owner.HeldItem.useAnimation * 0.7f), 1f), 5.0)));
		Color glowColor = Color.Lerp(mainColor, Color.Khaki, glowUp - 1f);
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Asset<Texture2D> glowBlade = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowBlade", (AssetRequestMode)2);
		Asset<Texture2D> glowBottom = ModContent.Request<Texture2D>("CalamityMod/Particles/SquareRotated", (AssetRequestMode)2);
		Asset<Texture2D> woosh = ModContent.Request<Texture2D>("CalamityMod/Particles/VerticalSmearLarge", (AssetRequestMode)2);
		Color val;
		if (hitTiles || falling || stealth)
		{
			for (int i = 0; i < 25; i++)
			{
				Vector2 drawPos = base.Projectile.Center;
				val = glowColor;
				((Color)(ref val)).A = 0;
				Color auraColor = val * 0.05f * glowUp;
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 25f).ToRotationVector2() * glowUp * 2f;
				Vector2 position = drawPos - Main.screenPosition + drawOffset + Main.rand.NextVector2Circular(6f, 6f) * (1f - glowUp);
				val = auraColor;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(tex, position, null, val * base.Projectile.Opacity, base.Projectile.rotation, tex.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
			}
		}
		if (stealth)
		{
			for (int j = 0; j < 4; j++)
			{
				Vector2 fxVel = Vector2.UnitY.RotatedBy((float)Math.PI / 2f * (float)j).RotatedBy(base.Projectile.rotation) * ((j == 0) ? 1.2f : 1f) * 80f * throwCompletion;
				Vector2 fxPos = crossCenter + fxVel;
				if (j == 0 && base.Projectile.timeLeft > 240)
				{
					Texture2D value = woosh.Value;
					Vector2 position2 = base.Projectile.Center - Main.screenPosition;
					val = Color.Khaki;
					((Color)(ref val)).A = 0;
					Main.EntitySpriteDraw(value, position2, null, val * 0.1f, fxVel.ToRotation() + base.Projectile.rotation + Main.GlobalTimeWrappedHourly * (float)base.Projectile.direction * 27f, woosh.Size() / 2f, base.Projectile.scale * 0.45f * throwCompletion, (SpriteEffects)0);
					Texture2D value2 = woosh.Value;
					Vector2 position3 = base.Projectile.Center - Main.screenPosition;
					val = mainColor;
					((Color)(ref val)).A = 0;
					Main.EntitySpriteDraw(value2, position3, null, val * 0.15f, fxVel.ToRotation() - 1.3f * (float)base.Projectile.direction + base.Projectile.rotation + Main.GlobalTimeWrappedHourly * (float)base.Projectile.direction * 27f, woosh.Size() / 2f, base.Projectile.scale * 0.41f * throwCompletion, (SpriteEffects)0);
				}
				for (int y = 0; y < 2; y++)
				{
					Texture2D value3 = glowBlade.Value;
					Vector2 position4 = fxPos - Main.screenPosition;
					val = ((y != 0) ? Color.White : mainColor);
					((Color)(ref val)).A = 0;
					Main.EntitySpriteDraw(value3, position4, null, val * (0.3f * glowUp), fxVel.ToRotation() + (float)Math.PI / 2f, glowBlade.Size() / 2f, new Vector2(0.4f * ((y != 0) ? 0.65f : 1f), 1f * throwCompletion * glowUp * ((j == 0) ? 1.5f : 1f)) * base.Projectile.scale * 0.04f, (SpriteEffects)0);
				}
				float softGlowUp = (float)Math.Pow(glowUp, 0.15000000596046448);
				for (int k = 0; k < 3; k++)
				{
					Texture2D value4 = glowBottom.Value;
					Vector2 position5 = fxPos - Main.screenPosition - fxVel * 0.55f;
					val = ((k != 0) ? Color.White : mainColor);
					((Color)(ref val)).A = 0;
					Main.EntitySpriteDraw(value4, position5, null, val * (0.1f * glowUp), fxVel.ToRotation() + (float)Math.PI / 2f, glowBottom.Size() / 2f, new Vector2(1f * ((k != 0) ? 0.65f : 1f) * softGlowUp, 1.7f * throwCompletion * softGlowUp * ((j == 0) ? 1.5f : 1f)) * base.Projectile.scale * ((k == 2) ? 0.3f : 0.25f), (SpriteEffects)0);
				}
			}
		}
		Vector2 position6 = base.Projectile.Center - Main.screenPosition;
		val = Color.White;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(tex, position6, null, Color.Lerp(val, lightColor, 2f - glowUp) * (float)((!inSky) ? 1 : 0), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		overPlayers.Add(index);
	}

	public ExorcismProj()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		fallTime = 55;
		hitRate = 60;
		mainColor = Color.Gold;
		base._002Ector();
	}
}
