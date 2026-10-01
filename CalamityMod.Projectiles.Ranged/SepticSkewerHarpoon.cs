using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.DataStructures;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SepticSkewerHarpoon : ModProjectile, ILocalizedModType, IModType
{
	public Vector2[] OldVelocities;

	public bool canDamage;

	public bool returning;

	public float CenterX;

	public float CenterY;

	public bool setPosition;

	public int returnTime;

	public NPC chosenTarget;

	public bool stuckInTarget;

	public bool canStick;

	public Vector2 placementCenter;

	private float placementDistance;

	private Vector2 placementVelocity;

	public Vector2 storedVelocity;

	public bool collideWithTiles;

	public bool hasHitTile;

	public Color bColor;

	public bool ripped;

	public bool pullingTarget;

	public bool hasLatchedTarget;

	public bool spawnPullBlood;

	public bool calledToPull;

	public bool strongEnemy;

	public bool normalHit;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float time => ref base.Projectile.ai[0];

	public bool pullCheckValid
	{
		get
		{
			if (chosenTarget != null && (float)chosenTarget.life < (float)base.Projectile.damage * 20f && !calledToPull && chosenTarget.realLife == -1 && !normalHit)
			{
				if (!CalamityPlayer.areThereAnyDamnBosses)
				{
					return true;
				}
				return chosenTarget.life <= chosenTarget.lifeMax / 4;
			}
			return false;
		}
	}

	public Vector2 DrawStartPosition
	{
		get
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			if (base.Projectile.owner < 0 || base.Projectile.owner >= Main.player.Length)
			{
				return Vector2.Zero;
			}
			return Main.player[base.Projectile.owner].Center;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 900;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e22: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_103e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f68: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0943: Unknown result type (might be due to invalid IL or missing references)
		//IL_094e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0806: Unknown result type (might be due to invalid IL or missing references)
		//IL_0811: Unknown result type (might be due to invalid IL or missing references)
		//IL_0852: Unknown result type (might be due to invalid IL or missing references)
		//IL_085d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0871: Unknown result type (might be due to invalid IL or missing references)
		//IL_0876: Unknown result type (might be due to invalid IL or missing references)
		//IL_0972: Unknown result type (might be due to invalid IL or missing references)
		//IL_097c: Unknown result type (might be due to invalid IL or missing references)
		//IL_098a: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_09af: Unknown result type (might be due to invalid IL or missing references)
		//IL_088a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0883: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_089e: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c84: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aa8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b37: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		Vector2.Distance(Owner.Center, base.Projectile.Center);
		if (calledToPull && !hasLatchedTarget)
		{
			ripped = true;
			pullingTarget = false;
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.extraUpdates = (int)MathHelper.Clamp(base.Projectile.ai[1], 1f, 50f);
		}
		if (!stuckInTarget && !hasHitTile && !returning)
		{
			storedVelocity = base.Projectile.velocity;
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 7f)
		{
			collideWithTiles = false;
		}
		if (time >= (float)returnTime * ((!pullCheckValid) ? ((float)((!stuckInTarget) ? 1 : 5)) : (strongEnemy ? 1.5f : 0.9f)) || ripped)
		{
			stuckInTarget = false;
			returning = true;
			if (pullCheckValid && chosenTarget != null && spawnPullBlood)
			{
				CalamityGlobalNPC calamityGlobalNPC = chosenTarget.Calamity();
				pullingTarget = true;
				canDamage = false;
				chosenTarget.damage = 0;
				calamityGlobalNPC.pacified = true;
				chosenTarget.Center = base.Projectile.Center;
				chosenTarget.velocity = base.Projectile.velocity;
			}
			else
			{
				canDamage = true;
			}
		}
		if (hasLatchedTarget && !pullingTarget)
		{
			chosenTarget.velocity = storedVelocity * (strongEnemy ? 0.4f : 2f);
			storedVelocity *= (strongEnemy ? 0.982f : 0.975f);
		}
		if (returning)
		{
			int startTime = 1000;
			int endTime = startTime + (pullingTarget ? 45 : (ripped ? 25 : 85));
			if (setPosition)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ChainPull");
				style.Pitch = Main.rand.NextFloat(0f, 0.05f);
				style.Volume = ((base.Projectile.numHits > 0 && ripped) ? 0.6f : 0.4f);
				SoundEngine.PlaySound(in style, Owner.Center);
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					base.Projectile.localNPCImmunity[i] = 0;
				}
				canStick = false;
				time = 1000f;
				CenterX = base.Projectile.Center.X;
				CenterY = base.Projectile.Center.Y;
				setPosition = false;
				if (base.Projectile.numHits > 0 && !pullingTarget)
				{
					float ripIntensity = ((!ripped) ? 1 : 2);
					if (ripped)
					{
						SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Custom/Perforator/PerfHiveIchorShoot");
						SoundStyle pull2 = new SoundStyle("CalamityMod/Sounds/Item/FinalDawnSlash");
						style = soundStyle with
						{
							Pitch = Main.rand.NextFloat(0f, -0.2f),
							Volume = 0.7f
						};
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						style = pull2 with
						{
							Pitch = Main.rand.NextFloat(0.3f, 0.4f),
							Volume = 0.9f
						};
						SoundEngine.PlaySound(in style, base.Projectile.Center);
					}
					else
					{
						SoundStyle soundStyle2 = new SoundStyle("CalamityMod/Sounds/Custom/Perforator/PerfHiveShoot3");
						SoundStyle pull3 = new SoundStyle("CalamityMod/Sounds/Item/WulfrumKnifeTileHit2");
						style = soundStyle2 with
						{
							Pitch = -0.2f,
							Volume = 0.6f
						};
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						style = pull3 with
						{
							Pitch = -0.6f,
							Volume = 0.5f
						};
						SoundEngine.PlaySound(in style, base.Projectile.Center);
					}
					for (int j = 0; (float)j < 7f * ripIntensity; j++)
					{
						Vector2 vel = ((base.Projectile.Center - Owner.Center).SafeNormalize(Vector2.UnitX) * -18f).RotatedByRandom(0.2f * ripIntensity) * Main.rand.NextFloat(0.2f, 0.6f) * ripIntensity;
						if (j % 3 == 0)
						{
							GeneralParticleHandler.SpawnParticle(new AltLineParticle(base.Projectile.Center, vel, affectedByGravity: true, (int)(18f * ripIntensity), Main.rand.NextFloat(0.55f, 0.8f) * ripIntensity, ((!ChildSafety.Disabled) ? Color.LimeGreen : Color.DarkRed) * 0.8f));
						}
						else
						{
							GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center, vel, affectedByGravity: true, (int)(18f * ripIntensity), Main.rand.NextFloat(0.55f, 0.8f) * ripIntensity, ((!ChildSafety.Disabled) ? Color.LimeGreen : Color.DarkRed) * 0.8f, AddativeBlend: false, needed: false, GlowCenter: false));
						}
						Dust.NewDustPerfect(base.Projectile.Center, (!ChildSafety.Disabled) ? 75 : 5, vel * 3f, 100, default(Color), Main.rand.NextFloat(0.8f, 1.4f)).noGravity = true;
					}
					if (!pullingTarget)
					{
						for (int k = 0; k < 4 + (ripped ? 2 : 0); k++)
						{
							Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, ((base.Projectile.Center - Owner.Center).SafeNormalize(Vector2.UnitX) * -18f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.3f, 1.2f), ModContent.ProjectileType<SepticSkewerBacteria>(), base.Projectile.damage / 9, base.Projectile.knockBack, base.Projectile.owner);
						}
					}
				}
			}
			storedVelocity += (base.Projectile.Center - Owner.Center).SafeNormalize(Vector2.UnitX) * 0.5f;
			base.Projectile.Center = new Vector2(MathHelper.Lerp(CenterX, Owner.Center.X, Utils.GetLerpValue(startTime, endTime, time, clamped: true)), MathHelper.Lerp(CenterY, Owner.Center.Y, Utils.GetLerpValue(startTime, endTime, time, clamped: true)));
			if (time >= (float)endTime)
			{
				base.Projectile.Kill();
			}
			if (time >= (float)(endTime - 2) && pullingTarget)
			{
				if (spawnPullBlood && chosenTarget != null && chosenTarget.life > 0)
				{
					SoundStyle style;
					if (base.Projectile.ai[1] == 0f && strongEnemy)
					{
						style = new SoundStyle("CalamityMod/Sounds/NPCKilled/PerfHiveDeath");
						style.Pitch = Main.rand.NextFloat(0.1f, 0.2f);
						style.Volume = 0.95f;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						style = new SoundStyle("CalamityMod/Sounds/NPCKilled/PerfLargeDeath");
						style.Pitch = Main.rand.NextFloat(0f, 0.1f);
						style.Volume = 0.85f;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						for (int l = 0; l < 3; l++)
						{
							GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, (!ChildSafety.Disabled) ? Color.LimeGreen : bColor, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.5f + (float)l * 0.2f, 30, UseAdditiveBlend: false, 1f, fade: true, 1f, (SpriteEffects)0));
						}
						Owner.SetScreenshake(8.5f);
					}
					style = new SoundStyle("CalamityMod/Sounds/NPCKilled/PerfLargeDeath");
					style.Pitch = Main.rand.NextFloat(0.2f, 0.3f);
					style.Volume = 0.85f;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					float intensity = (strongEnemy ? 1.2f : 0.5f);
					for (int m = 0; m < (int)(40f * intensity); m++)
					{
						Vector2 vel2 = (Vector2.One * -28f).RotatedByRandom(100.0) * Main.rand.NextFloat(0.2f, 0.9f) * intensity;
						if (m % 3 == 0)
						{
							GeneralParticleHandler.SpawnParticle(new AltLineParticle(base.Projectile.Center, vel2, affectedByGravity: true, 35, Main.rand.NextFloat(0.55f, 1.3f), ((!ChildSafety.Disabled) ? Color.LimeGreen : Color.DarkRed) * 0.8f));
						}
						else
						{
							GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center, vel2, affectedByGravity: true, 35, Main.rand.NextFloat(0.55f, 1.3f), ((!ChildSafety.Disabled) ? Color.LimeGreen : Color.DarkRed) * 0.8f, AddativeBlend: false, needed: false, GlowCenter: false));
						}
						for (int r = 0; r < 2; r++)
						{
							Vector2 vel3 = (Vector2.One * -28f).RotatedByRandom(100.0) * Main.rand.NextFloat(0.1f, 0.8f) * intensity;
							Dust.NewDustPerfect(base.Projectile.Center, (!ChildSafety.Disabled) ? 75 : 5, vel3, 100, default(Color), Main.rand.NextFloat(0.9f, 1.7f)).noGravity = false;
						}
						if (strongEnemy)
						{
							Dust.NewDustPerfect(base.Projectile.Center, 278, vel2 * 0.6f, 0, (!ChildSafety.Disabled) ? Color.LimeGreen : Color.DarkRed, Main.rand.NextFloat(0.7f, 0.9f)).noGravity = false;
						}
					}
					NPC closestTarget = null;
					float distance = 2000f;
					for (int index = 0; index < Main.npc.Length; index++)
					{
						if (Main.npc[index].CanBeChasedBy())
						{
							_ = Main.npc[index].width / 2;
							_ = Main.npc[index].height / 2;
							bool canHit = true;
							if (((Vector2.Distance(base.Projectile.Center, Main.npc[index].Center) < distance) & canHit) && Main.npc[index] != chosenTarget)
							{
								distance = Vector2.Distance(base.Projectile.Center, Main.npc[index].Center);
								closestTarget = Main.npc[index];
							}
						}
					}
					if (closestTarget != null)
					{
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.Center, (closestTarget.Center - Owner.Center + closestTarget.velocity * 1.5f).SafeNormalize(Vector2.UnitX) * 18f, base.Projectile.type, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, base.Projectile.ai[1] + 1f);
					}
					if (!chosenTarget.SpawnedFromStatue)
					{
						int heal = Math.Max(25 - (int)base.Projectile.ai[1], 10);
						Owner.HealPlayer(heal);
					}
					spawnPullBlood = false;
				}
				canDamage = true;
			}
		}
		if ((time <= (float)returnTime * 0.6f && !stuckInTarget) || (returning && Main.rand.NextBool(5)))
		{
			Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(9f, 9f), Main.rand.NextBool(7) ? 28 : 215, storedVelocity * Main.rand.NextFloat(0.05f, 0.15f), 0, default(Color), Main.rand.NextFloat(0.5f, 0.9f)).noGravity = true;
		}
		if (stuckInTarget)
		{
			placementCenter = chosenTarget.Center + placementVelocity * placementDistance + storedVelocity * 2f;
			base.Projectile.Center = placementCenter;
			if (chosenTarget.life <= 0 || chosenTarget == null)
			{
				returning = true;
				stuckInTarget = false;
			}
		}
		else if (time >= (float)returnTime * 0.4f && !returning)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.95f;
		}
		if (collideWithTiles && Collision.SolidCollision(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 30f, 4, 4) && base.Projectile.ai[1] < 1f)
		{
			hasHitTile = true;
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= -0.5f;
			Vector2 sparkVelocity = base.Projectile.velocity * 3f;
			for (int n = 0; n < 6; n++)
			{
				float sparkScale1 = Main.rand.NextFloat(0.3f, 0.8f);
				Vector2 sparkvelocity1 = sparkVelocity.RotatedByRandom(0.44999998807907104) * Main.rand.NextFloat(0.5f, 0.7f);
				GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center, sparkvelocity1, affectedByGravity: true, 40, sparkScale1, Main.rand.NextBool() ? ((!ChildSafety.Disabled) ? Color.LimeGreen : bColor) : Color.Green));
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/CeramicImpact", 2);
				style.Pitch = 0.4f;
				style.Volume = 0.5f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			time = (float)returnTime * 0.5f;
			collideWithTiles = false;
			canStick = false;
		}
		if (!hasHitTile || returning)
		{
			base.Projectile.rotation = storedVelocity.ToRotation() + MathHelper.ToRadians(90f);
		}
		if (base.Projectile.ai[2] == 5f && !pullingTarget && !hasLatchedTarget)
		{
			calledToPull = true;
		}
		AdjustOldVelocityArray();
		time++;
	}

	public void AdjustOldVelocityArray()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		for (int i = OldVelocities.Length - 1; i > 0; i--)
		{
			OldVelocities[i] = OldVelocities[i - 1];
		}
		OldVelocities[0] = storedVelocity;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		if (!stuckInTarget && canStick && !target.Calamity().pacified)
		{
			for (int i = 0; i <= 8; i++)
			{
				Vector2 spinninpoint = base.Projectile.velocity * 0.5f;
				GeneralParticleHandler.SpawnParticle(new LineParticle(scale: Main.rand.NextFloat(0.3f, 0.8f), velocity: spinninpoint.RotatedByRandom(0.44999998807907104) * Main.rand.NextFloat(0.5f, 0.7f), relativePosition: base.Projectile.Center, affectedByGravity: false, lifetime: 40, color: Main.rand.NextBool() ? ((!ChildSafety.Disabled) ? Color.LimeGreen : bColor) : Color.Green));
				float sparkScale2 = Main.rand.NextFloat(0.4f, 1f);
				Vector2 sparkvelocity2 = spinninpoint.RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(0.9f, 1.6f);
				GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center, sparkvelocity2, affectedByGravity: false, 40, sparkScale2, Main.rand.NextBool() ? ((!ChildSafety.Disabled) ? Color.LimeGreen : bColor) : Color.Green));
				Dust.NewDustPerfect(base.Projectile.Center, (!ChildSafety.Disabled) ? 75 : 5, sparkvelocity2, 100, default(Color), Main.rand.NextFloat(0.8f, 1.4f)).noGravity = true;
			}
			time = 1f;
			collideWithTiles = false;
			canDamage = false;
			placementDistance = 0f - Vector2.Distance(target.Center, base.Projectile.Center);
			placementVelocity = (target.Center - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
			placementCenter = placementVelocity * (placementDistance * 0.01f);
			chosenTarget = target;
			stuckInTarget = true;
			canStick = false;
			storedVelocity = base.Projectile.velocity;
			base.Projectile.velocity = Vector2.Zero;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/WulfrumKnifeTileHit2");
			style.Volume = 0.5f;
			style.Pitch = Main.rand.NextFloat(-0.3f, -0.4f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			if (pullCheckValid)
			{
				NPC nPC = chosenTarget;
				nPC.velocity += storedVelocity * (strongEnemy ? 0.3f : 1.5f);
				hasLatchedTarget = true;
				if ((float)chosenTarget.lifeMax >= (float)base.Projectile.damage * 28f || chosenTarget.boss)
				{
					strongEnemy = true;
				}
				style = new SoundStyle("CalamityMod/Sounds/Item/HeliumFlashCoreImpact");
				style.Volume = 0.55f;
				style.Pitch = Main.rand.NextFloat(-0.3f, -0.4f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				if (strongEnemy)
				{
					style = new SoundStyle("CalamityMod/Sounds/Item/MetalEcho");
					style.Volume = 0.85f;
					style.Pitch = Main.rand.NextFloat(0.8f, 0.9f);
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
			}
			else
			{
				normalHit = true;
			}
		}
		bool hitTarget = chosenTarget != null && target == chosenTarget;
		modifiers.SourceDamage *= ((!hitTarget) ? 0.2f : (ripped ? 2f : ((base.Projectile.numHits < 1) ? 0.01f : 1f)));
		if (hitTarget && pullingTarget)
		{
			chosenTarget.velocity = -storedVelocity * 3f;
			modifiers.SetInstantKill();
		}
		if (!pullCheckValid)
		{
			target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 180);
		}
	}

	public override bool? CanDamage()
	{
		if (!canDamage)
		{
			return false;
		}
		return null;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 25 * (spawnPullBlood ? 1 : 4), targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		Texture2D chain = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/SepticChain", (AssetRequestMode)2).Value;
		Vector2 end = base.Projectile.Center - storedVelocity.SafeNormalize(Vector2.UnitX) * 35f;
		List<Vector2> controlPoints = new List<Vector2> { DrawStartPosition };
		for (int i = 0; i < OldVelocities.Length; i++)
		{
			float swayResponsiveness = Utils.GetLerpValue(0f, 6f, i, clamped: true) * Utils.GetLerpValue(OldVelocities.Length, (float)OldVelocities.Length - 6f, i, clamped: true);
			Vector2 swayTotalOffset = OldVelocities[i] * swayResponsiveness;
			controlPoints.Add(Vector2.Lerp(DrawStartPosition, end, (float)i / (float)OldVelocities.Length) + swayTotalOffset);
		}
		controlPoints.Add(end);
		int chainPointCount = (int)(Vector2.Distance(controlPoints.First(), controlPoints.Last()) / 7f);
		if (chainPointCount < 12)
		{
			chainPointCount = 12;
		}
		List<Vector2> chainPoints = new BezierCurve(controlPoints.ToArray()).GetPoints(chainPointCount);
		if (hasLatchedTarget)
		{
			for (int j = 0; j < chainPoints.Count; j++)
			{
				Vector2 positionAtPoint = chainPoints[j];
				if (!(Vector2.Distance(Owner.Center, positionAtPoint) > 1400f) && !(Vector2.Distance(positionAtPoint, base.Projectile.Center) < 10f))
				{
					float angleAtPoint = ((j == chainPoints.Count - 1) ? (end - chainPoints[j]).ToRotation() : (chainPoints[j + 1] - chainPoints[j]).ToRotation());
					angleAtPoint += (float)Math.PI / 2f;
					Vector2 position = positionAtPoint - Main.screenPosition + Main.rand.NextVector2Circular(3f, 3f);
					Color chartreuse = Color.Chartreuse;
					((Color)(ref chartreuse)).A = 0;
					Main.EntitySpriteDraw(chain, position, null, chartreuse * Utils.GetLerpValue(0f, returnTime, time, clamped: true), angleAtPoint, chain.Size() / 2f, 1.35f, (SpriteEffects)0);
				}
			}
		}
		for (int k = 0; k < chainPoints.Count; k++)
		{
			Vector2 positionAtPoint2 = chainPoints[k];
			if (!(Vector2.Distance(Owner.Center, positionAtPoint2) > 1400f) && !(Vector2.Distance(positionAtPoint2, base.Projectile.Center) < 10f))
			{
				float angleAtPoint2 = ((k == chainPoints.Count - 1) ? (end - chainPoints[k]).ToRotation() : (chainPoints[k + 1] - chainPoints[k]).ToRotation());
				angleAtPoint2 += (float)Math.PI / 2f;
				Main.EntitySpriteDraw(chain, positionAtPoint2 - Main.screenPosition, null, Lighting.GetColor(positionAtPoint2.ToTileCoordinates()), angleAtPoint2, chain.Size() / 2f, 0.85f, (SpriteEffects)0);
			}
		}
		return true;
	}

	public SepticSkewerHarpoon()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		OldVelocities = (Vector2[])(object)new Vector2[20];
		canDamage = true;
		setPosition = true;
		returnTime = 75;
		canStick = true;
		collideWithTiles = true;
		bColor = Color.Chartreuse;
		spawnPullBlood = true;
		base._002Ector();
	}
}
